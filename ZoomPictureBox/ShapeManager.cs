using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    /// <summary>
    /// 図形管理
    /// </summary>
    public partial class ZoomPictureBox
    {

        /// <summary>
        /// 図形移動イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="name">名前</param>
        /// <param name="shapeObj">図形</param>
        public delegate void MoveShapeEventHandler(Object sender, string name, Object shapeObj);
        /// <summary>
        /// 図形移動イベント
        /// </summary>
        public event MoveShapeEventHandler MoveShapeEvent;
        /// <summary>
        /// 図形移動イベント
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="shapeObj">図形</param>
        protected virtual void OnMoveShapeEvent(string name,Object shapeObj)
        {
            MoveShapeEvent?.Invoke(this, name, shapeObj);
        }
        /// <summary>
        /// 追加順序
        /// </summary>
        private int addOrder = 0;

        /// <summary>
        /// 図形辞書
        /// </summary>
        private Dictionary<string, Shape.BaseShape> shapeDictionary_ = new Dictionary<string, Shape.BaseShape>();

        /// <summary>
        /// 図形追加
        /// </summary>
        /// <param name="shape">図形</param>
        /// <param name="force">true:既にある場合は置き換え/false:既にある場合は置き換えない</param>
        /// <returns>true:図形追加/false:既存図形あり</returns>
        public bool AddShape(Shape.BaseShape shape,bool force = false)
        {
			// 再表示イベントを登録
			shape.UpdateShape += Shape_UpdateShape;
			shape.UpdateZOrder += Shape_UpdateZOrder;
			if (shapeDictionary_.ContainsKey(shape.Name))
            {
                if (force)
                {   // 強制追加 = 置き換え
					// イベントを削除
					shapeDictionary_[shape.Name].UpdateShape -= Shape_UpdateShape;
					shapeDictionary_[shape.Name].UpdateZOrder -= Shape_UpdateZOrder;
					shapeDictionary_[shape.Name].Copy(shape,false,false,true);
                    // 描画
					if (autoRefresh_) Refresh();
                }
                return false;
            }
            // 追加インデックス
            shape.Index = addOrder;
			shape.ZOrder = shapeDictionary_.Count;
			addOrder++;
            // 新規追加
            shapeDictionary_.Add(shape.Name, shape);

			// 描画
			if (autoRefresh_) Refresh();
			return true;
        }
		/// <summary>
		/// 図形のZ-Order変更
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="newZOrder">新しいZ-Order値</param>
		private void Shape_UpdateZOrder(object sender, int newZOrder)
		{
			if (sender is Shape.BaseShape current_shape)
			{
				if (newZOrder == Shape.BaseShape.Z_ORDER_TO_BOTTOM)
				{   // 一番下へ
					int no = 1;
					foreach (Shape.BaseShape item in shapeDictionary_.Values.OrderBy(c => c.ZOrder))
					{
						if (item.Equals(current_shape) == false)
							item.ZOrder = no++;
					}
					current_shape.ZOrder = 0;
				}
				else if (newZOrder == Shape.BaseShape.Z_ORDER_TO_TOP)
				{   // 一番上へ
					int no = 0;
					foreach (Shape.BaseShape item in shapeDictionary_.Values.OrderBy(c => c.ZOrder))
					{
						if (item.Equals(current_shape) == false)
							item.ZOrder = no++;
					}
					current_shape.ZOrder = shapeDictionary_.Count - 1;
				}
				else
				{   // 入れ替え
					Shape.BaseShape beforeShape = shapeDictionary_.Values.FirstOrDefault(c => c.ZOrder == newZOrder);
					if (beforeShape != null)
					{
						beforeShape.ZOrder = current_shape.ZOrder;
						current_shape.ZOrder = newZOrder;
					}
				}
			}
		}

		/// <summary>
		/// 再描画イベント処理
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="replace">置き換える図形オブジェクト(nullの場合は再描画のみ)</param>
		private void Shape_UpdateShape(object sender,Shape.BaseShape replace = null)
		{
			if ((replace != null) && (sender is Shape.BaseShape sender_shape))
			{
				if (shapeDictionary_.ContainsKey(sender_shape.Name))
				{
					shapeDictionary_[sender_shape.Name].UpdateShape -= Shape_UpdateShape;
					shapeDictionary_[sender_shape.Name].UpdateZOrder -= Shape_UpdateZOrder;
					shapeDictionary_.Remove(sender_shape.Name);
				}
				// 辞書を更新
				replace.UpdateShape += Shape_UpdateShape;
				replace.UpdateZOrder += Shape_UpdateZOrder;
				shapeDictionary_[replace.Name] = replace;
				// 選択中図形の更新
				if ((shape != null) && (shape.Equals(sender_shape)))
					shape = replace;
			}
			Refresh();
		}

		/// <summary>
		/// 図形の取得
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>図形。見つからない場合はnull</returns>
		public Shape.BaseShape GetShape(string name) 
        {
            if (shapeDictionary_.ContainsKey(name))
                return shapeDictionary_[name];
            return null;
        }
        /// <summary>
        /// 図形の変更
        /// </summary>
        /// <param name="shape">図形</param>
        /// <param name="isAdd">true:辞書にない場合は追加</param>
        /// <returns>true:変更した</returns>
        public bool ChangeShape(Shape.BaseShape shape,bool isAdd)
        {
			// 再表示イベントを登録
			shape.UpdateShape += Shape_UpdateShape;
			shape.UpdateZOrder += Shape_UpdateZOrder;

			if (shapeDictionary_.ContainsKey(shape.Name))
            {   // 既にある場合は置き換え
				// イベントを削除
				shapeDictionary_[shape.Name].UpdateShape -= Shape_UpdateShape;
				shapeDictionary_[shape.Name].UpdateZOrder -= Shape_UpdateZOrder;
				shapeDictionary_[shape.Name].Copy(shape, false, false,true);
				// 描画
				if (autoRefresh_) Refresh();
				return true;
            }
            if (isAdd)
            {   
                // 追加インデックス
                shape.Index = addOrder;
				shape.ZOrder = shapeDictionary_.Count;
                addOrder++;
                // 新規に追加する
                shapeDictionary_.Add(shape.Name, shape);
				// 描画
				if (autoRefresh_) Refresh();
				return true;
            }
            return false;

        }
        /// <summary>
        /// 図形の削除
        /// </summary>
        /// <param name="name">図形名</param>
        /// <returns>true:削除成功</returns>
        public bool RemoveShape(string name)
        {
            if (shapeDictionary_.ContainsKey(name))
            {
				// イベントを削除
				shapeDictionary_[shape.Name].UpdateShape -= Shape_UpdateShape;
				shapeDictionary_[shape.Name].UpdateZOrder -= Shape_UpdateZOrder;
				// 図形を削除
				shapeDictionary_.Remove(name);
				// ZOrderを更新
				UpdateShapeZOrder();
				// 描画
				if (autoRefresh_) Refresh();
				return true;
            }
            return false;
        }
		/// <summary>
		/// 図形のZ-Orderを更新する
		/// </summary>
		private void UpdateShapeZOrder()
		{
			int index = 0;
			foreach(Shape.BaseShape shape in shapeDictionary_.Values.OrderBy(c=>c.ZOrder))
			{
				shape.ZOrder = index;
				index++;
			}
		}
        /// <summary>
        /// 図形の描画
        /// </summary>
        /// <param name="graphics">グラフィック</param>
        /// <param name="matrixInv">変換逆行列</param>
        /// <param name="size">描画対象のサイズ</param>
        /// <returns>true:描画図形あり</returns>
        private bool DrawShape(Graphics graphics, Matrix matrixInv, Size size)
        {
			bool isDraw = false;
            foreach(Shape.BaseShape shape in shapeDictionary_.Values.OrderBy(c=>c.ZOrder))
            {
                if (shape.IsDraw(matrixInv, size))
                    isDraw = shape.Draw(graphics, matrixInv, size);
            }
            return isDraw;
        }
        /// <summary>
        /// 図形表示の設定
        /// </summary>
        /// <param name="isVisible">true:図形描画する/false:図形描画しない</param>
        /// <param name="name">図形名。nullの場合は全図形</param>
        /// <returns>true:設定OK</returns>
        public bool SetShapeVisible(bool isVisible,string name = null)
        {
            if (shapeDictionary_.Count == 0)
                return false;

            if (name != null)
            {
                if (shapeDictionary_.ContainsKey(name))
                {
                    shapeDictionary_[name].Visible = isVisible;
                    return true;
                }
                return false;
            }
            foreach (Shape.BaseShape shape in shapeDictionary_.Values)
            {
                shape.Visible = isVisible;
            }
			// 再描画
			if (autoRefresh_) Refresh();
			return true;
        }
        /// <summary>
        /// 図形の有効・無効を設定
        /// </summary>
        /// <param name="isEnable">true:有効/false:無効</param>
        /// <param name="name">図形名。nullの場合は全図形</param>
        /// <returns>true:設定OK</returns>
        public bool SetShapeEnable(bool isEnable, string name = null)
        {
            if (shapeDictionary_.Count == 0)
                return false;

            if (name != null)
            {
                if (shapeDictionary_.ContainsKey(name))
                {
                    shapeDictionary_[name].Enable = isEnable;
                    return true;
                }
                return false;
            }
            foreach (Shape.BaseShape shape in shapeDictionary_.Values)
            {
                shape.Enable = isEnable;
            }
			// 再描画
			if (autoRefresh_) Refresh();
			return true;
        }
        /// <summary>
        /// 当たり判定
        /// </summary>
        /// <param name="graphics">グラフィック</param>
        /// <param name="matrixInv">変換逆行列</param>
        /// <param name="size">描画対象のサイズ</param>
        /// <returns>true:描画図形あり</returns>
        private Shape.BaseShape HitTest(Graphics graphics, Matrix matrixInv, Size size, Point point,
			out Shape.BaseShape.HIT_KIND kind,out int anchorPoint)
        {
			if (shape != null)
				if (shape.HitTest(graphics, matrixInv, size, point, out kind, out anchorPoint))
					return shape;
            foreach (Shape.BaseShape any_shape in shapeDictionary_.Values.OrderByDescending(i=>i.ZOrder))
            {
                if (any_shape.HitTest(graphics, matrixInv, size, point,out kind,out anchorPoint))
                    return any_shape;
            }
			kind = Shape.BaseShape.HIT_KIND.NONE;
			anchorPoint = -1;
            return null ;
        }
        /// <summary>
        /// 図形名の一覧を取得
        /// </summary>
        /// <returns></returns>
        public string[] GetShapeNames()
        {
            if (shapeDictionary_.Count != 0)
                return shapeDictionary_.Keys.ToArray();
            return null;
        }
		/// <summary>
		/// 図形のクリア
		/// </summary>
		public void ClearShape()
		{
			foreach(Shape.BaseShape item in shapeDictionary_.Values)
			{	// イベントを削除
				item.UpdateZOrder -= Shape_UpdateZOrder;
				item.UpdateShape -= Shape_UpdateShape;
			}
			shapeDictionary_.Clear();
			shape = null;
			// 再描画
			Refresh();
		}
    }
}

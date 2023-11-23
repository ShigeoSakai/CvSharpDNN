using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Shape
{
	/// <summary>
	/// 正多角形クラス
	/// </summary>
	[ShapeName("正多角形"), DefaultProperty("Center")]
	public class RegularPolygon : Polygon
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "正多角形";
		
		/// <summary>
		/// 座標値の取得・設定
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public new System.Drawing.PointF this[int index]
		{
			get
			{
				if ((index >= 0) && (index < Count))
					return base[index];
				return new System.Drawing.Point();
			}
			set
			{	// アンカー指定と座標値で更新
				if ((index >= 0) && (index < Count))
					CalcPointsFromAnchor(index, value);
			}
		}
		/// <summary>
		/// 回転角度(度)
		/// </summary>
		[Category("図形"), DisplayName("回転角度"),DefaultValue(0.0F), Description("回転角度")]
		public float RotateAngle { get; set; } = 0.0F;
		/// <summary>
		/// 半径
		/// </summary>
		[Category("図形"), DisplayName("半径"), DefaultValue(10.0F), Description("多角形の半径")]
		public float Radius { get; set; } = 10.0F;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="center"中心座標></param>
		/// <param name="radius">半径</param>
		/// <param name="numOfvertex">辺の数</param>
		public RegularPolygon(string name, System.Drawing.Point center, float radius, int numOfvertex) : base(name)
		{
			// 中心点
			Center = center;
			// 半径
			Radius = radius;
			// 角度加算値
			CalcVertPosition(numOfvertex);
		}
		/// <summary>
		/// 値をコピー
		/// </summary>
		/// <param name="src">コピー元</param>
		/// <param name="copySelected">選択中をコピーするか(デフォルト:true)</param>
		/// <param name="copyIndex">インデックスをコピーするか(デフォルト:false)</param>
		public override void Copy(BaseShape src, bool copySelected = true, bool copyIndex = false , bool copyMenu = false)
		{
			base.Copy(src, copySelected, copyIndex, copyMenu);
			if (src is RegularPolygon polygon)
			{
				RotateAngle = polygon.RotateAngle;
				Radius = polygon.Radius;
			}
		}
		/// <summary>
		/// 頂点の設定
		/// </summary>
		/// <param name="points"></param>
		public override bool Set(params System.Drawing.Point[] points)
		{
			throw new NotSupportedException("正多角形はSet()メソッド非対応");
		}
		/// <summary>
		/// 頂点の追加
		/// </summary>
		/// <param name="points"></param>
		public override bool Add(params System.Drawing.Point[] points)
		{
			throw new NotSupportedException("正多角形はAdd()メソッド非対応");
		}
		/// <summary>
		/// 度からラジアンに変換
		/// </summary>
		/// <param name="degree"></param>
		/// <returns></returns>
		private double ToRadian(double degree)
		{
			return Math.PI * degree / 180.0;
		}
		/// <summary>
		/// ラジアンから度へ変換
		/// </summary>
		/// <param name="radian"></param>
		/// <returns></returns>
		private double ToDegree(double radian)
		{
			return radian * (180.0 / Math.PI);
		}

		/// <summary>
		/// 頂点座標の算出
		/// </summary>
		/// <param name="numOfvertex">辺の数</param>
		private void CalcVertPosition(int numOfvertex)
		{
			// 角度加算値
			double addAngle = 360 / numOfvertex;
			double angle = RotateAngle;
			for (int index = 0; index < numOfvertex; index++)
			{
				double radian = ToRadian(angle)
;				float px = (float)(Radius * Math.Cos(radian)) + Center.X;
				float py = (float)(Radius * Math.Sin(radian)) + Center.Y;
				if (points_.Count <= index)
					points_.Add(new System.Drawing.PointF(px, py));
				else
					points_[index] = new System.Drawing.PointF(px, py);
				angle += addAngle;
			}
			// 外接矩形の更新
			updateRectangle();
		}

		/// <summary>
		/// アンカーの移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <param name="anchorPoint">アンカー位置</param>
		/// <param name="limitSize">移動リミット</param>
		/// <returns>true:移動可能</returns>
		public override bool MoveAnchor(int offsetX, int offsetY, int anchorPoint, System.Drawing.Size limitSize)
		{
			if ((limitSize.IsEmpty) || (IsEditable == false) || (anchorPoint < 0) || (anchorPoint >= points_.Count))
				return base.MoveShape(offsetX, offsetY, limitSize);

			// 制御点のチェック
			if ((anchorPoint >= 0) && (anchorPoint < points_.Count))
			{
				// 移動後の点座標
				System.Drawing.PointF movePos = new System.Drawing.PointF(points_[anchorPoint].X + offsetX, points_[anchorPoint].Y + offsetY);
				if ((movePos.X >= 0) && (movePos.X <= limitSize.Width) &&
					(movePos.Y >= 0) && (movePos.Y <= limitSize.Height))
				{
					// 座標値を計算
					return CalcPointsFromAnchor(anchorPoint, movePos);
				}
			}
			return false;
		}
		/// <summary>
		/// 指定アンカー位置を元に頂点座標を計算
		/// </summary>
		/// <param name="anchorPoint">アンカー番号</param>
		/// <param name="newPoint">アンカー番号の座標</param>
		/// <returns></returns>
		private bool CalcPointsFromAnchor(int anchorPoint,System.Drawing.PointF newPoint)
		{
			if ((anchorPoint >= 0) && (anchorPoint < points_.Count))
			{
				double orig_angle = ToDegree(Math.Atan2(points_[anchorPoint].Y - Center.Y, points_[anchorPoint].X - Center.X));
				double move_angle = ToDegree(Math.Atan2(newPoint.Y - Center.Y, newPoint.X - Center.X));
				// 回転角度を算出
				RotateAngle += (float)(move_angle - orig_angle);
				// 半径を算出
				Radius = (float)Math.Sqrt(Math.Pow(newPoint.X - Center.X, 2.0) + Math.Pow(newPoint.Y - Center.Y, 2.0));
				// 座標を計算
				CalcVertPosition(points_.Count);
				return true;
			}
			return false;
		}
	}
}

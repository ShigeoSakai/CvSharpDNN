using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Drawing.Shape
{
    /// <summary>
    /// 描画図形クラス
    /// </summary>
	[DefaultProperty("Center")]
    public class BaseShape
    {
		/// <summary>
		/// 図形名
		/// </summary>
		protected virtual string SHAPE_NAME() => "点";
		/// <summary>
		/// 図形の名前
		/// </summary>
		[Category("図形"), DisplayName("名前"), Description("図形の名称")]
		public string Name { get; set; }
		/// <summary>
		/// 中心座標
		/// </summary>
		[Category("図形"), DisplayName("中心"), Description("中心座標")]
		public virtual System.Drawing.PointF Center { get; set; } = new System.Drawing.PointF(0,0);
		/// <summary>
		/// 描画色
		/// </summary>
		[Category("描画"), DisplayName("描画色"), DefaultValue(typeof(System.Drawing.Color),"Red"),
			Description("図形の描画色")]
		public virtual System.Drawing.Color Color { get; set; } = System.Drawing.Color.Red;
		/// <summary>
		/// マーカー描画
		/// </summary>
		[Category("描画"), DisplayName("マーカー描画"), DefaultValue(false),
			Description("マーカー(中心点,制御点等)を描画するか")]
		public virtual bool MarkerDraw { get; set; } = false;
		/// <summary>
		/// マーカー
		/// </summary>
		[Category("描画"), DisplayName("マーカー"), DefaultValue(typeof(MARKER), "CROSS"),
			Description("マーカー(中心点,制御点等)の形状")]
		public virtual MARKER Marker { get; set; } = MARKER.CROSS;
		/// <summary>
		/// マーカー線幅
		/// </summary>
		[Category("描画"), DisplayName("マーカー線の幅"), DefaultValue(2.0F),
			Description("マーカーの線幅")]
		public virtual float MarkerLineWidth { get; set; } = 2.0F;
		/// <summary>
		/// マーカー色
		/// </summary>
		[Category("描画"), DisplayName("マーカー色"),
			Description("マーカーの色。未指定の場合は、描画色を使用する")]
		public virtual System.Drawing.Color? MarkerColor { get; set; }
		/// <summary>
		/// マーカーサイズ
		/// </summary>
		[Category("描画"), DisplayName("マーカーサイズ"), DefaultValue(10.0F),
			Description("マーカー(中心点,制御点等)の描画サイズ")]
		public virtual float MarkerSize { get; set; } = 10.0F;
		/// <summary>
		/// 有効・無効
		/// </summary>
		[Category("図形"), DisplayName("有効"), DefaultValue(true),
			Description("図形の有効・無効")]
		public virtual bool Enable { get; set; } = true;
		/// <summary>
		/// 表示・非表示
		/// </summary>
		[Category("図形"), DisplayName("表示"), DefaultValue(true),
			Description("図形の表示・非表示")]
		public virtual bool Visible { get; set; } = true;
		/// <summary>
		/// ラベル表示
		/// </summary>
		[Category("ラベル"), DisplayName("ラベル表示"), DefaultValue(false),
			Description("ラベル表示をするかどうか")]
		public bool ShowLable { get; set; } = false;
		/// <summary>
		/// 表示文字列
		/// </summary>
		private string text_;
		[Category("ラベル"), DisplayName("文字列"),
			Description("図形ラベル表示文字列")]
		public string Text
		{
			get
			{
				if (text_ == null)
					return Name;
				return text_;
			}
			set { text_ = value; }
		}
		/// <summary>
		/// ラベル表示位置
		/// </summary>
		[Category("ラベル"), DisplayName("表示位置"), DefaultValue(typeof(LABEL_POSITION), "TOP_LEFT"),
			Description("図形ラベルの表示位置")]
		public LABEL_POSITION LabelPosition { get; set; } = LABEL_POSITION.TOP_LEFT;
		/// <summary>
		/// ラベルフォント
		/// </summary>
		[Category("ラベル"), DisplayName("フォント"),DefaultValue(typeof(System.Drawing.Font), "Microsoft Sans Serif, 10pt"),
			Description("図形ラベルのフォント")]
		public System.Drawing.Font LabelFont { get; set; } = new System.Drawing.Font("Microsoft Sans Serif", 10.0F);
		/// <summary>
		/// ラベル文字色
		/// </summary>
		[Category("ラベル"), DisplayName("文字色"),
			Description("図形ラベルの文字色。未設定の場合は描画色を使う")]
		public System.Drawing.Color? LabelColor { get; set; }
		/// <summary>
		/// ラベル塗りつぶし
		/// </summary>
		[Category("ラベル"), DisplayName("塗りつぶし"),DefaultValue(false),
			Description("図形ラベルを塗りつぶすかどうか")]
		public bool LabelFill { get; set; } = false;
		/// <summary>
		/// ラベル塗りつぶし色
		/// </summary>
		[Category("ラベル"), DisplayName("塗りつぶし色"),
			Description("図形ラベルを塗りつぶし色。未設定の場合はラベル文字色の反転色を使う")]
		public System.Drawing.Color? LabelFillColor { get; set; }
		/// <summary>
		/// ラベル枠
		/// </summary>
		[Category("ラベル"), DisplayName("文字枠"), DefaultValue(false),
			Description("図形ラベルの文字枠を描画するかどうか")]
		public bool LabelBorder { get; set; } = false;
		/// <summary>
		/// ラベル枠色
		/// </summary>
		[Category("ラベル"), DisplayName("文字枠色"),
			Description("図形ラベル枠の描画色。未設定の場合はラベル文字色を使う")]
		public System.Drawing.Color? LabelBorderColor { get; set; }

		/// <summary>
		/// 選択色
		/// </summary>
		[Category("動作"), DisplayName("選択色"),
			Description("図形選択時の色。未設定の場合は描画色の反転色を使う")]
		public System.Drawing.Color? SelectedColor { get; set; } = null;
		/// <summary>
		/// 選択時の塗りつぶし色
		/// </summary>
		[Category("動作"), DisplayName("選択塗りつぶし色"),
			Description("図形選択時の塗りつぶし色。未設定の場合は塗りつぶし色の反転色を使う")]
		public System.Drawing.Color? SelectedFillColor { get; set; } = null;
		/// <summary>
		/// 選択文字色
		/// </summary>
		[Category("動作"), DisplayName("選択文字色"),
			Description("図形選択時のラベル文字色。未設定の場合はラベル文字色の反転色を使う")]
		public System.Drawing.Color? SelectedLabelColor { get; set; } = null;
		/// <summary>
		/// 選択時のラベル塗りつぶし色
		/// </summary>
		[Category("動作"), DisplayName("選択文字塗りつぶし色"),
			Description("図形選択時のラベル文字塗りつぶし色。未設定の場合はラベル文字塗りつぶし色の反転色を使う")]
		public System.Drawing.Color? SelectedLabelFillColor { get; set; } = null;
		/// <summary>
		/// 選択時のラベル枠色
		/// </summary>
		[Category("動作"), DisplayName("選択文字枠色"),
			Description("図形選択時のラベル文字枠色。未設定の場合はラベル文字枠色の反転色を使う")]
		public System.Drawing.Color? SelectedLabelBorderColor { get; set; } = null;
		/// <summary>
		/// インデックス(追加順序)
		/// </summary>
		[Category("図形"), DisplayName("インデックス"),Description("図形のインデックス")]
		public int Index { get; set; } = 0;
		/// <summary>
		/// 編集可能かどうか
		/// </summary>
		[Category("動作"), DisplayName("編集可能"),DefaultValue(true), Description("図形が編集可能かどうか")]
		public bool IsEditable { get; set; } = true;
		/// <summary>
		/// 当たり判定幅
		/// </summary>
		[Category("動作"), DisplayName("当たり判定幅"), DefaultValue(8), Description("当たり判定の幅")]
		public int HitMargin { get; set; } = 8;

		/// <summary>
		/// アンカー色
		/// </summary>
		[Category("アンカー"), DisplayName("アンカー色"), DefaultValue(typeof(System.Drawing.Color), "LightGreen"),
			Description("アンカー描画色")]
		public System.Drawing.Color AnchorColor { get; set; } = System.Drawing.Color.LightGreen;
		/// <summary>
		/// アンカー半径
		/// </summary>
		[Category("アンカー"), DisplayName("アンカー半径"), DefaultValue(10.0F),Description("アンカー描画半径")]
		public float AnchorRadius { get; set; } = 10.0F;
		/// <summary>
		/// TAG
		/// </summary>
		[Category("図形"), DisplayName("タグ")]
		public object Tag { get; set; } = null;
		/// <summary>
		/// 選択中
		/// </summary>
		[Browsable(false)]
		public bool Selected { get; set; } = false;

		/// <summary>
		/// デバッグモード定義
		/// </summary>
		[Flags]
		public enum DEBUG_MODE
		{
			NONE = 0x0000,
			OUTER_LINE = 0x0001,
			NEAR_LINE = 0x0002,
		}
		/// <summary>
		/// デバッグモード
		/// </summary>
		[Category("デバッグ"), DisplayName("モード"),DefaultValue(typeof(DEBUG_MODE),"NONE")]
		public virtual DEBUG_MODE DebugMode { get; set; } = DEBUG_MODE.NONE;

		/// <summary>
		/// 座標値の取得・生成
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>座標値を返す</returns>
		/// <remarks>
		/// index = 0 ... 中心座標
		/// </remarks>
		public virtual System.Drawing.PointF this[int index]
		{
			get
			{
				if (index == 0)
					return Center;
				return new System.Drawing.PointF();
			}
			set
			{
				if (index == 0)
					Center = value;
			}
		}
		/// <summary>
		/// 座標値の件数
		/// </summary>
		[Browsable(false)]
		public virtual int Count { get { return 1; } }

		/// <summary>
		/// Z-Order
		/// </summary>
		[Browsable(false)]
		public int ZOrder { get; set; } = 0;

		/// <summary>
		/// コンテキストメニュー
		/// </summary>
		[Browsable(false)]
		public virtual ContextMenuStrip ContextMenu { get; protected set; } = new ContextMenuStrip();

		/// <summary>
		/// 図形更新イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元(図形)</param>
		public delegate void UpdateShapeEventHandler(object sender, BaseShape replace = null);
		/// <summary>
		/// 図形更新イベント
		/// </summary>
		[Browsable(false)]
		public event UpdateShapeEventHandler UpdateShape;
		/// <summary>
		/// 図形更新イベント発行
		/// </summary>
		protected virtual void OnUpdateShape()
		{
			UpdateShape?.Invoke(this);
		}
		/// <summary>
		/// 図形更新イベント発行
		/// </summary>
		protected virtual void OnUpdateShape(BaseShape replace)
		{
			UpdateShape?.Invoke(this,replace);
		}

		/// <summary>
		/// 最前面に移動時の指定Z-Order
		/// </summary>
		public const int Z_ORDER_TO_TOP = -1;
		/// <summary>
		/// 最背面に移動時の指定Z-Order
		/// </summary>
		public const int Z_ORDER_TO_BOTTOM = 0;
		/// <summary>
		/// Z-Order更新イベントハンドラ
		/// </summary>
		/// <param name="sender">送信元(図形)</param>
		public delegate void UpdateZOrderEventHandler(object sender, int newZOrder);
		/// <summary>
		/// Z-Order更新イベント
		/// </summary>
		[Browsable(false)]
		public event UpdateZOrderEventHandler UpdateZOrder;
		/// <summary>
		/// Z-Order更新イベント発行
		/// </summary>
		protected virtual void OnUpdateZOrder(int newZOrder)
		{
			UpdateZOrder?.Invoke(this,newZOrder);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		public BaseShape(string name)
        {
            Name = name;
			// コンテキストメニュー生成
			CreateContextMenu();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="isEditable">編集可能かどうか</param>
		public BaseShape(string name,bool isEditable) :this(name)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public BaseShape(string name, System.Drawing.Color color):this(name)
        {
            Color = color;
		}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="src">コピー元</param>
		public BaseShape(BaseShape src)
		{
			// コンテキストメニュー生成
			CreateContextMenu();
			// コピー
			Copy(src,true,false,false);
		}
		/// <summary>
		/// 値をコピー
		/// </summary>
		/// <param name="src">コピー元</param>
		/// <param name="copySelected">選択中をコピーするか(デフォルト:true)</param>
		/// <param name="copyIndex">インデックスをコピーするか(デフォルト:false)</param>
		/// <param name="copyMenu">コンテキストメニューをコピーするか(デフォルト:false)</param>
		public virtual void Copy(BaseShape src, bool copySelected = true, bool copyIndex = false , bool copyMenu = false)
        {
            // 名前
            Name = src.Name;
            /// 中心座標
            Center = src.Center;
            /// 描画色
            Color = src.Color;
			// マーカー描画
			MarkerDraw = src.MarkerDraw;
            /// マーカー
            Marker = src.Marker;
			// マーカー線幅
			MarkerLineWidth = src.MarkerLineWidth;
			// マーカー色
			MarkerColor = src.MarkerColor;
			/// マーカーサイズ
			MarkerSize = src.MarkerSize;
            /// 有効・無効
            Enable = src.Enable;
            /// 表示・非表示
            Visible = src.Visible;
            /// ラベル表示
            ShowLable = src.ShowLable;
            /// 表示文字列
            Text = src.Text;
            /// ラベル表示位置
            LabelPosition = src.LabelPosition;
            /// ラベルフォント
            LabelFont = src.LabelFont;
            /// ラベル文字色
            LabelColor = src.LabelColor;
            /// ラベル塗りつぶし
            LabelFill = src.LabelFill;
            /// ラベル塗りつぶし色
            LabelFillColor = src.LabelFillColor;
            /// ラベル枠
            LabelBorder = src.LabelBorder;
            /// ラベル枠色
            LabelBorderColor = src.LabelBorderColor;
            /// 選択色
            SelectedColor = src.SelectedColor;
            /// 選択時の塗りつぶし色
            SelectedFillColor = src.SelectedFillColor;
            /// 選択文字色
            SelectedLabelColor = src.SelectedLabelColor;
            /// 選択時のラベル塗りつぶし色
            SelectedLabelFillColor = src.SelectedLabelFillColor;
            /// 選択時のラベル枠色
            SelectedLabelBorderColor = src.SelectedLabelBorderColor;
			// 編集可能
			IsEditable = src.IsEditable;
			// アンカー色
			AnchorColor = src.AnchorColor;
			// アンカー半径
			AnchorRadius = src.AnchorRadius;
			// Z-Order
			ZOrder = src.ZOrder;
			// TAG
			Tag = src.Tag;
			// コンテキストメニュー
			if ((src.ContextMenu != null) && (copyMenu))
			{
				ContextMenu = new ContextMenuStrip();
				foreach (ToolStripMenuItem item in src.ContextMenu.Items)
					ContextMenu.Items.Add(item);
			}

			// 選択中
            if (copySelected)
                Selected = src.Selected;

            // インデックス
            if (copyIndex)
                Index = src.Index;
        }

        /// <summary>
        /// ラベル設定
        /// </summary>
        /// <param name="text">ラベル文字列</param>
        /// <param name="position">ラベル表示位置</param>
        public void SetText(string text,LABEL_POSITION position = LABEL_POSITION.TOP_LEFT)
        {
            Text = text;
            LabelPosition = position;
            ShowLable = true;
        }
        /// <summary>
        /// ラベル設定
        /// </summary>
        /// <param name="text">ラベル文字列</param>
        /// <param name="textColor">ラベル文字列色</param>
        /// <param name="position">ラベル表示位置</param>
        /// <param name="isFill">ラベル枠塗りつぶしありなし</param>
        /// <param name="isBorder">ラベル枠ありなし</param>
        /// <remarks>
        /// 塗りつぶし色は、文字列色の反転
        /// 枠線は文字列色
        /// </remarks>
        public void SetText(string text, System.Drawing.Color textColor,
            LABEL_POSITION position = LABEL_POSITION.TOP_LEFT, bool isFill = false,bool isBorder = false)
        {
            SetText(text, position);
            LabelColor = textColor;
            if (isFill)
            {
                LabelFillColor = System.Drawing.Color.FromArgb((byte)(~textColor.R), (byte)(~textColor.G), (byte)(~textColor.B));
            }
            LabelFill = isFill;
            if (isBorder)
            {
                LabelBorderColor = textColor;
            }
            LabelBorder = isBorder;
        }
        /// <summary>
        /// ラベル設定
        /// </summary>
        /// <param name="text">ラベル文字列</param>
        /// <param name="textColor">ラベル文字列色</param>
        /// <param name="fillColor">ラベル枠塗りつぶし色</param>
        /// <param name="borderColor">ラベル枠色</param>
        /// <param name="position">ラベル表示位置</param>
        /// <param name="isFill">ラベル枠塗りつぶしありなし</param>
        /// <param name="isBorder">ラベル枠ありなし</param>
        public void SetText(string text, System.Drawing.Color textColor, System.Drawing.Color fillColor, System.Drawing.Color borderColor ,
            LABEL_POSITION position = LABEL_POSITION.TOP_LEFT, bool isFill = false, bool isBorder = false)
        {
            SetText(text, position);
            LabelColor = textColor;
            if (isFill)
            {
                LabelFillColor = fillColor;
            }
            LabelFill = isFill;
            if (isBorder)
            {
                LabelBorderColor = borderColor;
            }
            LabelBorder = isBorder;
        }


        /// <summary>
        /// 描画するかどうか
        /// </summary>
        /// <returns>true:描画対象</returns>
        protected bool IsDrawable()
        {
            return (Enable) && (Visible);
        }

        /// <summary>
        /// 座標変換
        /// </summary>
        /// <param name="matrixInv">逆アフィン行列</param>
        /// <param name="size">表示サイズ</param>
        /// <returns>有効な場合は、描画座標(PointF)の配列。無効な場合はnull</returns>
        protected virtual List<System.Drawing.PointF> CalcDraw(Matrix matrixInv, System.Drawing.Size size)
        {
            if (IsDrawable())
            {
				System.Drawing.PointF[] pts = { new System.Drawing.PointF(Center.X, Center.Y) };
                matrixInv.TransformPoints(pts);
                if ((pts[0].X >= 0.0F) && (pts[0].X < (float)size.Width) &&
                    (pts[0].Y >= 0.0F) && (pts[0].Y < (float)size.Height))
                    return pts.ToList();
            }
            return null;
        }

		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <typeparam name="PLIST">PointFのListまたは配列</typeparam>
		/// <param name="pts">計算した領域</param>
		/// <param name="point">指定座標</param>
		/// <returns>true:指定座標が領域に含まれている</returns>
		protected virtual bool IsContain<PLIST>(PLIST pts , System.Drawing.Point point) where PLIST:IList<System.Drawing.PointF>
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                (int)(pts[0].X - MarkerSize / 2 - HitMargin),
                (int)(pts[0].Y - MarkerSize / 2 - HitMargin),
                (int)(MarkerSize + HitMargin * 2),
                (int)(MarkerSize + HitMargin * 2));
            // 図形に含めれているか
            if (rect.Contains(point))
                return true;

            return false;
        }
		/// <summary>
		/// 当たり判定
		/// </summary>
		/// <param name="graphics">グラフィック</param>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <param name="point">指定座標</param>
		/// <param name="kind">当たり種別</param>
		/// <param name="anchorPoint">アンカー番号。-1の場合は図形に当たった</param>
		/// <returns>true:図形に当たった</returns>
		/// <remarks>
		/// 戻り値   kind   anchorPoint<br>
		/// false    NONE        -1               当たりなし<br>
		///  true    SHAPE       -1               図形に当たった<br>
		///  true    TEXT        -1               ラベルに当たった<br>
		///  true    ANCHOR  アンカー番号         アンカーに当たった<br>
		/// </remarks>
		public bool HitTest(System.Drawing.Graphics graphics, Matrix matrixInv, System.Drawing.Size size, System.Drawing.Point point,
			out HIT_KIND kind,out int anchorPoint)
        {
			// デフォルトは当たりなし
			kind = HIT_KIND.NONE;
			anchorPoint = -1;
			// 編集不可の場合は当たり判定NG
			if (IsEditable == false)
				return false;

            // 領域座標を計算
            List<System.Drawing.PointF> pts = CalcDraw(matrixInv, size);
			if (pts != null)
			{
				// アンカーの当たり判定
				if (HitTestAnchor(pts, point, out anchorPoint))
				{
					kind = HIT_KIND.ANCHOR;
					return true;
				}
				if (IsContain(pts, point) == false)
				{
					// 文字列ラベルの領域チェック
					if ((ShowLable) && (Text != null))
					{
						if (HitTestText(graphics,pts,size,point,matrixInv,Text,LabelFont))
						{
							kind = HIT_KIND.TEXT;
							return true;
						}
					}
					return false;
				}
				kind = HIT_KIND.SHAPE;
				return true;
			}
			return false;
        }
		/// <summary>
		/// テキスト部分の当たり判定
		/// </summary>
		/// <typeparam name="PLIST">PointFのListまたは配列</typeparam>
		/// <param name="graphics">描画グラフィック</param>
		/// <param name="pts">描画点</param>
		/// <param name="size">画面サイズ</param>
		/// <param name="point">当たり判定座標</param>
		/// <param name="matrixInv">アフィン変換逆行列</param>
		/// <param name="text">表示テキスト</param>
		/// <param name="font">フォント</param>
		/// <returns>true:ラベルに当たった</returns>
		protected virtual bool HitTestText<PLIST>(System.Drawing.Graphics graphics,PLIST pts, 
			System.Drawing.Size size, System.Drawing.Point point, Matrix matrixInv,
			string text,System.Drawing.Font font) where PLIST:IList<System.Drawing.PointF>
		{
			// 文字列の描画サイズ
			System.Drawing.SizeF textSize = TextRenderer.MeasureText(graphics, text, font);
			// 描画サイズ
			System.Drawing.RectangleF textRect = CalcTextPosition(textSize, size, pts, matrixInv);
			// マージン分拡張
			textRect.X = textRect.X - HitMargin;
			textRect.Y = textRect.Y - HitMargin;
			textRect.Width = textRect.Width + HitMargin * 2.0F;
			textRect.Height = textRect.Height + HitMargin * 2.0F;

			return textRect.Contains(point.X, point.Y);
		}
		/// <summary>
		/// 当たり判定種別
		/// </summary>
		public enum HIT_KIND
		{
			NONE = 0,	//<! 当たりなし
			SHAPE,		//<! 図形
			TEXT,		//<! ラベル(文字)
			ANCHOR,		//<! アンカー
		}
		/// <summary>
		/// アンカーの当たり判定
		/// </summary>
		/// <typeparam name="PLIST">PointFのListまたは配列</typeparam>
		/// <param name="pts">描画点一覧</param>
		/// <param name="point">当たり判定座標</param>
		/// <param name="anchorPoint">アンカー番号。-1の場合は当たりなし</param>
		/// <param name="startIndex">対象アンカーの先頭のインデックス(デフォルト:0)</param>
		/// <param name="lastIndex">対象アンカーの最後のインデックス(デフォルト:-1)</param>
		/// <returns>true:アンカーに当たった</returns>
		/// <remarks>
		/// lastIndexが-1の場合は、startIndex～最後の描画点で当たり判定を行う。
		/// </remarks>
		protected virtual bool HitTestAnchor<PLIST>(PLIST pts, System.Drawing.Point point,out int anchorPoint,
			int startIndex = 0,int lastIndex = -1) where PLIST:IList<System.Drawing.PointF>
		{
			anchorPoint = -1;
			if (lastIndex < 0) lastIndex = pts.Count - 1;
			for(int index = startIndex; index <= lastIndex; index ++)
			{
				if (HitTestAnchor(pts[index],point))
				{
					anchorPoint = index;
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// アンカーの当たり判定
		/// </summary>
		/// <param name="pts">描画点</param>
		/// <param name="point">当たり判定座標</param>
		/// <returns>true:アンカーに当たった</returns>
		protected virtual bool HitTestAnchor(System.Drawing.PointF pts, System.Drawing.Point point)
		{
			System.Drawing.RectangleF rect = new System.Drawing.RectangleF(pts.X - AnchorRadius - HitMargin, pts.Y - AnchorRadius - HitMargin,
				(AnchorRadius + HitMargin) * 2, (AnchorRadius + HitMargin) * 2);
			if (rect.Contains((float)point.X, (float)point.Y))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 描画対象かどうか
		/// </summary>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>true:描画対象</returns>
		public virtual bool IsDraw(Matrix matrixInv, System.Drawing.Size size)
        {
            return (CalcDraw(matrixInv, size) != null);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="graphics">描画グラフィック</param>
        /// <param name="matrixInv">逆アフィン行列</param>
        /// <param name="size">表示サイズ</param>
        /// <returns>true:描画した</returns>
        public virtual bool Draw(System.Drawing.Graphics graphics, Matrix matrixInv, System.Drawing.Size size)
        {
            // 図形の描画
            List<System.Drawing.PointF> pts = DrawShape(graphics, matrixInv, size);
            if (pts != null)
            {
                if (ShowLable)
                {   // ラベル文字列の描画
                    DrawText(graphics, size, pts , matrixInv);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 色の選択
        /// </summary>
        protected enum COLOR_SELECT
        {
            NORMAL_COLOR,
            FILL_COLOR,
            TEXT_COLOR,
            TEXT_FILL_COLOR,
            TEXT_BORDER_COLOR,
			MARKER_COLOR,
        }

        /// <summary>
        /// 描画色を取得
        /// </summary>
		/// <param name="colorSelect">取得する色の指定</param>
        /// <returns>描画色</returns>
        protected virtual System.Drawing.Color GetDrawColor(COLOR_SELECT colorSelect)
        {
			System.Drawing.Color drawColor;
			System.Drawing.Color? selectedColor;

            switch(colorSelect)
            {
                case COLOR_SELECT.TEXT_COLOR:
					// ラベル文字色取得
                    if (LabelColor.HasValue)
                        drawColor = LabelColor.Value;
					else if (LabelBorderColor.HasValue)
						drawColor = LabelBorderColor.Value;
					else if (LabelFillColor.HasValue)
						drawColor = System.Drawing.Color.FromArgb((byte)(~LabelFillColor.Value.R), (byte)(~LabelFillColor.Value.G), (byte)(~LabelFillColor.Value.B));
					else
						drawColor = Color;
                    selectedColor = SelectedLabelColor;
                    break;
                case COLOR_SELECT.TEXT_FILL_COLOR:
					// ラベル塗りつぶし色取得
                    if (LabelFillColor.HasValue)
                        drawColor = LabelFillColor.Value;
                    else if (LabelColor.HasValue)
                        drawColor = System.Drawing.Color.FromArgb((byte)(~LabelColor.Value.R), (byte)(~LabelColor.Value.G), (byte)(~LabelColor.Value.B));
					else if (LabelBorderColor.HasValue)
						drawColor = System.Drawing.Color.FromArgb((byte)(~LabelBorderColor.Value.R), (byte)(~LabelBorderColor.Value.G), (byte)(~LabelBorderColor.Value.B));
					else
						drawColor = System.Drawing.Color.FromArgb(127, Color);
                    selectedColor = SelectedLabelFillColor;
                    break;
                case COLOR_SELECT.TEXT_BORDER_COLOR:
					// ラベル枠色取得
                    if (LabelBorderColor.HasValue)
                        drawColor = LabelBorderColor.Value;
					else if (LabelColor.HasValue)
						drawColor = LabelColor.Value;
					else if (LabelFillColor.HasValue)
						drawColor = System.Drawing.Color.FromArgb((byte)(~LabelFillColor.Value.R), (byte)(~LabelFillColor.Value.G), (byte)(~LabelFillColor.Value.B));
					else
						drawColor = Color;
                    selectedColor = SelectedLabelBorderColor;
                    break;
				case COLOR_SELECT.MARKER_COLOR:
					// マーカー色の取得
					if (MarkerColor.HasValue)
						drawColor = MarkerColor.Value;
					else
						drawColor = Color;
					selectedColor = SelectedColor;
					break;
                default:
					// 通常の描画色取得
                    drawColor = Color;
                    selectedColor = SelectedColor;
                    break;
            }

            // 選択中かどうか
            if (Selected)
            {
                if (selectedColor.HasValue)
                    drawColor = selectedColor.Value;
                else
                    drawColor = System.Drawing.Color.FromArgb(drawColor.A, (byte)(~drawColor.R), (byte)(~drawColor.G), (byte)(~drawColor.B));
            }
            return drawColor;
        }

		/// <summary>
		/// 指定座標にマーカーを描画する
		/// </summary>
		/// <param name="graphics">描画グラフィック</param>
		/// <param name="marker">マーカー種別</param>
		/// <param name="point">描画する座標</param>
		/// <param name="drawColor">描画色</param>
		/// <param name="fillcolor">塗りつぶし色</param>
		/// <param name="lineWidth">線幅</param>
		/// <param name="radius">描画半径</param>
		/// <param name="dashStyle">線種</param>
		protected void DrawMarker(System.Drawing.Graphics graphics, MARKER marker, System.Drawing.PointF point,
			System.Drawing.Color drawColor, System.Drawing.Color fillcolor, float lineWidth , float radius, DashStyle dashStyle)
		{
			// 描画Pen
			System.Drawing.Pen pen = new System.Drawing.Pen(drawColor, lineWidth)
			{
				DashStyle = dashStyle
			};
			switch (marker)
			{
				case MARKER.POINT:
					graphics.DrawRectangle(pen, point.X, point.Y, 1.0F, 1.0F);
					break;
				case MARKER.CIRCLE_FILL:
				case MARKER.CIRCLE:
					if (marker == MARKER.CIRCLE_FILL)
					{   // 先に塗りつぶす
						System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(fillcolor);
						graphics.FillEllipse(brush, point.X - radius / 2.0F, point.Y - radius / 2.0F, radius, radius);
					}
					// 外枠を描画
					graphics.DrawEllipse(pen, point.X - radius / 2.0F, point.Y - radius / 2.0F, radius, radius);
					break;
				case MARKER.PLUS:
					graphics.DrawLine(pen, point.X - radius / 2.0F, point.Y, point.X + radius / 2.0F, point.Y);
					graphics.DrawLine(pen, point.X, point.Y - radius / 2.0F, point.X, point.Y + radius / 2.0F);
					break;
				case MARKER.CROSS:
					graphics.DrawLine(pen, point.X - radius / 2.0F, point.Y - radius / 2.0F, point.X + radius / 2.0F, point.Y + radius / 2.0F);
					graphics.DrawLine(pen, point.X + radius / 2.0F, point.Y - radius / 2.0F, point.X - radius / 2.0F, point.Y + radius / 2.0F);
					break;
				case MARKER.STAR:
					graphics.DrawLine(pen, point.X - radius / 2.0F, point.Y, point.X + radius / 2.0F, point.Y);
					graphics.DrawLine(pen, point.X, point.Y - radius / 2.0F, point.X, point.Y + radius / 2.0F);
					graphics.DrawLine(pen, point.X - (float)(radius / Math.Sqrt(2.0F)), point.Y - (float)(radius / Math.Sqrt(2.0F)),
						point.X + (float)(radius / Math.Sqrt(2.0F)), point.Y + (float)(radius / Math.Sqrt(2.0F)));
					graphics.DrawLine(pen, point.X + (float)(radius / Math.Sqrt(2.0F)), point.Y - (float)(radius / Math.Sqrt(2.0F)),
						point.X - (float)(radius / Math.Sqrt(2.0F)), point.Y + (float)(radius / Math.Sqrt(2.0F)));
					break;
			}
		}

		/// <summary>
		/// 図形の描画
		/// </summary>
		/// <param name="graphics">グラフィック</param>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>図形の頂点座標配列</returns>
		protected virtual List<System.Drawing.PointF> DrawShape(System.Drawing.Graphics graphics, Matrix matrixInv, System.Drawing.Size size)
        {
            List<System.Drawing.PointF> pts = CalcDraw(matrixInv, size);
            if (pts != null)
            {
				// マーカーを描画
				DrawMarker(graphics, Marker, pts[0],
					GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
					MarkerLineWidth, MarkerSize, DashStyle.Solid);
               return pts;
            }
            return null;
        }
        /// <summary>
        /// 文字列表示位置からオフセットを算出
        /// </summary>
        /// <param name="textSize">文字列表示サイズ</param>
        /// <param name="leftRight">1:左,2:中央,3:右</param>
        /// <param name="topBottom">1:上,2;中央,3:下</param>
        /// <param name="upDown">0:基準線より上,1:基準線より下</param>
        /// <param name="offsetX">オフセットX</param>
        /// <param name="offsetY">オフセットY</param>
        protected void CalcTextOffset(System.Drawing.SizeF textSize,out int leftRight,out int topBottom,out int upDown,out float offsetX,out float offsetY)
        {
            // 文字列表示位置の分解
            leftRight = (int)LabelPosition & 0x00F;
            topBottom = ((int)LabelPosition & 0x0F0) >> 4;
            upDown = ((int)LabelPosition & 0xF00) >> 8;

            if (leftRight == 1)
                offsetX = 0;
            else if (leftRight == 2)
                offsetX = -textSize.Width / 2.0F;
            else
                offsetX = -textSize.Width;

            if (topBottom == 2)
                offsetY = -textSize.Height / 2.0F;
            else if (upDown == 0)
                offsetY = -textSize.Height;
            else
                offsetY = 0;
        }

        /// <summary>
        /// テキスト表示位置の算出
        /// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
        /// <param name="textSize">文字列表示サイズ</param>
        /// <param name="size">表示サイズ</param>
        /// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">逆アフィン行列</param>
        /// <returns>テキスト表示領域</returns>
        protected virtual System.Drawing.RectangleF CalcTextPosition<PLIST>(System.Drawing.SizeF textSize, System.Drawing.Size size,
			PLIST pts, Matrix matrixInv) where PLIST:IList<System.Drawing.PointF>
        {
            // 表示位置とオフセットを計算
            CalcTextOffset(textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY);

			// リストの最後が中心点
			System.Drawing.PointF pt = new System.Drawing.PointF(pts.Last().X + offsetX, pts.Last().Y + offsetY);
            // はみ出す場合の処理
            if (pts.Last().X + offsetX >= size.Width)
                pt.X = size.Width - textSize.Width;
            if (pts.Last().X + offsetX < 0.0F)
                pt.X = 0;
            if (pts.Last().Y + offsetY >= size.Height)
                pt.Y = size.Height - textSize.Height;
            if (pts.Last().Y + offsetY < 0.0F)
                pt.Y = 0;

            return new System.Drawing.RectangleF(pt, textSize);
        }
		/// <summary>
		/// 文字列の描画
		/// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
		/// <param name="graphics">描画グラフィック</param>
		/// <param name="size">表示サイズ</param>
		/// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">逆アフィン行列</param>
		protected virtual void DrawText<PLIST>(System.Drawing.Graphics graphics, System.Drawing.Size size,PLIST pts, Matrix matrixInv)
			where PLIST:IList<System.Drawing.PointF>
        {
            if ((ShowLable) && (Text != null))
            {
				// 文字列の描画サイズ
				System.Drawing.SizeF textSize = TextRenderer.MeasureText(graphics, Text, LabelFont);
				// 描画サイズ
				System.Drawing.RectangleF textRect = CalcTextPosition(textSize,size, pts , matrixInv);

                if ((textRect.X >= 0) || (textRect.X < size.Width) ||
                    (textRect.Y >= 0) || (textRect.Y < size.Height) ||
                    (textRect.X + textRect.Width >= 0) || (textRect.X + textRect.Width < size.Width) ||
                    (textRect.Y + textRect.Height >= 0) || (textRect.Y + textRect.Height < size.Width))
                {
                    if (LabelFill)
                    {   // 塗りつぶし
						System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.TEXT_FILL_COLOR));
                        graphics.FillRectangle(brush, textRect);
                        brush.Dispose();
                    }
                    if (LabelBorder)
                    {   // 枠線
						System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.TEXT_BORDER_COLOR), 1.0F);
                        graphics.DrawRectangle(pen, textRect.X, textRect.Y, textRect.Width, textRect.Height);
                        pen.Dispose();
                    }
					// 文字列の描画
					System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.TEXT_COLOR));
                    graphics.DrawString(Text, LabelFont, textBrush, textRect.X, textRect.Y);
                    textBrush.Dispose();
                }
            }
        }
		/// <summary>
		/// 図形の移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <param name="limitSize">移動リミット</param>
		/// <returns>true:移動可能</returns>
		protected virtual bool MoveShape(int offsetX, int offsetY, System.Drawing.Size limitSize)
        {
            bool isMove = true;
            if (limitSize.IsEmpty)
				Center = new System.Drawing.Point((int)(Center.X + offsetX), (int)(Center.Y + offsetY));
            else
            {
                if ((Center.X + offsetX >= 0) && (Center.X + offsetX < limitSize.Width) &&
                    (Center.Y + offsetY >= 0) && (Center.Y + offsetY < limitSize.Height))
                {   // 移動可能
					Center = new System.Drawing.Point((int)(Center.X + offsetX), (int)(Center.Y + offsetY));
                }
                else
                {   // 移動しない...
                    isMove = false;
                }
            }
            return isMove;
        }

        /// <summary>
        /// 図形の移動
        /// </summary>
        /// <param name="offsetX">X移動量</param>
        /// <param name="offsetY">Y移動量</param>
        /// <param name="limitSize">移動リミット</param>
        /// <returns>true:移動可能</returns>
        public virtual bool Move(int offsetX,int offsetY, System.Drawing.Size limitSize)
        {
			if (IsEditable)
				return MoveShape(offsetX, offsetY,limitSize);
			return false;
        }
        /// <summary>
        /// 図形の移動
        /// </summary>
        /// <param name="offsetX">X移動量</param>
        /// <param name="offsetY">Y移動量</param>
        /// <returns>true:移動可能</returns>
        public virtual bool Move(int offsetX, int offsetY)
        {
			if (IsEditable)
				return MoveShape(offsetX, offsetY, new System.Drawing.Size());
			return false;
		}
		/// <summary>
		/// アンカーの移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <param name="anchorPoint">アンカー位置</param>
		/// <param name="limitSize">移動リミット</param>
		/// <returns>true:移動可能</returns>
		public virtual bool MoveAnchor(int offsetX,int offsetY,int anchorPoint, System.Drawing.Size limitSize)
		{
			if (IsEditable)
			{   // 中心座標を動かすだけ
				return MoveShape(offsetX, offsetY, limitSize);
			}
			return false;
		}

		/// <summary>
		/// 図形の選択
		/// </summary>
		/// <param name="isSelect">true:図形を選択/false:選択解除</param>
		/// <returns>以前の選択状態</returns>
		public bool Select(bool isSelect)
        {
            bool before = Selected;
            Selected = isSelect;
            return before;
        }
        /// <summary>
        /// 図形の座標値もしくは形
        /// </summary>
        /// <returns>自分自身</returns>
        public virtual object GetObject()
        {
            return this;
        }
		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected virtual void CreateContextMenu()
		{
			ToolStripMenuItem BringToFront = new ToolStripMenuItem("前面に移動", null, MenuBringToEventHandler, "BringToFront");
			BringToFront.DropDownItems.AddRange(new ToolStripItem[]
			{
				new ToolStripMenuItem("前面に移動", null, MenuBringToEventHandler, "BringToFrontSub"),
				new ToolStripMenuItem("最前面に移動", null, MenuBringToEventHandler, "BringToTop"),
			});
			ToolStripMenuItem BringToBack = new ToolStripMenuItem("背面に移動", null, MenuBringToEventHandler, "BringToBack");
			BringToBack.DropDownItems.AddRange(new ToolStripItem[]
			{
				new ToolStripMenuItem("背面に移動", null, MenuBringToEventHandler, "BringToBackSub"),
				new ToolStripMenuItem("最背面に移動", null, MenuBringToEventHandler, "BringToBottom"),
			});
			// プロパティメニューを生成する
			ContextMenu.Items.AddRange( new ToolStripItem[] {
				new ToolStripMenuItem("プロパティ", null, MenuPropretyEventHandler, "ShowProperty"),
				new ToolStripSeparator(){ Name = "UpDownSeparator1" },
				BringToFront,
				BringToBack,
			});
		}

		/// <summary>
		/// メニュー表示時のアンカー番号
		/// </summary>
		protected int MenuAnchorNo { get; private set; } = -1;
		/// <summary>
		/// メニュー表示時の座標(画像座標系)
		/// </summary>
		protected System.Drawing.Point MenuImageLocation { get; private set; } = new System.Drawing.Point();
		/// <summary>
		/// メニュー表示位置（画面座標系）
		/// </summary>
		protected System.Drawing.Point MenuLocation { get; private set; } = new System.Drawing.Point();
		/// <summary>
		/// コンテキストメニュー表示
		/// </summary>
		/// <param name="ctrl">コントロール</param>
		/// <param name="anchorNo">アンカー番号</param>
		/// <param name="imageLocation">画像座標</param>
		/// <param name="location">ローカル座標</param>
		/// <returns>true:メニュー表示(メニューあり)/false:メニューなし</returns>
		public virtual bool ShowMenu(Control ctrl,System.Drawing.Point location, System.Drawing.Point imageLocation, int anchorNo = -1)
		{
			if (ContextMenu != null)
			{
				MenuAnchorNo = anchorNo;
				MenuImageLocation = imageLocation;
				MenuLocation = ctrl.PointToScreen(location);
				ContextMenu.Show(ctrl, location);
				return true;
			}
			MenuAnchorNo = -1;
			MenuImageLocation = new System.Drawing.Point();
			return false;
		}
		/// <summary>
		/// コンテキストメニューの有効・無効設定
		/// </summary>
		/// <param name="anchorNo">アンカー番号 -1は図形選択</param>
		/// <param name="maxShapeNo">最大図形番号</param>
		/// <returns>true:メニューあり/false:メニューなし</returns>
		public virtual bool SetMenuEnable(int anchorNo = -1,int maxShapeNo = -1)
		{
			if (ZOrder == 0)
				ContextMenu.Items["BringToBack"].Visible = false;
			else
				ContextMenu.Items["BringToBack"].Visible = true;
			if (ZOrder == maxShapeNo)
				ContextMenu.Items["BringToFront"].Visible = false;
			else
				ContextMenu.Items["BringToFront"].Visible = true;
			return true;
		}
		/// <summary>
		/// プロパティメニュー実行
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuPropretyEventHandler(object sender, EventArgs e)
		{
			// プロパティフォームを生成
			Dialog.PropertyForm form = new Dialog.PropertyForm(string.Format("{0}のプロパティ",this.SHAPE_NAME()), this)
			{
				Location = MenuLocation,
			};
			// 値変更イベントを追加
			form.ChangeValueEvent += PropForm_ChangeValueEvent;
			// フォーム表示
			if (form.ShowDialog() == DialogResult.OK)
			{	// 図形再描画
				OnUpdateShape();
			}
			// イベント削除
			form.ChangeValueEvent -= PropForm_ChangeValueEvent;
			form.Dispose();
		}
		/// <summary>
		/// プロパティフォームの更新イベント処理
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="name">図形名</param>
		/// <param name="newValue">変更後の値</param>
		/// <param name="oldValue">変更前の値</param>
		/// <remarks>
		/// 図形を再描画する
		/// </remarks>
		private void PropForm_ChangeValueEvent(object sender, string name, object newValue, object oldValue)
		{
			// 図形再描画
			OnUpdateShape();
		}
		/// <summary>
		/// 図形のZOrder設定メニュー実行
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuBringToEventHandler(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu)
			{
				if ((menu.Name == "BringToFront") || (menu.Name == "BringToFrontSub"))
					OnUpdateZOrder(ZOrder + 1);
				if (menu.Name == "BringToTop")
					OnUpdateZOrder(Z_ORDER_TO_TOP);
				if (((menu.Name == "BringToBack") || (menu.Name == "BringToBackSub")) && (ZOrder > 0))
					OnUpdateZOrder(ZOrder - 1);
				if (menu.Name == "BringToBottom")
					OnUpdateZOrder(Z_ORDER_TO_BOTTOM);
			}
		}
	}
}

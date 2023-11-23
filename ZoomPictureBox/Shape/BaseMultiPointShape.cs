using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Shape
{
	/// <summary>
	/// 複数点を持つ図形のベースクラス
	/// </summary>
	[Browsable(false)]
	public class BaseMultiPointShape :BaseShape
	{
		/// 塗りつぶし色
		/// </summary>
		[Category("描画"), DisplayName("塗りつぶし色"),
			Description("図形の塗りつぶし色。未設定の場合は描画色の反転色を使う")]
		public virtual System.Drawing.Color? FillColor { get; set; }
		/// <summary>
		/// 線幅
		/// </summary>
		[Category("描画"), DisplayName("線幅"), DefaultValue(1.0F), Description("描画線の幅")]
		public virtual float LineWidth { get; set; } = 1.0F;
		/// <summary>
		/// 線種
		/// </summary>
		[Category("描画"), DisplayName("線の種類"), DefaultValue(typeof(DashStyle), "Solid"), Description("描画線の種類")]
		public virtual DashStyle LineStyle { get; set; } = DashStyle.Solid;
		/// <summary>
		/// 閉じた線か？
		/// </summary>
		public virtual bool IsClose { get; set; } = false;
		/// <summary>
		/// 塗りつぶし
		/// </summary>
		[Category("描画"), DisplayName("塗りつぶし"), DefaultValue(false), Description("図形を塗つぶすかどうか")]
		public bool Fill { get; set; } = false;
		/// <summary>
		/// 中心点の描画
		/// </summary>
		[Category("描画"), DisplayName("中心点の描画"), DefaultValue(false), Description("中心点の描画するかどうか")]
		public bool CenterDraw { get; set; } = false;
		/// <summary>
		/// 外形矩形
		/// </summary>
		[Category("外接矩形"), DisplayName("外接矩形"), Description("頂点群が外接する矩形")]
		public virtual System.Drawing.RectangleF BoundingRectangle { get; protected set; } = new System.Drawing.RectangleF();

		/// <summary>
		/// 描画点
		/// </summary>
		protected List<System.Drawing.PointF> points_ { get; set; } = new List<System.Drawing.PointF>();

		/// <summary>
		/// 座標値の取得・設定
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override System.Drawing.PointF this[int index]
		{
			get
			{
				if ((index >= 0) && (index < points_.Count))
					return new System.Drawing.PointF(points_[index].X,points_[index].Y);
				return new System.Drawing.Point();
			}
			set
			{
				if ((index >= 0) && (index < points_.Count))
				{
					points_[index] = value;
					updateRectangle();
				}
			}
		}
		/// <summary>
		/// 座標値の件数
		/// </summary>
		[Browsable(false)]
		public override int Count { get { return points_.Count; } }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		public BaseMultiPointShape(string name) : base(name) { }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="isEditable">編集可能かどうか</param>
		public BaseMultiPointShape(string name, bool isEditable) : base(name, isEditable) { }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public BaseMultiPointShape(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name,color)
		{
			LineWidth = lineWidth;
			LineStyle = lineStyle;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">頂点座標</param>
		public BaseMultiPointShape(string name, params System.Drawing.Point[] points) : this(name)
		{
			// 座標の設定
			SetLocal(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="isClose">true:閉じた図形</param>
		/// <param name="points">頂点座標</param>
		public BaseMultiPointShape(string name, bool isClose, params System.Drawing.Point[] points) : this(name, points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="isClose">true:閉じた図形</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public BaseMultiPointShape(string name, bool isClose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, color, lineWidth, lineStyle)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">頂点座標</param>
		public BaseMultiPointShape(string name, params System.Drawing.PointF[] points) : this(name)
		{
			// 座標の設定
			SetLocal(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="isClose">true:閉じた図形</param>
		/// <param name="points">頂点座標</param>
		public BaseMultiPointShape(string name, bool isClose, params System.Drawing.PointF[] points) : this(name, points)
		{
			IsClose = isClose;
		}

		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public BaseMultiPointShape(BaseShape shape) : base(shape) { }
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public BaseMultiPointShape(BaseMultiPointShape shape) : base(shape) { }

		/// <summary>
		/// 値をコピー
		/// </summary>
		/// <param name="src">コピー元</param>
		/// <param name="copySelected">選択中をコピーするか(デフォルト:true)</param>
		/// <param name="copyIndex">インデックスをコピーするか(デフォルト:false)</param>
		/// <param name="copyMenu">コンテキストメニューをコピーするか(デフォルト:false)</param>
		public override void Copy(BaseShape src, bool copySelected = true, bool copyIndex = false, bool copyMenu = false)
		{
			base.Copy(src, copySelected, copyIndex, copyMenu);
			if (src is BaseMultiPointShape shape)
			{
				/// 塗りつぶし色
				FillColor = shape.FillColor;
				/// 線幅
				LineWidth = shape.LineWidth;
				/// 線種
				LineStyle = shape.LineStyle;
				// 閉じた図形
				IsClose = shape.IsClose;
				// 塗りつぶし
				Fill = shape.Fill;
				// 中心点の描画
				CenterDraw = shape.CenterDraw;
				// 外接矩形
				BoundingRectangle = shape.BoundingRectangle;
				// 座標値
				points_ = new List<System.Drawing.PointF>(shape.points_);
			}
		}
		/// <summary>
		/// 制御点の設定(コンストラクタ用)
		/// </summary>
		/// <param name="points"></param>
		protected bool SetLocal(params System.Drawing.Point[] points)
		{
			// 制御点の生成
			points_.Clear();
			for (int index = 0; index < points.Length; index++)
				points_.Add(new System.Drawing.PointF(points[index].X, points[index].Y));
			// 外形矩形の更新
			updateRectangle();
			return true;

		}
		/// <summary>
		/// 制御点の設定(コンストラクタ用)
		/// </summary>
		/// <param name="points"></param>
		protected bool SetLocal(params System.Drawing.PointF[] points)
		{
			// 制御点の生成
			points_.Clear();
			points_.AddRange(points);
			// 外形矩形の更新
			updateRectangle();
			return true;
		}
		/// <summary>
		/// 制御点の設定
		/// </summary>
		/// <param name="points"></param>
		public virtual bool Set(params System.Drawing.Point[] points)
		{
			// 制御点の生成
			return SetLocal(points);
		}
		/// <summary>
		/// 制御点の設定
		/// </summary>
		/// <param name="points"></param>
		public virtual bool Set(params System.Drawing.PointF[] points)
		{
			// 制御点の生成
			return SetLocal(points);
		}
		/// <summary>
		/// 制御点の追加
		/// </summary>
		/// <param name="points"></param>
		public virtual bool Add(params System.Drawing.Point[] points)
		{
			// 制御点の追加
			for (int index = 0; index < points.Length; index++)
				points_.Add(new System.Drawing.PointF(points[index].X, points[index].Y));
			// 外形矩形の更新
			updateRectangle();
			return true;
		}
		/// <summary>
		/// 制御点の追加
		/// </summary>
		/// <param name="points"></param>
		public virtual bool Add(params System.Drawing.PointF[] points)
		{
			points_.AddRange(points);
			// 外形矩形の更新
			updateRectangle();
			return true;
		}
		/// <summary>
		/// 外形矩形の更新
		/// </summary>
		protected virtual void updateRectangle()
		{
			// 外形矩形
			BoundingRectangle = ShapeUtil.getBoundingRectangleF(points_);
			if (BoundingRectangle.IsEmpty == false)
			{	// 中心点
				Center = new System.Drawing.Point((int)((BoundingRectangle.Left + BoundingRectangle.Right) / 2.0F),
					(int)((BoundingRectangle.Top + BoundingRectangle.Bottom) / 2.0F));
			}
		}
		/// <summary>
		/// 描画色を取得
		/// </summary>
		/// <returns></returns>
		protected override System.Drawing.Color GetDrawColor(COLOR_SELECT colorSelect)
		{
			System.Drawing.Color drawColor;
			System.Drawing.Color? selectedColor;
			switch (colorSelect)
			{
				case COLOR_SELECT.FILL_COLOR:
					// 塗りつぶし色取得

					if (FillColor.HasValue)
						drawColor = FillColor.Value;
					else
						drawColor = System.Drawing.Color.FromArgb(127, Color);
					selectedColor = SelectedFillColor;
					break;
				case COLOR_SELECT.TEXT_FILL_COLOR:
					// ラベル塗りつぶし色取得
					if (LabelFillColor.HasValue)
						drawColor = LabelFillColor.Value;
					else if (FillColor.HasValue)
						drawColor = FillColor.Value;
					else if (LabelColor.HasValue)
						drawColor = System.Drawing.Color.FromArgb((byte)(~LabelColor.Value.R), (byte)(~LabelColor.Value.G), (byte)(~LabelColor.Value.B));
					else if (LabelBorderColor.HasValue)
						drawColor = System.Drawing.Color.FromArgb((byte)(~LabelBorderColor.Value.R), (byte)(~LabelBorderColor.Value.G), (byte)(~LabelBorderColor.Value.B));
					else
						drawColor = System.Drawing.Color.FromArgb(127, Color);
					selectedColor = SelectedLabelFillColor;
					break;
				default:
					// 基底クラスを呼び出す
					return base.GetDrawColor(colorSelect);
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
		/// 線の終端形状指定(ローカル)
		/// </summary>
		protected enum LINECAP_SHAPE_LOCAL
		{
			NONE = LINECAP_SHAPE.NONE,
			RECTANGLE = LINECAP_SHAPE.RECTANGLE,
			DIAMOND = LINECAP_SHAPE.DIAMOND,
			ARROW_LEFT = LINECAP_SHAPE.ARROW,
			CIRCLE = LINECAP_SHAPE.CIRCLE,
			ARROW_RIGHT,
		}
		/// <summary>
		/// 線の終端形状定義
		/// </summary>
		private readonly List<System.Drawing.PointF>[] LineCapeShapeDefinition = new List<System.Drawing.PointF>[]
		{
			// ■
			new List<System.Drawing.PointF>
			{
				new System.Drawing.PointF(0.5F,0.5F),new System.Drawing.PointF(-0.5F,0.5F),
				new System.Drawing.PointF(-0.5F,-0.5F),new System.Drawing.PointF(0.5F,-0.5F),
			},
			// ◆
			new List<System.Drawing.PointF>
			{
				new System.Drawing.PointF(0.5F,0.0F),new System.Drawing.PointF(0.0F,0.5F),
				new System.Drawing.PointF(-0.5F,0.0F),new System.Drawing.PointF(0.0F,-0.5F),
			},
			// ▶
			new List<System.Drawing.PointF>
			{
				new System.Drawing.PointF(0.0F,0.0F),new System.Drawing.PointF(-0.5F,0.5F),
				new System.Drawing.PointF(-0.5F,-0.5F)
			},
			// ◀
			new List<System.Drawing.PointF>
			{
				new System.Drawing.PointF(0.0F,0.0F),new System.Drawing.PointF(0.5F,0.5F),
				new System.Drawing.PointF(0.5F,-0.5F)
			},
		};
		/// <summary>
		/// 線の終端形状のIndex取得
		/// </summary>
		/// <param name="lineCap"></param>
		/// <returns></returns>
		private int GetLineCapShapeIndex(LINECAP_SHAPE_LOCAL lineCap)
		{
			switch (lineCap)
			{
				case LINECAP_SHAPE_LOCAL.RECTANGLE:
					return 0;
				case LINECAP_SHAPE_LOCAL.DIAMOND:
					return 1;
				case LINECAP_SHAPE_LOCAL.ARROW_LEFT:
					return 3;
				case LINECAP_SHAPE_LOCAL.ARROW_RIGHT:
					return 2;
				default:
					return -1;
			}
		}
		/// <summary>
		/// 線の終端形状の変換
		/// </summary>
		/// <param name="linecap"></param>
		/// <param name="isFirst"></param>
		/// <returns></returns>
		protected LINECAP_SHAPE_LOCAL LineCapConvert(LINECAP_SHAPE linecap, bool isFirst = true)
		{
			switch (linecap)
			{
				case LINECAP_SHAPE.RECTANGLE:
					return LINECAP_SHAPE_LOCAL.RECTANGLE;
				case LINECAP_SHAPE.DIAMOND:
					return LINECAP_SHAPE_LOCAL.DIAMOND;
				case LINECAP_SHAPE.CIRCLE:
					return LINECAP_SHAPE_LOCAL.CIRCLE;
				case LINECAP_SHAPE.ARROW:
					if (isFirst)
						return LINECAP_SHAPE_LOCAL.ARROW_LEFT;
					else
						return LINECAP_SHAPE_LOCAL.ARROW_RIGHT;
				default:
					return LINECAP_SHAPE_LOCAL.NONE;
			}
		}
		/// <summary>
		/// 線の終端を描画
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="lineCap"></param>
		/// <param name="point"></param>
		/// <param name="capSize"></param>
		/// <param name="lineAngle"></param>
		/// <param name="drawColor"></param>
		/// <param name="fillcolor"></param>
		/// <param name="lineWidth"></param>
		protected void DrawLineCap(System.Drawing.Graphics graphics, LINECAP_SHAPE_LOCAL lineCap, System.Drawing.PointF point,
			float capSize, float lineAngle, System.Drawing.Color drawColor, System.Drawing.Color fillcolor, float lineWidth)
		{
			if (lineCap == LINECAP_SHAPE_LOCAL.NONE)
				return;     // 何もしない
			if (lineCap == LINECAP_SHAPE_LOCAL.CIRCLE)
			{   // 指定位置に円を描画
				System.Drawing.Brush brush = new System.Drawing.SolidBrush(fillcolor);
				graphics.FillEllipse(brush, point.X - (capSize / 2.0F), point.Y - (capSize / 2.0F), capSize, capSize);
				System.Drawing.Pen pen = new System.Drawing.Pen(drawColor, lineWidth);
				graphics.DrawEllipse(pen, point.X - (capSize / 2.0F), point.Y - (capSize / 2.0F), capSize, capSize);
			}
			else
			{
				int cap_index = GetLineCapShapeIndex(lineCap);
				if ((cap_index >= 0) && (cap_index < LineCapeShapeDefinition.Length))
				{
					// 形状を取得
					List<System.Drawing.PointF> shape = LineCapeShapeDefinition[cap_index];
					// 変換マトリックス
					Matrix convMatrix = new Matrix();
					// 拡大・縮小
					convMatrix.Scale(capSize, capSize, MatrixOrder.Append);
					// 回転
					float angle = (float)(lineAngle * 180.0F / Math.PI);
					convMatrix.Rotate(angle, MatrixOrder.Append);
					// 平行移動
					convMatrix.Translate(point.X, point.Y, MatrixOrder.Append);
					// 形状を変換
					System.Drawing.PointF[] pts = new System.Drawing.PointF[shape.Count];
					for (int index = 0; index < pts.Length; index++)
						pts[index] = new System.Drawing.PointF(shape[index].X, shape[index].Y);
					convMatrix.TransformPoints(pts);
					// 図形を描画
					System.Drawing.Brush brush = new System.Drawing.SolidBrush(fillcolor);
					System.Drawing.Pen pen = new System.Drawing.Pen(drawColor, lineWidth);
					graphics.FillPolygon(brush, pts);
					graphics.DrawPolygon(pen, pts);
				}
			}
		}
		/// <summary>
		/// 図形の描画
		/// </summary>
		/// <param name="graphics">グラフィック</param>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>図形の頂点座標配列</returns>
		protected override List<System.Drawing.PointF> DrawShape(System.Drawing.Graphics graphics, Matrix matrixInv, System.Drawing.Size size)
		{
			List<System.Drawing.PointF> pts = CalcDraw(matrixInv, size);
			if (pts != null)
			{
				if (DebugMode.HasFlag(DEBUG_MODE.OUTER_LINE))
				{	// 外形枠描画
					System.Drawing.PointF[] rectPoints = new System.Drawing.PointF[]{ BoundingRectangle.Location,
						new System.Drawing.PointF(BoundingRectangle.Right,BoundingRectangle.Bottom)};
					matrixInv.TransformPoints(rectPoints);
					graphics.DrawRectangle(new System.Drawing.Pen(AnchorColor, MarkerLineWidth),
						rectPoints[0].X, rectPoints[0].Y, rectPoints[1].X - rectPoints[0].X, rectPoints[1].Y - rectPoints[0].Y);
				}
			}
			return pts;
		}

		/// <summary>
		/// 座標変換
		/// </summary>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>有効な場合は、描画座標(PointF)の配列。無効な場合はnull</returns>
		/// <remarks>
		/// [0] ... 始点座標
		/// [1] ... 終点座標
		/// </remarks>
		protected override List<System.Drawing.PointF> CalcDraw(Matrix matrixInv, System.Drawing.Size size)
		{
			if ((IsDrawable()) && (points_.Count > 0))
			{
				// 各点の座標
				System.Drawing.PointF[] pts = new System.Drawing.PointF[points_.Count];
				for (int index = 0; index < points_.Count; index++)
					pts[index] = new System.Drawing.PointF(points_[index].X, points_[index].Y);
				matrixInv.TransformPoints(pts);
				// 表示サイズ内か？
				for (int index = 0; index < points_.Count; index++)
				{
					if ((pts[index].X >= 0.0F) && (pts[index].X < size.Width) &&
						(pts[index].Y >= 0.0F) && (pts[index].Y < size.Height))
						return pts.ToList();
				}
			}
			return null;
		}
		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <param name="pts">計算した座標値</param>
		/// <param name="point">指定座標</param>
		/// <returns>true:指定座標が領域に含まれている</returns>
		protected override bool IsContain<PLIST>(PLIST pts, System.Drawing.Point point)
		{
			if (pts.Count >= 2)
			{
				for (int index = 0; index < pts.Count - 1; index++)
				{
					if (ShapeUtil.IsContain(pts[index], pts[index + 1], point, HitMargin))
						return true;
				}
				if (IsClose)
					return ShapeUtil.IsContain(pts.Last(), pts[0], point, HitMargin);
			}
			return false;
		}
		/// <summary>
		/// アンカーの当たり判定
		/// </summary>
		/// <param name="pts">計算した座標値</param>
		/// <param name="point">指定座標</param>
		/// <param name="anchorPoint">アンカー位置(出力)</param>
		/// <param name="startIndex">対象となる座標の先頭のインデックス(デフォルト:0)</param>
		/// <param name="lastIndex">対象となる座標の最後のインデックス(デフォルト:-1)</param>
		/// <returns>true:指定座標が領域に含まれている</returns>
		/// <remarks>
		/// lastIndexが-1の場合は、座標値の最後まで対象とする
		/// </remarks>
		protected override bool HitTestAnchor<PLIST>(PLIST pts, System.Drawing.Point point, out int anchorPoint,
				int startIndex = 0, int lastIndex = -1)
		{
			anchorPoint = -1;
			if (pts.Count >= 2)
			{
				return base.HitTestAnchor(pts, point, out anchorPoint, startIndex, lastIndex);
			}
			return false;
		}
		/// <summary>
		/// 図形の移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <param name="limitSize">表示領域サイズ</param>
		/// <returns>true:移動可能</returns>
		protected override bool MoveShape(int offsetX, int offsetY, System.Drawing.Size limitSize)
		{
			bool isMove = true;
			if (limitSize.IsEmpty)
			{
				for (int index = 0; index < points_.Count; index++)
					points_[index] = new System.Drawing.PointF(points_[index].X + offsetX, points_[index].Y + offsetY);
				// 外形矩形の更新
				updateRectangle();
			}
			else
			{
				if ((BoundingRectangle.Left + offsetX >= 0) && (BoundingRectangle.Right + offsetX < limitSize.Width) &&
					(BoundingRectangle.Top + offsetY >= 0) && (BoundingRectangle.Bottom + offsetY < limitSize.Height))
				{   // 移動可能
					for (int index = 0; index < points_.Count; index++)
						points_[index] = new System.Drawing.PointF(points_[index].X + offsetX, points_[index].Y + offsetY);
					// 外形矩形の更新
					updateRectangle();
				}
				else
				{   // 移動しない...
					isMove = false;
				}
			}
			return isMove;
		}
		/// <summary>
		/// アンカーの移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <param name="anchorPoint">アンカー位置</param>
		/// <param name="limitSize">表示領域サイズ</param>
		/// <returns>true:移動可能</returns>
		public override bool MoveAnchor(int offsetX, int offsetY, int anchorPoint, System.Drawing.Size limitSize)
		{
			if ((limitSize.IsEmpty) || (IsEditable == false) || (anchorPoint < 0) || (anchorPoint >= points_.Count))
				return base.MoveShape(offsetX, offsetY, limitSize);

			if ((this[anchorPoint].X + offsetX >= 0) && (this[anchorPoint].X + offsetX <= limitSize.Width) &&
				(this[anchorPoint].Y + offsetY >= 0) && (this[anchorPoint].Y + offsetY <= limitSize.Height))
			{
				this[anchorPoint] = new System.Drawing.PointF(this[anchorPoint].X + offsetX, this[anchorPoint].Y + offsetY);
				// 外形矩形の更新
				updateRectangle();
				return true;
			}
			return false;
		}
		/// <summary>
		/// テキスト表示位置の算出
		/// </summary>
		/// <param name="textSize">文字列表示サイズ</param>
		/// <param name="size">表示領域サイズ</param>
		/// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">変換逆行列</param>
		/// <returns>テキスト表示領域</returns>
		protected override System.Drawing.RectangleF CalcTextPosition<PLIST>(System.Drawing.SizeF textSize, System.Drawing.Size size,
			PLIST pts, Matrix matrixInv) 
		{
			// 表示位置とオフセットを計算
			CalcTextOffset(textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY);
			// 外接矩形座標の変換
			System.Drawing.PointF[] rectPos =
			{
				new System.Drawing.PointF(BoundingRectangle.X,BoundingRectangle.Y),
				new System.Drawing.PointF(BoundingRectangle.Right,BoundingRectangle.Bottom)
			};
			matrixInv.TransformPoints(rectPos);

			System.Drawing.PointF textPoint = new System.Drawing.PointF();
			// 横位置
			if (leftRight == 1)
			{
				textPoint.X = rectPos[0].X;
			}
			else if (leftRight == 2)
				textPoint.X = (rectPos[0].X + rectPos[1].X) / 2.0F + offsetX;
			else if (leftRight == 3)
			{
				textPoint.X = rectPos[1].X + offsetX;
			}
			// 縦位置
			if (topBottom == 1)
			{
				textPoint.Y = rectPos[0].Y + offsetY;
			}
			else if (topBottom == 2)
				textPoint.Y = (rectPos[0].Y + rectPos[1].Y) / 2.0F + offsetY;
			else if (topBottom == 3)
			{
				textPoint.Y = rectPos[1].Y + offsetY;
			}
			return new System.Drawing.RectangleF(textPoint, textSize);
		}

		/// <summary>
		/// メニューから制御点追加
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuPointAddEventHandler(object sender, EventArgs e)
		{
			int insert_pos = points_.Count;
			// どの線分上かチェック
			if (points_.Count >= 2)
			{
				for (int index = 0; index < points_.Count - 1; index++)
				{
					if (ShapeUtil.IsContain(points_[index], points_[index + 1], MenuImageLocation, HitMargin))
					{
						insert_pos = index + 1;
						break;
					}
				}
			}
			if (insert_pos < points_.Count)
				points_.Insert(insert_pos, MenuImageLocation);
			else
				points_.Add(MenuImageLocation);
			// 図形表示更新
			OnUpdateShape();
		}
		/// <summary>
		/// メニューから制御点削除
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuPointDelEventHandler(object sender, EventArgs e)
		{
			if ((MenuAnchorNo >= 0) && (MenuAnchorNo < points_.Count))
			{
				points_.RemoveAt(MenuAnchorNo);
				// 図形表示更新
				OnUpdateShape();
			}
		}
		/// <summary>
		/// ベジェ曲線に変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuToBezierEventHandler(object sender, EventArgs e)
		{
			Bezier bezier = new Bezier(this);
			// 図形表示更新
			OnUpdateShape(bezier);
		}
		/// <summary>
		/// ベジェ曲線に変換
		/// </summary>
		/// <param name="startLine">取得開始線分番号(デフォルト:0)</param>
		/// <param name="numOfLines">取得線分数(デフォルト:-1)</param>
		/// <returns>ベジェ曲線の制御ポイントリスト</returns>
		/// <remarks>
		/// numOfLinesが-1の場合は、startLineから最後の線分まで取得する
		/// </remarks>
		public virtual List<System.Drawing.PointF> ToBezier(int startLine= 0,int numOfLines = -1)
		{
			if (points_.Count >= 2)
			{
				List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();

				// 最大の線数(閉じていない場合は点数-1)
				int lineMax = points_.Count - 1;
				if (IsClose)
					lineMax++;

				if ((numOfLines < 0) || (numOfLines > lineMax)) numOfLines = lineMax;
				if ((startLine < 0) || (startLine >= lineMax)) startLine = 0;
				bool isFirst = true;
				for (int lineNo = startLine; lineNo < numOfLines; lineNo++)
				{
					int next_point = (lineNo + 1) % points_.Count;
					if (isFirst)
					{   // 最初の点
						result.Add(new System.Drawing.PointF(this[lineNo].X, this[lineNo].Y)); // ポイント
						isFirst = false;
					}
					// アンカー点
					result.Add(new System.Drawing.PointF((this[lineNo].X + this[next_point].X) / 2.0F, (this[lineNo].Y + this[next_point].Y) / 2.0F));
					result.Add(new System.Drawing.PointF((this[lineNo].X + this[next_point].X) / 2.0F, (this[lineNo].Y + this[next_point].Y) / 2.0F));
					// ポイント
					result.Add(new System.Drawing.PointF(this[next_point].X, this[next_point].Y)); // ポイント
				}
				return result;
			}
			return null;
		}
	}
}

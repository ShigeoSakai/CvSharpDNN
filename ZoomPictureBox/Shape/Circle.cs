using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing.Shape
{
	/// <summary>
	/// 円クラス
	/// </summary>
	[ShapeName("曲線"), DefaultProperty("Center")]
	public class Circle : BaseMultiPointShape
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "円";
		/// <summary>
		/// 座標値の取得・生成
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>座標値を返す</returns>
		/// <remarks>
		/// [0] ... 中心座標<br>
		/// [1] ... 半径アンカー座標<br>
		/// </remarks>
		public override System.Drawing.PointF this[int index]
		{
			get { return base[index]; }
			set
			{
				base[index] = value;
				if (index == 0)
				{	// アンカー位置の計算
					CalcAnchorPosition();
				}
				else if (index == 1)
				{   // アンカー角度と半径の計算
					CalcAnchorAngleAndRadius();
				}
			}
		}

		/// <summary>
		/// 円の中心
		/// </summary>
		[Category("図形"), DisplayName("中心"), Description("中心座標")]
		public override System.Drawing.PointF Center
		{
			get { return base.Center; }
			set
			{
				base.Center = value;
				points_[0] = value;
				// アンカー位置の計算
				CalcAnchorPosition();
			}
		}
		/// <summary>
		/// 半径
		/// </summary>
		private float radius_ = 0.0F;
		[Category("図形"), DisplayName("半径"),Description("円の半径")]
		public float Radius
		{
			get { return radius_; }
			set
			{
				radius_ = value;
				// アンカー位置の計算
				CalcAnchorPosition();
			}
		}
		/// <summary>
		/// アンカー角度
		/// </summary>
		protected double anchorAngle_ = Math.PI / 4;
		protected virtual double AnchorAngle
		{
			get { return anchorAngle_; }
			set
			{
				anchorAngle_ = value;
				// アンカー位置の計算
				CalcAnchorPosition();
			}
		}
		/// <summary>
		/// アンカー位置
		/// </summary>
		protected virtual System.Drawing.PointF AnchorPosition
		{
			get { return points_[1]; }
			set
			{
				points_[1] = value;
				// アンカー角度と半径の計算
				CalcAnchorAngleAndRadius();
			}
		}
		/// <summary>
		/// アンカー位置の計算
		/// </summary>
		private void CalcAnchorPosition()
		{
			float x = (float)(Radius * Math.Cos(anchorAngle_) + points_[0].X);
			float y = (float)(Radius * Math.Sin(anchorAngle_) + points_[0].Y);
			AnchorPosition = new System.Drawing.PointF(x, y);
			// 外接矩形
			BoundingRectangle = new System.Drawing.RectangleF(
					points_[0].X - Radius,
					points_[0].Y - Radius,
					Radius * 2.0F, Radius * 2.0F
 				);
		}
		/// <summary>
		/// アンカー角度と半径の計算
		/// </summary>
		private void CalcAnchorAngleAndRadius()
		{
			anchorAngle_ = Math.Atan2(AnchorPosition.Y - Center.Y, AnchorPosition.X - Center.X);
			radius_ = (float)Math.Sqrt(Math.Pow(AnchorPosition.Y - Center.Y, 2.0) + Math.Pow(AnchorPosition.X - Center.X, 2.0));
			// 外接矩形
			BoundingRectangle = new System.Drawing.RectangleF(
					points_[0].X - Radius,
					points_[0].Y - Radius,
					Radius * 2.0F, Radius * 2.0F
 				);
		}
		/// <summary>
		/// 制御点の生成
		/// </summary>
		private void CreatePoints()
		{
			points_.Clear();
			points_.Add(new System.Drawing.PointF());
			points_.Add(new System.Drawing.PointF());
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		public Circle(string name,System.Drawing.Point center,int radius):base(name)
		{
			// 制御点の生成
			CreatePoints();
			// 値を格納
			Center = center;
			Radius = radius;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Circle(string name, System.Drawing.Point center, int radius,bool isEditable):this(name,center,radius)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Circle(string name, System.Drawing.Point center, int radius, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle)
		{
			// 制御点の生成
			CreatePoints();
			// 値を格納
			Center = center;
			Radius = radius;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Circle(string name, System.Drawing.Point center, int radius, System.Drawing.Color color, bool isEditable,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name,center,radius, color, lineWidth, lineStyle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">外接矩形</param>
		public Circle(string name, System.Drawing.Rectangle rectangle) : base(name)
		{
			// 制御点の生成
			CreatePoints();
			// 値を格納
			Center = new System.Drawing.Point((int)(rectangle.X + rectangle.Width/2.0),(int)(rectangle.Y + rectangle.Height/2.0));
			Radius = (rectangle.Width <= rectangle.Height) ? rectangle.Width/2 : rectangle.Height/2;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">外接矩形</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Circle(string name, System.Drawing.Rectangle rectangle, bool isEditable) : this(name, rectangle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">外接矩形</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Circle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle)
		{
			// 制御点の生成
			CreatePoints();
			// 値を格納
			Center = new System.Drawing.Point((int)(rectangle.X + rectangle.Width / 2.0), (int)(rectangle.Y + rectangle.Height / 2.0));
			Radius = (rectangle.Width <= rectangle.Height) ? rectangle.Width / 2 : rectangle.Height / 2;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">外接矩形</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Circle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color color, bool isEditable,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, rectangle, color, lineWidth, lineStyle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// ３点から中心座標を半径を算出する
		/// </summary>
		/// <param name="point1">点1</param>
		/// <param name="point2">点2</param>
		/// <param name="point3">点3</param>
		private void CalcCenterAndRadius(System.Drawing.Point point1, System.Drawing.Point point2, System.Drawing.Point point3)
		{
			if (((point2.X == point1.X) && (point3.X == point1.X)) ||
				((point2.Y == point1.Y) && (point3.Y == point1.Y)))
			{   // 直線状なので計算できない
				throw new DivideByZeroException("3点が1直線上にある");
			}
			if ((point1.Equals(point2)) || (point1.Equals(point3)))
			{   // 2点もしくは1点なので計算できない
				throw new DivideByZeroException("同一点がある");
			}
			double cx = double.NaN, cy = double.NaN;
			double beta1 = Math.Pow(point2.X, 2.0) - Math.Pow(point1.X, 2.0) + Math.Pow(point2.Y, 2.0) - Math.Pow(point1.Y, 2.0);
			double beta2 = Math.Pow(point3.X, 2.0) - Math.Pow(point1.X, 2.0) + Math.Pow(point3.Y, 2.0) - Math.Pow(point1.Y, 2.0);
			if (point2.X == point1.X)
			{   // point3.X == point1.Xではない...(直線の条件になるので)
				// point2.Y == point1.Yではない...(同一点になるので)
				cy = beta1 / (2.0 * (point2.Y - point1.Y));
				cx = (beta2 - (2.0 * (point3.Y - point1.Y) * cy)) / (2.0 * (point3.X - point1.X));
			}
			else if (point3.X == point1.X)
			{   // point2.X == point1.Xではない...(直線の条件になるので)
				// point3.Y == point1.Yではない...(同一点になるので)
				cy = beta2 / (2.0 * (point3.Y - point1.Y));
				cx = (beta1 - (2.0 * (point2.Y - point1.Y) * cy)) / (2.0 * (point2.X - point1.X));
			}
			else if (point2.Y == point1.Y)
			{   // point3.Y == point1.Yではない...(直線の条件になるので)
				// point2.X == point1.Xではない...(同一点になるので)
				cx = beta1 / (2.0 * (point2.X - point1.X));
				cy = (beta2 - (2.0 * (point3.X - point1.X) * cx)) / (2.0 * (point3.Y - point1.Y));
			}
			else if (point3.Y == point1.Y)
			{   // point2.Y == point1.Yではない...(直線の条件になるので)
				// point3.X == point1.Xではない...(同一点になるので)
				cx = beta2 / (2.0 * (point3.X - point1.X));
				cy = (beta1 - (2.0 * (point2.X - point1.X) * cx)) / (2.0 * (point2.Y - point1.Y));
			}
			else
			{
				double alfaX = (point2.X - point1.X) / (point2.Y - point1.Y) - (point3.X - point1.X) / (point3.Y - point1.Y);
				double alfaY = (point2.Y - point1.Y) / (point2.X - point1.X) - (point3.Y - point1.Y) / (point3.X - point1.X);
				double betaX = beta1 / (2.0 * (point2.Y - point1.Y)) - beta2 / (2.0 * (point3.Y - point1.Y));
				double betaY = beta1 / (2.0 * (point2.X - point1.X)) - beta2 / (2.0 * (point3.X - point1.X));
				cx = betaX / alfaX;
				cy = betaY / alfaY;
			}
			// 半径を求める
			double radius = Math.Sqrt(Math.Pow(point1.X - cx, 2.0) + Math.Pow(point1.Y - cy, 2.0));
			Center = new System.Drawing.Point((int)(cx + 0.5), (int)(cx + 0.5));
			Radius = (int)(radius + 0.5);
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="point1">点1</param>
		/// <param name="point2">点2</param>
		/// <param name="point3">点3</param>
		public Circle(string name, System.Drawing.Point point1, System.Drawing.Point point2, System.Drawing.Point point3) : base(name)
		{
			// 制御点の生成
			CreatePoints();
			// 中心座標と半径を求める
			CalcCenterAndRadius(point1, point2, point3);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Circle(string name, System.Drawing.Point point1, System.Drawing.Point point2, System.Drawing.Point point3,
			System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle)
		{
			// 制御点の生成
			CreatePoints();
			// 中心座標と半径を求める
			CalcCenterAndRadius(point1, point2, point3);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">中心座標</param>
		/// <param name="radius">半径</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Circle(string name, System.Drawing.Point point1, System.Drawing.Point point2, System.Drawing.Point point3,
			System.Drawing.Color color, bool isEditable,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, point1,point2,point3,color,lineWidth,lineStyle)
		{
			IsEditable = isEditable;
		}

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
			if (src is Circle circle)
			{
				// 半径
				Radius = circle.Radius;
				// アンカー角度
				AnchorAngle = circle.anchorAngle_;
			}
		}
		/// <summary>
		/// 外形矩形の更新
		/// </summary>
		protected override void updateRectangle()
		{
			CalcAnchorAngleAndRadius();
		}
		/// <summary>
		/// 座標変換
		/// </summary>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>有効な場合は、描画座標(PointF)の配列。無効な場合はnull</returns>
		/// <remarks>
		/// [0] ... 内包する矩形の左上座標
		/// [1] ... 内包する矩形の右下座標
		/// [2] ... 円の中心
		/// [3] ... アンカー位置
		/// </remarks>
		protected override List<System.Drawing.PointF> CalcDraw(Matrix matrixInv, System.Drawing.Size size)
		{
			if (IsDrawable())
			{
				// 円の左上,右下の座標
				System.Drawing.PointF[] pts = { new System.Drawing.PointF(Center.X - Radius ,Center.Y - Radius) ,
							new System.Drawing.PointF(Center.X + Radius ,Center.Y + Radius),
							new System.Drawing.PointF(Center.X,Center.Y),
							new System.Drawing.PointF(AnchorPosition.X,AnchorPosition.Y),
				};
				matrixInv.TransformPoints(pts);
				pts[1].X += matrixInv.Elements[0];
				pts[1].Y += matrixInv.Elements[3];
				pts[3].X += matrixInv.Elements[0];
				pts[3].Y += matrixInv.Elements[3];

				if (((pts[0].X >= 0.0F) || (pts[0].X < size.Width) || (pts[0].Y >= 0.0F) || (pts[0].Y < size.Height)) ||
					((pts[1].X >= 0.0F) || (pts[1].X < size.Width) ||
					 (pts[1].Y >= 0.0F) || (pts[1].Y < size.Height)))
					return pts.ToList();
			}
			return null;
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
			List<System.Drawing.PointF> pts = base.DrawShape(graphics,matrixInv,size);
			if (pts != null)
			{
				// 描画するペンと線種
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth);
				pen.DashStyle = LineStyle;
				// 円を内包する矩形
				System.Drawing.RectangleF drawRect = new System.Drawing.RectangleF(pts[0].X, pts[0].Y, pts[1].X - pts[0].X, pts[1].Y - pts[0].Y);

				if (Fill)
				{	// 塗りつぶし
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
					graphics.FillEllipse(brush, drawRect);
				}
				// 円を描画
				graphics.DrawEllipse(pen, drawRect);

				if (CenterDraw)
				{
					// 中心描画
					List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					graphics.DrawRectangles(anchorPen, new System.Drawing.RectangleF[]
					{
						new System.Drawing.RectangleF(pts[3].X - AnchorRadius,pts[3].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
					});
					graphics.DrawLine(anchorPen, pts[2], pts[3]);
				}
				return pts;
			}
			return null;
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
		/// [3]のみが対象
		/// </remarks>
		protected override bool HitTestAnchor<PLIST>(PLIST pts, System.Drawing.Point point, out int anchorPoint,
			int startIndex = 0, int lastIndex = -1)
		{
			anchorPoint = -1;
			if (pts.Count >= 4)
			{
				System.Drawing.RectangleF rect = new System.Drawing.RectangleF(pts[3].X - AnchorRadius - HitMargin, pts[3].Y - AnchorRadius - HitMargin,
					(AnchorRadius + HitMargin) * 2, (AnchorRadius + HitMargin) * 2);
				if (rect.Contains((float)point.X, (float)point.Y))
				{
					anchorPoint = 1;
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <typeparam name="PLIST">PointFのListまたは配列</typeparam>
		/// <param name="pts">計算した領域</param>
		/// <param name="point">指定座標</param>
		/// <returns>true:指定座標が領域に含まれている</returns>
		protected override bool IsContain<PLIST>(PLIST pts, System.Drawing.Point point)
		{
			if (pts.Count >= 4)
			{
				// 中心との距離を算出
				double r = Math.Sqrt(Math.Pow(pts[2].X - point.X, 2.0) + Math.Pow(pts[2].Y - point.Y, 2.0));
				double r_base = Math.Sqrt(Math.Pow(pts[2].X - pts[3].X, 2.0) + Math.Pow(pts[2].Y - pts[3].Y, 2.0));
				if ((r_base - HitMargin <= r) && (r <= r_base + HitMargin))
					return true;
			}
			return false;
		}
		/// <summary>
		/// 図形の移動
		/// </summary>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		/// <returns>true:移動可能</returns>
		protected override bool MoveShape(int offsetX, int offsetY, System.Drawing.Size limitSize)
		{
			bool isMove = true;
			if (limitSize.IsEmpty)
				Center = new System.Drawing.Point((int)(Center.X + offsetX), (int)(Center.Y + offsetY));
			else
			{
				if ((Center.X - Radius + offsetX >= 0) && (Center.X + Radius + offsetX < limitSize.Width) &&
					(Center.Y - Radius + offsetY >= 0) && (Center.Y + Radius + offsetY < limitSize.Height))
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
		/// 半径入力のメニュー
		/// </summary>
		private ToolStripTextBox TbRadiusInput = new ToolStripTextBox("TbRadiusInput") { TextBoxTextAlign = HorizontalAlignment.Right };
		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected override void CreateContextMenu()
		{
			base.CreateContextMenu();
			ToolStripMenuItem radius = new ToolStripMenuItem("半径指定", null, MenuRadiusInputEventHandler, "RadiusInput");
			//radius.Click += MenuRadiusInputEventHandler;

			TbRadiusInput.TextChanged += TbRadiusInput_TextChanged;
		
			radius.DropDown.Items.Add(TbRadiusInput);
			
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator1"},
					new ToolStripMenuItem("中心座標指定",null,MenuCenterInputEventHandler,"CenterInput"),
					radius,
					new ToolStripSeparator(){Name = "Separator2"},
					new ToolStripMenuItem("正多角形に変換",null,MenuToPolygonInputEventHandler,"ToPolygon"),
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierInputEventHandler,"ToBezier"),
				});
			
		}
		/// <summary>
		/// メニューの半径値設定中フラグ
		/// </summary>
		private volatile bool isSetting = false;
		/// <summary>
		/// 半径値入力のテキスト変更イベント
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		private void TbRadiusInput_TextChanged(object sender, EventArgs e)
		{
			if (isSetting == false)
			{
				if (float.TryParse(TbRadiusInput.Text, out float r))
				{
					Radius = r;
					OnUpdateShape();
				}
			}
		}
		/// <summary>
		/// コンテキストメニューの有効・無効設定
		/// </summary>
		/// <param name="anchorNo">アンカー番号 -1は図形選択</param>
		/// <param name="maxShapeNo">最大図形番号</param>
		/// <returns>true:メニューあり/false:メニューなし</returns>
		public override bool SetMenuEnable(int anchorNo = -1, int maxShapeNo = -1)
		{
			isSetting = true;
			((ToolStripMenuItem)ContextMenu.Items["RadiusInput"]).DropDown.Items["TbRadiusInput"].Text = string.Format("{0:#,0.#}", Radius);
			isSetting = false;
			return true;
		}
		/// <summary>
		/// メニューから中心座標指定
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuCenterInputEventHandler(object sender, EventArgs e)
		{
			Dialog.AxisForm form = new Dialog.AxisForm("中心座標を指定", Center.X, Center.Y);
			form.Location = MenuLocation;
			if (form.ShowDialog() == DialogResult.OK)
			{
				Center = new System.Drawing.Point(form.IntX, form.IntY);
				OnUpdateShape();
			}
			form.Dispose();
		}
		/// <summary>
		/// メニューから半径
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuRadiusInputEventHandler(object sender, EventArgs e)
		{
		}
		/// <summary>
		/// 正多角形に変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuToPolygonInputEventHandler(object sender, EventArgs e)
		{
		}
		/// <summary>
		/// ベジェ曲線に変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuToBezierInputEventHandler(object sender, EventArgs e)
		{
			Bezier bezier = new Bezier(this);
			OnUpdateShape(bezier);
		}
		/// <summary>
		/// ベジェ曲線の取得
		/// </summary>
		/// <param name="startLine">取得開始線分番号(デフォルト:0)</param>
		/// <param name="numOfLines">取得線分数(デフォルト:-1)</param>
		/// <returns>ベジェ曲線の制御ポイントリスト</returns>
		/// <remarks>
		/// numOfLinesが-1の場合は、startLineから最後の線分まで取得する
		/// </remarks>
		public override List<System.Drawing.PointF> ToBezier(int startLine = 0, int numOfLines = -1)
		{
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();
			// 一本目   0,1,2,2    [0]   (CenterX,CenterY-Radius),(CenterX+Radius,CenterY-Radius),(CenterX+Radius,CenterY),(CenterX+Radius,CenterY)
			//                           ( 0,-r),( r,-r),( r, 0),( r, 0)
			// 二本目	[2],3,4,4  [1]   (CenterX+Radius,CenterY),(CenterX+Radius,CenterY+Radius),(CenterX,CenterY+Radius),(CenterX,CenterY+Radius)
			//                           ( r, 0),( r, r),( 0, r),( 0, r)
			// 三本目	[4],5,6,6  [2]   (CenterX,CenterY+Radius),(CenterX-Radius,CenterY+Radius),(CenterX-Radius,CenterY),(CenterX-Radius,CenterY)
			//                           ( 0, r),(-r, r),(-r, 0),(-r, 0)
			// 四本目	[6],7,0,0  [3]   (CenterX-Radius,CenterY),(CenterX-Radius,CenterY-Radius),(CenterX,CenterY-Radius),(CenterX,CenterY-Radius)
			//                           (-r, 0),(-r,-r),( 0,-r),( 0,-r)
			//
			//   X ...  0: 0, 1, 1   1: 1, 1, 0  2: 0,-1,-1  3:-1,-1, 0
			//   Y ...  0:-1,-1, 0   1: 0, 1, 1  2: 1, 1, 0  3: 0,-1,-1
			int[] SinA = new int[] { 0, 1, 0, -1 };
			int[] ValA = new int[] { 1, 1, -1, -1 };

			if ((numOfLines < 0) || (numOfLines > 4)) numOfLines = 4;
			if ((startLine < 0) || (startLine >= 4)) startLine = 0;
			bool isFirst = true;
			for (int lineNo = startLine; lineNo < numOfLines; lineNo++)
			{
				int x1_index = lineNo;
				int y1_index = (lineNo + 3) % 4;
				if (isFirst)
				{   // 始点
					result.Add(new System.Drawing.PointF(Center.X + SinA[x1_index] * Radius, Center.Y + SinA[y1_index] * Radius));
					isFirst = false;
				}
				// 2番目
				int x2_index = lineNo;
				int y2_index = (lineNo + 3) % 4;
				result.Add(new System.Drawing.PointF(Center.X + ValA[x2_index] * Radius, Center.Y + ValA[y2_index] * Radius));
				// 3,4番目
				int x3_index = (lineNo + 1) % 4;
				int y3_index = lineNo;
				result.Add(new System.Drawing.PointF(Center.X + SinA[x3_index] * Radius, Center.Y + SinA[y3_index] * Radius));
				result.Add(new System.Drawing.PointF(Center.X + SinA[x3_index] * Radius, Center.Y + SinA[y3_index] * Radius));
			}
			return result;
		}
	}
}

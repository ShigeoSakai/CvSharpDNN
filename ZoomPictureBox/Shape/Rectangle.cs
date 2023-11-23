using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Drawing.Shape
{
    /// <summary>
    /// 四角形クラス
    /// </summary>
	[ShapeName("矩形"),DefaultProperty("Location")]
    public class Rectangle : BaseMultiPointShape
    {
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "矩形";

		/// <summary>
		/// 制御点
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override System.Drawing.PointF this[int index]
		{
			get => base[index];
			set
			{
				switch (index)
				{
					case 0:
						BoundingRectangle = new System.Drawing.RectangleF(
							value, new System.Drawing.SizeF(BoundingRectangle.Right - value.X, BoundingRectangle.Bottom - value.Y));
						break;
					case 1:
						BoundingRectangle = new System.Drawing.RectangleF(
							BoundingRectangle.Left,value.Y,
							value.X - BoundingRectangle.Left,
							BoundingRectangle.Bottom - value.Y
							);
						break;
					case 2:
						BoundingRectangle = new System.Drawing.RectangleF(
							BoundingRectangle.Location,
							new System.Drawing.SizeF(value.X - BoundingRectangle.Left, value.Y - BoundingRectangle.Top));
						break;
					case 3:
						BoundingRectangle = new System.Drawing.RectangleF(
							value.X, BoundingRectangle.Top,
							BoundingRectangle.Right - value.X,
							value.Y - BoundingRectangle.Top
							);
						break;
				}
			}
		}
		/// <summary>
		/// 外接矩形 = 矩形
		/// </summary>
		public override System.Drawing.RectangleF BoundingRectangle
		{
			get => base.BoundingRectangle;
			protected set
			{
				base.BoundingRectangle = value;
				// 外接矩形⇒制御点
				SetPoints(base.BoundingRectangle);
			}
		}
		/// <summary>
		/// 外接矩形⇒制御点
		/// </summary>
		/// <param name="rect"></param>
		private void SetPoints(System.Drawing.RectangleF rect)
		{
			if (points_.Count >= 4)
			{
				points_[0] = rect.Location;
				points_[1] = new System.Drawing.PointF(rect.Right, rect.Top);
				points_[2] = new System.Drawing.PointF(rect.Right, rect.Bottom);
				points_[3] = new System.Drawing.PointF(rect.Left, rect.Bottom);
			}
			// 中心座標
			base.Center = new System.Drawing.PointF(rect.X + rect.Width / 2.0F, rect.Y + rect.Height / 2.0F);
		}
		/// <summary>
		/// 中心座標
		/// </summary>
		[Category("図形"), DisplayName("中心"), Description("中心座標")]
		public override System.Drawing.PointF Center
        {
            get { return base.Center; }
            set
            {
                BoundingRectangle = new System.Drawing.RectangleF(
					value.X - BoundingRectangle.Width / 2.0F,
					value.Y - BoundingRectangle.Height / 2.0F,
					BoundingRectangle.Width,BoundingRectangle.Height);
            }
        }
		/// <summary>
		/// 左上座標
		/// </summary>
		[Category("図形"), DisplayName("左上座標"), Description("左上座標")]
		public virtual System.Drawing.PointF Location
		{
			get { return BoundingRectangle.Location; }
			set
			{
				points_[0] = Location;
				// 図形の更新
				updateRectangle();
			}
		}
		/// <summary>
		/// 大きさ
		/// </summary>
		[Category("図形"), DisplayName("大きさ"), Description("大きさ")]
		public virtual System.Drawing.SizeF Size
		{
			get { return BoundingRectangle.Size; }
			set
			{
				points_[1] = new System.Drawing.PointF(points_[0].X + value.Width, points_[0].Y);
				points_[2] = new System.Drawing.PointF(points_[0].X + value.Width, points_[0].Y + value.Height);
				points_[3] = new System.Drawing.PointF(points_[0].X, points_[0].Y + value.Height);
				// 図形の更新
				updateRectangle();
			}
		}
		/// <summary>
		/// 制御点の生成
		/// </summary>
		private void CreatePoints()
		{
			points_.Clear();
			points_.AddRange(new System.Drawing.PointF[]
			{
				new System.Drawing.PointF(),
				new System.Drawing.PointF(),
				new System.Drawing.PointF(),
				new System.Drawing.PointF(),
			});
			// 図形の更新
			updateRectangle();
		}
		/// <summary>
		/// 制御点の生成
		/// </summary>
		private void CreatePoints(System.Drawing.Rectangle rect)
		{
			points_.Clear();
			points_.AddRange(new System.Drawing.PointF[]
			{
				new System.Drawing.PointF(rect.Left,rect.Top),
				new System.Drawing.PointF(rect.Right,rect.Top),
				new System.Drawing.PointF(rect.Right,rect.Bottom),
				new System.Drawing.PointF(rect.Left,rect.Bottom),
			});
			// 図形の更新
			updateRectangle();
		}
		/// <summary>
		/// 制御点の生成
		/// </summary>
		private void CreatePoints(int x, int y, int width, int height)
		{
			points_.Clear();
			points_.AddRange(new System.Drawing.PointF[]
			{
				new System.Drawing.PointF(x, y),
				new System.Drawing.PointF(x + width, y),
				new System.Drawing.PointF(x + width, y + height),
				new System.Drawing.PointF(x, y + height),
			});
			// 図形の更新
			updateRectangle();
		}

		/// <summary>
		/// 基底クラス呼び出し用コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		protected Rectangle(string name) : base(name) { CreatePoints(); }
		protected Rectangle(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle) { CreatePoints(); }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="rectangle">四角形領域</param>
        public Rectangle(string name, System.Drawing.Rectangle rectangle) : base(name)
        {
			// 制御点の生成
			CreatePoints(rectangle);
			// 閉じた図形
			IsClose = true;
        }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">四角形領域</param>
		public Rectangle(string name, System.Drawing.Rectangle rectangle, bool isEditable) : this(name,rectangle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">四角形領域</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Rectangle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color color ,
            float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name,color,lineWidth,lineStyle)
        {
			// 制御点の生成
            CreatePoints(rectangle);
			// 閉じた図形
			IsClose = true;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">四角形領域</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Rectangle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color color, bool isEditable,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, rectangle, color, lineWidth, lineStyle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">幅</param>
		/// <param name="height">高さ</param>
		public Rectangle(string name,int x,int y,int width,int height):base(name)
        {
			// 制御点の生成
			CreatePoints(x, y, width, height);
			// 閉じた図形
			IsClose = true;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">幅</param>
		/// <param name="height">高さ</param>
		public Rectangle(string name, int x, int y, int width, int height, bool isEditable) : this(name,x,y,width,height)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">幅</param>
		/// <param name="height">高さ</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Rectangle(string name, int x, int y, int width, int height,
			System.Drawing.Color color ,float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle)
        {
			// 制御点の生成
			CreatePoints(x, y, width, height);
			// 閉じた図形
			IsClose = true;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="width">幅</param>
		/// <param name="height">高さ</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Rectangle(string name, int x, int y, int width, int height,bool isEditable,
			System.Drawing.Color color, float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name,x,y,width,height, color, lineWidth, lineStyle)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// 外接矩形の更新
		/// </summary>
		protected override void updateRectangle()
		{
			base.BoundingRectangle = new System.Drawing.RectangleF(
				points_[0].X, points_[0].Y, points_[2].X - points_[0].X, points_[2].Y - points_[0].Y);
			// 中心座標
			base.Center = new System.Drawing.PointF((points_[0].X + points_[2].X / 2.0F), (points_[0].Y + points_[2].Y / 2.0F));
		}
		/// <summary>
		/// 座標変換
		/// </summary>
		/// <param name="matrixInv">逆アフィン行列</param>
		/// <param name="size">表示サイズ</param>
		/// <returns>有効な場合は、描画座標(PointF)の配列。無効な場合はnull</returns>
		protected override List<System.Drawing.PointF> CalcDraw(Matrix matrixInv, System.Drawing.Size size)
        {
            if (IsDrawable())
            {
				System.Drawing.PointF[] pts = { new System.Drawing.PointF(BoundingRectangle.X,BoundingRectangle.Y) ,
                            new System.Drawing.PointF(BoundingRectangle.X + BoundingRectangle.Width,BoundingRectangle.Y + BoundingRectangle.Height)};
                matrixInv.TransformPoints(pts);
				//  誤差補正をしているが.... PointF()を使うので不要？
                //pts[1].X += matrixInv.Elements[0];
                //pts[1].Y += matrixInv.Elements[3];
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
            List<System.Drawing.PointF> pts = CalcDraw(matrixInv, size);
            if (pts != null)
            {
				// 描画ペン
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth);
                pen.DashStyle = LineStyle;

                int x = (int)(pts[0].X + 0.5F);
                int y = (int)(pts[0].Y + 0.5F);
                int width = (int)Math.Ceiling(pts[1].X - pts[0].X);
                int height = (int)Math.Ceiling(pts[1].Y - pts[0].Y);
				if (width < 0)
				{
					x = (int)(pts[1].X + 0.5F);
					width = -width;
				}
				if (height < 0)
				{
					y = (int)(pts[1].Y + 0.5F);
					height = -height;
				}
				// 描画領域
				System.Drawing.Rectangle drawRect = new System.Drawing.Rectangle(x,y,width,height);

                if (Fill)
                {	// 塗りつぶし
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
                    graphics.FillRectangle(brush, drawRect);
                }
				// 描画
                graphics.DrawRectangle(pen, drawRect);

				if (MarkerDraw)
				{   // マーカーの描画
					DrawMarker(graphics, Marker, pts[0],
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
					DrawMarker(graphics, Marker, new System.Drawing.PointF(pts[1].X,pts[0].Y),
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
					DrawMarker(graphics, Marker, pts[1],
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
					DrawMarker(graphics, Marker, new System.Drawing.PointF(pts[0].X, pts[1].Y),
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
				}

				if (CenterDraw)
                {
                    // 中心描画
                    List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
                    pts.AddRange(centerPts);
                }

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					graphics.DrawRectangles(anchorPen, new System.Drawing.RectangleF[]
					{
						new System.Drawing.RectangleF(pts[0].X - AnchorRadius,pts[0].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
						new System.Drawing.RectangleF(pts[1].X - AnchorRadius,pts[0].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
						new System.Drawing.RectangleF(pts[1].X - AnchorRadius,pts[1].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
						new System.Drawing.RectangleF(pts[0].X - AnchorRadius,pts[1].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
					});
				}

                return pts;
            }
            return null;
        }
		/// <summary>
		/// テキスト表示位置の算出
		/// </summary>
		/// <param name="textSize">文字列表示サイズ</param>
		/// <param name="size">表示サイズ</param>
		/// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">変換逆行列</param>
		/// <returns>テキスト表示領域</returns>
		/// <remarks>線幅を考慮する</remarks>
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
				if (((topBottom == 1) && (upDown == 0)) ||
					((topBottom == 3) && (upDown == 1)))
					textPoint.X = rectPos[0].X - (LineWidth / 2.0F);
				else
					textPoint.X = rectPos[0].X + (LineWidth / 2.0F);
			}
			else if (leftRight == 2)
				textPoint.X = (rectPos[0].X + rectPos[1].X) / 2.0F + offsetX;
			else if (leftRight == 3)
			{
				if (((topBottom == 1) && (upDown == 0)) ||
					((topBottom == 3) && (upDown == 1)))
					textPoint.X = rectPos[1].X + offsetX + (LineWidth / 2.0F);
				else
					textPoint.X = rectPos[1].X + offsetX - (LineWidth / 2.0F);
			}
			// 縦位置
			if (topBottom == 1)
			{
				if (upDown == 0)
					textPoint.Y = rectPos[0].Y + offsetY - LineWidth/2.0F;
				else
					textPoint.Y = rectPos[0].Y + offsetY + LineWidth / 2.0F;
			}
			else if (topBottom == 2)
				textPoint.Y = (rectPos[0].Y + rectPos[1].Y) / 2.0F + offsetY;
			else if (topBottom == 3)
			{
				if (upDown == 0)
					textPoint.Y = rectPos[1].Y + offsetY - LineWidth/2.0F;
				else
					textPoint.Y = rectPos[1].Y + offsetY + LineWidth/2.0F;
			}
			return new System.Drawing.RectangleF(textPoint, textSize);
		}
		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <param name="pts">計算した領域</param>
		/// <param name="point">指定座標</param>
		/// <returns></returns>
		protected override bool IsContain<PLIST>(PLIST pts, System.Drawing.Point point)
        {
			System.Drawing.RectangleF rect = new System.Drawing.RectangleF(
                pts[0].X - HitMargin,
                pts[0].Y - HitMargin,
                pts[1].X - pts[0].X + HitMargin * 2,
                pts[1].Y - pts[0].Y + HitMargin * 2);
            // 図形に含めれているか
            if (rect.Contains(point))
                return true;

            return false;
        }
		/// <summary>
		/// アンカーの当たり判定
		/// </summary>
		/// <param name="pts"></param>
		/// <param name="point"></param>
		/// <returns></returns>
		protected override bool HitTestAnchor<PLIST>(PLIST pts, System.Drawing.Point point, out int anchorPoint,
			int startIndex = 0, int lastIndex = -1)
		{
			anchorPoint = -1;
			int[] pair = new int[] { 00, 10, 11, 01 };
			for (int index = 0; index < 4; index++)
			{
				int first = pair[index] / 10;
				int second = pair[index] % 10;

				System.Drawing.RectangleF rect = new System.Drawing.RectangleF(pts[first].X - AnchorRadius - HitMargin, pts[second].Y - AnchorRadius - HitMargin,
					(AnchorRadius + HitMargin) * 2, (AnchorRadius + HitMargin) * 2);
				if (rect.Contains((float)point.X, (float)point.Y))
				{
					anchorPoint = index;
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected override void CreateContextMenu()
		{
			base.CreateContextMenu();
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator3"},
					new ToolStripMenuItem("多角形に変換",null,MenuToPolygonEventHandler,"ToPolygon"),
					new ToolStripMenuItem("線群に変換",null,MenuToLineEventHandler,"ToLines"),
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierEventHandler,"ToBezier"),
				});
		}
		/// <summary>
		/// 多角形に変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuToPolygonEventHandler(object sender, EventArgs e)
		{
			Polygon polygon = new Polygon(this);
			// 図形表示更新
			OnUpdateShape(polygon);
		}
		/// <summary>
		/// 線群に変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuToLineEventHandler(object sender, EventArgs e)
		{
			Lines lines = new Lines(this,true,Fill);
			// 図形表示更新
			OnUpdateShape(lines);
		}

	}
}

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
	[ShapeName("楕円"), DefaultProperty("Center")]
	public class Ellipse : BaseMultiPointShape
	{
		/// <summary>
		/// 演算誤差範囲
		/// </summary>
		private const double TOLERANCE = 2.0;

		/// <summary>
		/// 座標値の設定、取得
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>座標値を返す</returns>
		/// <remarks>
		/// [0] ... 外接矩形と楕円接点(上端)<br>
		/// [1] ... 外接矩形 右上<br>
		/// [2] ... 外接矩形と楕円接点(右端)<br>
		/// [3] ... 外接矩形 右下<br>
		/// [4] ... 外接矩形と楕円接点(下端)<br>
		/// [5] ... 外接矩形 左下<br>
		/// [6] ... 外接矩形と楕円接点(左端)<br>
		/// [7] ... 外接矩形 左上<br>
		/// [8] ... 中心座標<br>
		/// [9] ... 円弧開始点<br>
		/// [10] .. 円弧終了点<br>
		/// </remarks>
		public override System.Drawing.PointF this[int index]
		{
			get => base[index];
			set
			{
				if (index == 8)
				{   // 中心点
					ShapeUtil.MovePoints(points_, value.X - points_[8].X, value.Y - points_[8].Y);
					base.Center = points_[8];
				}
				else if ((index == 9) || (index == 10))
				{   // 角度アンカーの移動
					MoveAngleAnchor(value, index);
				}
				else if (index % 2 == 1)
					CalcPointMove(value, index);    // 外接矩形アンカー
				else if (index < 8)
					CalcAnhorMove(value, index);    // 図形アンカー
				else
				{
					base[index] = value;
					// 楕円の計算
					CalcEllipse(points_, 0.0F);
				}
			}
		}
		/// <summary>
		/// 中心座標
		/// </summary>
		[Category("図形"), DisplayName("中心座標"), Description("中心座標")]
		public override System.Drawing.PointF Center
		{
			get => base.Center;
			set
			{
				base.Center = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// 回転角度(度)
		/// </summary>
		private float rotateAngle_ = 0.0F;
		[Category("図形"), DisplayName("回転角度"), Description("回転角度(度)")]
		public virtual float RotateAngle
		{
			get => rotateAngle_;
			set
			{
				rotateAngle_ = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}

		/// <summary>
		/// 回転していない左上座標
		/// </summary>
		protected virtual System.Drawing.PointF LeftTop { get; set; } = new System.Drawing.PointF();
		/// <summary>
		/// 横幅（ローカル）
		/// </summary>
		private float width_;
		/// <summary>
		/// 横幅
		/// </summary>
		[Category("図形"), DisplayName("幅"), Description("楕円の幅")]
		public virtual float Width
		{
			get => width_;
			set
			{
				width_ = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// 縦幅（ローカル）
		/// </summary>
		private float height_;
		/// <summary>
		/// 縦幅
		/// </summary>
		[Category("図形"), DisplayName("高さ"), Description("楕円の高さ")]
		public virtual float Height
		{
			get => height_;
			set
			{
				height_ = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// 描画開始角度(度)
		/// </summary>
		private float startAngle_ = 0.0F;
		[Category("図形"), DisplayName("円弧開始角度"), Description("円弧開始角度(度)"), DefaultValue(0.0F)]
		public virtual float StartAngle
		{
			get => startAngle_;
			set
			{   // -180～+180の範囲に収める
				if (value < -180.0F)
					startAngle_ = 360.0F + value;
				else if (value > 180.0F)
					startAngle_ = value - 360.0F;
				else
					startAngle_ = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// 描画角度(度)
		/// </summary>
		private float sweepAngle_ = 360.0F;
		[Category("図形"), DisplayName("円弧描画角度"), Description("円弧描画角度(度)"), DefaultValue(360.0F)]
		public virtual float SweepAngle
		{
			get => sweepAngle_;
			set
			{   // 0～360の範囲に収める
				if (value < 0)
					sweepAngle_ = 360.0F + value;
				else if (value > 360.0F)
					sweepAngle_ = value - 360.0F;
				else
					sweepAngle_ = value;
				// 外接矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// 円弧かどうか
		/// </summary>
		public bool IsArc { get; protected set; } = false;
		/// <summary>
		/// 扇形を描画するか
		/// </summary>
		[Category("図形"), DisplayName("扇形"), Description("true:扇形/false:円弧"), DefaultValue(false)]
		public virtual bool IsPie { get; set; } = false;
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">回転矩形の座標値</param>
		/// <param name="angle">回転角度</param>
		public Ellipse(string name, System.Drawing.Point[] points, float angle = 0.0F) : base(name)
		{
			if (CheckPoints(points, angle) == false)
				throw new ArgumentException("楕円の座標が算出できません");
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="rectangle">外接矩形</param>
		/// <param name="angle">回転角度</param>
		public Ellipse(string name, System.Drawing.Rectangle rectangle, float angle = 0.0F) : base(name)
		{
			// 中心座標
			base.Center = new System.Drawing.PointF((rectangle.Left + rectangle.Right) / 2.0F,
				(rectangle.Top + rectangle.Bottom) / 2.0F);
			// 回転角度
			rotateAngle_ = angle;
			// 縦横を保存
			width_ = rectangle.Width;
			height_ = rectangle.Height;
			// 外接矩形の算出
			CalcPoints();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="center">中心座標</param>
		/// <param name="width">X方向の幅</param>
		/// <param name="height">Y方向の高さ</param>
		/// <param name="angle">回転角度</param>
		public Ellipse(string name, System.Drawing.Point center, int width, int height, float angle = 0.0F) : base(name)
		{
			// 中心座標
			base.Center = new System.Drawing.PointF(center.X, center.Y);
			// 回転角度
			rotateAngle_ = angle;
			// 縦横を保存
			width_ = width;
			height_ = height;
			// 外接矩形の算出
			CalcPoints();
		}

		/// <summary>
		/// 入力点のチェック
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		private bool CheckPoints(System.Drawing.Point[] points, float angle)
		{
			// 楕円の計算
			return CalcEllipse(ShapeUtil.ToPointFArray(points), angle);
		}
		/// <summary>
		/// 楕円の算出
		/// </summary>
		/// <param name="points"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		private bool CalcEllipse<PLIST>(PLIST points, float angle) where PLIST : IList<System.Drawing.PointF>
		{
			// 4点での取得インデックス
			int topLeft = 0;
			int topRight = 1;
			int bottomRight = 2;
			int bottomLeft = 3;

			if ((points.Count != 4) && (points.Count != 8 + 1) && (points.Count != 8 + 3))
				return false;
			else if ((points.Count == 8 + 1) || (points.Count == 8 + 3))
			{   // 9点(8点+中心座標)での取得インデックス
				topLeft = 7;
				topRight = 1;
				bottomRight = 3;
				bottomLeft = 5;
			}

			double topLine = Math.Pow(points[topRight].X - points[topLeft].X, 2.0) + Math.Pow(points[topRight].Y - points[topLeft].Y, 2.0);
			double bottomLine = Math.Pow(points[bottomLeft].X - points[bottomRight].X, 2.0) + Math.Pow(points[bottomLeft].Y - points[bottomRight].Y, 2.0);
			double leftLine = Math.Pow(points[bottomLeft].X - points[topLeft].X, 2.0) + Math.Pow(points[bottomLeft].Y - points[topLeft].Y, 2.0);
			double rightLine = Math.Pow(points[bottomRight].X - points[topRight].X, 2.0) + Math.Pow(points[bottomRight].Y - points[topRight].Y, 2.0);
			// 誤差範囲か？
			if ((Math.Abs(topLine - bottomLine) < TOLERANCE) && (Math.Abs(leftLine - rightLine) < TOLERANCE))
			{
				// 傾き(上の線から)
				double r_angle = Math.Atan2(points[topRight].Y - points[topLeft].Y, points[topRight].X - points[topLeft].X);
				double x_clen = (Math.Sqrt(topLine) + Math.Sqrt(bottomLine)) / 4.0;
				double y_clen = (Math.Sqrt(leftLine) + Math.Sqrt(rightLine)) / 4.0;
				// 中心座標
				base.Center = ShapeUtil.CalcRotatePointR(x_clen,y_clen,r_angle, points[topLeft]);
				// 傾き
				rotateAngle_ = ShapeUtil.ToDegreeF(r_angle) + angle;
				// 縦横を保存
				width_ = (float)(x_clen * 2.0);
				height_ = (float)(y_clen * 2.0);
				// 外接矩形を求める	
				CalcPoints();
				return true;
			}
			return false;
		}
		/// <summary>
		/// 外接矩形を求める
		/// </summary>
		/// <param name="x_with"></param>
		/// <param name="y_width"></param>
		private void CalcPoints()
		{
			points_.Clear();
			double r_angle = ShapeUtil.ToRadian(RotateAngle);
			// 上部接点
			points_.Add(
				ShapeUtil.CalcRotatePointD(0.0, (-height_ / 2.0), RotateAngle, Center));
			// 右上点
			points_.Add(
				ShapeUtil.CalcRotatePointD((width_ / 2.0), (-height_ / 2.0), RotateAngle, Center));
			// 右部接点
			points_.Add(
				ShapeUtil.CalcRotatePointD((width_ / 2.0), 0.0, RotateAngle, Center));
			// 右下点
			points_.Add(
				ShapeUtil.CalcRotatePointD((width_ / 2.0), (height_ / 2.0), RotateAngle, Center));
			// 下部接点
			points_.Add(
				ShapeUtil.CalcRotatePointD(0.0, (height_ / 2.0), RotateAngle, Center));
			// 左下点
			points_.Add(
				ShapeUtil.CalcRotatePointD((-width_ / 2.0), (height_ / 2.0), RotateAngle, Center));
			// 左部接点
			points_.Add(
				ShapeUtil.CalcRotatePointD((-width_ / 2.0), 0, RotateAngle, Center));
			// 左上点
			points_.Add(
				ShapeUtil.CalcRotatePointD((-width_ / 2.0), (-height_ / 2.0), RotateAngle, Center));
			// 中心座標
			points_.Add(Center);
			// 開始角度座標
			points_.Add(CalcAnglePoint(StartAngle));
			// 終了角度座標
			points_.Add(CalcAnglePoint(StartAngle + SweepAngle));

			// 回転していない左上座標
			LeftTop = new System.Drawing.PointF(
				Center.X - width_ / 2.0F, Center.Y - height_ / 2.0F);
			// 円弧かどうか
			IsArc = (Math.Round(SweepAngle) % 360 != 0);

		}
		/// <summary>
		/// 入力点のチェック
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		private bool CheckPoints(System.Drawing.PointF[] points, float angle)
		{
			// 座標計算
			return CalcEllipse(points, angle);
		}

		/// <summary>
		/// 図形の拡大縮小
		/// </summary>
		/// <param name="newValue"></param>
		/// <param name="anchorNo"></param>
		private void CalcPointMove(System.Drawing.PointF newValue, int anchorNo)
		{
			// 次と前のアンカー位置を求める
			// anchorNo   next  before
			//    1(右上)　7      3    next = anchorNo - 2  before = anchorNo + 2    -1, 3    1
			//    3(右下)  5      1    next = anchorNo + 2  before = anchorNo - 2     5, 1    3
			//    5(左下)  3      7    next = anchorNo - 2  before = anchorNo + 2     3, 7    1
			//    7(左上)  1      5    next = anchorNo + 2  before = anchorNo - 2     9, 5    3
			int sign = (anchorNo % 4 == 1) ? 1 : -1;
			int nextAnchor = (anchorNo - 2 * sign) % 8;
			int beforeAnchor = (anchorNo + 2 * sign) % 8;
			if (nextAnchor < 0) nextAnchor += 8;
			if (beforeAnchor < 0) beforeAnchor += 8;

			// 移動角度
			double theater = Math.Atan2(newValue.Y - points_[anchorNo].Y, newValue.X - points_[anchorNo].X);
			// 移動距離
			double length = Math.Sqrt(Math.Pow(newValue.Y - points_[anchorNo].Y, 2.0) + Math.Pow(newValue.X - points_[anchorNo].X, 2.0));
			// 計算角度
			double rotate_angle = ShapeUtil.ToRadian(RotateAngle);
			double calc_angle = theater - rotate_angle;
			// 増分
			double diffWidth = length * Math.Cos(calc_angle);
			double diffHeight = length * Math.Sin(calc_angle);

			// Point1移動
			points_[nextAnchor] = new System.Drawing.PointF(
				(float)(points_[nextAnchor].X - diffHeight * Math.Sin(rotate_angle)),
				(float)(points_[nextAnchor].Y + diffHeight * Math.Cos(rotate_angle)));
			// Point3移動
			points_[beforeAnchor] = new System.Drawing.PointF(
				(float)(points_[beforeAnchor].X + diffWidth * Math.Cos(rotate_angle)),
				(float)(points_[beforeAnchor].Y + diffWidth * Math.Sin(rotate_angle)));
			// Point0設定
			points_[anchorNo] = new System.Drawing.PointF(newValue.X, newValue.Y);
			// 座標計算
			CalcEllipse(points_.ToArray(), 0.0F);
		}
		/// <summary>
		/// 図形アンカーの移動
		/// </summary>
		/// <param name="newValue"></param>
		/// <param name="anchorNo"></param>
		public void CalcAnhorMove(System.Drawing.PointF newValue, int anchorNo)
		{
			// 中心から現在のアンカー位置の角度と距離
			double angle = Math.Atan2(points_[anchorNo].Y - Center.Y, points_[anchorNo].X - Center.X);
			double length = Math.Sqrt(Math.Pow(points_[anchorNo].Y - Center.Y, 2.0) + Math.Pow(points_[anchorNo].X - Center.X, 2.0));
			// 中心から移動点の角度と距離
			double angle_new = Math.Atan2(newValue.Y - Center.Y, newValue.X - Center.X);
			double length_new = Math.Sqrt(Math.Pow(newValue.Y - Center.Y, 2.0) + Math.Pow(newValue.X - Center.X, 2.0));
			// 差
			double diff_angle = angle_new - angle;
			double diff_length = length_new - length;

			if ((anchorNo == 0) || (anchorNo == 4))
			{   // 高さ変更
				rotateAngle_ += ShapeUtil.ToDegreeF(diff_angle);
				// 縦横を保存
				height_ += (float)diff_length;
				// 外接矩形を求める	
				CalcPoints();
			}
			else if ((anchorNo == 2) || (anchorNo == 6))
			{   // 幅変更
				rotateAngle_ += ShapeUtil.ToDegreeF(diff_angle);
				// 縦横を保存
				width_ += (float)diff_length;
				// 外接矩形を求める	
				CalcPoints();
			}
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
				// 外接矩形の変換
				List<System.Drawing.PointF> pts = base.CalcDraw(matrixInv, size);
				return pts;
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

				// 近似直線
				if (DebugMode.HasFlag(DEBUG_MODE.NEAR_LINE))
				{
					if (IsArc)
					{   // 円弧の場合
						System.Drawing.PointF[] ptList = CalcArcPoint().ToArray();
						// 座標変換
						matrixInv.TransformPoints(ptList);
						graphics.DrawLines(new System.Drawing.Pen(AnchorColor, MarkerLineWidth), ptList);
					}
					else
					{	// 楕円の場合
						List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(ToBezier(pts));
						if (lines != null)
						{
							foreach (System.Drawing.PointF[] l_pts in lines)
								graphics.DrawLines(new System.Drawing.Pen(AnchorColor, MarkerLineWidth), l_pts);
						}
					}
				}

				// 描画を保存
				GraphicsState state = graphics.Save();
				// 回転行列を設定
				Matrix mat = matrixInv.Clone();
				// 回転角度設定
				mat.RotateAt(RotateAngle, Center, MatrixOrder.Prepend);
				// 一時的にアフィン行列を設定
				graphics.Transform = mat;
				// 描画開始位置(回転していない左上座標)
				float drawX = Center.X - width_ / 2.0F;
				float drawY = Center.Y - height_ / 2.0F;

				if (Fill)
				{   // 塗りつぶし
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
					if (IsArc)
						graphics.FillPie(brush, drawX, drawY, width_, height_, StartAngle, SweepAngle);
					else
						graphics.FillEllipse(brush, drawX, drawY, width_, height_);
				}
				// 描画
				if ((IsArc) && (IsPie))
					graphics.DrawPie(pen, drawX, drawY, width_, height_, StartAngle, SweepAngle);
				else if (IsArc)
					graphics.DrawArc(pen, drawX, drawY, width_, height_, StartAngle, SweepAngle);
				else
					graphics.DrawEllipse(pen, drawX, drawY, width_, height_);

				// 元に戻す
				graphics.Restore(state);

				if (CenterDraw)
				{   // 中心描画
					List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
					pts.AddRange(centerPts);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					int last_index = 8;
					if (IsArc)
						last_index = 10;
					for (int index = 0; index <= last_index; index++)
					{
						graphics.DrawRectangle(anchorPen,
							pts[index].X - AnchorRadius, pts[index].Y - AnchorRadius, AnchorRadius * 2, AnchorRadius * 2);
					}
				}
				if (DebugMode.HasFlag(DEBUG_MODE.OUTER_LINE))
				{   // デバッグ用枠表示
					graphics.DrawPolygon(new System.Drawing.Pen(AnchorColor, MarkerLineWidth),
						new System.Drawing.PointF[] {
						pts[7],pts[1],pts[3],pts[5]
						});
					System.Drawing.PointF[] b_rect = { BoundingRectangle.Location,
						new System.Drawing.PointF(BoundingRectangle.Right,BoundingRectangle.Bottom)};
					matrixInv.TransformPoints(b_rect);
					graphics.DrawRectangle(new System.Drawing.Pen(AnchorColor, MarkerLineWidth),
						b_rect[0].X, b_rect[0].Y, b_rect[1].X - b_rect[0].X, b_rect[1].Y - b_rect[0].Y);
				}
			}
			return pts;
		}
		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <param name="pts">計算した領域</param>
		/// <param name="point">指定座標</param>
		/// <returns></returns>
		protected override bool IsContain<PLIST>(PLIST pts, System.Drawing.Point point)
		{
			if (pts != null)
			{
				if (IsArc)
				{   // 円弧
					System.Drawing.RectangleF rect = ShapeUtil.getBoundingRectangleF(pts, 0, 7);
					if (rect.Contains(point))
					{
						if (IsPie)
						{   // 中心と開始点、終了点を結ぶ線分
							if ((ShapeUtil.IsContain(pts[8], pts[9], point, HitMargin)) ||
								(ShapeUtil.IsContain(pts[8], pts[10], point, HitMargin)))
								return true;
						}
						//  幅...[2]と[6]の距離
						double width = Math.Sqrt(Math.Pow(pts[6].X - pts[2].X, 2.0) + Math.Pow(pts[6].Y - pts[2].Y, 2.0));
						//  高さ ... [0]と[4]の距離
						double height = Math.Sqrt(Math.Pow(pts[4].X - pts[0].X, 2.0) + Math.Pow(pts[4].Y - pts[0].Y, 2.0));
						// 分割点の取得
						List<System.Drawing.PointF> divPoints = CalcArcPoint(pts[8], (float)width, (float)height, RotateAngle, StartAngle, SweepAngle);
						for (int index = 0; index < divPoints.Count - 1; index++)
							if (ShapeUtil.IsContain(divPoints[index], divPoints[index + 1], point, HitMargin))
								return true;
					}
				}
				else
				{	// 楕円
					//  4分割して判定する
					// 先頭は0, 2, 4, 6
					for (int st = 0; st <= 7; st += 2)
					{
						int second = (st + 1) % 8;
						int third = (st + 2) % 8;
						System.Drawing.RectangleF rect = ShapeUtil.getBoundingRectangle(pts[st], pts[second], pts[third]);
						if (rect.Contains(point))
						{
							List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(ToBezier(pts[st], pts[second], pts[third]));
							foreach (System.Drawing.PointF[] line in lines)
							{
								for (int index = 0; index < line.Length - 1; index++)
									if (ShapeUtil.IsContain(line[index], line[index + 1], point, HitMargin))
										return true;
							}
						}
					}
				}
			}
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
			if (pts != null)
			{
				int end_index = 8;
				if (IsArc)
					end_index = 10;

				if (Selected)
				{   // 選択中
					if ((lastIndex < 0) || (lastIndex > end_index)) lastIndex = end_index;
					return base.HitTestAnchor(pts, point, out anchorPoint, startIndex, lastIndex);
				}
				else
				{
					if (IsArc)
					{	// 円弧  ... 開始点と終了点（Pieの場合中心も）を対象とする
						if ((HitTestAnchor(pts[9], point)) || HitTestAnchor(pts[10], point))
							return true;
						if ((IsPie) && (HitTestAnchor(pts[8], point)))
							return true;
					}
					else
					{	// 楕円 ... 外接矩形の接点は対象とする
						for (int index = 0; index <= end_index; index += 2)
						{   // 中心座標は除く
							if (index == 8) continue;
							if (HitTestAnchor(pts[index], point))
							{
								anchorPoint = index;
								return true;
							}
						}
					}
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
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierEventHandler,"ToBezier"),
				});
		}
		/// <summary>
		/// ベジェ曲線に変換
		/// </summary>
		/// <returns></returns>
		public override List<System.Drawing.PointF> ToBezier(int startLine = 0, int numOfLines = -1)
		{
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();
			if (IsArc)
			{   // 円弧の場合
				// 開始角度
				result.Add(new System.Drawing.PointF(points_[9].X, points_[9].Y));
				// 制御点
				result.Add(new System.Drawing.PointF(points_[9].X, points_[9].Y));

				// 計算角度
				float calc_angle = StartAngle + SweepAngle;
				int calc_index = 4;
				while (calc_angle > 90.0F)
				{
					calc_angle -= 90.0F;
					// 制御点
					result.Add(new System.Drawing.PointF(points_[calc_index].X, points_[calc_index].Y));
					// ポイント
					result.Add(new System.Drawing.PointF(points_[calc_index].X, points_[calc_index].Y));
					if (calc_angle > 90.0F)
					{
						int next_index = (calc_index + 1) % 8;
						// 制御点
						result.Add(new System.Drawing.PointF(points_[next_index].X, points_[next_index].Y));
					}
					else
					{
						// 終了点の制御点
						result.Add(new System.Drawing.PointF(points_[10].X, points_[10].Y));
					}
					calc_index = (calc_index + 2) % 8;
				}
				// 終了点の制御点
				result.Add(new System.Drawing.PointF(points_[10].X, points_[10].Y));
				// 終了点
				result.Add(new System.Drawing.PointF(points_[10].X, points_[10].Y));
				if (IsPie)
				{   // 扇型の場合
					// 終了点の制御点
					result.Add(new System.Drawing.PointF(points_[10].X, points_[10].Y));
					// 中心の制御点
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 中心
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 中心の制御点
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 制御点
					result.Add(new System.Drawing.PointF(points_[9].X, points_[9].Y));
					// 開始角度
					result.Add(new System.Drawing.PointF(points_[9].X, points_[9].Y));
				}
			}
			else
			{	// 楕円の場合
				// 一本目   0,1,2,2    [0]
				// 二本目	[2],3,4,4  [1]
				// 三本目	[4],5,6,6  [2]
				// 四本目	[6],7,0,0  [3]
				if ((numOfLines < 0) || (numOfLines > 4)) numOfLines = 4;
				if ((startLine < 0) || (startLine >= 4)) startLine = 0;
				bool isFirst = true;
				for (int lineNo = startLine; lineNo < numOfLines; lineNo++)
				{
					int lineIndex = lineNo * 2;
					if (isFirst)
					{   // 始点
						result.Add(new System.Drawing.PointF(points_[lineIndex].X, points_[lineIndex].Y));
						isFirst = false;
					}
					// 2番目
					int second_point = (lineIndex + 1) % 8;
					result.Add(new System.Drawing.PointF(points_[second_point].X, points_[second_point].Y));
					// 3,4番目
					int third_point = (lineIndex + 2) % 8;
					result.Add(new System.Drawing.PointF(points_[third_point].X, points_[third_point].Y));
					result.Add(new System.Drawing.PointF(points_[third_point].X, points_[third_point].Y));
				}
			}
			return result;
		}
		/// <summary>
		/// ベジェ曲線に変換
		/// </summary>
		/// <typeparam name="PLIST"></typeparam>
		/// <param name="pts"></param>
		/// <param name="startLine"></param>
		/// <param name="numOfLines"></param>
		/// <returns></returns>
		protected virtual List<System.Drawing.PointF> ToBezier<PLIST>(PLIST pts, int startLine = 0, int numOfLines = -1)
			where PLIST : IList<System.Drawing.PointF>
		{
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();

			if (IsArc)
			{	// 円弧の場合
				// 開始角度
				result.Add(new System.Drawing.PointF(pts[9].X, pts[9].Y));
				// 制御点
				result.Add(new System.Drawing.PointF(pts[9].X, pts[9].Y));

				// 計算角度
				float calc_angle = StartAngle + SweepAngle;
				int calc_index = 4;
				while(calc_angle > 90.0F)
				{
					calc_angle -= 90.0F;
					// 制御点
					result.Add(new System.Drawing.PointF(pts[calc_index].X, pts[calc_index].Y));
					// ポイント
					result.Add(new System.Drawing.PointF(pts[calc_index].X, pts[calc_index].Y));
					if (calc_angle > 90.0F)
					{
						int next_index = (calc_index + 1) % 8;
						// 制御点
						result.Add(new System.Drawing.PointF(pts[next_index].X, pts[next_index].Y));
					}
					calc_index = (calc_index + 2) % 8;
				}
				// 終了点の制御点
				result.Add(new System.Drawing.PointF(pts[10].X, pts[10].Y));
				// 終了点
				result.Add(new System.Drawing.PointF(pts[10].X, pts[10].Y));
				if (IsPie)
				{   // 扇型の場合
					// 終了点の制御点
					result.Add(new System.Drawing.PointF(pts[10].X, pts[10].Y));
					// 中心の制御点
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 中心
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 中心の制御点
					result.Add(new System.Drawing.PointF(Center.X, Center.Y));
					// 制御点
					result.Add(new System.Drawing.PointF(pts[9].X, pts[9].Y));
					// 開始角度
					result.Add(new System.Drawing.PointF(pts[9].X, pts[9].Y));
				}
			}
			else
			{   // 楕円の曲線取得
				// 一本目   0,1,2,2    [0]
				// 二本目	[2],3,4,4  [1]
				// 三本目	[4],5,6,6  [2]
				// 四本目	[6],7,0,0  [3]
				if ((numOfLines < 0) || (numOfLines > 4)) numOfLines = 4;
				if ((startLine < 0) || (startLine >= 4)) startLine = 0;
				bool isFirst = true;

				for (int lineNo = startLine; lineNo < numOfLines; lineNo++)
				{
					int lineIndex = lineNo * 2;
					if (isFirst)
					{   // 始点
						result.Add(new System.Drawing.PointF(pts[lineIndex].X, pts[lineIndex].Y));
						isFirst = false;
					}
					// 2番目
					int second_point = (lineIndex + 1) % 8;
					result.Add(new System.Drawing.PointF(pts[second_point].X, pts[second_point].Y));
					// 3,4番目
					int third_point = (lineIndex + 2) % 8;
					result.Add(new System.Drawing.PointF(pts[third_point].X, pts[third_point].Y));
					result.Add(new System.Drawing.PointF(pts[third_point].X, pts[third_point].Y));
				}
			}
			return result;
		}
		/// <summary>
		/// 指定された3点で構成するベジェ曲線
		/// </summary>
		/// <param name="pt1">1点目</param>
		/// <param name="pt2">2点目</param>
		/// <param name="pt3">3点目</param>
		/// <returns></returns>
		protected virtual List<System.Drawing.PointF> ToBezier(
			System.Drawing.PointF pt1, System.Drawing.PointF pt2, System.Drawing.PointF pt3)
		{
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();
			result.Add(pt1);
			result.Add(pt2);
			result.Add(pt3);
			result.Add(pt3);
			return result;
		}
		/// <summary>
		/// 回転座標の計算
		/// </summary>
		/// <param name="degree">回転角度</param>
		/// <returns>座標値</returns>
		protected virtual System.Drawing.PointF CalcAnglePoint(float degree)
		{
			return CalcAnglePoint(degree, Center, Width, Height, RotateAngle);
		}
		/// <summary>
		/// 回転座標の計算
		/// </summary>
		/// <param name="degree">回転角度</param>
		/// <param name="center">中心座標</param>
		/// <param name="width">楕円幅</param>
		/// <param name="height">楕円高さ</param>
		/// <param name="rotateAngle">楕円回転角度</param>
		/// <returns>座標値</returns>
		protected virtual System.Drawing.PointF CalcAnglePoint(float degree,
			System.Drawing.PointF center,float width,float height,float rotateAngle)
		{
			// 開始角度
			double angle = ShapeUtil.ToRadian(degree);
			double r_1 = Math.Pow(Math.Cos(angle) / (width / 2.0), 2.0) + Math.Pow(Math.Sin(angle) / (height / 2.0), 2.0);
			if (r_1 != 0.0)
			{
				double r = Math.Sqrt(1.0 / r_1);
				// 求まった座標(原点ベース)
				System.Drawing.PointF calcPoint = new System.Drawing.PointF(
					(float)(r * Math.Cos(angle)), (float)(r * Math.Sin(angle)));
				// 回転
				double rotate = ShapeUtil.ToRadian(rotateAngle);
				System.Drawing.PointF rotatePoint = new System.Drawing.PointF(
					(float)(calcPoint.X * Math.Cos(rotate) - calcPoint.Y * Math.Sin(rotate)),
					(float)(calcPoint.X * Math.Sin(rotate) + calcPoint.Y * Math.Cos(rotate)));
				// 平行移動
				return new System.Drawing.PointF(
					rotatePoint.X + center.X, rotatePoint.Y + center.Y);
			}
			return new System.Drawing.PointF();
		}

		/// <summary>
		/// 角度アンカーの移動
		/// </summary>
		/// <param name="value"></param>
		/// <param name="anchorNo"></param>
		protected virtual void MoveAngleAnchor(System.Drawing.PointF value, int anchorNo)
		{
			if ((anchorNo == 9) || (anchorNo == 10))
			{
				// 元々の角度
				double angle_orig = Math.Atan2(points_[anchorNo].Y - Center.Y, points_[anchorNo].X - Center.X);
				if (angle_orig < 0) angle_orig = 2 * Math.PI + angle_orig;  // 0 <= θ < 2π の範囲に変更する
																			// 新しい値
				double angle_new = Math.Atan2(value.Y - Center.Y, value.X - Center.X);
				if (angle_new < 0) angle_new = 2 * Math.PI + angle_new;     // 0 <= θ < 2π の範囲に変更する
																			// 差
				double diff_angle = ShapeUtil.ToDegree(angle_new - angle_orig);

				if (anchorNo == 9)
					StartAngle += (float)diff_angle;
				else
					SweepAngle += (float)diff_angle;
			}
		}
		/// <summary>
		/// テキスト表示位置の算出
		/// </summary>
		/// <param name="textSize">文字列表示サイズ</param>
		/// <param name="size">表示サイズ</param>
		/// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">変換逆行列</param>
		/// <returns>テキスト表示領域</returns>
		protected override System.Drawing.RectangleF CalcTextPosition<PLIST>(System.Drawing.SizeF textSize, System.Drawing.Size size,
			PLIST pts, Matrix matrixInv)
		{
			// 表示位置とオフセットを計算
			CalcTextOffset(textSize, out int leftRight, out int topBottom, out int upDown, out float offsetX, out float offsetY);
			///////////////////////////
			//// 回転なしの表示座標を算出
			///////////////////////////
			System.Drawing.PointF textPoint = new System.Drawing.PointF();
			switch(LabelPosition)
			{
				case LABEL_POSITION.TOP_LEFT:
				case LABEL_POSITION.TOP_LEFT_INNER:
					textPoint.X = pts[7].X + offsetX;
					textPoint.Y = pts[7].Y + offsetY;
					break;
				case LABEL_POSITION.TOP_CENTER:
				case LABEL_POSITION.TOP_CENTER_INNER:
					textPoint.X = pts[0].X + offsetX;
					textPoint.Y = pts[0].Y + offsetY;
					break;
				case LABEL_POSITION.TOP_RIGHT:
				case LABEL_POSITION.TOP_RIGHT_INNER:
					textPoint.X = pts[1].X + offsetX;
					textPoint.Y = pts[1].Y + offsetY;
					break;
				case LABEL_POSITION.CENTER:
					textPoint.X = pts[8].X + offsetX;
					textPoint.Y = pts[8].Y + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_LEFT:
				case LABEL_POSITION.BOTTOM_LEFT_INNER:
					textPoint.X = pts[5].X + offsetX;
					textPoint.Y = pts[5].Y + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_CENTER:
				case LABEL_POSITION.BOTTOM_CENTER_INNER:
					textPoint.X = pts[4].X + offsetX;
					textPoint.Y = pts[4].Y + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_RIGHT:
				case LABEL_POSITION.BOTTOM_RIGHT_INNER:
					textPoint.X = pts[3].X + offsetX;
					textPoint.Y = pts[3].Y + offsetY;
					break;
			}
			return new System.Drawing.RectangleF(textPoint, textSize);
		}
		/// <summary>
		/// 文字表示用アフィン行列を取得
		/// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
		/// <param name="pts">点群</param>
		/// <returns>文字表示用アフィン行列</returns>
		private Matrix CalcTextMatrix<PLIST>(PLIST pts) where PLIST:IList<System.Drawing.PointF>
		{
			Matrix mat = new Matrix();
			switch (LabelPosition)
			{
				case LABEL_POSITION.TOP_LEFT:
				case LABEL_POSITION.TOP_LEFT_INNER:
					mat.RotateAt(RotateAngle, pts[7]);
					break;
				case LABEL_POSITION.TOP_CENTER:
				case LABEL_POSITION.TOP_CENTER_INNER:
					mat.RotateAt(RotateAngle, pts[0]);
					break;
				case LABEL_POSITION.TOP_RIGHT:
				case LABEL_POSITION.TOP_RIGHT_INNER:
					mat.RotateAt(RotateAngle, pts[1]);
					break;
				case LABEL_POSITION.CENTER:
					mat.RotateAt(RotateAngle, pts[8]);
					break;
				case LABEL_POSITION.BOTTOM_LEFT:
				case LABEL_POSITION.BOTTOM_LEFT_INNER:
					mat.RotateAt(RotateAngle, pts[5]);
					break;
				case LABEL_POSITION.BOTTOM_CENTER:
				case LABEL_POSITION.BOTTOM_CENTER_INNER:
					mat.RotateAt(RotateAngle, pts[4]);
					break;
				case LABEL_POSITION.BOTTOM_RIGHT:
				case LABEL_POSITION.BOTTOM_RIGHT_INNER:
					mat.RotateAt(RotateAngle, pts[3]);
					break;
			}
			return mat;
		}

		/// <summary>
		/// 文字列の描画
		/// </summary>
		/// <param name="graphics">グラフィック</param>
		/// <param name="size">表示サイズ</param>
		/// <param name="pts">頂点座標リスト</param>
		/// <param name="matrixInv">変換逆行列</param>
		protected override void DrawText<PLIST>(System.Drawing.Graphics graphics, System.Drawing.Size size, PLIST pts, Matrix matrixInv)
		{
			if ((ShowLable) && (Text != null))
			{
				// 描画を保存
				GraphicsState state = graphics.Save();

				// 文字列の描画サイズ
				System.Drawing.SizeF textSize = TextRenderer.MeasureText(graphics, Text, LabelFont);
				// 描画サイズ
				System.Drawing.RectangleF textRect = CalcTextPosition(textSize, size, pts, matrixInv);
				// 一時的にアフィン行列を設定
				graphics.Transform = CalcTextMatrix(pts);
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

				// 描画を元に戻す
				graphics.Restore(state);
			}
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
		protected override bool HitTestText<PLIST>(System.Drawing.Graphics graphics, PLIST pts,
			System.Drawing.Size size, System.Drawing.Point point, Matrix matrixInv,
			string text, System.Drawing.Font font)
		{
			if ((ShowLable) && (Text != null))
			{
				// 文字列の描画サイズ
				System.Drawing.SizeF textSize = TextRenderer.MeasureText(graphics, Text, LabelFont);
				// 描画サイズ
				System.Drawing.RectangleF textRect = CalcTextPosition(textSize, size, pts, matrixInv);
				// アフィン行列を算出
				Matrix mat = CalcTextMatrix(pts);
				// 逆行列にする
				mat.Invert();
				// 検証座標
				System.Drawing.PointF[] axis = { new System.Drawing.PointF(point.X, point.Y) };
				// 座標を変換
				mat.TransformPoints(axis);
				// 範囲チェック
				if ((axis[0].X >= textRect.X - HitMargin) && (axis[0].X <= textRect.Right + HitMargin) &&
					(axis[0].Y >= textRect.Y - HitMargin) && (axis[0].Y <= textRect.Bottom + HitMargin))
					return true;
			}
			return false;
		}
		/// <summary>
		/// 円弧の分割点を取得する
		/// </summary>
		/// <returns>円弧の分割点</returns>
		private List<System.Drawing.PointF> CalcArcPoint()
		{
			return CalcArcPoint(Center, Width, Height, RotateAngle, StartAngle, SweepAngle);
		}
		/// <summary>
		/// 円弧の分割点を取得する
		/// </summary>
		/// <param name="center">中心座標</param>
		/// <param name="width">楕円幅</param>
		/// <param name="height">楕円高さ</param>
		/// <param name="rotateAngle">楕円回転角度</param>
		/// <param name="startAngle">開始角度</param>
		/// <param name="sweepAngle">描画角度</param>
		/// <returns>円弧の分割点</returns>
		private List<System.Drawing.PointF> CalcArcPoint(
			System.Drawing.PointF center,float width,float height,float rotateAngle,
			float startAngle,float sweepAngle) 
		{
			// 結果
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();
			// 10°単位で求める(最小は4分割)
			int divNum = (int)(sweepAngle / 10.0F);
			if (divNum < 4) divNum = 4;
			// 分割角度
			float stepAngle = sweepAngle / divNum;

			float angle = startAngle;
			for (int index = 0; index < divNum; index++, angle += stepAngle)
			{
				// 座標値を計算して追加
				result.Add(CalcAnglePoint(angle,center,width,height,rotateAngle));
			}
			// 終点を追加
			result.Add(CalcAnglePoint(startAngle + sweepAngle, center, width, height, rotateAngle));
			return result;

		}
	}
}

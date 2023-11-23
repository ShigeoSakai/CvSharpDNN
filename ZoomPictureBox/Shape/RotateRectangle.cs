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
	///	回転矩形クラス
	/// </summary>
	[ShapeName("回転矩形"), DefaultProperty("Center")]
	public class RotateRectangle : BaseMultiPointShape
	{
		/// <summary>
		/// 基準位置
		/// </summary>
		public enum REFERENCE_POSITION
		{
			LEFT_TOP,
			RIGHT_TOP,
			RIGHT_BOTTOM,
			LEFT_BOTTOM,
			CENTER,
		}
		/// <summary>
		/// 座標値の取得と設定
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override System.Drawing.PointF this[int index]
		{
			get => base[index];
			set
			{
				CalcPointsFromAnchor(index, value);
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
				// 矩形の算出
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
				// 矩形の算出
				CalcPoints();
			}
		}
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
				// 矩形の算出
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
				// 矩形の算出
				CalcPoints();
			}
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="center">中心座標</param>
		/// <param name="width">図形幅</param>
		/// <param name="height">図形高さ</param>
		/// <param name="angle">回転角度</param>
		public RotateRectangle(string name,System.Drawing.PointF center,float width,float height,float angle) : base(name)
		{
			base.Center = center;
			width_ = width;
			height_ = height;
			rotateAngle_ = angle;
			// 矩形の算出
			CalcPoints();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="center">中心座標</param>
		/// <param name="size">矩形サイズ</param>
		/// <param name="angle">回転角度</param>
		public RotateRectangle(string name, System.Drawing.PointF center, System.Drawing.SizeF size, float angle) :
			this(name, center, size.Width, size.Height, angle){ }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="x">中心X座標</param>
		/// <param name="y">中心Y座標</param>
		/// <param name="width">矩形幅</param>
		/// <param name="height">矩形高さ</param>
		/// <param name="angle">回転角度</param>
		public RotateRectangle(string name, float x, float y, float width, float height, float angle) :
			this(name, new System.Drawing.PointF(x, y), width, height, angle){ }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="rectangle">矩形</param>
		/// <param name="angle">回転角度</param>
		/// <param name="reference">矩形の回転基準点</param>
		public RotateRectangle(string name,System.Drawing.RectangleF rectangle,float angle,REFERENCE_POSITION reference):base(name)
		{
			// 基準位置から中心を算出
			switch(reference)
			{
				case REFERENCE_POSITION.LEFT_TOP:
					base.Center = ShapeUtil.CalcRotatePointD(rectangle.Width / 2.0, rectangle.Height / 2.0, angle, rectangle.Location);
					break;
				case REFERENCE_POSITION.RIGHT_TOP:
					base.Center = ShapeUtil.CalcRotatePointD(-rectangle.Width / 2.0, rectangle.Height / 2.0, angle, 
						new System.Drawing.PointF(rectangle.Right,rectangle.Top));
					break;
				case REFERENCE_POSITION.RIGHT_BOTTOM:
					base.Center = ShapeUtil.CalcRotatePointD(-rectangle.Width / 2.0, -rectangle.Height / 2.0, angle,
						new System.Drawing.PointF(rectangle.Right, rectangle.Bottom));
					break;
				case REFERENCE_POSITION.LEFT_BOTTOM:
					base.Center = ShapeUtil.CalcRotatePointD(rectangle.Width / 2.0, -rectangle.Height / 2.0, angle,
						new System.Drawing.PointF(rectangle.Left, rectangle.Bottom));
					break;
				default:
					base.Center = new System.Drawing.PointF(
						rectangle.X + rectangle.Width / 2.0F, rectangle.Y + rectangle.Height / 2.0F);
					break;
			}
			width_ = rectangle.Width;
			height_ = rectangle.Height;
			rotateAngle_ = angle;
			// 矩形の算出
			CalcPoints();
		}
		/// <summary>
		/// 中心座標,幅,高さ,角度から矩形座標を計算
		/// </summary>
		private void CalcPoints()
		{
			points_.Clear();
			// 左上点
			points_.Add(ShapeUtil.CalcRotatePointD((-Width / 2.0), (-Height/ 2.0),RotateAngle,Center));
			// 右上点
			points_.Add(ShapeUtil.CalcRotatePointD((Width / 2.0), (-Height / 2.0), RotateAngle, Center));
			// 右下点
			points_.Add(ShapeUtil.CalcRotatePointD((Width / 2.0F), (Height / 2.0F), RotateAngle, Center));
			// 左下点
			points_.Add(ShapeUtil.CalcRotatePointD((-Width / 2.0F), (Height / 2.0F), RotateAngle, Center));
			// 中心点
			points_.Add(Center);
		}
		/// <summary>
		/// アンカーから座標を更新する
		/// </summary>
		/// <param name="anchorNo">アンカー番号</param>
		/// <param name="value">新しい座標値</param>
		private void CalcPointsFromAnchor(int anchorNo,System.Drawing.PointF value)
		{
			if (anchorNo == 4)
			{   // 中心点を移動
				base.Center = value;
			}
			else if ((anchorNo >= 0) & (anchorNo < 4))
			{
				// 現在座標の中心からの角度
				double orig_angle = Math.Atan2(points_[anchorNo].Y - Center.Y, points_[anchorNo].X - Center.X);
				// 新しい点の中心からの角度
				double new_angle = Math.Atan2(value.Y - Center.Y, value.X - Center.X);
				// 新しい点の中心からの距離
				double length = Math.Sqrt(Math.Pow(value.X - Center.X, 2.0) + Math.Pow(value.Y - Center.Y, 2.0));
				float calc_angle = ShapeUtil.ToDegreeF(new_angle - orig_angle) % 180.0F;
				// 回転角度の更新
				rotateAngle_ = (rotateAngle_ + calc_angle) % 180.0F;
				// 幅と高さを求める
				width_ = (float)Math.Abs(length * Math.Cos(new_angle - ShapeUtil.ToRadian(RotateAngle)) * 2.0);
				height_ = (float)Math.Abs(length * Math.Sin(new_angle - ShapeUtil.ToRadian(RotateAngle)) * 2.0);
			}
			// 全ての制御点を算出
			CalcPoints();
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
				System.Drawing.PointF[] drawPoints = pts.Take(4).ToArray();
				// 描画ペン
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth) { DashStyle = LineStyle };
				if (Fill) 
				{   // 塗りつぶし
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
					graphics.FillPolygon(brush, drawPoints);
				}
				// 図形を描画
				graphics.DrawPolygon(pen, drawPoints);

				if (CenterDraw)
				{   // 中心描画
					List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
					pts.AddRange(centerPts);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					for (int index = 0; index < pts.Count; index++)
					{
						graphics.DrawRectangle(anchorPen,
							pts[index].X - AnchorRadius, pts[index].Y - AnchorRadius, AnchorRadius * 2, AnchorRadius * 2);
					}
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
			for(int index = 0; index < 4; index ++)
			{
				int next_index = (index + 1) % 4;
				if (ShapeUtil.IsContain(pts[index], pts[next_index], point, HitMargin))
					return true;
			}
			return false;
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
			switch (LabelPosition)
			{
				case LABEL_POSITION.TOP_LEFT:
				case LABEL_POSITION.TOP_LEFT_INNER:
					textPoint.X = pts[0].X + offsetX;
					textPoint.Y = pts[0].Y + offsetY;
					break;
				case LABEL_POSITION.TOP_CENTER:
				case LABEL_POSITION.TOP_CENTER_INNER:
					textPoint.X = (pts[0].X + pts[1].X) / 2.0F + offsetX;
					textPoint.Y = (pts[0].Y + pts[1].Y) / 2.0F + offsetY;
					break;
				case LABEL_POSITION.TOP_RIGHT:
				case LABEL_POSITION.TOP_RIGHT_INNER:
					textPoint.X = pts[1].X + offsetX;
					textPoint.Y = pts[1].Y + offsetY;
					break;
				case LABEL_POSITION.CENTER:
					textPoint.X = pts[4].X + offsetX;
					textPoint.Y = pts[4].Y + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_LEFT:
				case LABEL_POSITION.BOTTOM_LEFT_INNER:
					textPoint.X = pts[3].X + offsetX;
					textPoint.Y = pts[3].Y + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_CENTER:
				case LABEL_POSITION.BOTTOM_CENTER_INNER:
					textPoint.X = (pts[2].X + pts[3].X) / 2.0F + offsetX;
					textPoint.Y = (pts[2].Y + pts[3].Y) / 2.0F + offsetY;
					break;
				case LABEL_POSITION.BOTTOM_RIGHT:
				case LABEL_POSITION.BOTTOM_RIGHT_INNER:
					textPoint.X = pts[2].X + offsetX;
					textPoint.Y = pts[2].Y + offsetY;
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
		private Matrix CalcTextMatrix<PLIST>(PLIST pts) where PLIST : IList<System.Drawing.PointF>
		{
			Matrix mat = new Matrix();
			switch (LabelPosition)
			{
				case LABEL_POSITION.TOP_LEFT:
				case LABEL_POSITION.TOP_LEFT_INNER:
					mat.RotateAt(RotateAngle, pts[0]);
					break;
				case LABEL_POSITION.TOP_CENTER:
				case LABEL_POSITION.TOP_CENTER_INNER:
					mat.RotateAt(RotateAngle, new System.Drawing.PointF((pts[0].X + pts[1].X)/2.0F, (pts[0].Y + pts[1].Y) / 2.0F));
					break;
				case LABEL_POSITION.TOP_RIGHT:
				case LABEL_POSITION.TOP_RIGHT_INNER:
					mat.RotateAt(RotateAngle, pts[1]);
					break;
				case LABEL_POSITION.CENTER:
					mat.RotateAt(RotateAngle, pts[4]);
					break;
				case LABEL_POSITION.BOTTOM_LEFT:
				case LABEL_POSITION.BOTTOM_LEFT_INNER:
					mat.RotateAt(RotateAngle, pts[3]);
					break;
				case LABEL_POSITION.BOTTOM_CENTER:
				case LABEL_POSITION.BOTTOM_CENTER_INNER:
					mat.RotateAt(RotateAngle, new System.Drawing.PointF((pts[2].X + pts[3].X) / 2.0F, (pts[2].Y + pts[3].Y) / 2.0F));
					break;
				case LABEL_POSITION.BOTTOM_RIGHT:
				case LABEL_POSITION.BOTTOM_RIGHT_INNER:
					mat.RotateAt(RotateAngle, pts[2]);
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

	}
}

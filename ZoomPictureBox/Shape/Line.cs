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
	/// 直線クラス
	/// </summary>
	public class Line : BaseMultiPointShape
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "線";

		/// <summary>
		/// 始点
		/// </summary>
		[Category("図形"), DisplayName("始点"), Description("線分の始点座標")]
		public override System.Drawing.PointF Center { get =>base.Center; set { base.Center = value; } }
		/// <summary>
		/// 終点
		/// </summary>
		[Category("図形"), DisplayName("終点"), Description("線分の終点座標")]
		public System.Drawing.PointF End { get; set; }

		/// <summary>
		/// 始点の形状
		/// </summary>
		[Category("描画"), DisplayName("始点の形状"), DefaultValue(typeof(LINECAP_SHAPE), "NONE"), Description("始点の形状")]
		public LINECAP_SHAPE StartCap { get; set; } = LINECAP_SHAPE.NONE;
		/// <summary>
		/// 終点の形状
		/// </summary>
		[Category("描画"), DisplayName("終点の形状"), DefaultValue(typeof(LINECAP_SHAPE), "NONE"), Description("終点の形状")]
		public LINECAP_SHAPE EndCap { get; set; } = LINECAP_SHAPE.NONE;

		/// <summary>
		/// 座標値の取得・設定
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public override System.Drawing.PointF this[int index]
		{
			get
			{
				if (index == 0)
					return Center;
				if (index == 1)
					return End;
				return new System.Drawing.PointF();
			}
			set
			{
				if (index == 0)
					Center = value;
				else if (index == 1)
					End = value;
			}
		}
		/// <summary>
		/// 座標値の件数
		/// </summary>
		[Browsable(false)]
		public override int Count { get { return 2; } }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="start">開始点</param>
		/// <param name="end">終了点</param>
		public Line(string name, System.Drawing.Point start, System.Drawing.Point end) : base(name)
		{
			Center = start;
			End = end;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="start">開始点</param>
		/// <param name="end">終了点</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Line(string name, System.Drawing.Point start, System.Drawing.Point end, bool isEditable) : this(name, start, end)
		{
			IsEditable = isEditable;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="start">開始点</param>
		/// <param name="end">終了点</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Line(string name, System.Drawing.Point start, System.Drawing.Point end,
			System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, color, lineWidth, lineStyle)
		{
			Center = start;
			End = end;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="start">開始点</param>
		/// <param name="end">終了点</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		/// <param name="isEditable">true:編集可能/false:編集不可</param>
		public Line(string name, System.Drawing.Point start, System.Drawing.Point end,
			System.Drawing.Color color, bool isEditable,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, start, end, color, lineWidth, lineStyle)
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
			if (src is Line line)
			{
				End = line.End;
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
			if (IsDrawable())
			{
				// 始点、終点の座標
				System.Drawing.PointF[] pts = { new System.Drawing.PointF(Center.X,Center.Y) ,
							new System.Drawing.PointF(End.X,End.Y),
				};
				matrixInv.TransformPoints(pts);
				pts[1].X += matrixInv.Elements[0];
				pts[1].Y += matrixInv.Elements[3];

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
			List<System.Drawing.PointF> pts = base.DrawShape(graphics, matrixInv, size);
			if (pts != null)
			{
				// 描画するペンと線種
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
				{
					DashStyle = LineStyle,
				};
				// 直線を描画
				graphics.DrawLine(pen, pts[0],pts[1]);
				// 線の終端(始点側)
				LINECAP_SHAPE_LOCAL startCap = LineCapConvert(StartCap);
				DrawLineCap(graphics, startCap, pts[0], MarkerSize, (float)Math.Atan2(pts[1].Y - pts[0].Y, pts[1].X - pts[0].X),
					GetDrawColor(COLOR_SELECT.NORMAL_COLOR), GetDrawColor(COLOR_SELECT.FILL_COLOR), LineWidth);
				// 線の終端(終点側)
				LINECAP_SHAPE_LOCAL endCap = LineCapConvert(EndCap,false);
				DrawLineCap(graphics, endCap, pts[1], MarkerSize, (float)Math.Atan2(pts[1].Y - pts[0].Y, pts[1].X - pts[0].X),
					GetDrawColor(COLOR_SELECT.NORMAL_COLOR), GetDrawColor(COLOR_SELECT.FILL_COLOR), LineWidth);

				if (MarkerDraw)
				{	// 始点と終点にマーカーを描画
					DrawMarker(graphics, Marker, pts[0],
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
					DrawMarker(graphics, Marker, pts[1],
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					graphics.DrawRectangles(anchorPen, new System.Drawing.RectangleF[]
					{
						new System.Drawing.RectangleF(pts[0].X - AnchorRadius,pts[0].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
						new System.Drawing.RectangleF(pts[1].X - AnchorRadius,pts[1].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2),
					});
				}
				return pts;
			}
			return null;
		}
		/// <summary>
		/// 指定座標が領域に含まれているかどうか
		/// </summary>
		/// <param name="pts">計算した領域</param>
		/// <param name="point">指定座標</param>
		/// <returns></returns>
		protected override bool IsContain<PLIST>(PLIST pts, System.Drawing.Point point)
		{
			if (pts.Count >= 2)
			{
				return ShapeUtil.IsContain(pts[0], pts[1], point, HitMargin);
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
			if (pts.Count >= 2)
			{
				return base.HitTestAnchor(pts, point, out anchorPoint);
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
			{
				Center = new System.Drawing.PointF(Center.X + offsetX, Center.Y + offsetY);
				End = new System.Drawing.PointF(End.X + offsetX, End.Y + offsetY);
			}
			else
			{
				if ((Center.X + offsetX >= 0) && (End.X + offsetX < limitSize.Width) &&
					(Center.Y + offsetY >= 0) && (End.Y + offsetY < limitSize.Height))
				{   // 移動可能
					Center = new System.Drawing.PointF(Center.X + offsetX, Center.Y + offsetY);
					End = new System.Drawing.PointF(End.X + offsetX, End.Y + offsetY);
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
		/// <param name="limitSize">移動リミット</param>
		/// <returns>true:移動可能</returns>
		public override bool MoveAnchor(int offsetX, int offsetY, int anchorPoint, System.Drawing.Size limitSize)
		{
			if ((limitSize.IsEmpty) || (IsEditable == false) || ((anchorPoint != 0) && (anchorPoint != 1)))
				return base.MoveShape(offsetX, offsetY, limitSize);

			if (anchorPoint == 0)
			{
				if ((Center.X + offsetX >= 0) && (Center.X + offsetX <= limitSize.Width) &&
					(Center.Y + offsetY >= 0) && (Center.Y + offsetY <= limitSize.Height))
				{
					Center = new System.Drawing.PointF(Center.X + offsetX, Center.Y + offsetY);
					return true;
				}
			}
			else if (anchorPoint == 1)
			{
				if ((End.X + offsetX >= 0) && (End.X + offsetX <= limitSize.Width) &&
					(End.Y + offsetY >= 0) && (End.Y + offsetY <= limitSize.Height))
				{
					End = new System.Drawing.PointF(End.X + offsetX, End.Y + offsetY);
					return true;
				}
			}
			return false;
		}

	}
}

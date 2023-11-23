using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Drawing.Shape
{
	/// <summary>
	/// 曲線クラス
	/// </summary>
	[ShapeName("曲線"), DefaultProperty("BoundingRectangle")]
	public class Curve : Lines
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "曲線";

		/// <summary>
		/// スプラインテンション
		/// </summary>
		[Category("図形"), DisplayName("テンション"), DefaultValue(1.0F), Description("スプラインのテンション値")]
		public virtual float Tension { get; set; } = 1.0F;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name,params System.Drawing.Point[] points):base(name)
		{
			// 座標の設定
			Set(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name,bool isClose, params System.Drawing.Point[] points) : this(name,points)
		{
			IsClose = isClose;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name, float tension=1.0F, params System.Drawing.Point[] points) : this(name,points)
		{
			Tension = Tension;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name,bool isClose, float tension = 1.0F, params System.Drawing.Point[] points) : this(name,tension, points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name, params System.Drawing.PointF[] points) : base(name)
		{
			// 座標の設定
			Set(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name,bool isClose, params System.Drawing.PointF[] points) : this(name,points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name, float tension = 1.0F, params System.Drawing.PointF[] points) : this(name,points)
		{
			Tension = Tension;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Curve(string name,bool isClose, float tension = 1.0F, params System.Drawing.PointF[] points) : this(name,tension, points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid, float tension = 1.0F) : base(name, color, lineWidth, lineStyle)
		{
			Tension = Tension;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, bool isClose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid, float tension = 1.0F) : this(name, color, lineWidth, lineStyle,tension)
		{
			IsClose = isClose;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,
			params System.Drawing.Point[] points) : this(name, color, lineWidth, lineStyle)
		{
			// 座標の設定
			Set(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name,bool isClose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,
			params System.Drawing.Point[] points) : this(name, color, lineWidth, lineStyle, points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,float tension = 1.0F,
			params System.Drawing.Point[] points) : this(name, color, lineWidth, lineStyle, points)
		{
			Tension = tension;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name,bool isCLose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid, float tension = 1.0F,
			params System.Drawing.Point[] points) : this(name, color, lineWidth, lineStyle,tension, points)
		{
			IsClose = IsClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,
			params System.Drawing.PointF[] points) : base(name, color, lineWidth, lineStyle)
		{
			// 座標の設定
			Set(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, bool isClose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,
			params System.Drawing.PointF[] points) : this(name, color, lineWidth, lineStyle,points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid,float tension=1.0F, 
			params System.Drawing.PointF[] points) : this(name, color, lineWidth, lineStyle, points)
		{
			Tension = tension;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Curve(string name, bool isClose, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid, float tension = 1.0F,
			params System.Drawing.PointF[] points) : this(name, color, lineWidth, lineStyle,tension, points)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Curve(BaseMultiPointShape shape) : base(shape) {}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Curve(BaseMultiPointShape shape, bool isClose) : this(shape) { IsClose = isClose; }
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Curve(BaseMultiPointShape shape, bool isClose, bool isFill) : this(shape,isClose) { Fill = isFill; }
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
			if (src is Curve curve)
			{
				// スプラインテンション
				Tension = curve.Tension;
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
			// BaseMultiPointShapeのDrawShape()を呼び出す
			List<System.Drawing.PointF> pts = BaseDrawShape(graphics, matrixInv, size);
			if ((pts != null) && (pts.Count > 1))
			{
				// 描画するペンと線種
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
				{
					DashStyle = LineStyle,
				};
				if ((IsClose) && (Fill))
				{	// 塗りつぶし
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
					graphics.FillClosedCurve(brush, pts.ToArray(), FillMode.Winding, Tension);
				}
				// 曲線を描画
				if (IsClose)
					graphics.DrawClosedCurve(pen, pts.ToArray(), Tension, FillMode.Alternate);
				else
					graphics.DrawCurve(pen, pts.ToArray(),Tension);

				if (pts.Count >= 2)
				{
					// 線の終端(始点側)
					LINECAP_SHAPE_LOCAL startCap = LineCapConvert(StartCap);
					DrawLineCap(graphics, startCap, pts[0], MarkerSize, (float)Math.Atan2(pts[1].Y - pts[0].Y, pts[1].X - pts[0].X),
						GetDrawColor(COLOR_SELECT.NORMAL_COLOR), GetDrawColor(COLOR_SELECT.FILL_COLOR), LineWidth);
					// 線の終端(終点側)
					LINECAP_SHAPE_LOCAL endCap = LineCapConvert(EndCap, false);
					int lastOne = pts.Count - 2;
					DrawLineCap(graphics, endCap, pts[lastOne + 1], MarkerSize,
						(float)Math.Atan2(pts[lastOne + 1].Y - pts[lastOne].Y, pts[lastOne + 1].X - pts[lastOne + 1].X),
						GetDrawColor(COLOR_SELECT.NORMAL_COLOR), GetDrawColor(COLOR_SELECT.FILL_COLOR), LineWidth);
				}

				if (MarkerDraw)
				{	// マーカーを描画
					for(int index = 0; index < pts.Count; index ++)
						DrawMarker(graphics, Marker, pts[index],
							GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
							MarkerLineWidth, MarkerSize, DashStyle.Solid);
				}

				if (CenterDraw)
				{   // 中心描画
					List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					for (int index = 0; index < pts.Count; index++)
						graphics.DrawRectangle(anchorPen,
							pts[index].X - AnchorRadius, pts[index].Y - AnchorRadius, AnchorRadius * 2, AnchorRadius * 2);
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
			if (pts.Count >= 3)
			{
				return base.IsContain(pts, point);
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
			if (pts.Count >= 3)
			{
				return base.HitTestAnchor(pts, point, out anchorPoint, startIndex , lastIndex);
			}
			return false;
		}
		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected override void CreateContextMenu()
		{
			CreateContextMenuBase();
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator3"},
					new ToolStripMenuItem("線群に変換",null,MenuToLinesEventHandler,"ToLines"),
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierEventHandler,"ToBezier"),
				});
		}
		/// <summary>
		/// 線群に変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuToLinesEventHandler(object sender, EventArgs e)
		{
			Lines lines = new Lines(this,IsClose,Fill);
			lines.Set(points_.ToArray());
			// 図形表示更新
			OnUpdateShape(lines);
		}
	}
}

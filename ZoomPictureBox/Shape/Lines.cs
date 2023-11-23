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
	/// 線群クラス
	/// </summary>
	[ShapeName("線群"),DefaultProperty("BoundingRectangle")]
	public class Lines :BaseMultiPointShape
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "線群";

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
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		protected Lines(string name) : base(name) {  }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">頂点座標</param>
		public Lines(string name,params System.Drawing.Point[] points):this(name)
		{
			SetLocal(points);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="isClose">true:閉じた図形</param>
		/// <param name="points">頂点座標</param>
		public Lines(string name, bool isClose, params System.Drawing.Point[] points) : this(name,points)
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
		public Lines(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name,color,lineWidth,lineStyle){  }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="isClose">true:閉じた図形</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Lines(string name, bool isClose ,System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : this(name, color, lineWidth, lineStyle)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Lines(BaseMultiPointShape shape) : base(shape) { }

		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Lines(BaseMultiPointShape shape,bool isClose) : base(shape)
		{
			IsClose = isClose;
		}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Lines(BaseMultiPointShape shape, bool isClose,bool isFill) : this(shape,isClose)
		{
			Fill = isFill;
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
			if ((pts != null) && (pts.Count > 1))
			{
				// 塗りつぶし
				if ((Fill) && (IsClose) && (pts.Count > 2))
				{
					System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(GetDrawColor(COLOR_SELECT.FILL_COLOR));
					graphics.FillPolygon(brush, pts.ToArray());
				}
				// 描画するペンと線種
				System.Drawing.Pen pen = new System.Drawing.Pen(GetDrawColor(COLOR_SELECT.NORMAL_COLOR), LineWidth)
				{
					DashStyle = LineStyle,
				};
				if (MarkerDraw)
				{	// 最初の点のマーカー
					DrawMarker(graphics, Marker, pts[0],
						GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
						MarkerLineWidth, MarkerSize, DashStyle.Solid);
				}
				for (int index = 0; index < pts.Count -1; index ++)
				{
					// 直線を描画
					graphics.DrawLine(pen, pts[index], pts[index+1]);

					if (MarkerDraw)
					{   // マーカー描画
						DrawMarker(graphics, Marker, pts[index + 1],
							GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
							MarkerLineWidth, MarkerSize, DashStyle.Solid);
					}
				}
				// 閉じた図形か？
				if ((IsClose) && (pts.Count > 2))
					graphics.DrawLine(pen, pts.Last(), pts[0]);

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
				if (CenterDraw)
				{	// 中心描画
					List<System.Drawing.PointF> centerPts = base.DrawShape(graphics, matrixInv, size);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					for(int index = 0 ; index < pts.Count ; index ++)
						graphics.DrawRectangle(anchorPen,							
							pts[index].X - AnchorRadius,pts[index].Y - AnchorRadius, AnchorRadius * 2,AnchorRadius * 2);
				}
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
		protected List<System.Drawing.PointF> BaseDrawShape(System.Drawing.Graphics graphics, Matrix matrixInv, System.Drawing.Size size)
		{
			return base.DrawShape(graphics, matrixInv, size);
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
					new ToolStripMenuItem("ポリゴンに変換",null,MenuToPolygonEventHandler,"ToPolygon"),
					new ToolStripMenuItem("曲線に変換",null,MenuToCurveEventHandler,"ToCurve"),
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierEventHandler,"ToBezier"),
				});
		}
		/// <summary>
		/// コンテキストメニュー生成(線群基本メニュー)
		/// </summary>
		protected void CreateContextMenuBase()
		{
			base.CreateContextMenu();
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator1"},
					new ToolStripMenuItem("制御点追加",null,MenuPointAddEventHandler,"PointAdd"),
					new ToolStripMenuItem("制御点削除",null,MenuPointDelEventHandler,"PointDel"),
					new ToolStripSeparator(){Name = "Separator2"},
					new ToolStripMenuItem("オープンパス",null,MenuPathChangeEventHandler,"OpenPath"),
					new ToolStripMenuItem("クローズパス",null,MenuPathChangeEventHandler,"ClosePath"),
				});
		}
		/// <summary>
		/// コンテキストメニューの有効・無効設定
		/// </summary>
		/// <param name="anchorNo">アンカー番号 -1は図形選択</param>
		/// <param name="maxShapeNo">最大図形番号</param>
		/// <returns>true:メニューあり/false:メニューなし</returns>
		public override bool SetMenuEnable(int anchorNo = -1, int maxShapeNo = -1)
		{
			if (ContextMenu.Items.ContainsKey("PointAdd"))
				ContextMenu.Items["PointAdd"].Enabled = (anchorNo < 0);
			if (ContextMenu.Items.ContainsKey("PointDel"))
				ContextMenu.Items["PointDel"].Enabled = ((anchorNo >= 0) && (anchorNo < points_.Count));
			if (ContextMenu.Items.ContainsKey("OpenPath"))
				ContextMenu.Items["OpenPath"].Enabled = IsClose;
			if (ContextMenu.Items.ContainsKey("ClosePath"))
				ContextMenu.Items["ClosePath"].Enabled = !IsClose;
			return true;
		}
		/// <summary>
		/// ポリゴンに変換
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
		/// 曲線に変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuToCurveEventHandler(object sender, EventArgs e)
		{
			Curve curve = new Curve(this,IsClose,Fill);
			curve.Set(points_.ToArray());
			// 図形表示更新
			OnUpdateShape(curve);
		}

		
		/// <summary>
		/// パスの変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuPathChangeEventHandler(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu)
			{
				IsClose = (menu.Name == "ClosePath");
				// 図形表示更新
				OnUpdateShape();
			}
		}
	}
}

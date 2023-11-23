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
	/// 多角形クラス
	/// </summary>
	[ShapeName("多角形"),DefaultProperty("BoundingRectangle")]
	public class Polygon : Lines
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "多角形";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		/// <param name="points"></param>
		public Polygon(string name, params System.Drawing.Point[] points) : base(name, true, points) { }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="color">描画色</param>
		/// <param name="lineWidth">ライン幅</param>
		/// <param name="lineStyle">線種</param>
		public Polygon(string name, System.Drawing.Color color,
			float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, true, color, lineWidth, lineStyle) { }
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="shape"></param>
		public Polygon(BaseMultiPointShape shape) : base(shape) { IsClose = true; }

		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected override void CreateContextMenu()
		{
			CreateContextMenuBase();
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator3"},
					new ToolStripMenuItem("線群に変換",null,MenuToLineEventHandler,"ToLine"),
					new ToolStripMenuItem("ベジェ曲線に変換",null,MenuToBezierEventHandler,"ToBezier"),
				});
		}
		/// <summary>
		/// 線群に変換
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MenuToLineEventHandler(object sender, EventArgs e)
		{
			Lines lines = new Lines(this);
			// 図形表示更新
			OnUpdateShape(lines);
		}
	}
}

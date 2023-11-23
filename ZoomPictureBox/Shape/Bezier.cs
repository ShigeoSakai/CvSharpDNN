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
	/// ベジェ曲線
	/// </summary>
	[ShapeName("ベジェ曲線"), DefaultProperty("BoundingRectangle")]
	public class Bezier :Lines
	{
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "ベジェ曲線";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">座標点</param>
		public Bezier(string name,params System.Drawing.Point[] points) : base(name)
		{
			if (Set(points) == false)
				throw new ArgumentException("座標点数は3n+1でなければなりません");
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="points">座標点</param>
		public Bezier(string name, params System.Drawing.PointF[] points) : base(name)
		{
			if (Set(points) == false)
				throw new ArgumentException("座標点数は3n+1でなければなりません");
		}
		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="src">コピー元</param>
		public Bezier(BaseMultiPointShape src) : base(src)
		{
			if (src is Bezier)
			{	// 何もしない(基底クラスでコピーされているはず)

			}
			else if (src is BaseMultiPointShape shape)
			{   // 線群の場合
				points_.Clear();
				// ベジェ曲線を取得し追加
				List<System.Drawing.PointF> list = shape.ToBezier();
				if (list != null)
				{
					points_.AddRange(list);
					if ((shape is Circle) || ((shape is Ellipse ellipse) && (ellipse.IsArc == false)))
					{	// 閉じた曲線にする
						IsClose = true;
					}
					// 外接矩形の更新
					updateRectangle();
				}
			}
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
			if (src is Bezier bezier)
			{
				// 座標の設定
				if (Set(bezier.points_.ToArray()) == false)
					throw new ArgumentException("座標点数は3n+1でなければなりません");
			}
		}
		/// <summary>
		/// 制御点の設定
		/// </summary>
		/// <param name="points">制御点</param>
		/// <returns>true:設定OK</returns>
		public override bool Set(params System.Drawing.Point[] points)
		{
			if (ShapeUtil.CheckBezierPoints(points.Length))
			{
				// 制御点の生成
				return base.Set(points);
			}
			return false;
		}
		/// <summary>
		/// 制御点の追加
		/// </summary>
		/// <param name="points">制御点</param>
		/// <returns>true:追加OK</returns>
		public override bool Add(params System.Drawing.Point[] points)
		{
			if (ShapeUtil.CheckBezierPoints(points_.Count + points.Length))
			{
				// 制御点の追加
				return base.Add(points);
			}
			return false;
		}
		/// <summary>
		/// 制御点の設定
		/// </summary>
		/// <param name="points">制御点</param>
		/// <returns>true:設定OK</returns>
		public override bool Set(params System.Drawing.PointF[] points)
		{
			if (ShapeUtil.CheckBezierPoints(points.Length))
			{
				// 制御点の生成
				return base.Set(points);
			}
			return false;
		}
		/// <summary>
		/// 制御点の追加
		/// </summary>
		/// <param name="points">制御点</param>
		/// <returns>true:追加OK</returns>
		public override bool Add(params System.Drawing.PointF[] points)
		{
			if (ShapeUtil.CheckBezierPoints(points_.Count + points.Length))
			{
				// 制御点の追加
				return base.Add(points);
			}
			return false;
		}

		/// <summary>
		/// 外形矩形の更新
		/// </summary>
		/// <remarks>
		/// 外接矩形と中心座標を更新する
		/// </remarks>
		protected override void updateRectangle()
		{
			int xmin = int.MaxValue, ymin = int.MaxValue, xmax = int.MinValue, ymax = int.MinValue;

			List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(points_);
			if (lines != null)
			{
				foreach(System.Drawing.PointF[] pts in lines)
				{
					foreach(System.Drawing.PointF pt in pts)
					{
						if (xmin > pt.X) xmin = (int)(pt.X - 0.5F);
						if (ymin > pt.Y) ymin = (int)(pt.Y - 0.5F);
						if (xmax < pt.X) xmax = (int)(pt.X + 0.5F);
						if (ymax < pt.Y) ymax = (int)(pt.Y + 0.5F);
					}
				}
			}
			// 外形矩形
			BoundingRectangle = new System.Drawing.Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
			// 中心点
			Center = new System.Drawing.Point((xmax + xmin) / 2, (ymax + ymin) / 2);
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
				// 曲線を描画
				graphics.DrawBeziers(pen, pts.ToArray());

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
				{   // マーカーを描画
					for (int index = 0; index < pts.Count; index+=3 )
						DrawMarker(graphics, Marker, pts[index],
							GetDrawColor(COLOR_SELECT.MARKER_COLOR), GetDrawColor(COLOR_SELECT.MARKER_COLOR),
							MarkerLineWidth, MarkerSize, DashStyle.Solid);
				}

				if (Selected)
				{   // アンカーを描画
					System.Drawing.Pen anchorPen = new System.Drawing.Pen(AnchorColor, LineWidth);
					for (int index = 0; index < pts.Count; index++)
						graphics.DrawRectangle(anchorPen,
							pts[index].X - AnchorRadius, pts[index].Y - AnchorRadius, AnchorRadius * 2, AnchorRadius * 2);
					for(int index = 0; index < pts.Count - 1; index++)
					{	
						if (index % 3 != 1)
						{
							graphics.DrawLine(anchorPen, pts[index], pts[index + 1]);
						}
					}
				}
				if (DebugMode.HasFlag(DEBUG_MODE.NEAR_LINE))
				{   // 近似線表示
					List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(pts);
					foreach (System.Drawing.PointF[] item in lines)
						graphics.DrawLines(new System.Drawing.Pen(AnchorColor, MarkerLineWidth), item);
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
		/// <returns>true:指定座標が領域に含まれている</returns>
		protected override bool IsContain<TLIST>(TLIST pts, System.Drawing.Point point)
		{
			if (pts != null)
			{
				// 先頭は0, 3, 6, 9,...
				for(int st = 0; st < pts.Count; st += 3 )
				{
					System.Drawing.RectangleF rect = ShapeUtil.getBoundingRectangleF(pts, st, st + 3);
					if (rect.Contains(point))
					{
						List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(pts,st,st+3);
						foreach (System.Drawing.PointF[] line in lines)
						{
							for (int index = 0; index < line.Length - 1; index++)
								if (ShapeUtil.IsContain(line[index], line[index + 1], point, HitMargin))
									return true;
						}
					}
				}
			}
			return false;
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
		protected override bool HitTestAnchor<PLIST>(PLIST pts, System.Drawing.Point point, out int anchorPoint,
			int startIndex = 0, int lastIndex = -1)
		{
			anchorPoint = -1;
			if (pts != null)
			{
				if (Selected)
				{   // 選択中
					return base.HitTestAnchor(pts, point, out anchorPoint);
				}
				else
				{
					for (int index = 0; index < pts.Count; index += 3)
						if (HitTestAnchor(pts[index], point))
						{
							anchorPoint = index;
							return true;
						}
				}
			}
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
		public override bool MoveAnchor(int offsetX, int offsetY, int anchorPoint, System.Drawing.Size limitSize)
		{
			if ((limitSize.IsEmpty) || (IsEditable == false) || (anchorPoint < 0) || (anchorPoint >= points_.Count))
				return base.MoveShape(offsetX, offsetY, limitSize);

			if ((this[anchorPoint].X + offsetX >= 0) && (this[anchorPoint].X + offsetX <= limitSize.Width) &&
				(this[anchorPoint].Y + offsetY >= 0) && (this[anchorPoint].Y + offsetY <= limitSize.Height))
			{
				this[anchorPoint] = new System.Drawing.PointF(this[anchorPoint].X + offsetX, this[anchorPoint].Y + offsetY);
				if (IsClose)
				{	// 閉じた曲線の場合は、先頭と最後を同じ座標にする
					if (anchorPoint == 0)
						this[points_.Count-1] = new System.Drawing.PointF(this[0].X, this[0].Y);
					else if (anchorPoint == points_.Count -1)
						this[0] = new System.Drawing.PointF(this[points_.Count - 1].X, this[points_.Count - 1].Y);
				}
				// 外形矩形の更新
				updateRectangle();
				return true;
			}
			return false;
		}

		/// <summary>
		/// コンテキストメニュー生成
		/// </summary>
		protected override void CreateContextMenu()
		{
			base.CreateContextMenuBase();
			ContextMenu.Items.AddRange(new ToolStripItem[]
				{
					new ToolStripSeparator(){Name = "Separator3"},
					new ToolStripMenuItem("線群に変換",null,MenuToLinesEventHandler,"ToLines"),
					new ToolStripMenuItem("曲線に変換",null,MenuToCurveEventHandler,"ToCurve"),
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
			bool isAnchor = false;
			if ((anchorNo % 3 == 0) && (points_.Count >= 7))
 				isAnchor = true;

			if (ContextMenu.Items.ContainsKey("PointAdd"))
				ContextMenu.Items["PointAdd"].Enabled = !isAnchor;
			if (ContextMenu.Items.ContainsKey("PointDel"))
				ContextMenu.Items["PointDel"].Enabled = isAnchor;

			if (ContextMenu.Items.ContainsKey("OpenPath"))
				ContextMenu.Items["OpenPath"].Enabled = IsClose;
			if (ContextMenu.Items.ContainsKey("ClosePath"))
				ContextMenu.Items["ClosePath"].Enabled = !IsClose;

			return true;
		}
		/// <summary>
		/// メニューから制御点追加
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected override void MenuPointAddEventHandler(object sender, EventArgs e)
		{
			int insert_pos = points_.Count;
			// どの線分上かチェック
			List<System.Drawing.PointF[]> lines = ShapeUtil.CalcBezierNearLines(points_);
			for(int index = 0; index < lines.Count; index ++)
			{
				bool isFound = false;
				for(int inner_index = 0; inner_index < lines[index].Length -1; inner_index ++)
					if (ShapeUtil.IsContain(lines[index][inner_index], lines[index][inner_index+1],MenuImageLocation, HitMargin))
					{
						insert_pos = index;
						isFound = true;
						break;
					}
				if (isFound) break;
			}
			if (insert_pos < points_.Count)
			{
				points_.InsertRange(insert_pos * 3 + 2,
					new System.Drawing.PointF[]{
						new System.Drawing.PointF(MenuImageLocation.X - 2 * AnchorRadius,MenuImageLocation.Y - 2 * AnchorRadius),
						MenuImageLocation,
						new System.Drawing.PointF(MenuImageLocation.X + 2 * AnchorRadius,MenuImageLocation.Y + 2 * AnchorRadius),
					});
			}
			else
			{	// 最後の点の制御点 + 追加点の制御点 ＋ 追加点
				points_.AddRange(new System.Drawing.PointF[]
				{
					new System.Drawing.PointF(MenuImageLocation.X - 2 * AnchorRadius,MenuImageLocation.Y - 2 * AnchorRadius),
					new System.Drawing.PointF(MenuImageLocation.X - 2 * AnchorRadius,MenuImageLocation.Y - 2 * AnchorRadius),
					MenuImageLocation
				});
			}
			// 図形表示更新
			OnUpdateShape();
		}
		/// <summary>
		/// メニューから制御点削除
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected override void MenuPointDelEventHandler(object sender, EventArgs e)
		{
			if ((MenuAnchorNo >= 0) && (MenuAnchorNo < points_.Count) && (MenuAnchorNo % 3 == 0))
			{
				if (MenuAnchorNo == 0)
				{
					points_.RemoveAt(1);
					points_.RemoveAt(0);
				}
				else if (MenuAnchorNo == points_.Count -1)
				{
					points_.RemoveAt(MenuAnchorNo);
					points_.RemoveAt(MenuAnchorNo - 1);
				}
				else
				{
					points_.RemoveAt(MenuAnchorNo + 1);
					points_.RemoveAt(MenuAnchorNo);
					points_.RemoveAt(MenuAnchorNo-1);
				}
				// 図形表示更新
				OnUpdateShape();
			}
		}
		/// <summary>
		/// 線群に変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected virtual void MenuToLinesEventHandler(object sender, EventArgs e)
		{
			Lines lines = new Lines(this);
			lines.Set(new System.Drawing.Point((int)points_[0].X, (int)points_[0].Y));
			for(int index = 3;index < points_.Count; index += 3)
				lines.Add(new System.Drawing.Point((int)points_[index].X,(int)points_[index].Y));
			// 図形表示更新
			OnUpdateShape(lines);
		}
		/// <summary>
		/// 曲線に変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected override void MenuToCurveEventHandler(object sender, EventArgs e)
		{
			Curve curve = new Curve(this);
			curve.Set(points_[0]);
			for (int index = 3; index < points_.Count; index += 3)
				curve.Add(points_[index]);
			// 図形表示更新
			OnUpdateShape(curve);
		}
		/// <summary>
		/// パスの変換
		/// </summary>
		/// <param name="sender">送信元オブジェクト</param>
		/// <param name="e">イベント引数</param>
		protected override void MenuPathChangeEventHandler(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu)
			{
				IsClose = (menu.Name == "ClosePath");
				if (IsClose)
				{
					if ((this[0].X != this[Count-1].X) || (this[0].Y != this[Count - 1].Y))
					{   // 最終点から始点に繋ぐ線を生成する
						System.Drawing.PointF offsetEnd = new System.Drawing.PointF(
							this[Count - 2].X - this[Count - 1].X, this[Count - 2].Y - this[Count - 1].Y);
						points_.Add(new System.Drawing.PointF(this[Count - 1].X - offsetEnd.X, this[Count - 1].Y - offsetEnd.Y));
						System.Drawing.PointF offsetStart = new System.Drawing.PointF(
							this[1].X - this[0].X, this[1].Y - this[0].Y);
						points_.Add(new System.Drawing.PointF(this[0].X - offsetStart.X, this[0].Y - offsetStart.Y));
						points_.Add(new System.Drawing.PointF(this[0].X, this[0].Y));
						updateRectangle();
					}
				}
				// 図形表示更新
				OnUpdateShape();
			}
		}
	}
}

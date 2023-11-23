using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Shape
{
	public static class ShapeUtil
	{
		/// <summary>
		/// 度からラジアンに変換
		/// </summary>
		/// <param name="degree"></param>
		/// <returns></returns>
		public static double ToRadian(double degree)
		{
			return Math.PI * degree / 180.0;
		}
		/// <summary>
		/// 度からラジアンに変換
		/// </summary>
		/// <param name="degree"></param>
		/// <returns></returns>
		public static float ToRadian(float degree)
		{
			return (float)ToRadian((double)degree);
		}
		/// <summary>
		/// 度からラジアンに変換
		/// </summary>
		/// <param name="degree"></param>
		/// <returns></returns>
		public static float ToRadianF(double degree)
		{
			return (float)ToRadian(degree);
		}
		/// <summary>
		/// ラジアンから度へ変換
		/// </summary>
		/// <param name="radian"></param>
		/// <returns></returns>
		public static double ToDegree(double radian)
		{
			return radian * (180.0 / Math.PI);
		}
		/// <summary>
		/// ラジアンから度へ変換
		/// </summary>
		/// <param name="radian"></param>
		/// <returns></returns>
		public static float ToDegree(float radian)
		{
			return (float)ToDegree((double)radian);
		}
		/// <summary>
		/// ラジアンから度へ変換
		/// </summary>
		/// <param name="radian"></param>
		/// <returns></returns>
		public static float ToDegreeF(double radian)
		{
			return (float)ToDegree(radian);
		}
		/// <summary>
		/// 原点基準で座標を回転
		/// </summary>
		/// <param name="point">座標</param>
		/// <param name="angle">角度(度)</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointD(System.Drawing.PointF point, float angle)
		{
			return CalcRotatePointR(point,ToRadian(angle));
		}
		/// <summary>
		/// 原点基準で座標を回転
		/// </summary>
		/// <param name="point">座標</param>
		/// <param name="angle">角度(RADIAN)</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointR(System.Drawing.PointF point, double angle)
		{
			return new System.Drawing.PointF(
				(float)(point.X * Math.Cos(angle) - point.Y * Math.Sin(angle)),
				(float)(point.X * Math.Sin(angle) + point.Y * Math.Cos(angle)));
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="point">座標</param>
		/// <param name="angle">角度(度)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointD(System.Drawing.PointF point, float angle,System.Drawing.PointF offset)
		{
			return CalcRotatePointR(point, ToRadian(angle), offset);
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="point">座標</param>
		/// <param name="angle">角度(RADIAN)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointR(System.Drawing.PointF point, double angle, System.Drawing.PointF offset)
		{
			return new System.Drawing.PointF(
				(float)(point.X * Math.Cos(angle) - point.Y * Math.Sin(angle) + offset.X),
				(float)(point.X * Math.Sin(angle) + point.Y * Math.Cos(angle) + offset.Y));
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="angle">角度(RADIAN)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointR(float x, float y, double angle, System.Drawing.PointF offset)
		{
			return new System.Drawing.PointF(
				(float)(x * Math.Cos(angle) - y * Math.Sin(angle) + offset.X),
				(float)(x * Math.Sin(angle) + y * Math.Cos(angle) + offset.Y));
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="angle">角度(RADIAN)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointR(double x, double y, double angle, System.Drawing.PointF offset)
		{
			return new System.Drawing.PointF(
				(float)(x * Math.Cos(angle) - y * Math.Sin(angle) + offset.X),
				(float)(x * Math.Sin(angle) + y * Math.Cos(angle) + offset.Y));
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="angle">角度(度)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointD(float x, float y, float angle, System.Drawing.PointF offset)
		{
			return CalcRotatePointR(x,y,ToRadian(angle),offset);
		}
		/// <summary>
		/// 座標を回転後、平行移動する
		/// </summary>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <param name="angle">角度(度)</param>
		/// <param name="offset">平行移動量</param>
		/// <returns>回転後の座標</returns>
		public static System.Drawing.PointF CalcRotatePointD(double x, double y, float angle, System.Drawing.PointF offset)
		{
			return CalcRotatePointR(x,y,ToRadian(angle),offset);
		}
		/// <summary>
		/// 2点間の直線に含まれるかどうか
		/// </summary>
		/// <param name="pt0">点1</param>
		/// <param name="pt1">点2</param>
		/// <param name="point">チェックする点</param>
		/// <param name="HitMargin">当たり判定幅</param>
		/// <returns>true:直線に含まれる</returns>
		public static bool IsContain(System.Drawing.PointF pt0, System.Drawing.PointF pt1, System.Drawing.Point point , int HitMargin)
		{
			if (pt0.X - pt1.X != 0.0F)
			{
				// y = ax + b と (x0,y0)の距離は...
				//  len = abs(ax0-y0+b)/sqrt(a^2+1)
				// y = ax + b が(xs,ys),(xe,ye)を通るので
				//  ys = axs + b
				//  ye = axe + b
				// (ys-ye) = (xs-xe)a  .. a = (ys-ye)/(xs-xe)
				// b = ys - axs;
				double a = (pt0.Y - pt1.Y) / (pt0.X - pt1.X);
				double b = pt0.Y - a * pt0.X;
				double len = Math.Abs(a * point.X - point.Y + b) / Math.Sqrt(a * a + 1);
				if ((len <= HitMargin) && (a != 0.0))
				{
					// 2直線が直交する場合、傾きの積が -1
					//  aβ = -1 ... β = -1/a
					// pointを通り直交する線分
					//  py = -px/a +c ... c = py + px/a
					//  y = -x/a + py + px/a
					//  y = ax + ys - axs との交点
					//  0 = (a+1/a)x + ys - axs - py - px/a
					//  x = (py+px/a - ys + axs)/(a+1/a)
					//  y = ...
					double xc = (point.Y + point.X / a - pt0.Y + a * pt0.X) / (a + 1 / a);
					double yc = a + xc + pt0.Y - a * pt0.X;
					float ll = (pt0.X <= pt1.X) ? pt0.X : pt1.X;
					float lr = (pt0.X <= pt1.X) ? pt1.X : pt0.X;
					if ((ll <= xc) && (xc <= lr))
					{
						return true;
					}
					double ll_len = Math.Pow(xc - pt0.X, 2.0) + Math.Pow(yc - pt0.Y, 2.0);
					double lr_len = Math.Pow(xc - pt1.X, 2.0) + Math.Pow(yc - pt1.Y, 2.0);
					if ((ll_len <= HitMargin) || (lr_len <= HitMargin))
						return true;
				}
				else
				{   // 横線
					double len_y = Math.Abs(point.Y - pt0.Y);
					if (len_y <= HitMargin)
					{
						float lt_pos = (pt0.X >= pt1.X) ? pt0.X : pt1.X;
						float lb_pos = (pt0.X >= pt1.X) ? pt1.X : pt0.X;
						if ((lb_pos - HitMargin <= point.X) && (point.X <= lt_pos + HitMargin))
							return true;
					}
				}
			}
			else
			{   // 縦線
				double len = Math.Abs(point.X - pt0.X);
				if (len <= HitMargin)
				{
					float lt_pos = (pt0.Y >= pt1.Y) ? pt0.Y : pt1.Y;
					float lb_pos = (pt0.Y >= pt1.Y) ? pt1.Y : pt0.Y;
					if ((lb_pos - HitMargin <= point.Y) && (point.Y <= lt_pos + HitMargin))
						return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 点群を移動させる
		/// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
		/// <param name="points">点群</param>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		public static void MovePoints<PLIST>(PLIST points, int offsetX , int offsetY) where PLIST:IList<System.Drawing.PointF>
		{
			for(int index = 0; index < points.Count; index ++)
			{
				points[index] = new System.Drawing.PointF(points[index].X + offsetX, points[index].Y + offsetY);
			}
		}
		/// <summary>
		/// 点群を移動させる
		/// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
		/// <param name="points">点群</param>
		/// <param name="offsetX">X移動量</param>
		/// <param name="offsetY">Y移動量</param>
		public static void MovePoints<PLIST>(PLIST points, float offsetX, float offsetY) where PLIST : IList<System.Drawing.PointF>
		{
			for (int index = 0; index < points.Count; index++)
			{
				points[index] = new System.Drawing.PointF(points[index].X + offsetX, points[index].Y + offsetY);
			}
		}
		/// <summary>
		/// Point配列からList<PointF>に変換
		/// </summary>
		/// <typeparam name="PLIST">Pointの配列もしくはリスト</typeparam>
		/// <param name="points">点の座標配列/リスト</param>
		/// <returns>PointFのリスト</returns>
		public static List<System.Drawing.PointF> ToPointFList<PLIST>(PLIST points) where PLIST:IList<System.Drawing.Point>
		{
			List<System.Drawing.PointF> result = new List<System.Drawing.PointF>();
			foreach (System.Drawing.Point pt in points)
				result.Add(new System.Drawing.PointF(pt.X, pt.Y));
			return result;
		}
		/// <summary>
		/// Point配列からPointF[]に変換
		/// </summary>
		/// <typeparam name="PLIST">Pointの配列もしくはリスト</typeparam>
		/// <param name="points">点の座標配列/リスト</param>
		/// <returns>PointFの配列</returns>
		public static System.Drawing.PointF[] ToPointFArray<PLIST>(PLIST points) where PLIST : IList<System.Drawing.Point>
		{
			System.Drawing.PointF[] result = new System.Drawing.PointF[points.Count];
			for (int index = 0; index < points.Count; index++)
				result[index] = new System.Drawing.PointF(points[index].X, points[index].Y);
			return result;
		}
		/// <summary>
		/// 指定点群で囲まれた領域を取得する
		/// </summary>
		/// <param name="points">点群</param>
		/// <param name="startIndex">開始インデックス</param>
		/// <param name="lastIndex">終了インデックス</param>
		/// <param name="step">増分</param>
		/// <returns>領域矩形</returns>
		public static System.Drawing.RectangleF getBoundingRectangleF<PLIST>(PLIST points, int startIndex = 0, int lastIndex = -1, int step = 1) 
			where PLIST:IList<System.Drawing.PointF>
		{
			if (lastIndex < 0)
				lastIndex = points.Count - 1;
			if ((points.Count > startIndex) && (points.Count > lastIndex))
			{
				float xmin = float.MaxValue, ymin = float.MaxValue, xmax = float.MinValue, ymax = float.MinValue;
				for (int index = startIndex; index <= lastIndex; index += step)
				{
					if (xmin > points[index].X) xmin = points[index].X;
					if (ymin > points[index].Y) ymin = points[index].Y;
					if (xmax < points[index].X) xmax = points[index].X;
					if (ymax < points[index].Y) ymax = points[index].Y;
				}
				// 外形矩形
				return new System.Drawing.RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
			}
			return new System.Drawing.RectangleF();
		}

		/// <summary>
		/// 指定点群で囲まれた領域を取得する
		/// </summary>
		/// <param name="points">点群</param>
		/// <param name="startIndex">開始インデックス</param>
		/// <param name="lastIndex">終了インデックス</param>
		/// <param name="step">増分</param>
		/// <returns>領域矩形</returns>
		public static System.Drawing.RectangleF getBoundingRectangle<PLIST>(PLIST points, int startIndex = 0, int lastIndex = -1, int step = 1)
			where PLIST : IList<System.Drawing.Point>
		{
			if (lastIndex < 0)
				lastIndex = points.Count - 1;
			if ((points.Count > startIndex) && (points.Count > lastIndex))
			{
				float xmin = float.MaxValue, ymin = float.MaxValue, xmax = float.MinValue, ymax = float.MinValue;
				for (int index = startIndex; index <= lastIndex; index += step)
				{
					if (xmin > points[index].X) xmin = points[index].X;
					if (ymin > points[index].Y) ymin = points[index].Y;
					if (xmax < points[index].X) xmax = points[index].X;
					if (ymax < points[index].Y) ymax = points[index].Y;
				}
				// 外形矩形
				return new System.Drawing.RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
			}
			return new System.Drawing.RectangleF();
		}
		/// <summary>
		/// 指定点群で囲まれた領域を取得する
		/// </summary>
		/// <param name="points">点群</param>
		/// <returns>領域矩形</returns>
		public static System.Drawing.RectangleF getBoundingRectangle(params System.Drawing.PointF[] points)
		{
			float xmin = float.MaxValue, ymin = float.MaxValue, xmax = float.MinValue, ymax = float.MinValue;
			for (int index = 0; index < points.Length; index++)
			{
				if (xmin > points[index].X) xmin = points[index].X;
				if (ymin > points[index].Y) ymin = points[index].Y;
				if (xmax < points[index].X) xmax = points[index].X;
				if (ymax < points[index].Y) ymax = points[index].Y;
			}
			// 外形矩形
			return new System.Drawing.RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
		}
		/// <summary>
		/// 指定点群で囲まれた領域を取得する
		/// </summary>
		/// <param name="points">点群</param>
		/// <returns>領域矩形</returns>
		public static System.Drawing.RectangleF getBoundingRectangle(params System.Drawing.Point[] points)
		{
			float xmin = float.MaxValue, ymin = float.MaxValue, xmax = float.MinValue, ymax = float.MinValue;
			for (int index = 0; index < points.Length; index++)
			{
				if (xmin > points[index].X) xmin = points[index].X;
				if (ymin > points[index].Y) ymin = points[index].Y;
				if (xmax < points[index].X) xmax = points[index].X;
				if (ymax < points[index].Y) ymax = points[index].Y;
			}
			// 外形矩形
			return new System.Drawing.RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
		}


		#region [ベジェ関連]
		/// <summary>
		/// 座標点数のチェック
		/// </summary>
		/// <param name="num">座標点数</param>
		/// <returns>true:チェックOK</returns>
		/// <remarks>ベジェ曲線の座標点数は、3n+1</remarks>
		public static bool CheckBezierPoints(int num)
		{
			num--;
			if (num % 3 != 0)
				return false;
			return true;
		}
		/// <summary>
		/// 2点をratio:(1-ratio)で分割する点を求める
		/// </summary>
		/// <param name="pt1">点1</param>
		/// <param name="pt2">点2</param>
		/// <param name="ratio">比率 0 < ratio < 1 </param>
		/// <returns>指定比率で分割する点</returns>
		private static System.Drawing.PointF CalcBezierDivisionPoint(
			System.Drawing.PointF pt1, System.Drawing.PointF pt2, float ratio)
		{
			double theater = Math.Atan2(pt2.Y - pt1.Y, pt2.X - pt1.X);
			double radius = Math.Sqrt(Math.Pow(pt2.Y - pt1.Y, 2.0) + Math.Pow(pt2.X - pt1.X, 2.0));
			return new System.Drawing.PointF(
				(float)(radius * ratio * Math.Cos(theater) + pt1.X),
				(float)(radius * ratio * Math.Sin(theater) + pt1.Y));
		}
		/// <summary>
		/// 指定比率で分割する点を求める
		/// </summary>
		/// <param name="pt1">点1</param>
		/// <param name="pt2">点2</param>
		/// <param name="pt3">点3</param>
		/// <param name="pt4">点4</param>
		/// <param name="ratio">比率 0 < ratio < 1 </param>
		/// <returns>指定比率で分割する点</returns>
		private static System.Drawing.PointF CalcBezierDivisionPoint(
			System.Drawing.PointF pt1, System.Drawing.PointF pt2,
			System.Drawing.PointF pt3, System.Drawing.PointF pt4, float ratio)
		{
			// pt1とpt2の計算点  ... pt5
			System.Drawing.PointF pt5 = CalcBezierDivisionPoint(pt1, pt2, ratio);
			// pt2とpt3の計算点  ... pt6
			System.Drawing.PointF pt6 = CalcBezierDivisionPoint(pt2, pt3, ratio);
			// pt3とpt4の計算点  ... pt7
			System.Drawing.PointF pt7 = CalcBezierDivisionPoint(pt3, pt4, ratio);
			// pt5とpt6の計算点 ... pt8
			System.Drawing.PointF pt8 = CalcBezierDivisionPoint(pt5, pt6, ratio);
			// pt6とpt7の計算点 ... pt9
			System.Drawing.PointF pt9 = CalcBezierDivisionPoint(pt6, pt7, ratio);
			// pt8とpt9の計算点
			return CalcBezierDivisionPoint(pt8, pt9, ratio);
		}
		/// <summary>
		/// 近似点群を求める
		/// </summary>
		/// <typeparam name="PLIST">PointFの配列もしくはリスト</typeparam>
		/// <param name="points">ベジェ曲線の点群</param>
		/// <param name="startIndex">開始インデックス</param>
		/// <param name="lastIndex">終了インデックス</param>
		/// <param name="minDivision">最小分割数</param>
		/// <param name="divisionLength">分割長さ</param>
		/// <returns>近似点群</returns>
		public static List<System.Drawing.PointF[]> CalcBezierNearLines<PLIST>(PLIST points,
			int startIndex = 0, int lastIndex = -1, int minDivision = 6, float divisionLength = 50.0F) where PLIST:IList<System.Drawing.PointF>
		{
			if (lastIndex < 0)
				lastIndex = points.Count - 1;
			if ((points.Count > startIndex) && (points.Count > lastIndex) && (CheckBezierPoints(lastIndex - startIndex + 1)))
			{
				List<System.Drawing.PointF[]> result = new List<System.Drawing.PointF[]>();
				for (int index = startIndex; index < lastIndex - 2; index += 3)
				{
					// 分割数を求める
					double length = Math.Sqrt(Math.Pow(points[index + 3].X - points[index].X, 2.0) +
						Math.Pow(points[index + 3].Y - points[index].Y, 2.0));
					int divVal = (int)(length / divisionLength);
					// 最小分割数に丸める
					if (divVal < minDivision) divVal = minDivision;
					// 倍率パラメータ
					float multVal = 1.0F / divVal;
					float m_value = 0.0F;
					// 配列を生成
					System.Drawing.PointF[] calcPt = new System.Drawing.PointF[divVal + 1];
					// [0]に先頭位置を入れる
					calcPt[0] = points[index];
					for (int d_index = 0; d_index < divVal; d_index++)
					{
						// 倍率
						m_value += multVal;
						// 分割ポイントの算出
						calcPt[d_index + 1] = CalcBezierDivisionPoint(
							points[index], points[index + 1], points[index + 2], points[index + 3], m_value);
					}
					// 結果を追加
					result.Add(calcPt);
				}
				return result;
			}
			return null;
		}
		#endregion [ベジェ関連]
	}
}

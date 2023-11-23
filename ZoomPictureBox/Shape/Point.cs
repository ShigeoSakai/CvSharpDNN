using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Drawing.Shape
{
	/// <summary>
	/// 点(基本図形を継承するだけ)
	/// </summary>
	[ShapeName("点"), DefaultProperty("Center")]
	public class Point :BaseShape
    {
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "点";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="center">座標</param>
		public Point(string name, System.Drawing.Point center):base(name)
        {
            Center = center;
        }
    }
}

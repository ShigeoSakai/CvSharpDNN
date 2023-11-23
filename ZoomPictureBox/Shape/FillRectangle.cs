using System.Drawing.Drawing2D;

namespace Drawing.Shape
{
    public class FillRectangle : Rectangle
    {
		/// <summary>
		/// 図形名
		/// </summary>
		/// <returns></returns>
		protected override string SHAPE_NAME() => "塗りつぶし矩形";

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="rectangle">四角形領域</param>
		public FillRectangle(string name, System.Drawing.Rectangle rectangle) : base(name, rectangle)
        {
            Fill = true;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="rectangle">四角形領域</param>
        /// <param name="fillColor">塗りつぶし色</param>
        public FillRectangle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color fillColor) : base(name,rectangle)
        {
            Fill = true;
            FillColor = fillColor;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="rectangle">四角形領域</param>
        /// <param name="fillColor">塗りつぶし色</param>
        /// <param name="color">描画色</param>
        /// <param name="lineWidth">ライン幅</param>
        /// <param name="lineStyle">線種</param>
        public FillRectangle(string name, System.Drawing.Rectangle rectangle, System.Drawing.Color fillColor, System.Drawing.Color color, 
            float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, rectangle, color, lineWidth, lineStyle)
        {
            Fill = true;
            FillColor = fillColor;
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="fillColor">塗りつぶし色</param>
        public FillRectangle(string name, int x, int y, int width, int height, System.Drawing.Color fillColor) : base(name,x,y,width,height)
        {
            Fill = true;
            FillColor = fillColor;
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
        public FillRectangle(string name, int x, int y, int width, int height, System.Drawing.Color fillColor,
			System.Drawing.Color color, float lineWidth = 1.0F, DashStyle lineStyle = DashStyle.Solid) : base(name, x,y,width,height,color,lineWidth,lineStyle)
        {
            Fill = true;
            FillColor = fillColor;
        }

    }
}

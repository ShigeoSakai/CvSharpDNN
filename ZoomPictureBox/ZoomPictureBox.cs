using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;

namespace Drawing
{
    public partial class ZoomPictureBox : PictureBox
    {
        //-------------------------------------------------
        // 定数値
        //-------------------------------------------------
		/// <summary>
		/// 倍率とグリッドサイズのクラス
		/// </summary>
		private class ZoomAndGrid
		{
			/// <summary>
			/// ズーム倍率
			/// </summary>
			public float ZoomFactor { get; private set; }
			/// <summary>
			/// グリッドのサイズ
			/// </summary>
			public float GridSize { get; private set; }
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="zoom">倍率</param>
			/// <param name="grid">グリッドサイズ</param>
			public ZoomAndGrid(float zoom,float grid)
			{
				ZoomFactor = zoom;
				GridSize = grid;
			}
		}
        /// <summary>
        /// 等倍のインデックス
        /// </summary>
        private const int ZOOM_1x_INDEX = 9;
        /// <summary>
        /// 0,5倍のインデックス
        /// </summary>
        private const int ZOOM_HALF_INDEX = 6;

		/// <summary>
		/// 倍率とグリッドサイズ
		/// </summary>
		private readonly ZoomAndGrid[] ZOOM_GRID = new ZoomAndGrid[]
		{
			new ZoomAndGrid(0.03125F,12.5F),
			new ZoomAndGrid(0.0625F, 12.5F),
			new ZoomAndGrid(0.125F,  12.5F),
			new ZoomAndGrid(0.25F,   12.5F),
			new ZoomAndGrid(0.3F,    15.0F),
			new ZoomAndGrid(0.4F,    8.0F ),
			new ZoomAndGrid(0.5F,    10.0F),
			new ZoomAndGrid(0.65F,   13.0F),
			new ZoomAndGrid(0.8F,    8.0F ),
			new ZoomAndGrid(1.0F,    10.0F),
			new ZoomAndGrid(1.25F,   12.5F),
			new ZoomAndGrid(1.5F,    7.5F ),
			new ZoomAndGrid(2.0F,    10.0F),
			new ZoomAndGrid(2.5F,    12.5F),
			new ZoomAndGrid(3.0F,    15.0F),
			new ZoomAndGrid(4.0F,    20.0F),
			new ZoomAndGrid(5.0F,    25.0F),
			new ZoomAndGrid(6.0F,    6.0F ),
			new ZoomAndGrid(8.0F,    8.0F ),
			new ZoomAndGrid(10.0F,   10.0F),
			new ZoomAndGrid(15.0F,   15.0F),
			new ZoomAndGrid(20.0F,   20.0F),
			new ZoomAndGrid(25.0F,   25.0F),
			new ZoomAndGrid(30.0F,   30.0F),
		};
        //-------------------------------------------------
        // パブリッククラス
        //-------------------------------------------------
        /// <summary>
        /// 画像情報クラス
        /// </summary>
        public class PIXEL_INFO
        {
            /// <summary>
            /// 座標
            /// </summary>
            public Point Point { get; }
            /// <summary>
            /// 色情報
            /// </summary>
            public Color Color { get; }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="pt">座標値</param>
            /// <param name="color">色情報</param>
            public PIXEL_INFO(Point pt, Color color)
            {
                Point = pt;
                Color = color;
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>変換後の文字列</returns>
            /// <remarks>
            /// "(x座標値,y座標値) R:R値 B:B値 G:G値"
            /// </remarks>
            public override string ToString()
            {
                return string.Format("({0,4},{1,4}) R:{2,3} G:{3,3} B:{4,3}", Point.X, Point.Y, Color.R, Color.G, Color.B);
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <param name="text">書式</param>
            /// <returns>変換後の文字列</returns>
            /// <remarks>
            /// 書式
            ///   "p","pt","point","a","ax","axis","xy" => "(x座標値,y座標値)"
            ///   "c","color","rgb"                     => "R:R値 B:B値 G:G値"
            ///   "x","ptx","pointx","pt_x","point_x"   => "X座標値"
            ///   "y","pty","pointy","pt_y","point_y"   => "Y座標値"
            ///   "r","red","rgb_r"                     => "R値"
            ///   "g","green","rgb_g"                   => "G値"
            ///   "b","blue","rgb_b"                    => "B値"
            ///   上記以外
            ///     {0} ... X座標値 ,{1} ... Y座標値 , {2} ... R値 , {3} ... G値 , {4} ... B値　でフォーマット
            ///   空文字列
            ///     "(x座標値,y座標値) R:R値 B:B値 G:G値"
            /// </remarks>
            public string ToString(string text)
            {
                string small = text.ToLower();
                if ((small == "p") || (small == "pt") || (small == "point") ||
                    (small == "a") || (small == "ax") || (small == "axis") ||
                    (small == "xy"))
                    return string.Format("({0,4},{1,4})", Point.X, Point.Y);
                else if ((small == "c") || (small == "color") ||
                    (small == "rgb"))
                    return string.Format("R:{0,3} G:{1,3} B:{2,3}", Color.R, Color.G, Color.B);
                else if ((small == "x") || (small == "ptx") || (small == "pointx") ||
                    (small == "pt_x") || (small == "point_x"))
                    return string.Format("{0,4}", Point.X);
                else if ((small == "y") || (small == "pty") || (small == "pointy") ||
                    (small == "pt_y") || (small == "point_y"))
                    return string.Format("{0,4}", Point.Y);
                else if ((small == "r") || (small == "red") || (small == "rgb_r"))
                    return string.Format("{0,3}", Color.R);
                else if ((small == "g") || (small == "green") || (small == "rgb_g"))
                    return string.Format("{0,3}", Color.G);
                else if ((small == "b") || (small == "blue") || (small == "rgb_b"))
                    return string.Format("{0,3}", Color.B);
                else if (text.Trim().Length > 0)
                    return string.Format(text, Point.X, Point.Y, Color.R, Color.G, Color.B);

                return string.Format("({0,4},{1,4}) R:{2,3} G:{3,3} B:{4,3}", Point.X, Point.Y, Color.R, Color.G, Color.B);
            }

        }

        /// <summary>
        /// 保存画像の種別
        /// </summary>
        public enum SAVE_IMAGE_TYPE
        {
            ORIGNAL,    // 元画像
            DISPLAY     // 表示画像
        }
        /// <summary>
        /// 表示ラベルの位置
        /// </summary>
        public enum INFO_LOCATION_DEF
        {
            [Description("左上")]
            TOP_LEFT = 0,
            [Description("中央上")]
            TOP_CENTER = 1,
            [Description("右上")]
            TOP_RIGHT = 2,
            [Description("左下")]
            BOTTOM_LEFT = 3,
            [Description("中央下")]
            BOTTOM_CENTER = 4,
            [Description("右下")]
            BOTTOM_RIGHT = 5
        }

        //-------------------------------------------------
        // イベント
        //-------------------------------------------------
        #region イベント
        /// <summary>
        /// 画素情報イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="info">画素情報</param>
        public delegate void PixelInfoEventHandler(object sender, PIXEL_INFO info);
        /// <summary>
        /// 画素情報イベント
        /// </summary>
        [Description("画像サイズ、倍率変更イベント")]
        public event PixelInfoEventHandler PixelInfoEvent;
        /// <summary>
        /// 画素情報更新
        /// </summary>
        /// <param name="info">画素情報</param>
        protected void OnPixelInfoEvent(PIXEL_INFO info)
        {
            PixelInfoEvent?.Invoke(this, info);
        }
        /// <summary>
        /// 画素情報更新
        /// </summary>
        /// <param name="point">座標</param>
		/// <param name="color">色情報</param>
        protected void OnPixelInfoEvent(Point point, Color color)
        {
            PixelInfoEvent?.Invoke(this, new PIXEL_INFO(point, color));
        }
        /// <summary>
        /// 画像サイズ、倍率変更イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="size">画像サイズ</param>
        /// <param name="ratio">倍率</param>
        public delegate void PictureSizeChageEventHandler(object sender, Size size, float ratio);
        /// <summary>
        /// 画像サイズ、倍率変更イベント
        /// </summary>
        [Description("画像サイズ、倍率変更イベント")]
        public event PictureSizeChageEventHandler PictureSizeChangeEvent;
        /// <summary>
        /// 画像サイズ、倍率変更
        /// </summary>
        /// <param name="size">画像サイズ</param>
        /// <param name="ratio">倍率</param>
        protected void OnPictureSizeChangeEvent(Size size, float ratio)
        {
            PictureSizeChangeEvent?.Invoke(this, size, ratio);
        }

        /// <summary>
        /// アフィン行列更新イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="matrix">アフィン行列</param>
        /// <param name="zoomIndex">ズーム倍率インデックス</param>
        /// <param name="dispRect">現在表示している実画像の領域</param>
        public delegate void MatrixUpdateEventHandler(object sender, Matrix matrix, int zoomIndex, Rectangle dispRect);
        /// <summary>
        /// アフィン行列更新イベント
        /// </summary>
        [Description("アフィン行列更新イベント")]
        public event MatrixUpdateEventHandler MatrixUpdateEvent;
        /// <summary>
        /// アフィン行列更新イベント
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="zoomIndex">ズーム倍率</param>
        protected void OnMatrixUpdateEvent(Matrix matrix, int zoomIndex)
        {
            MatrixUpdateEvent?.Invoke(this, matrix.Clone(), zoomIndex, affinMatrix_.GetRectangle(this.Size));
        }

        /// <summary>
        /// ファイル名取得イベントハンドラ
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="imageType">保存種別</param>
        /// <returns>ファイル名</returns>
        public delegate string GetFileNameEventHandler(object sender, SAVE_IMAGE_TYPE imageType);
        /// <summary>
        /// ファイル名取得イベント
        /// </summary>
        [Description("ファイル名取得イベント")]
        public event GetFileNameEventHandler GetFileNameEvent;
        /// <summary>
        /// ファイル名取得イベント
        /// </summary>
        /// <param name="imageType">保存種別</param>
        /// <returns>ファイル名</returns>
        protected string OnGetFileNameEvent(SAVE_IMAGE_TYPE imageType)
        {
            if (GetFileNameEvent != null)
                return GetFileNameEvent(this, imageType);
            return null;
        }
        #endregion
        //-------------------------------------------------
        // プロパティ
        //-------------------------------------------------
        #region プロパティ
        /// <summary>
        /// 画像イメージ
        /// </summary>
        private Image image_ = null;
        /// <summary>
        /// 画像(元を隠す)
        /// </summary>
        [Description("PictureBoxに表示されるイメージ")]
        public new Image Image
        {
            get { return image_; }
            set
            {
                if (image_ != null)
                {
                    Image.Dispose();
                    image_ = null;
                }
                image_ = value;
                ClearShape();
                // 最小倍率の更新
                UpdateMinZoomFactor(true);

                SizeModeChangeProcedure();
				// 描画処理
				if (autoRefresh_) Refresh();
			}
		}
        /// <summary>
        /// SizeModeに合わせて拡大・縮小倍率を操作
        /// </summary>
        private void SizeModeChangeProcedure()
        {
            if (image_ != null)
            {
				switch (imageSizeMode_)
				{
					case PictureBoxSizeMode.Normal:
                        // 等倍に変更
                        UpdateZoomFactor(ZOOM_1x_INDEX, new Point(0, 0), true, true);
						break;
					case PictureBoxSizeMode.AutoSize:
					case PictureBoxSizeMode.StretchImage:
					case PictureBoxSizeMode.Zoom:
						// 全部が表示できるサイズ
						UpdateZoomFactor(minZoomFactorIndex_, new Point(0, 0), true, true);
						break;
					case PictureBoxSizeMode.CenterImage:
						// 画像を中央に
						UpdateZoomFactor(ZOOM_1x_INDEX, new Point(this.ClientSize.Width/2, this.ClientSize.Height/2), true, true);
						break;

				}
			}
		}


        /// <summary>
        /// 画像のサイズモード
        /// </summary>
        private PictureBoxSizeMode imageSizeMode_ = PictureBoxSizeMode.Normal;
		public new PictureBoxSizeMode SizeMode 
        { 
            get { return imageSizeMode_; }
            set {  
                if (imageSizeMode_ != value) 
                { 
                    imageSizeMode_ = value;
                    // サイズモード変更
                    SizeModeChangeProcedure();
                    // 画像更新
					if (autoRefresh_) Refresh(); 
                } 
            }
        }


		/// <summary>
		/// アルファチャネルの使用
		/// </summary>
		private bool useAlfa_ = false;
        /// <summary>
        /// アルファチャネルの使用
        /// </summary>
        [Category("表示"), Description("アルファチャネルの使用"),DefaultValue(false)]
        public bool UseAlfa
        {
            get { return useAlfa_; }
            set
            {
                if (useAlfa_ != value)
                {
                    useAlfa_ = value;
                    // 画像再生成
                    initPictureBox();
                    // 描画処理
                    Refresh();
                }
            }
        }
        /// <summary>
        /// グリッド表示ありなし
        /// </summary>
        private bool showGrid_ = false;
        /// <summary>
        /// グリッド表示ありなし
        /// </summary>
        [Category("グリッド線"),Description("グリッド線の表示"), DefaultValue(false)]
        public bool ShowGrid
        {
            get { return showGrid_; }
            set
            {
                if (showGrid_ != value)
                {
                    showGrid_ = value;
                    // 再描画
                    Refresh();
                }
            }
        }
        /// <summary>
        /// グリッド色
        /// </summary>
        private Color gridColor_ = Color.Black;
        /// <summary>
        /// グリッド色
        /// </summary>
        [Category("グリッド線"), Description("グリッド線の色"), DefaultValue(typeof(Color), "Black")]
        public Color GridColor
        {
            get { return gridColor_; }
            set
            {
                if (gridColor_ != value)
                {
                    gridColor_ = value;
                    if (showGrid_)
                    {   // 再描画
                        Refresh();
                    }
                }
            }
        }
        /// <summary>
        /// 線の種類
        /// </summary>
        public DashStyle gridDashStyle_ = DashStyle.Solid;
        /// <summary>
        /// 線の種類
        /// </summary>
        [Category("グリッド線"), Description("グリッド線の種類"), DefaultValue(typeof(DashStyle), "Solid")]
        public DashStyle GridDashStyle
        {
            get { return gridDashStyle_; }
            set
            {
                if (gridDashStyle_ != value)
                {
                    gridDashStyle_ = value;
                    if (showGrid_)
                    {   // 再描画
                        Refresh();
                    }
                }
            }
        }
        /// <summary>
        /// アフィン行列更新イベント送信可否
        /// </summary>
        [Category("動作"), Description("アフィン行列更新イベント送信可否"), DefaultValue(false)]
        public bool SendUpdateMatrixEvent { get; set; } = false;


        /// <summary>
        /// 情報パネルの表示・非表示
        /// </summary>
        private bool showInfoPanel_ = false;
        /// <summary>
        /// 情報パネルの表示・非表示
        /// </summary>
        [Category("情報パネル"), Description("情報パネルの表示"), DefaultValue(false)]
        public bool ShowInfoPanel
        {
            get { return showInfoPanel_; }
            set
            {
                if (showInfoPanel_ != value)
                {
                    showInfoPanel_ = value;
                    LbInfo.Visible = value;
                }
            }
        }
        /// <summary>
        /// ShowInfoItem用プロパティコンバータ
        /// </summary>
        public class ShowInfoItemConverter : ExpandableObjectConverter
        {
            //コンバータがオブジェクトを指定した型に変換できるか
            //（変換できる時はTrueを返す）
            //ここでは、CustomClass型のオブジェクトには変換可能とする
            public override bool CanConvertTo(
                ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(ShowInfoItem))
                    return true;
                return base.CanConvertTo(context, destinationType);
            }

            //指定した値オブジェクトを、指定した型に変換する
            //CustomClass型のオブジェクトをString型に変換する方法を提供する
            public override object ConvertTo(ITypeDescriptorContext context,
                CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string) &&
                    value is ShowInfoItem)
                {
                    ShowInfoItem cc = (ShowInfoItem)value;
                    return cc.ToString();
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

            //コンバータが特定の型のオブジェクトをコンバータの型に変換できるか
            //（変換できる時はTrueを返す）
            //ここでは、String型のオブジェクトなら変換可能とする
            public override bool CanConvertFrom(
                ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                return base.CanConvertFrom(context, sourceType);
            }

            //指定した値をコンバータの型に変換する
            //String型のオブジェクトをCustomClass型に変換する方法を提供する
            public override object ConvertFrom(ITypeDescriptorContext context,
                CultureInfo culture, object value)
            {
                if (value is string)
                {
                    string[] ss = value.ToString().Split(new char[] { ',' }, 4);
                    ShowInfoItem cc = new ShowInfoItem(ss);
                    return cc;
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        /// <summary>
        /// ShowInfoItemプロパティ
        /// </summary>
        [TypeConverter(typeof(ShowInfoItemConverter))]
        public class ShowInfoItem
        {
            [Description("PictureBoxの座標"), DefaultValue(false)]
            public bool Coordinate { get; set; } = false;
            [Description("画像の座標"), DefaultValue(true)]
            public bool ImageCoordinate { get; set; } = true;
            [Description("色情報"), DefaultValue(true)]
            public bool PixelInfo { get; set; } = true;
            [Description("ズーム倍率"), DefaultValue(true)]
            public bool ZoomRatio { get; set; } = true;

            /// <summary>
            /// 文字列をBoolに変換
            /// </summary>
            /// <param name="text">文字列</param>
            /// <returns>変換したbool値。変換できない場合がfalse</returns>
            private bool stringToBool(string text)
            {
                if (bool.TryParse(text, out bool result))
                    return result;
                return false;
            }
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public ShowInfoItem() { }
            /// <summary>
            /// コンストラクタ(文字列配列指定)
            /// </summary>
            /// <param name="list">各項目のbool文字列</param>
            /// <remarks>
            /// index=0 ... PictureBoxの座標表示
            /// index=1 ... 画像の座標表示
            /// index=2 ... 色情報表示
            /// index=3 ... ズーム倍率表示
            /// </remarks>
            public ShowInfoItem(string[] list)
            {
                if (list.Length > 0)
                    Coordinate = stringToBool(list[0]);
                if (list.Length > 1)
                    ImageCoordinate = stringToBool(list[1]);
                if (list.Length > 2)
                    PixelInfo = stringToBool(list[2]);
                if (list.Length > 3)
                    ZoomRatio = stringToBool(list[3]);
            }
            /// <summary>
            /// 文字列変換
            /// </summary>
            /// <returns>文字列</returns>
            public override string ToString()
            {
                return Coordinate + "," + ImageCoordinate + "," +
                    PixelInfo + "," + ZoomRatio;
            }
        }

        /// <summary>
        /// 情報表示プロパティ
        /// </summary>
        [Category("情報パネル"), Description("情報パネルの表示内容"),DefaultValue(typeof(ShowInfoItem),"False,True,True,True")]
        public ShowInfoItem InfoItem { get; set; } = new ShowInfoItem();

        /// <summary>
        /// 表示ラベルの位置
        /// </summary>
        private INFO_LOCATION_DEF infoLocaltion_ = INFO_LOCATION_DEF.TOP_LEFT;
        /// <summary>
        /// 表示ラベルの位置
        /// </summary>
        [Category("情報パネル"), Description("情報パネルの表示位置"), DefaultValue(typeof(INFO_LOCATION_DEF), "TOP_LEFT")]
        public INFO_LOCATION_DEF InfoLocation
        {
            get { return infoLocaltion_; }
            set
            {
                if (infoLocaltion_ != value)
                {
                    infoLocaltion_ = value;
                    // 表示位置の更新
                    UpdateInfoLocation();
                }
            }
        }
		/// <summary>
		/// 自動更新
		/// </summary>
		private bool autoRefresh_ = true;
		public bool AutoRefresh
		{
			get { return autoRefresh_; }
			set
			{
				if ((autoRefresh_ != value) && (value))
					Refresh();
				autoRefresh_ = value;
			}
		}
        #endregion

        //-------------------------------------------------
        // ローカルクラス
        //-------------------------------------------------
        #region アフィン行列クラス
        /// <summary>
        /// アフィン行列クラス
        /// </summary>
        private class AffinMatrixClass
        {
            /// <summary>
            /// アフィン行列更新イベントハンドラ
            /// </summary>
            /// <param name="sender">送信元</param>
            /// <param name="matrix">アフィン行列</param>
            public delegate void UpdateMatrixEventHandler(object sender, Matrix matrix);
            /// <summary>
            /// アフィン行列更新イベント
            /// </summary>
            public event UpdateMatrixEventHandler UpdateMatrixEvent;
            /// <summary>
            /// アフィン行列更新イベント
            /// </summary>
            /// <param name="matrix">アフィン行列</param>
            protected void OnUpdateMatrixEvent(Matrix matrix)
            {
                UpdateMatrixEvent?.Invoke(this,matrix.Clone());
            }
            /// <summary>
            /// アフィン行列
            /// </summary>
            private Matrix mat_ = new Matrix();

            /// <summary>
            /// 逆行列の取得
            /// </summary>
            public Matrix InvMatrix { get; private set; } = new Matrix();

            /// <summary>
            /// アフィン行列設定
            /// </summary>
            /// <param name="matrix">アフィン行列</param>
            public void SetMatrix(Matrix matrix)
            {
                mat_.Dispose();
                mat_ = matrix.Clone();
                // 逆行列
                InvMatrix.Dispose();
                InvMatrix = matrix.Clone();
                InvMatrix.Invert();
                // 行列更新イベント通知
                OnUpdateMatrixEvent(mat_);
            }
            /// <summary>
            /// ピクチャボックスのサイズが実画像のどの領域になるか
            /// </summary>
            /// <param name="pictBoxSize">ピクチャボックスのサイズ</param>
            /// <returns>実画像の領域</returns>
            /// <remarks>正行列変換</remarks>
            public Rectangle GetRectangle(Size pictBoxSize)
            {
                // 変換元座標(原点～PictureBoxのサイズ)
                Point[] pts = { new Point(0, 0), new Point(pictBoxSize.Width, pictBoxSize.Height) };
                // 座標変換(正行列)
                mat_.TransformPoints(pts);
                // 結果領域を返す
                return new Rectangle(pts[0].X, pts[0].Y, pts[1].X - pts[0].X, pts[1].Y - pts[0].Y);
            }
            /// <summary>
            /// ピクチャボックスの領域が実画像のどの領域になるか
            /// </summary>
            /// <param name="rectangle">ピクチャボックスの領域</param>
            /// <param name="imageSize">画像サイズ</param>
            /// <returns>実画像の領域</returns>
            /// <remarks>正行列変換</remarks>
            public Rectangle GetRectangle(Rectangle rectangle,Size imageSize)
            {
                // 変換元座標(原点～PictureBoxのサイズ)
                PointF[] ptsF = { new PointF(rectangle.X, rectangle.Y),
                    new PointF(rectangle.Width + rectangle.X, rectangle.Height + rectangle.Y) };
                // 座標変換(正行列)
                mat_.TransformPoints(ptsF);

                int x = (int)(ptsF[0].X + 0.5F);
                int y = (int)(ptsF[0].Y + 0.5F);
                int width = (int)Math.Ceiling(ptsF[1].X - ptsF[0].X);
                int height = (int)Math.Ceiling(ptsF[1].Y - ptsF[0].Y);
                if (x + width >= imageSize.Width)
                    x = imageSize.Width - width;
                if (x < 0)
                    x = 0;
                if (y + height >= imageSize.Height)
                    y = imageSize.Height - height;
                if (y < 0)
                    y = 0;
                // 結果領域を返す
                return new Rectangle(x,y,width,height);
            }
            /// <summary>
            /// 実画像座標→PictBox画像座標変換
            /// </summary>
            /// <param name="rectangle">実画像領域</param>
            /// <param name="pictBoxSize">ピクチャボックスのサイズ</param>
            /// <returns>ピクチャボックスの領域</returns>
            /// <remarks>
            ///逆変換を用いて、実画像座標→PictBox画像座標に変換する。
            /// </remarks>
            public Rectangle GetInvRectangle(Rectangle rectangle, Size pictBoxSize)
            {
                PointF[] ptsF = { new PointF(rectangle.X, rectangle.Y),
                    new PointF(rectangle.Width + rectangle.X, rectangle.Height + rectangle.Y) };
                // 座標変換(逆行列)
                InvMatrix.TransformPoints(ptsF);
                int x = (int)(ptsF[0].X + 0.5F);
                int y = (int)(ptsF[0].Y + 0.5F);
                int width = (int)Math.Ceiling(ptsF[1].X - ptsF[0].X);
                int height = (int)Math.Ceiling(ptsF[1].Y - ptsF[0].Y);
                if (x < 0)
                {
                    width += x;
                    x = 0;
                }
                if (x + width >= pictBoxSize.Width)
                    width = pictBoxSize.Width - x;
                if (y < 0)
                {
                    height += y;
                    y = 0;
                }
                if (y + height >= pictBoxSize.Height)
                    height = pictBoxSize.Height - y;

                // 結果領域を返す
                return new Rectangle(x, y, width, height);
            }
            /// <summary>
            /// 画面上の座標から実画像座標への変換
            /// </summary>
            /// <param name="point">ピクチャボックス座標</param>
            /// <returns>実画像座標</returns>
            public Point GetPoint(Point point)
            {
                Point[] pts = { new Point(point.X, point.Y) };
                // 正行列で変換
                mat_.TransformPoints(pts);
                return pts[0];
            }
			/// <summary>
			/// 画面上の座標(x1,y1)と(x2,y2)の差を算出
			/// (x2,y2)を基準としたオフセット値
			/// </summary>
			/// <param name="x1"></param>
			/// <param name="y1"></param>
			/// <param name="x2">基準X座標</param>
			/// <param name="y2">基準y座標</param>
			/// <returns>(x2,y2)を基準としたオフセット値</returns>
			public Point GetOffset(int x1,int y1,int x2,int y2)
			{
				Point[] pts = { new Point(x1,y1),new Point(x2,y2) };
				// 正行列で変換
				mat_.TransformPoints(pts);
				return new Point(pts[0].X - pts[1].X, pts[0].Y - pts[1].Y);
			}
			/// <summary>
			/// 画面上の座標newPtとoldPtの差を算出
			/// oldPtを基準としたオフセット値
			/// </summary>
			/// <param name="newPt"></param>
			/// <param name="oldPt">基準座標</param>
			/// <returns>oldPtを基準としたオフセット値</returns>
			public Point GetOffset(Point newPt, Point oldPt)
			{
				return GetOffset(newPt.X, newPt.Y, oldPt.X, oldPt.Y);
			}
			/// <summary>
			/// 実画像座標→PictBox画像座標変換
			/// </summary>
			/// <param name="point">実画像座標</param>
			/// <returns>PictBox画像座標変換</returns>
			public Point GetInvPoint(Point point)
			{
				Point[] pts = { new Point(point.X, point.Y) };
				// 逆行列で変換
				InvMatrix.TransformPoints(pts);
				return pts[0];
			}
			/// <summary>
			/// 実画像座標→PictBox画像座標変換
			/// </summary>
			/// <param name="point">実画像座標</param>
			/// <returns>PictBox画像座標変換</returns>
			public PointF GetInvPoint(PointF point)
			{
				PointF[] pts = { new PointF(point.X, point.Y) };
				// 逆行列で変換
				InvMatrix.TransformPoints(pts);
				return pts[0];
			}
			/// <summary>
			/// 画面上の座標から実画像座標への変換(ベクタ)
			/// </summary>
			/// <param name="point">ピクチャボックス座標(ベクタ</param>
			/// <returns>実画像座標(ベクタ</returns>
			public Point GetVector(Point point)
            {
                Point[] pts = { new Point(point.X, point.Y) };
                // 正行列で変換
                mat_.TransformVectors(pts);
                return pts[0];
            }

            /// <summary>
            /// ズーム倍率の設定
            /// </summary>
            /// <param name="zoomFactor">ズーム倍率(!=0)</param>
            /// <param name="center">中心座標(画面系)</param>
            /// <param name="pictBoxSize">ピクチャボックスのサイズ</param>
            /// <param name="imageSize">画像サイズ</param>
            public void SetZoomFactor(float zoomFactor,Point center,Size pictBoxSize, Size imageSize)
            {
                float newZoomFactor = 1.0F / zoomFactor;
                float oldZoomFactor = mat_.Elements[0];
                // オフセットを算出
                float offsetX = (oldZoomFactor - newZoomFactor) * center.X + mat_.Elements[4] + newZoomFactor / 2.0F;
                float offsetY = (oldZoomFactor - newZoomFactor) * center.Y + mat_.Elements[5] + newZoomFactor / 2.0F;
                if (oldZoomFactor == newZoomFactor)
                {
                    offsetX = center.X;
                    offsetY = center.Y;
				}


                float newBoxSizeW = newZoomFactor * pictBoxSize.Width;
                float newBoxSizeH = newZoomFactor * pictBoxSize.Height;
                // オフセット値の補正(X軸)
                if (offsetX + newBoxSizeW > imageSize.Width)
                    offsetX = imageSize.Width - newBoxSizeW;
                if (offsetX < 0)
                    offsetX = 0;
                // オフセット値の補正(Y軸)
                if (offsetY + newBoxSizeH > imageSize.Height)
                    offsetY = imageSize.Height - newBoxSizeH;
                if (offsetY < 0)
                    offsetY = 0;
                // 新しい変換マトリックス
                Matrix newMat = new Matrix(newZoomFactor, 0, 0, newZoomFactor,
                    offsetX, offsetY);
                // アフィン行列を入れ替え
                mat_.Dispose();
                mat_ = newMat;
                // 逆行列
                InvMatrix.Dispose();
                InvMatrix = mat_.Clone();
                InvMatrix.Invert();
                // 行列更新イベント通知
                OnUpdateMatrixEvent(mat_);
            }
            /// <summary>
            /// 移動
            /// </summary>
            /// <param name="offsetX">X移動量(画面座標系)</param>
            /// <param name="offsetY">Y移動量(画面座標系)</param>
            /// <param name="pictBoxSize">ピクチャボックスのサイズ</param>
            /// <param name="imageSize">画像サイズ</param>
            public void Move(int offsetX,int offsetY,Size pictBoxSize,Size imageSize)
            {
                PointF[] pts = { new PointF(offsetX, offsetY),
                    new PointF(pictBoxSize.Width,pictBoxSize.Height) };
                mat_.TransformVectors(pts);
                // 移動量制限(画像サイズ枠外 X軸)
                float newOffsetX = mat_.Elements[4] - pts[0].X;
                if (newOffsetX + pts[1].X > imageSize.Width)
                    newOffsetX = imageSize.Width - pts[1].X;
                if (newOffsetX < 0)
                    newOffsetX = 0;
                // 移動量制限(画像サイズ枠外 Y軸)
                float newOffsetY = mat_.Elements[5] - pts[0].Y;
                if (newOffsetY + pts[1].Y > imageSize.Height)
                    newOffsetY = imageSize.Height - pts[1].Y;
                if (newOffsetY < 0)
                    newOffsetY = 0;

                // 新しい変換マトリックス
                Matrix newMat = new Matrix(mat_.Elements[0], mat_.Elements[1], mat_.Elements[2], mat_.Elements[3],
                    newOffsetX, newOffsetY);
                // アフィン行列を入れ替え
                mat_.Dispose();
                mat_ = newMat;
                // 逆行列
                InvMatrix.Dispose();
                InvMatrix = mat_.Clone();
                InvMatrix.Invert();
                // 行列更新イベント通知
                OnUpdateMatrixEvent(mat_);
            }
            /// <summary>
            /// 文字列変換(Matrixを表示)
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("Elm[0] = {0}, Elm[1] = {1}, Elm[2] = {2}, Elm[3] = {3}, Elm[4] = {4}, Elm[5] = {5}",
                    mat_.Elements[0], mat_.Elements[1], mat_.Elements[2], mat_.Elements[3], mat_.Elements[4], mat_.Elements[5]);
            }
            /// <summary>
            /// 指定行列を文字列変換
            /// </summary>
            /// <param name="mat">アフィン行列</param>
            /// <returns></returns>
            public string MatToString(Matrix mat)
            {
                return string.Format("Elm[0] = {0}, Elm[1] = {1}, Elm[2] = {2}, Elm[3] = {3}, Elm[4] = {4}, Elm[5] = {5}",
                    mat.Elements[0], mat.Elements[1], mat.Elements[2], mat.Elements[3], mat.Elements[4], mat.Elements[5]);
            }
        }
        #endregion

        //-------------------------------------------------
        // ローカル変数
        //-------------------------------------------------

        /// <summary>
        /// 描画グラフィック
        /// </summary>
        private Graphics graphics_ = null;

        /// </summary>
        /// アフィン行列
        /// </summary>
        AffinMatrixClass affinMatrix_ = new AffinMatrixClass();
        /// <summary>
        /// 倍率
        /// </summary>
        private float zoomFactor_ = 1.0F;
        /// <summary>
        /// 倍率インデックス(default 1.0F)
        /// </summary>
        private int zoomFactorIndex_ = ZOOM_1x_INDEX;
        /// <summary>
        /// 最小倍率
        /// </summary>
        private float minZoomFactor_ = 0.5F;
        /// <summary>
        /// 最小倍率インデックス(default 0.5F)
        /// </summary>
        private int minZoomFactorIndex_ = ZOOM_HALF_INDEX;

        /// <summary>
        /// マウスボタンの押下位置
        /// </summary>
        private Point oldPoint_;
        /// <summary>
        /// マウス移動中
        /// </summary>
        private bool isMouseMoving_ = false;

        /// <summary>
        /// 画像サイズ、倍率変更
        /// </summary>
        /// <param name="ratio">倍率</param>
        protected void OnPictureSizeChange(float ratio)
        {
            // 表示する画像サイズを取得
            Size size = new Size(0, 0);
            if (image_ != null)
                size = image_.Size;
            PictureSizeChangeEvent?.Invoke(this, size, ratio);
        }

        //-------------------------------------------------
        // パブリックメソッド
        //-------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZoomPictureBox()
        {
            InitializeComponent();

            // PictureBoxの初期化
            initPictureBox();
            // アフィン行列更新イベントハンドラ登録
            affinMatrix_.UpdateMatrixEvent += AffinMatrix_UpdateMatrixEvent;
            //情報表示パネル初期化
            InitInfoPanel();
        }

        /// <summary>
        /// アフィン行列設定
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="zoomIndex">ズーム倍率</param>
        public void SetMatrix(Matrix matrix, int zoomIndex)
        {
            bool isZoomUpdate = false;
            // ズーム倍率更新
            if (zoomFactorIndex_ != zoomIndex)
            {
                if (zoomIndex < minZoomFactorIndex_)
                    zoomFactorIndex_ = minZoomFactorIndex_;
                else
                    zoomFactorIndex_ = zoomIndex;
                if (ZOOM_GRID[zoomFactorIndex_].ZoomFactor < minZoomFactor_)
                    zoomFactor_ = minZoomFactor_;
                else
                    zoomFactor_ = ZOOM_GRID[zoomFactorIndex_].ZoomFactor;
                isZoomUpdate = true;
            }
            // アフィン行列更新
            affinMatrix_.SetMatrix(matrix);
			// 再描画
			if (autoRefresh_) Refresh();

			// ズーム倍率更新通知
			if (isZoomUpdate)
                OnPictureSizeChange(zoomFactor_);
        }

        /// <summary>
        /// メモリから画像を設定
        /// </summary>
        /// <param name="imgPtr">画像のメモリ</param>
        /// <param name="width">画像幅</param>
        /// <param name="height">画像高さ</param>
        /// <param name="ch">チャネル数(1,3,4)</param>
        /// <param name="UseDefaultPalette">デフォルトパレットを使用するか</param>
        /// <param name="color_map">カラーマップ</param>
        /// <remarks>
        /// チャネル数が1(8bit Indexed)の場合
        ///    UseDefaultPalette = true   ... デフォルトのカラーパレットを使用
        ///    UseDefaultPalette = false
        ///         color_map = null      ... グレイスケールのカラーパレットを使用
        ///         color_mapがnull以外   ... 指定のカラーマップを使用
        /// </remarks>
        public void SetImage(IntPtr imgPtr, int width, int height, int ch,bool UseDefaultPalette = true, ColorMap.ColorMapClass color_map = null)
        {
            // ビットマップを生成
            PixelFormat pixelFormat = PixelFormat.Format8bppIndexed;
            int onePixel = 1;
            if (ch == 3)
            {
                onePixel = 3;
                pixelFormat = PixelFormat.Format24bppRgb;
            }
            else if (ch == 4)
            {
                onePixel = 4;
                pixelFormat = PixelFormat.Format32bppArgb;
            }
            Bitmap bmp = new Bitmap(width, height, pixelFormat);
            if ((pixelFormat == PixelFormat.Format8bppIndexed) && (UseDefaultPalette == false))
            {   // カラーパレットを作成(グレースケール)
                ColorPalette pal = bmp.Palette;
                if (color_map == null)
                {
                    color_map = new ColorMap.Sequential2.GrayColorMap();
                }
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = color_map.Get(i);
                }
                bmp.Palette = pal;
            }

            // ビットマップデータをロック
            BitmapData bd = bmp.LockBits(
                       new Rectangle(0, 0, bmp.Width, bmp.Height),
                       ImageLockMode.WriteOnly,
                       pixelFormat);
            int stride = bd.Stride;

            IntPtr oneLine = imgPtr;
            IntPtr dest = bd.Scan0;
            byte[] tmpBuf = new byte[width * onePixel];
            // 1ラインずつコピー
            for (int h = 0; h < height; h++)
            {
                Marshal.Copy(oneLine, tmpBuf, 0, width * onePixel);
                Marshal.Copy(tmpBuf, 0, dest, width * onePixel);
                dest += stride;
                oneLine += (width * onePixel);
            }
            // ビットマップデータをロック解除
            bmp.UnlockBits(bd);

            // 画像に設定
            if (image_ != null)
            {
                image_.Dispose();
                image_ = null;
            }
            image_ = bmp;
            // 最小倍率の更新
            UpdateMinZoomFactor(true);
			// 描画処理
			if (autoRefresh_) Refresh();
		}

		/// <summary>
		/// 画像をファイルに保存する
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public bool Save(string filename)
        {
            if (image_ != null)
            {
                try
                {
                    image_.Save(filename);
                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }
        /// <summary>
        /// 表示する範囲を設定
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool SetDispRect(Rectangle rect)
        {
            // 倍率を計算
            float x_ratio = (float)this.Width / (float)rect.Width;
            float y_ratio = (float)this.Height / (float)rect.Height;
            // 小さい方を採用
            float ratio = x_ratio;
            if (ratio > y_ratio)
                ratio = y_ratio;
            // 近い倍率を検索
            int resultIndex = -1;
            for(int index = 1; index < ZOOM_GRID.Length; index ++)
            {
                if ((ZOOM_GRID[index -1].ZoomFactor <= ratio ) && (ZOOM_GRID[index].ZoomFactor > ratio))
                {   // 近い方
                    if (Math.Abs(ZOOM_GRID[index - 1].ZoomFactor - ratio) < Math.Abs(ZOOM_GRID[index].ZoomFactor - ratio))
                        resultIndex = index - 1;
                    else
                        resultIndex = index;
                    break;
                }
            }
            // 最大以上なら、最大のindex
            if ((resultIndex == -1) && (ZOOM_GRID.Last().ZoomFactor <= ratio))
                resultIndex = ZOOM_GRID.Length - 1;
            // ズーム倍率が見つかったら
            if (resultIndex >= 0)
            {
                if (resultIndex <= minZoomFactorIndex_)
                {
                    zoomFactor_ = minZoomFactor_;
                    zoomFactorIndex_ = minZoomFactorIndex_;
                }
                else
                {
                    zoomFactorIndex_ = resultIndex;
                    zoomFactor_ = ZOOM_GRID[zoomFactorIndex_].ZoomFactor;
                }
                // アフィン行列を設定
                Matrix mat = new Matrix(1.0F/zoomFactor_, 0, 0, 1.0F / zoomFactor_, (float)rect.X, (float)rect.Y);
                affinMatrix_.SetMatrix(mat);

				// 再描画
				Refresh();
                return true;
            }
            return false;
        }
		/// <summary>
		/// 指定された画像上の座標を中央に表示する
		/// </summary>
		/// <param name="point">画像上の座標</param>
		/// <returns></returns>
		public bool SetCenter(Point point)
		{
			if (image_ != null)
			{
				// 表示上の座標
				Point dispPt = affinMatrix_.GetInvPoint(point);
				// 移動量を算出
				int offsetX = 0, offsetY = 0;
				int nowCenterX = this.Width / 2;
				int nowCenterY = this.Height / 2;
				offsetX = nowCenterX - dispPt.X;
				offsetY = nowCenterY - dispPt.Y;
				// 変換マトリックスを平行移動
				affinMatrix_.Move(offsetX, offsetY, this.Size, image_.Size);
				// 再描画
				Refresh();
				return true;
			}
			return false;
		}
		/// <summary>
		/// 指定された画像上の領域を中央に表示する
		/// </summary>
		/// <param name="rect">画像上の領域</param>
		/// <returns></returns>
		public bool SetCenter(Rectangle rect)
		{
			if (image_ != null)
			{
				// 現在表示している領域
				Rectangle dispRect = affinMatrix_.GetRectangle(this.Size);
				if ((dispRect.Width < rect.Width) || (dispRect.Height < rect.Height))
				{   // 現在表示している領域の方が小さい ⇒ 表示領域を拡張
					return SetDispRect(rect);
				}
				else
				{   // 中心座標を求めて平行移動
					return SetCenter(new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2));
				}
			}
			return false;
		}
        //-------------------------------------------------
        // イベントハンドラ
        //-------------------------------------------------

        /// <summary>
        /// サイズ変更イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            // ピクチャボックスの初期化
            initPictureBox();
            // 最小倍率の更新
            UpdateMinZoomFactor(true);
            // 表示位置の更新
            UpdateInfoLocation();
            // 描画
            Refresh();
        }

        #region マウスイベント
        /// <summary>
        /// マウスホィールイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // ビットマップが未設定の場合戻る
            if (image_ == null) return;

            if (e.Delta > 0)
                UpdateZoomFactor(zoomFactorIndex_ + 1, e.Location, true);
            else if (e.Delta < 0)
                UpdateZoomFactor(zoomFactorIndex_ - 1, e.Location, true);
            // 画像の描画  
            Refresh();
            // 情報更新
            infoUpdate(e.Location);
        }
        /// <summary>
        /// 当たり図形
        /// </summary>
        private Shape.BaseShape shape;
		/// <summary>
		/// 当たり図形のアンカー位置(-1)は全体
		/// </summary>
		private int anchorPoint;
        /// <summary>
        /// マウスダウンイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // ビットマップが未設定の場合戻る
            if (image_ == null) return;
            // マウスをクリックした位置の記録  
            oldPoint_.X = e.X;
            oldPoint_.Y = e.Y;

			bool isRefresh = false;
			// 当たり判定
			Shape.BaseShape nowShape =
				HitTest(graphics_, affinMatrix_.InvMatrix, this.Size, e.Location,out Shape.BaseShape.HIT_KIND kind,out int anchor);

			if ((shape != null) && ((nowShape == null) || (shape.Equals(nowShape) == false)))
			{	// 選択図形が変わったので...
				shape.Select(false);
				anchorPoint = -1;
				isRefresh = true;
			}
			// 選択中の図形
			shape = nowShape;
			if (shape != null)
			{
				shape.Select(true);
				if (kind == Shape.BaseShape.HIT_KIND.ANCHOR)
					anchorPoint = anchor;
				else
					anchorPoint = -1;
				isRefresh = true;
			}
			if (e.Button == MouseButtons.Left)
			{
				// マウス移動フラグを立てる  
				isMouseMoving_ = true;
				// 情報更新
				infoUpdate(e.Location);
			}
			// 再描画
			if (isRefresh)
				Refresh();
        }
        /// <summary>
        /// マウスアップイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // マウス移動フラグを解放  
            isMouseMoving_ = false;
			if (e.Button == MouseButtons.Left)
			{	// 左ボタンは何もしない
				if (shape != null)
				{
					//shape.Select(false);
					//Refresh();
					//shape = null;
					//anchorPoint = -1;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{	// 右ボタンはメニューを表示
				if ((shape != null) && (shape.SetMenuEnable(anchorPoint,shapeDictionary_.Count-1)))
				{   // 図形のメニューを表示
					Point imagePt = affinMatrix_.GetPoint(oldPoint_);
					shape.ShowMenu(this, oldPoint_, imagePt, anchorPoint);
				}
				else
				{	// 通常のメニューを表示
					CtxMenuStrip.Show(this, oldPoint_);
				}
			}
			// 情報更新
			infoUpdate(e.Location);
        }
        /// <summary>
        /// マウス移動イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // ビットマップが未設定の場合戻る
            if (image_ == null) return;

            // マウスをクリックしながら移動中のとき  
            if (isMouseMoving_)
            {
                // 以前の座標から移動量を算出
                int xx = e.X - oldPoint_.X;
                int yy = e.Y - oldPoint_.Y;

                // 拡大縮小倍率以下は移動しない
                if ((Math.Abs(xx / zoomFactor_) >= 1.0F) || (Math.Abs(yy / zoomFactor_) >= 1.0F))
                {
                    int mx = (int)((int)(xx / zoomFactor_) * zoomFactor_);
                    int my = (int)((int)(yy / zoomFactor_) * zoomFactor_);
					// 座標位置
					PointF newPos = new PointF(e.X, e.Y);
                    if (shape != null)
                    {   // 図形選択時は図形を移動
						Point move = affinMatrix_.GetOffset(e.X, e.Y, oldPoint_.X, oldPoint_.Y);
						if (anchorPoint == -1)
						{
							if (shape.Move(move.X, move.Y, image_.Size))
							{   // 図形移動イベント
								OnMoveShapeEvent(shape.Name, shape.GetObject());
							}
						}
						else
						{	// アンカーを移動
							if (shape.MoveAnchor(move.X, move.Y, anchorPoint, image_.Size))
							{
								// 移動後の座標値
								newPos = affinMatrix_.GetInvPoint(shape[anchorPoint]);
								// 図形移動イベント
								OnMoveShapeEvent(shape.Name, shape.GetObject());
							}
						}
					}
                    else
                    {   // 画像の移動  
                        affinMatrix_.Move(mx, my, this.Size, image_.Size);
                    }
                    // 画像の描画  
                    Refresh();

                    // ポインタ位置の保持  
                    oldPoint_.X = (int)newPos.X;
                    oldPoint_.Y = (int)newPos.Y;
                }
            }
            // 情報更新
            infoUpdate(e.Location);
        }
        #endregion

        #region メニューイベント
        /// <summary>
        /// 元画像を保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMenuItemOrigSave_Click(object sender, System.EventArgs e)
        {
            if (image_ != null)
            {
                string filename = OnGetFileNameEvent(SAVE_IMAGE_TYPE.ORIGNAL);
                if (filename == null)
                {   // ファイル保存ダイアログを開く
                    filename = DialogGetSaveFileName(SAVE_IMAGE_TYPE.ORIGNAL);
                }
                if (filename != null)
                {   // ファイルを保存する
                    try
                    {
                        image_.Save(filename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            string.Format("ファイル'{0}'に保存できません。\n{1}", filename, ex.ToString()),
                            "ファイル保存エラー",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// 表示画像を保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMenuItemImageSave_Click(object sender, System.EventArgs e)
        {
            if (base.Image != null)
            {
                string filename = OnGetFileNameEvent(SAVE_IMAGE_TYPE.DISPLAY);
                if (filename == null)
                {   // ファイル保存ダイアログを開く
                    filename = DialogGetSaveFileName(SAVE_IMAGE_TYPE.DISPLAY);
                }
                if (filename != null)
                {   // ファイルを保存する
                    string saveFileName = Path.Combine(Path.GetDirectoryName(filename),
                        Path.GetFileNameWithoutExtension(filename) + "_clip" + Path.GetExtension(filename));
                    try
                    {
                        base.Image.Save(saveFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            string.Format("ファイル'{0}'に保存できません。\n{1}", saveFileName, ex.ToString()),
                            "ファイル保存エラー",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 元画像をクリップボードコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMenuItemOrigCopy_Click(object sender, System.EventArgs e)
        {
            // 元画像をクリップボードにコピー
            if (image_ != null)
            {
                Clipboard.SetImage(image_);
            }
        }
        /// <summary>
        /// 表示画像をクリップボードにコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMenuItemImageCopy_Click(object sender, System.EventArgs e)
        {
            if (base.Image != null)
            {
                Clipboard.SetImage(base.Image);
            }
        }
        #endregion

        /// <summary>
        /// アフィン行列更新イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="matrix"></param>
        private void AffinMatrix_UpdateMatrixEvent(object sender, Matrix matrix)
        {
            if (SendUpdateMatrixEvent)
                OnMatrixUpdateEvent(matrix, zoomFactorIndex_);
        }

        //-------------------------------------------------
        // ローカルメソッド
        //-------------------------------------------------

        /// <summary>
        /// PictureBoxの初期化
        /// </summary>
        /// <remarks>
        /// 自分のサイズと同じサイズのBitmap画像を用意
        /// グラフィックオブジェクトを設定
        /// </remarks>
        private void initPictureBox()
        {
            // 自分のサイズと同じサイズビットマップを作成
            if (base.Image != null)
            {
                base.Image.Dispose();
                base.Image = null;
            }
            if (useAlfa_)
                base.Image = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            else
                base.Image = new Bitmap(this.Width, this.Height, PixelFormat.Format24bppRgb);
            // グラフィックスを生成
            if (graphics_ != null)
            {
                graphics_.Dispose();
                graphics_ = null;
            }
            graphics_ = Graphics.FromImage(base.Image);
            // 補間モードの設定（NearestNeighbor）  
            graphics_.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

		/// <summary>
		/// リフレッシュ（描画処理）
		/// </summary>
		public override void Refresh()
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate () { Refresh(); });
				return;
			}
			// ピクチャボックスを背景色でクリア  
			graphics_.Clear(this.BackColor);

			if (image_ != null)
			{
				Point offsetPt = new Point(0, 0);
				// 描画先(ピクチャボックスのサイズ)
				Rectangle dstRect = new Rectangle(new Point(0, 0), this.Size);
				// 描画元
				Rectangle srcRect = affinMatrix_.GetRectangle(dstRect, image_.Size);

				if (zoomFactor_ >= 5.0F)
				{
					// 5倍以上は、画素単位でコピー
					CopyImage((Bitmap)base.Image, (Bitmap)image_, dstRect, srcRect, (int)zoomFactor_, ref offsetPt);
				}
				else
				{
					// ピクチャボックスを背景色でクリア  
					graphics_.Clear(this.BackColor);
					// 描画
					graphics_.DrawImage(image_, dstRect, srcRect, GraphicsUnit.Pixel);
				}
				// グリッド表示
				if (showGrid_)
				{
					Pen linePen = new Pen(gridColor_, 1.0F);
					linePen.DashStyle = gridDashStyle_;

					for (float x = offsetPt.X; x < (float)this.Width; x += ZOOM_GRID[zoomFactorIndex_].GridSize)
						graphics_.DrawLine(linePen, x, 0.0F, x, (float)this.Height);
					for (float y = offsetPt.Y; y < (float)this.Height; y += ZOOM_GRID[zoomFactorIndex_].GridSize)
						graphics_.DrawLine(linePen, 0.0F, y, (float)this.Width, y);

					linePen.Dispose();
				}
				// 図形描画
				DrawShape(graphics_, affinMatrix_.InvMatrix, this.Size);
			}
			base.Refresh();
		}

		#region 画素単位のコピー
		/// <summary>
		/// 画素単位の拡大コピー
		/// </summary>
		/// <param name="dst">コピー先</param>
		/// <param name="src">コピー元</param>
		/// <param name="dstRect">コピー先領域</param>
		/// <param name="srcRect">コピー元領域</param>
		/// <param name="onePixcelSize">1ピクセルのサイズ(拡大倍率)</param>
		private void CopyImage(Bitmap dst, Bitmap src, Rectangle dstRect, Rectangle srcRect, int onePixcelSize,ref Point startOffset)
        {
            // 書き込みオフセット
            int copyOffsetX = 0;
            int copyOffsetY = 0;

            // 書き込みビットマップデータをロック
            BitmapData w_bd = dst.LockBits(
                       dstRect,
                       ImageLockMode.WriteOnly,
                       dst.PixelFormat);
            // 1画素のサイズ
            int dstPixelByte = GetPixelFormatSize(dst.PixelFormat);
            // カラーパレット
            ColorPalette dstPalette = dst.Palette;

            // 読み込みビットマップデータをロック
            BitmapData r_bd = src.LockBits(
                       srcRect,
                       ImageLockMode.ReadOnly,
                       src.PixelFormat);
            // 1画素のサイズ
            int srcPixelByte = GetPixelFormatSize(src.PixelFormat);
            // カラーパレット
            ColorPalette srcPalette = src.Palette;

            if (srcPixelByte != 0)
            {
                // 1ライン分の元データ
                byte[] pixelData = new byte[srcPixelByte * r_bd.Width];

                // 書き込みバイト数
                int dstLength = r_bd.Width * GetPixelFormatSize(dst.PixelFormat) * onePixcelSize;
                int dstLengthTmp = dstLength;
                if (dstLength > w_bd.Width * GetPixelFormatSize(dst.PixelFormat))
                {
                    dstLength = w_bd.Width * GetPixelFormatSize(dst.PixelFormat);
                    if ((srcRect.X + srcRect.Width)  >= src.Width)
                    {   // オフセットを調整して、逆からにする
                        copyOffsetX = dstLengthTmp - dstLength;
                    }
                }
                // 書き込み行数
                int dstHeight = r_bd.Height * onePixcelSize;
                int dstHeightTmp = dstHeight;
                if (dstHeight > w_bd.Height)
                {
                    dstHeight = w_bd.Height;
                    if ((srcRect.Y + srcRect.Height) >= src.Height)
                    {   // オフセットを調整して、逆からにする
                        copyOffsetY = dstHeightTmp - dstHeight;
                    }
                }
                int copyOffsetYTmp = copyOffsetY;
                for (int y = 0; y < r_bd.Height; y++)
                {
                    // 読み込み元アドレス
                    IntPtr readAddr = r_bd.Scan0 + y * r_bd.Stride;
                    // バイト配列にコピー
                    Marshal.Copy(readAddr, pixelData, 0, srcPixelByte * r_bd.Width);

                    // コピー先形式に変換
                    byte[] dstData = ConvertDstFormat(pixelData, w_bd.Width, dst.PixelFormat, 
                        r_bd.Width,src.PixelFormat, srcPalette.Entries, onePixcelSize);

                    for (int row = copyOffsetYTmp; (row < onePixcelSize) && (y * onePixcelSize + row - copyOffsetY < w_bd.Height); row++)
                    {
                        // コピー先のアドレス
                        IntPtr writeAddr = w_bd.Scan0 + (y * onePixcelSize + row - copyOffsetY) * w_bd.Stride;
                        Marshal.Copy(dstData, copyOffsetX, writeAddr, dstLength);
                    }
                    // 先頭に戻す
                    copyOffsetYTmp = 0;
                }
            }

            // ビットマップデータをロック解除
            dst.UnlockBits(w_bd);
            src.UnlockBits(r_bd);

            startOffset.X = onePixcelSize - (int)(copyOffsetX / GetPixelFormatSize(dst.PixelFormat));
            startOffset.Y = onePixcelSize - copyOffsetY;
        }
        /// <summary>
        /// コピー先のフォーマットに変換
        /// </summary>
        /// <param name="srcData">コピー元データ</param>
        /// <param name="dstWidth">コピー先画像幅</param>
        /// <param name="dstFormat">コピー先フォーマット</param>
        /// <param name="srcWidth">コピー元画像幅</param>
        /// <param name="srcFormat">コピー元フォーマット</param>
        /// <param name="srcPalette">コピー元パレット</param>
        /// <param name="onePixcelSize">拡大サイズ(1pixelの大きさ)</param>
        /// <returns></returns>
        private byte[] ConvertDstFormat(byte[] srcData, int dstWidth, PixelFormat dstFormat,
            int srcWidth, PixelFormat srcFormat, Color[] srcPalette,int onePixcelSize)
        {
            // コピー先の必要バイト数
            int dstLength = srcWidth * GetPixelFormatSize(dstFormat) * onePixcelSize;
            // 格納先を確保
            byte[] dstData = new byte[dstLength];
            // 読み込み元をColorに変換
            Color[] oneLineColor = ConvertColor(srcData, srcWidth, srcFormat, srcPalette);
            int w_index = 0;
            // コピー先に書き込み
            for(int index = 0; index < oneLineColor.Length; index ++ )
            {
                for(int cpnum = 0; (cpnum < onePixcelSize) && (w_index < dstLength) ; cpnum ++)
                {
                    dstData[w_index] = oneLineColor[index].B;
                    w_index++;
                    if (w_index < dstLength)
                    {
                        dstData[w_index] = oneLineColor[index].G;
                        w_index++;
                    }
                    if (w_index < dstLength)
                    {
                        dstData[w_index] = oneLineColor[index].R;
                        w_index++;
                    }
                    if ((w_index < dstLength) && 
                        ((dstFormat == PixelFormat.Format32bppArgb) || (dstFormat == PixelFormat.Format32bppPArgb)))
                    {
                        dstData[w_index] = oneLineColor[index].A;
                        w_index++;
                    }
                    if ((w_index < dstLength) &&
                        (dstFormat == PixelFormat.Format32bppRgb))
                    {
                        w_index++;
                    }
                }
            }

            return dstData;
        }
        /// <summary>
        /// コピー元データからColor配列を生成する
        /// </summary>
        /// <param name="srcData">コピー元データ</param>
        /// <param name="srcWidth">コピー元画像幅</param>
        /// <param name="srcFormat">コピー元フォーマット</param>
        /// <param name="srcPalette">コピー元パレット</param>
        /// <returns></returns>
        private Color[] ConvertColor(byte[] srcData, int srcWidth, PixelFormat srcFormat, Color[] srcPalette)
        {
            Color[] result = new Color[srcWidth];
            int w_index = 0;
            int r_index = 0;
            while ((w_index < srcWidth) && (r_index < srcData.Length))
            {
                if (srcFormat == PixelFormat.Format8bppIndexed)
                {   // 8bit Indexed Format
                    result[w_index] = srcPalette[srcData[r_index]];
                    w_index++;
                    r_index++;
                }
                else if (srcFormat == PixelFormat.Format24bppRgb)
                {   // 24bit RGB
                    result[w_index] = Color.FromArgb(srcData[r_index + 2], srcData[r_index + 1], srcData[r_index]);
                    w_index++;
                    r_index += 3;
                }
                else if ((srcFormat == PixelFormat.Format32bppArgb) ||
                    (srcFormat == PixelFormat.Format32bppPArgb) ||
                    (srcFormat == PixelFormat.Format32bppRgb))
                {   // 32bit ARGB
                    result[w_index] = Color.FromArgb(srcData[r_index + 3], srcData[r_index + 2], srcData[r_index + 1], srcData[r_index]);
                    w_index++;
                    r_index += 4;
                }
                else if (srcFormat == PixelFormat.Format16bppGrayScale)
                {   // 16bit TIFF
                    r_index++;
                    result[w_index] = Color.FromArgb(srcData[r_index], srcData[r_index], srcData[r_index]);
                    w_index++;
                    r_index++;
                }
            }

            return result;
        }

        /// <summary>
        /// PixelFormatから使用するBYTEサイズの算出
        /// </summary>
        /// <param name="format">PixelFormat</param>
        /// <returns>バイトサイズ</returns>
        private int GetPixelFormatSize(PixelFormat format)
        {
            switch(format)
            {
                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                    return 2;
                case PixelFormat.Format24bppRgb:
                    return 3;
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 4;
                case PixelFormat.Format48bppRgb:
                    return 6;
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 8;
                case PixelFormat.Format8bppIndexed:
                    return 1;
                // 以降はbit単位なので無視
                case PixelFormat.Format4bppIndexed:
                case PixelFormat.Format1bppIndexed:
                default:
                    return 0;
            }
        }
        #endregion

        /// <summary>
        /// 最小倍率の計算
        /// </summary>
        /// <param name="minZoomFactor"></param>
        /// <param name="minZoomIndex"></param>
        /// <param name="pictureBoxSize"></param>
        /// <param name="imageSize"></param>
        /// <returns></returns>
        private bool CalcZoomFactor(ref float minZoomFactor,ref int minZoomIndex,Size pictureBoxSize,Size imageSize)
        {
            float x_scale = (float)pictureBoxSize.Width / (float)imageSize.Width;
            float y_scale = (float)pictureBoxSize.Height / (float)imageSize.Height;
            // 小さい方を返す
            float zoom = (x_scale < y_scale) ? x_scale : y_scale;
            bool isFind = false;
            // 最小倍率のインデックスを求める
            for (int index = 0; index < ZOOM_GRID.Length; index++)
            {
                if (ZOOM_GRID[index].ZoomFactor > zoom)
                {
                    minZoomIndex = index - 1;
                    isFind = true;
                    break;
                }
            }
            if (isFind)
            {
                minZoomFactor = zoom;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 最小倍率の更新
        /// </summary>
        private void UpdateMinZoomFactor(bool isUpdateEvent)
        {
            if (image_ != null)
            {
                float minZoom = 1.0F;
                int minIndex = 0;
                if (CalcZoomFactor(ref minZoom, ref minIndex, this.Size, image_.Size))
                {   // 最小倍率が求まった
                    minZoomFactor_ = minZoom;
                    minZoomFactorIndex_ = minIndex;
                    // 現在の倍率が最小倍率より小さい場合...
                    if (zoomFactorIndex_ < minIndex)
                        UpdateZoomFactor(minIndex,new Point(0,0),false);
					else
						UpdateZoomFactor(zoomFactorIndex_, new Point(0, 0), false);
				}
                // イベント発行
                if (isUpdateEvent)
                    OnPictureSizeChangeEvent(image_.Size, zoomFactor_);
            }
        }
        /// <summary>
        /// ズーム倍率の更新
        /// </summary>
        /// <param name="index"></param>
        /// <param name="center"></param>
        private void UpdateZoomFactor(int index,Point center, bool isUpdateEvent,bool forceUpdate = false)
        {
            if (image_ != null)
            {
                // 前の値を保存
                float oldZoomFactor = zoomFactor_;
                // index範囲チェック
                if (index >= ZOOM_GRID.Length)
                    index = ZOOM_GRID.Length - 1;
                if (index < minZoomFactorIndex_)
                    index = minZoomFactorIndex_;
                // インデックスの更新
                zoomFactorIndex_ = index;
                // 倍率の範囲チェック
                if (ZOOM_GRID[index].ZoomFactor < minZoomFactor_)
                    zoomFactor_ = minZoomFactor_;
                else
                    zoomFactor_ = ZOOM_GRID[index].ZoomFactor;

                if ((zoomFactor_ != oldZoomFactor) || (forceUpdate))
                {
                    // アフィン行列を更新
                    affinMatrix_.SetZoomFactor(zoomFactor_, center, this.Size, image_.Size);
                    if (isUpdateEvent)
                    {
                        // イベント発行
                        OnPictureSizeChange(zoomFactor_);
                    }
                }
            }
        }


        /// <summary>
        /// 画像情報更新
        /// </summary>
        /// <param name="pictBoxPt">マウスの座標</param>
        private void infoUpdate(Point pictBoxPt)
        {
            if (image_ != null)
            {
                // 画素値を取得する
                Point pt = affinMatrix_.GetPoint(pictBoxPt);
                if ((pt.X >= 0) && (pt.X < image_.Size.Width) &&
                    (pt.Y >= 0) && (pt.Y < image_.Size.Height))
                {
                    Bitmap bmp = image_ as Bitmap;
                    if (bmp != null)
                    {
                        PIXEL_INFO pixelInfo = new PIXEL_INFO(pt, bmp.GetPixel(pt.X, pt.Y));
                        // イベント発行
                        OnPixelInfoEvent(pixelInfo);
                        // ラベル更新
                        SetInfoLabel(pictBoxPt, pixelInfo, zoomFactor_);
                    }
                }
            }
        }

        /// <summary>
        /// ファイル保存のダイアログを開く
        /// </summary>
        /// <param name="imageType">保存種別</param>
        /// <returns></returns>
        private string DialogGetSaveFileName(SAVE_IMAGE_TYPE imageType)
        {
            string filename = null;
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            // ファイル名
            sfd.FileName = "画像ファイル";
            if (imageType == SAVE_IMAGE_TYPE.DISPLAY)
                sfd.FileName += "_clip";
            sfd.FileName += ".bmp";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "ビットマップファイル(*.bmp)|*.bmp|" +
                "JPEGファイル(*.jpg;*.jpeg)|*.jpg;*.jpeg;|" +
                "TIFFファイル(*.tif;*.tiff)|*.tif;*.tiff;|" +
                "PNGファイル(*.png)|*.png|" +
                "すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
            }
            sfd.Dispose();
            return filename;
        }

        #region 情報パネル
        /// <summary>
        /// 情報パネル初期化
        /// </summary>
        private void InitInfoPanel()
        {
            // 親子設定
            LbInfo.Parent = this;
            // 位置指定
            LbInfo.Location = new Point(0, 0);
            // とりあえず非表示
            LbInfo.Visible = showInfoPanel_;
        }

        /// <summary>
        /// 情報表示ラベル設定
        /// </summary>
        /// <param name="pictBoxPt"></param>
        /// <param name="pixelInfo"></param>
        /// <param name="zoomRatio"></param>
        private void SetInfoLabel(Point pictBoxPt,PIXEL_INFO pixelInfo,float zoomRatio)
        {
            if ((showInfoPanel_) && (image_ != null))
            {
                string infoText = "";
                if (InfoItem.Coordinate)
                {
                    infoText = string.Format("({0},{1})", pictBoxPt.X, pictBoxPt.Y);
                }
                if (InfoItem.ImageCoordinate)
                {
                    if (infoText.Length > 0)
                        infoText += "\n";
                    infoText += pixelInfo.ToString("point");
                }
                if (InfoItem.PixelInfo)
                {
                    if (infoText.Length > 0)
                        infoText += "\n";
                    if (GetPixelFormatSize(image_.PixelFormat) == 1)
                        infoText += pixelInfo.ToString("R");
                    else
                        infoText += pixelInfo.ToString("Color");
                }
                if (InfoItem.ZoomRatio)
                {
                    if (infoText.Length > 0)
                        infoText += "\n";
                    infoText += string.Format("{0:P2}", zoomRatio);
                }
                LbInfo.Text = infoText;
                // 表示位置の更新
                UpdateInfoLocation();
            }
        }
        /// <summary>
        /// 表示ラベル位置の更新
        /// </summary>
        private void UpdateInfoLocation()
        {
            switch(infoLocaltion_)
            {
                case INFO_LOCATION_DEF.TOP_LEFT:
                    LbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    LbInfo.Location = new Point(0, 0);
                    break;
                case INFO_LOCATION_DEF.TOP_RIGHT:
                    LbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    LbInfo.Location = new Point(this.Width - LbInfo.Width, 0);
                    break;
                case INFO_LOCATION_DEF.TOP_CENTER:
                    LbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    LbInfo.Location = new Point((this.Width - LbInfo.Width/2), 0);
                    break;
                case INFO_LOCATION_DEF.BOTTOM_LEFT:
                    LbInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                    LbInfo.Location = new Point(0, this.Height - LbInfo.Height -2);
                    break;
                case INFO_LOCATION_DEF.BOTTOM_RIGHT:
                    LbInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                    LbInfo.Location = new Point(this.Width - LbInfo.Width, this.Height - LbInfo.Height -2);
                    break;
                case INFO_LOCATION_DEF.BOTTOM_CENTER:
                    LbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    LbInfo.Location = new Point((this.Width - LbInfo.Width) / 2, this.Height - LbInfo.Height-2);
                    break;
            }
        }
        #endregion
    }
}

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.Detection
{
	[DisplayName("YoLov3"),
		NetworkSize(320, 416,512, 608,640),
		UseBackground(false)]
	public class YoLov3 :YoLo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="framework">フレームワーク</param>
		/// <param name="modelfile">モデルファイル</param>
		/// <param name="configfile">Configファイル</param>
		/// <param name="sizeInt">ネットワーク入力サイズ</param>
		/// <param name="numOfClass">クラス数</param>
		/// <exception cref="Exception"></exception>
		public YoLov3(Framework framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			base(framework, modelfile, configfile, sizeInt, numOfClass)
		{ }
		/// <summary>
		/// BLOBの取得
		/// </summary>
		/// <param name="inputImage"></param>
		/// <returns></returns>
		protected override Mat getBlob(ref Bitmap inputImage)
		{
			Mat blob = base.getBlob(ref inputImage);

			x_factor *= inputSize_;
			y_factor *= inputSize_;

			return blob;
		}
	}
}

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.Detection
{
	[DisplayName("UltralyticsYoLo"),
	NetworkSize(416, 640),
	UseBackground(false)]
	public class UltralyticsYoLo : YoLo
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
		public UltralyticsYoLo(Framework framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			base(framework, modelfile, configfile, sizeInt, numOfClass)
		{ }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputs"></param>
		/// <returns></returns>
		/// <remarks>
		///   クラスID ... data[4]以降にクラス別信頼度が格納。最大値のインデックスがクラスID<br>
		///   (x,y)    ... (data[0],data[1])<br>
		///   (w,h)    ... (data[2],data[3])<br>
		/// </remarks>
		protected override NMSData post_process(Mat[] outputs, float confidence_threshold)
		{
			if ((outputs.Length > 0) && (outputs[0] != null) && (outputs[0].Empty() == false))
			{
				long length = outputs[0].Total();
				int one_width = numOfClass_ + 4;
				int rowMax = (int)length / one_width;
				// MatをReShape
				Mat tmpMat = outputs[0].Reshape(0, one_width, rowMax);
				// 縦横を入れ替え
				Mat mat = tmpMat.Transpose();
				// 結果クラス
				NMSData results = new NMSData();
				for (int row = 0; row < rowMax; row++)
				{
					// 信頼度の配列をMatにする
					Mat oneRow = new Mat(mat, new Rect(4, row, numOfClass_, 1));
					// 最大値と位置を検索
					Cv2.MinMaxLoc(oneRow, out _, out double maxValue, out _, out OpenCvSharp.Point maxLoc);
					// 信頼度が閾値以上なら...
					if (maxValue >= confidence_threshold)
					{	// 座標値は中心座標なので...
						float cx = mat.At<float>(row, 0);
						float cy = mat.At<float>(row, 1);
						float w = mat.At<float>(row, 2);
						float h = mat.At<float>(row, 3);
						results.Add(maxLoc.X, (float)(maxValue),
							new Rect((int)((cx - 0.5F * w) * x_factor), (int)((cy - 0.5F * h) * y_factor),
							(int)(w * x_factor), (int)(h * y_factor)));
					}
				}
				return results;
			}
			return null;
		}
	}
}

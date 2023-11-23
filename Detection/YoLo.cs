using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.Detection
{
	[DisplayName("YoLo"),
		NetworkSize(320, 416, 512, 608, 640, 896, 1280),
		UseBackground(false)]
	public class YoLo : DetectionBase
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
		public YoLo(Framework framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			base(framework, modelfile, configfile, sizeInt, numOfClass)
		{ }

		/// <summary>
		/// スケールファクタ
		/// </summary>
		/// <returns>0.0～1.0の値にするので、1/255</returns>
		protected virtual double GetImageScaleFactor() { return 1.0 / 255.0; }

		/// <summary>
		/// BLOBの取得
		/// </summary>
		/// <param name="inputImage"></param>
		/// <returns></returns>
		protected override Mat getBlob(ref Bitmap inputImage)
		{
			// スカラー値(R:0,G:0,B:0)
			Scalar scalar = new Scalar(0.0, 0.0, 0.0);
			// 画像のMat
			Mat imageMat = getMatImage(ref inputImage);
			Mat beforeBlob;

			// 拡大・縮小比率を求めて、小さいほうを採用する
			float x_ratio = (float)inputSize_ / (float)imageMat.Cols;
			float y_ratio = (float)inputSize_ / (float)imageMat.Rows;
			float img_scale_factor;
			if (x_ratio == y_ratio)
			{
				// 比率が同じなら、そのままで処理できる
				img_scale_factor = 1.0F;
				x_factor = y_factor = 1.0F / x_ratio;
				beforeBlob = imageMat;
			}
			else
			{
				if (x_ratio < y_ratio)
				{
					// 求まった拡大・縮小率を保存
					img_scale_factor = x_ratio;
					x_factor = y_factor = 1.0F / x_ratio;
				}
				else
				{
					// 求まった拡大・縮小率を保存
					img_scale_factor = y_ratio;
					x_factor = y_factor = 1.0F / y_ratio;
				}
				// リサイズ後の画像
				Mat resizeImage = new Mat();
				// リサイズ後のサイズ
				int width = (int)(imageMat.Cols * img_scale_factor);
				int height = (int)(imageMat.Rows * img_scale_factor);
				// 画像のリサイズ
				Cv2.Resize(imageMat, resizeImage,
					new OpenCvSharp.Size(width, height),
					0.0, 0.0, InterpolationFlags.Linear);
				// BLOB入力用Matを確保
				//    一旦(114,114,114)を画像イメージ分書き込む
				beforeBlob = new Mat(inputSize_, inputSize_, resizeImage.Type(), new OpenCvSharp.Scalar(114, 114, 114));
				// 貼り付ける領域のROIを設定
				Mat roi = new Mat(beforeBlob, new OpenCvSharp.Rect(0, 0, width, height));
				// リサイズしたイメージを貼り付け
				resizeImage.CopyTo(roi);

				// 使った領域を解放
				resizeImage.Dispose();
				resizeImage = null;
			}
			// ブロブを生成(YoLo系はRGB並び)
			Mat blob = CvDnn.BlobFromImage(beforeBlob, GetImageScaleFactor(), new OpenCvSharp.Size(inputSize_, inputSize_),
					scalar, true, false);

			imageMat.Dispose();
			imageMat = null;

			return blob;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputs"></param>
		/// <returns></returns>
		/// <remarks>
		///   クラスID ... data[5]以降にクラス別信頼度が格納。最大値のインデックスがクラスID<br>
		///   信頼度   ... data[4]<br>
		///   (x,y)    ... (data[0],data[1])<br>
		///   (w,h)    ... (data[2],data[3])<br>
		/// </remarks>
		protected override NMSData post_process(Mat[] outputs, float confidence_threshold)
		{
			if (outputs.Length > 0)
			{
				// 結果クラス
				NMSData results = new NMSData();

				foreach (Mat output in outputs)
				{
					if ((output != null) && (output.Empty() == false))
					{
						long length = output.Total();
						int one_width = numOfClass_ + 5;
						int rowMax = (int)length / one_width;
						// MatをReShape
						Mat mat = output.Reshape(0, rowMax, one_width);
						for (int row = 0; row < rowMax; row++)
						{
							// この行の信頼度
							float confidence = mat.At<float>(row, 4);
							if (confidence > confidence_threshold)
							{
								// 信頼度の配列をMatにする
								Mat oneRow = new Mat(mat, new Rect(5, row, numOfClass_, 1));
								// 最大値と位置を検索
								Cv2.MinMaxLoc(oneRow, out _, out double maxValue, out _, out OpenCvSharp.Point maxLoc);
								// 信頼度が閾値以上なら...
								if (maxValue > confidence_threshold)
								{   // 座標値は中心座標なので...
									float cx = mat.At<float>(row, 0);
									float cy = mat.At<float>(row, 1);
									float w = mat.At<float>(row, 2);
									float h = mat.At<float>(row, 3);
									results.Add(maxLoc.X, (float)(confidence),
										new Rect((int)((cx - 0.5F * w) * x_factor), (int)((cy - 0.5F * h) * y_factor),
										(int)(w * x_factor), (int)(h * y_factor)));
									//Console.WriteLine("[{3}]{0} ... {1:p} {2:p} ({4},{5})-{6}.{7}",maxLoc.X, confidence,maxValue,results.Count - 1,cx,cy,w,h);
								}
								if (oneRow.IsSubmatrix() == false)
									oneRow.Dispose();
							}
						}
						if (mat.IsSubmatrix() == false)
							mat.Dispose();
					}
				}
				return results;
			}
			return null;
		}
	}
}

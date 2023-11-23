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
	/// <summary>
	/// Caffe SSD推論
	/// </summary>
	[DisplayName("CaffeSSD"),NetworkSize(300,512),UseBackground(true)]
	public class CaffeSSD : DetectionBase
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
		public CaffeSSD(Framework framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			base(framework, modelfile, configfile, sizeInt, numOfClass)
		{ }

		/// <summary>
		/// BLOBの取得
		/// </summary>
		/// <param name="inputImage"></param>
		/// <returns></returns>
		protected override Mat getBlob(ref Bitmap inputImage)
		{
			Scalar scalar = new Scalar(104.0, 117.0, 123.0);
			Mat imageMat = getMatImage(ref inputImage);
			double scale_factor = 1.0;
			x_factor = inputImage.Width;
			y_factor = inputImage.Height;

			Mat blob = CvDnn.BlobFromImage(imageMat, scale_factor, new OpenCvSharp.Size(inputSize_, inputSize_),
					scalar, false, false);

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
		///   クラスID ... data[1]<br>
		///   信頼度   ... data[2]<br>
		///   (x1,y1)  ... (data[3],data[4])<br>
		///   (x2,y2)  ... (data[5],data[6])<br>
		///   widthとheightを計算する
		/// </remarks>
		protected override NMSData post_process(Mat[] outputs, float confidence_threshold)
		{
			if ((outputs.Length > 0) && (outputs[0] != null) && (outputs[0].Empty() == false))
			{
				long length = outputs[0].Total();
				// SSDは横が7なので..
				int rowMax = (int)length / 7;
				// MatをReShape
				Mat mat = outputs[0].Reshape(0, rowMax, 7);
				NMSData results = new NMSData(); 
				for (int row = 0; row < rowMax; row++)
				{
					//Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
					//	mat.At<float>(row,0), mat.At<float>(row,1), mat.At<float>(row,2), mat.At<float>(row,3), mat.At<float>(row,4),
					//	mat.At<float>(row, 5), mat.At<float>(row, 6), mat.At<float>(row, 7));
					float confidence = mat.At<float>(row, 2);
					if (confidence >= confidence_threshold)
					{
						int classId = (int)mat.At<float>(row, 1);
						float x1 = mat.At<float>(row, 3) * x_factor;
						float y1 = mat.At<float>(row, 4) * y_factor;
						float x2 = mat.At<float>(row, 5) * x_factor;
						float y2 = mat.At<float>(row, 6) * y_factor;
						results.Add(classId, confidence,new Rect((int)x1, (int)y1, (int)(x2-x1), (int)(y2-y1)));
					}
				}
				return results;
			}
			return null;
		}
	}
}

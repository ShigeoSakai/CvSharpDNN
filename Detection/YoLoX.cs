using OpenCvSharp;
using OpenCvSharp.Flann;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace CVSharpDNN.Detection
{
	[DisplayName("YoLoX"),
	NetworkSize(416, 640),
	UseBackground(false)]
	public class YoLoX : YoLo
	{
		/// <summary>
		/// YoLoX Stride
		/// </summary>
		private int[] yoloxStrides_ = new int[3] { 8, 16, 32 };

		/// <summary>
		/// グリッド位置
		/// </summary>
		private List<Point3i> yoloxGrid_ = new List<Point3i>();

		/// <summary>
		/// スケールファクタ
		/// </summary>
		/// <returns>そのままなので、1</returns>
		protected override double GetImageScaleFactor() { return 1.0; }


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="framework">フレームワーク</param>
		/// <param name="modelfile">モデルファイル</param>
		/// <param name="configfile">Configファイル</param>
		/// <param name="sizeInt">ネットワーク入力サイズ</param>
		/// <param name="numOfClass">クラス数</param>
		/// <exception cref="Exception"></exception>
		public YoLoX(Framework framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			base(framework, modelfile, configfile, sizeInt, numOfClass)
		{
			if ((network != null) && (network.Empty() == false))
			{   //グリッド位置を生成
				yoloxGrid_.Clear();
				// YoLoXロジック
				//   1 x (image.width/8) x (image.height/8)
				//   1 x (image.width/16) x (image.height/16)
				//   1 x (image.width/32) x (image.height/32)
				int total_lines = 0;
				for (int index = 0; index < yoloxStrides_.Length; index++)
				{
					int x = inputSize_ / yoloxStrides_[index];
					int y = inputSize_ / yoloxStrides_[index];
					total_lines += x * y;
					yoloxGrid_.Add(new Point3i(x, y, total_lines));
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="outputs"></param>
		/// <param name="confidence_threshold"></param>
		/// <returns></returns>
		protected override NMSData post_process(Mat[] outputs, float confidence_threshold)
		{
			if ((outputs.Length > 0) && (outputs[0] != null) && (outputs[0].Empty() == false))
			{
				long length = outputs[0].Total();
				int one_width = numOfClass_ + 5;
				int rowMax = (int)length / one_width;
				// MatをReShape
				Mat mat = outputs[0].Reshape(0, rowMax, one_width);
				NMSData results = new NMSData();
				int gridRow_index = 0;
				int grid_subValue = 0;
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
						{
							// 座標を計算
							float data_x = mat.At<float>(row, 0);
							float data_y = mat.At<float>(row, 1);
							float data_w = mat.At<float>(row, 2);
							float data_h = mat.At<float>(row, 3);


							// グリッド分割した時の位置
							int grid_row = row - grid_subValue;

							int grid_x = grid_row % yoloxGrid_[gridRow_index].X;
							int grid_y = grid_row / yoloxGrid_[gridRow_index].Y;
							// 中心座標
							float cx = (data_x + grid_x) * (float)yoloxStrides_[gridRow_index];
							float cy = (data_y + grid_y) * (float)yoloxStrides_[gridRow_index];
							// 幅と高さ
							float w = (float)(Math.Exp(data_w) * (float)yoloxStrides_[gridRow_index]);
							float h = (float)(Math.Exp(data_h) * (float)yoloxStrides_[gridRow_index]);

							// 実座標の左上座標と幅、高さにする
							int left = (int)((cx - 0.5F * w) * x_factor);
							int top = (int)((cy - 0.5F * h) * y_factor);
							int width = (int)(w * x_factor);
							int height = (int)(h * y_factor);
							results.Add(maxLoc.X, (float)(confidence),
								new Rect(left, top, width, height));
						}
					}
					if (row >= yoloxGrid_[gridRow_index].Z)
					{
						grid_subValue = yoloxGrid_[gridRow_index].Z;
						gridRow_index++;
					}
				}
				return results;
			}
			return null;
		}
	}
}

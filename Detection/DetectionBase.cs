using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;

namespace CVSharpDNN.Detection
{
	/// <summary>
	/// 推論ロジックベースクラス
	/// </summary>
	public class DetectionBase:IDisposable
	{
		/// <summary>
		/// NMSデータクラス
		/// </summary>
		protected class NMSData
		{
			/// <summary>
			/// クラスIDリスト
			/// </summary>
			private List<int> classes_ = new List<int>();
			/// <summary>
			/// 信頼度リスト
			/// </summary>
			private List<float> confidences_ = new List<float>();
			/// <summary>
			/// 矩形リスト
			/// </summary>
			private List<Rect> rects_ = new List<Rect>();
			/// <summary>
			/// NMS結果
			/// </summary>
			private int[] indicate_ = null;

			/// <summary>
			/// クラスIDリスト
			/// </summary>
			public List<int> Classes { get { return classes_; } }
			/// <summary>
			/// 信頼度リスト
			/// </summary>
			public List<float> Confidences { get { return confidences_; } }
			/// <summary>
			/// 矩形リスト
			/// </summary>
			public List<Rect> Rects { get { return rects_; } }
			/// <summary>
			/// NMS結果
			/// </summary>
			public int[] Indicate { get; private set; }
			/// <summary>
			/// 結果登録件数
			/// </summary>
			public int Count { get { return classes_.Count; } }
			/// <summary>
			/// 情報を追加
			/// </summary>
			/// <param name="classid">クラスID</param>
			/// <param name="confidence">信頼度</param>
			/// <param name="rect">矩形</param>
			public void Add(int classid,float confidence,Rect rect)
			{
				classes_.Add(classid);
				confidences_.Add(confidence);
				rects_.Add(rect);
			}
			/// <summary>
			/// NMS結果を設定
			/// </summary>
			/// <param name="indicate">NMS結果</param>
			/// <returns>NMS結果件数</returns>
			public int SetIndicate(int[] indicate)
			{
				indicate_ = indicate;
				return indicate_.Length;
			}
			/// <summary>
			/// 推論結果を取得
			/// </summary>
			/// <returns>推論結果(NMS統合済み)</returns>
			public List<DetectionResult> GetResult()
			{
				if ((indicate_ == null) || (classes_.Count == 0) ||
					(confidences_.Count == 0) || (rects_.Count == 0))
					return null;
				List<DetectionResult> result = new List<DetectionResult> ();
				for (int i = 0; i < indicate_.Length; i++)
				{
					int index = indicate_[i];
					if ((index >= 0) && (index < classes_.Count))
					{
						result.Add(new DetectionResult(
							classes_[index],
							confidences_[index],
							rects_[index].X, rects_[index].Y, rects_[index].Width, rects_[index].Height));
					}
				}
				return result;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="sizeInt">ネットワーク入力サイズ</param>
		/// <param name="numOfClass">クラス数</param>
		public DetectionBase(int sizeInt, int numOfClass) 
		{
			inputSize_ = sizeInt;
			numOfClass_ = numOfClass;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="framework">フレームワーク</param>
		/// <param name="modelfile">モデルファイル</param>
		/// <param name="configfile">Configファイル</param>
		/// <param name="sizeInt">ネットワーク入力サイズ</param>
		/// <param name="numOfClass">クラス数</param>
		/// <exception cref="Exception"></exception>
		public DetectionBase(Framework framework,string modelfile, string configfile, int sizeInt, int numOfClass) : this(sizeInt, numOfClass)
		{
			// ネットワークを解放
			disposeNetwork();

			string dnn_model = "";
			string dnn_config = "";
			string dnn_framework = "";

			// モデルのチェック
			bool model_check = false;
			string[] ext = framework.GetModelExt();
			if ((ext != null) && (ext.Length > 0))
			{
				foreach (string ext_str in ext)
					if (ext_str == Path.GetExtension(modelfile))
					{
						dnn_model = modelfile;
						model_check = true;
						break;
					}
				if ((model_check == false) && (framework.GetFrameworkString() != null))
				{   // フレームワーク指定
					dnn_model = modelfile;
					dnn_framework = framework.GetFrameworkString();
					model_check = true;
				}
			}
			else
				throw new Exception("フレームワークのモデル拡張子定義がおかしい!");
			
			// Configファイル
			bool config_check = true;
			if ((model_check) && (framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG))
			{   // Configファイルチェック
				config_check = false;
				string[] config_ex = framework.GetConfigExt();
				if ((config_ex != null) && (config_ex.Length > 0))
				{
					foreach (string ext_str in config_ex)
						if (ext_str == Path.GetExtension(configfile))
						{
							dnn_config = configfile;
							config_check = true;
							break;
						}
					if ((config_check == false) && (dnn_framework != "") &&
						(framework.GetFrameworkString() != null))
					{   // フレームワーク指定
						dnn_config = configfile;
						dnn_framework = framework.GetFrameworkString();
						config_check = true;
					}
				}
				else
					throw new Exception("フレームワークのConfig拡張子定義がおかしい!");
			}
			if ((model_check) && (config_check))
			{   // ネットワークを構成
				try
				{
					network = CvDnn.ReadNet(dnn_model, dnn_config, dnn_framework);
					return;
				}
				catch (Exception ex) { throw new Exception("Network Read Error!", ex); }
			}
			if (model_check == false)
				throw new Exception("モデルファイル名の拡張子が一致しない!");
			else if (config_check == false)
				throw new Exception("Configファイル名の拡張子が一致しない!");
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="framework">フレームワーク</param>
		/// <param name="modelfile">モデルファイル</param>
		/// <param name="configfile">Configファイル</param>
		/// <param name="sizeInt">ネットワーク入力サイズ</param>
		/// <param name="numOfClass">クラス数</param>
		/// <exception cref="Exception"></exception>
		public DetectionBase(FrameworkClass framework, string modelfile, string configfile, int sizeInt, int numOfClass) :
			this(framework.Framework, modelfile, configfile, sizeInt, numOfClass)
		{ }

		/// <summary>
		/// デストラクタ
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		public virtual void Dispose()
		{
			disposeNetwork();
		}

		/// <summary>
		/// DNNネットワーク
		/// </summary>
		protected Net network = null;
		/// <summary>
		/// ネットワーク入力サイズ
		/// </summary>
		protected int inputSize_;
		/// <summary>
		/// クラス数
		/// </summary>
		protected int numOfClass_;
		/// <summary>
		/// X方向の係数
		/// </summary>
		protected float x_factor = 1.0F;
		/// <summary>
		/// Y方向の係数
		/// </summary>
		protected float y_factor = 1.0F;

		/// <summary>
		/// Blobの取得
		/// </summary>
		/// <param name="inputImage">入力Bitmap画像</param>
		/// <returns>Mat画像(BGR)</returns>
		protected virtual Mat getBlob(ref Bitmap inputImage) { return getMatImage(ref inputImage); }

		/// <summary>
		/// 画像をMatに変換
		/// </summary>
		/// <param name="inputImage"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
        protected Mat getMatImage(ref Bitmap inputImage)
		{
			if (inputImage != null)
			{
				// Bitmap⇒Mat変換
				Mat imageMat = BitmapConverter.ToMat(inputImage);
				if (imageMat != null)
				{
					// 結果
					Mat result = new Mat();
					switch (inputImage.PixelFormat)
					{
						case PixelFormat.Format8bppIndexed:
						case PixelFormat.Format1bppIndexed:
							// グレイスケール or 白黒
							Cv2.CvtColor(imageMat, result, ColorConversionCodes.GRAY2BGR);
							break;
						case PixelFormat.Format32bppArgb:
						case PixelFormat.Format32bppPArgb:
							// RGBA
							Cv2.CvtColor(imageMat, result, ColorConversionCodes.BGRA2BGR);
							break;
						case PixelFormat.Format24bppRgb:
						case PixelFormat.Format32bppRgb:
							// RGB
							result = imageMat.Clone();
							break;
						default:
							throw new Exception(string.Format("Illegal Image Format:{0}",inputImage.PixelFormat));
					}
					// 変換元Mat解放
					imageMat.Dispose();
					imageMat = null;
					return result;
				}
			}
			return null;
		}
		/// <summary>
		/// ネットワークの解放
		/// </summary>
		protected void disposeNetwork()
		{
			if (network != null)
			{
				network.Dispose();
				network = null;
			}
		}
		/// <summary>
		/// 出力結果を整形する
		/// </summary>
		/// <param name="result">出力結果Mat</param>
		/// <returns>整形したデータ</returns>
		protected virtual NMSData post_process(Mat[] result,float confidence_threshold) { return null; }

#if TIME_MEASUREMENT
		private Stopwatch stopwatch = new Stopwatch();

		public long PreTime {  get; private set; }
		public long SetInputTime { get; private set; }

		public long ForwardTime { get; private set; }
		public long PostTime { get; private set; }
#endif // TIME_MEASUREMENT
		/// <summary>
		/// 推論を実行する
		/// </summary>
		/// <param name="image">画像(Bitmap)</param>
		/// <returns>推論結果</returns>
		/// <exception cref="Exception"></exception>
		public List<DetectionResult> Predict(ref Bitmap image,float confidence_threshold, float nms_threshold)
		{
			if (image != null)
			{
				if ((network != null) && (network.Empty() == false))
				{
					try
					{
#if TIME_MEASUREMENT
						stopwatch.Reset();
						stopwatch.Start();
#endif // TIME_MEASUREMENT
						// BLOBイメージを取得
						Mat imageMat = getBlob(ref image);

#if TIME_MEASUREMENT
						stopwatch.Stop();
						PreTime = stopwatch.ElapsedMilliseconds;

						stopwatch.Reset();
						stopwatch.Start();
#endif // TIME_MEASUREMENT

						// BLOBイメージを設定
						network.SetInput(imageMat);

#if TIME_MEASUREMENT
						stopwatch.Stop();
						SetInputTime = stopwatch.ElapsedMilliseconds;
#endif // TIME_MEASUREMENT
						// 出力を準備
						int output_layer = network.GetUnconnectedOutLayersNames().Count();

						Mat[] outputs = new Mat[output_layer];
						for (int i = 0; i < output_layer; i++)
							outputs[i] = new Mat();

#if TIME_MEASUREMENT
						stopwatch.Reset();
						stopwatch.Start();
#endif // TIME_MEASUREMENT

						// 推論実行
						network.Forward(outputs, network.GetUnconnectedOutLayersNames());

#if TIME_MEASUREMENT
						stopwatch.Stop();
						ForwardTime = stopwatch.ElapsedMilliseconds;

						stopwatch.Reset();
						stopwatch.Start();
#endif // TIME_MEASUREMENT
						// 結果を整形
						NMSData nmsData = post_process(outputs, confidence_threshold);
						if ((nmsData != null) && (nmsData.Count > 0))
						{	// NMSを実行
							CvDnn.NMSBoxes(nmsData.Rects, nmsData.Confidences, confidence_threshold, nms_threshold, out int[] indicates, 1.0F, 0);
							nmsData.SetIndicate(indicates);
#if TIME_MEASUREMENT
							stopwatch.Stop();
							PostTime = stopwatch.ElapsedMilliseconds;
#endif // TIME_MEASUREMENT
							// 結果を返す
							return nmsData.GetResult();
						}
#if TIME_MEASUREMENT
						stopwatch.Stop();
						PostTime = stopwatch.ElapsedMilliseconds;
#endif // TIME_MEASUREMENT
						return null;
					}
					catch (OpenCVException ocvExp)
					{   // OpenCV例外
						throw new Exception("Predict() OpenCV Exception", ocvExp);
					}
					catch (Exception ex)
					{	// その他例外
						throw new Exception("Predict() other Exception", ex);
					}
				}
				else
				{   // ネットワークが未設定
					throw new Exception("Network Not Loaded!");
				}
			}
			else
			{   // 画像が不正
				throw new Exception("Image is Null!");
			}
		}
		/// <summary>
		/// ターゲットを設定
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public bool SetTarget(Target target)
		{
			if ((network != null) && (network.Empty() == false))
			{
				try
				{
					network.SetPreferableTarget(target);
					return true;
				}
				catch (Exception ex) { throw new Exception("Exception", ex); }
			}
			return false;
		}
		/// <summary>
		/// ターゲットの取得
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public Target GetTarget()
		{
			if ((network != null) && (network.Empty() == false))
			{
				try
				{
					Target target = network.GetPreferableTarget();
					return target;
				}
				catch (Exception ex) { throw new Exception("Exception", ex); }
			}
			return Target.CPU;
		}
		/// <summary>
		/// バックエンドを設定
		/// </summary>
		/// <param name="backend"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public bool SetBackend(Backend backend)
		{
			if ((network != null) && (network.Empty() == false))
			{
				try
				{
					network.SetPreferableBackend(backend);
					return true;
				}
				catch (Exception ex) { throw new Exception("Exception", ex); }
			}
			return false;
		}
		/// <summary>
		/// バックエンドの取得
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public Backend GetBackend()
		{
			if ((network != null) && (network.Empty() == false))
			{
				try
				{
					Backend backend = network.GetPreferableBackend();
					return backend;
				}
				catch (Exception ex) { throw new Exception("Exception", ex); }
			}
			return Backend.DEFAULT;
		}
	}
}

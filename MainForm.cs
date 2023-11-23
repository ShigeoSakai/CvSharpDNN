using CVSharpDNN.ClassDefine;
using CVSharpDNN.Detection;
using CVSharpDNN.DetectionDefine;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSharpDNN
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// アプリケーション設定
		/// </summary>
		private AppConfig config = new AppConfig();

		/// <summary>
		/// 画像ファイルの拡張子
		/// </summary>
		private string[] imageExt =
		{
			".jpg",".jpeg",".bmp",".tif",".tiff",".png"
		};
		/// <summary>
		/// モデルのデフォルトディレクトリ
		/// </summary>
		private string ModelBaseFolder;
		/// <summary>
		/// 推論モジュールの定義
		/// </summary>
		private DetectionDefine.DetectionDefineList detectionDefList = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			// 設定値を反映
			TbImageFolder.Text = config.ImageFolder;

			// 推論モデルディレクトリ
			ModelBaseFolder = config.DetectionDefineDir;
			// 推論モデル
			string detectDefine = config.DetectionDefine;
			//string detectDefine = null;
			if ((detectDefine == null) || (detectDefine.Length == 0))
			{
				// デフォルトを生成
				makeDefaultDetectionDefList(ModelBaseFolder);
				// 生成した値を保存
				config.DetectionDefine = detectionDefList.ToString();
			}
			else
			{
				detectionDefList = new DetectionDefine.DetectionDefineList(detectDefine);
			}
			// コンボボックスを生成
			detectionDefList.MakeComboBox(ref CbDetectionDef, config.DetectionSelected);

			// クラス定義のコンボボックス生成
			ClassComboItem.MakeComboBox(ref CbClass);
			// デバイスサポートリストを取得
			CvDnn.SUPPORT_DEVICES[] list = CvDnn.GetAvailableBackends();
			if ((list != null) && (list.Length > 0))
				BackendAndTargetClass.MakeCombobox(ref CbBackendAndtarget, list);

			// 画像フォルダが有効なら開く
			if (Directory.Exists(TbImageFolder.Text))
				BtImageFolderOpen_Click(this, new EventArgs());

		}
		/// <summary>
		/// 選択された画像
		/// </summary>
		private Bitmap SelectImage = null;
		/// <summary>
		/// 推論オブジェクト
		/// </summary>
		private DetectionBase network = null;
		/// <summary>
		/// クラス定義
		/// </summary>
		private ClassDefineBase classDef = null;
		/// <summary>
		/// 推論実行済か
		/// </summary>
		private bool execDetection = false;
		/// <summary>
		/// 画像フォルダ選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtImageFolderSelect_Click(object sender, EventArgs e)
		{
			// フォルダ設定
			string folder = Directory.GetCurrentDirectory();
			if ((TbImageFolder.Text.Trim().Length > 0) && (Directory.Exists(TbImageFolder.Text.Trim())))
			{
				folder = TbImageFolder.Text.Trim();
			}
			// フォルダ選択ダイアログ
			FolderBrowserDialog fbd = new FolderBrowserDialog()
			{
				SelectedPath = folder,
				RootFolder = Environment.SpecialFolder.Desktop,
				Description = "画像が入っているフォルダを選択",
				ShowNewFolderButton = false,
			};
			// ダイアログを開く
			if (fbd.ShowDialog() == DialogResult.OK)
			{   // 画像ファイルを読み取る
				TbImageFolder.Text = fbd.SelectedPath;
				config.ImageFolder = fbd.SelectedPath;
				// 画像フォルダを開く
				BtImageFolderOpen_Click(sender, e);
			}
			// ダイアログ解放
			fbd.Dispose();
		}
		/// <summary>
		/// 画像フォルダを開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtImageFolderOpen_Click(object sender, EventArgs e)
		{
			// 指定フォルダが存在するか？
			if ((TbImageFolder.Text.Trim().Length > 0) && (Directory.Exists(TbImageFolder.Text.Trim())))
			{
				// リストをクリア
				LbImageFile.Items.Clear();
				foreach (string filename in Directory.EnumerateFiles(TbImageFolder.Text, "*.*", SearchOption.TopDirectoryOnly))
				{
					string ext = Path.GetExtension(filename);
					if (imageExt.Contains(ext))
					{
						LbImageFile.Items.Add(Path.GetFileName(filename));
					}
				}
				// 画像ファイルがあったら先頭を選択する
				if (LbImageFile.Items.Count > 0)
					LbImageFile.SelectedIndex = 0;
			}
		}
		/// <summary>
		/// 画像ファイルが選択された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LbImageFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((TbImageFolder.Text.Trim().Length > 0) && (Directory.Exists(TbImageFolder.Text.Trim())) &&
				(LbImageFile.SelectedIndex >= 0) && (LbImageFile.SelectedIndex < LbImageFile.Items.Count))
			{
				string imagePath = Path.Combine(TbImageFolder.Text, (string)LbImageFile.SelectedItem);
				if (File.Exists(imagePath))
				{
					if (SelectImage != null)
					{
						SelectImage.Dispose();
						SelectImage = null;
					}
					using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
					{
						SelectImage = new Bitmap(fs);
					}
					if (SelectImage != null)
					{
						ImagePictureBox.Image = SelectImage;
					}
				}
			}
		}

		/// <summary>
		/// 推論の読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtDetectionLoad_Click(object sender, EventArgs e)
		{
			// ネットワーク読み込み
			if (ReadNetwork(false))
			{
				// コンボボックスから定義を取得
				DetectionDefine.DetectionDefine detection = detectionDefList.GetComboItem(ref CbDetectionDef);
				if (detection != null)
				{   // 設定を保存
					config.DetectionSelected = detection.Name;
				}
			}
		}
		/// <summary>
		/// ネットワーク読み込み
		/// </summary>
		/// <param name="isReloadNetwork"></param>
		/// <returns></returns>
		private bool ReadNetwork(bool isReloadNetwork = false)
		{
			// コンボボックスから定義を取得
			DetectionDefine.DetectionDefine detection = detectionDefList.GetComboItem(ref CbDetectionDef);
			if (detection != null)
			{
				string modelFile = Path.Combine(detection.BaseDir, detection.ModelFilename);
				string configFile = null;
				if (detection.ConfigFilename != null)
					configFile = Path.Combine(detection.BaseDir, detection.ConfigFilename);
				Type detectionType = detection.DetectionType;
				Framework framework = detection.Framework.Framework;
				int networkSize = detection.NetworkSize;

				// 背景の使用を取得
				bool useBackground = false;
				UseBackgroundAttribute attr = detectionType.GetCustomAttribute<UseBackgroundAttribute>();
				if (attr != null)
				{
					useBackground = attr.UseBackground;
				}

				// モデルとConfigファイルチェック
				if ((File.Exists(modelFile)) &&
					((framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_ONLY) ||
					((configFile != null) && (File.Exists(configFile)))))
				{
					// クラスの生成
					if (classDef != null)
					{   // 生成していたら解放
						classDef.Dispose();
						classDef = null;
					}
					// クラス定義が有効か
					if ((CbClass.SelectedIndex >= 0) && (CbClass.SelectedItem is ClassComboItem item))
					{   // クラス定義を生成
						ConstructorInfo ctor = item.Type.GetConstructor(new Type[] { typeof(bool) });
						if (ctor != null)
							classDef = (ClassDefineBase)ctor.Invoke(new object[] { useBackground });
					}
					if (classDef != null)
					{
						// 既にネットワークが読み込まれていたら解放
						if (network != null)
						{
							network.Dispose();
							network = null;
						}
						// 推論モジュールの生成
						ConstructorInfo ctor = detectionType.GetConstructor(
							new Type[] { typeof(Framework), typeof(string), typeof(string), typeof(int), typeof(int) });
						if (ctor != null)
						{
							network = (DetectionBase)ctor.Invoke(new object[] {
								framework, modelFile, configFile, networkSize , classDef.Count()
							});
						}
						if ((network == null) || (ctor == null))
						{   // 推論モジュールの生成に失敗
							MessageBox.Show("推論ネットワークの生成に失敗しました。",
							"ネットワーク生成エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return false;
						}
						// バックエンドの取得
						Backend nowBackend = network.GetBackend();
						// ターゲットの取得
						Target nowTarget = network.GetTarget();
						if (isReloadNetwork)
						{
							CvDnn.SUPPORT_DEVICES backendTarget = BackendAndTargetClass.GetItem(ref CbBackendAndtarget);
							if (backendTarget != null)
							{
								if (backendTarget.backend != nowBackend)
									if (network.SetBackend(backendTarget.backend) == false)
									{
										MessageBox.Show(string.Format("バックエンド{0}指定に失敗しました。", backendTarget.backend),
										"バックエンド指定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return false;
									}
								if (backendTarget.target != nowTarget)
									if (network.SetTarget(backendTarget.target) == false)
									{
										MessageBox.Show(string.Format("ターゲット{0}指定に失敗しました。", backendTarget.target),
										"ターゲット指定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return false;
									}
							}
						}
						else
						{   // コンボボックスに設定
							BackendAndTargetClass.SetItem(ref CbBackendAndtarget, nowBackend, nowTarget);
						}
						// 推論実行をクリア
						execDetection = false;
						return true;
					}
					MessageBox.Show("不正なクラス定義の指定",
							"ネットワーク生成エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				MessageBox.Show("モデルファイルが読み込めません",
						"ネットワーク生成エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			MessageBox.Show("推論モジュールの定義が読み込めません",
					"ネットワーク生成エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}
		/// <summary>
		/// バックエンドとターゲットのクラス
		/// </summary>
		public class BackendAndTargetClass
		{
			/// <summary>
			/// バックエンド
			/// </summary>
			public Backend Backend { get; private set; }
			/// <summary>
			/// ターゲット
			/// </summary>
			public Target Target { get; private set; }
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="backend">バックエンド</param>
			/// <param name="target">ターゲット</param>
			public BackendAndTargetClass(Backend backend, Target target)
			{
				Backend = backend;
				Target = target;
			}
			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return Backend.ToString() + " - " + Target.ToString();
			}
			/// <summary>
			/// 同じかどうか
			/// </summary>
			/// <param name="obj">オブジェクト</param>
			/// <returns></returns>
			public override bool Equals(object obj)
			{
				if (obj is BackendAndTargetClass item)
					return (item.Backend == Backend) && (item.Target == Target);
				else if (obj is Backend backend)
					return (Backend == backend);
				else if (obj is Target target)
					return (Target == target);

				return base.Equals(obj);
			}
			/// <summary>
			/// ハッシュコードの取得
			/// </summary>
			/// <returns></returns>
			public override int GetHashCode() { return base.GetHashCode(); }
			/// <summary>
			/// コンボボックス作成
			/// </summary>
			/// <param name="comboBox">コンボボックス</param>
			/// <param name="list">サポートデバイスリスト</param>
			public static void MakeCombobox(ref ComboBox comboBox, CvDnn.SUPPORT_DEVICES[] list)
			{
				comboBox.Items.Clear();
				foreach (CvDnn.SUPPORT_DEVICES item in list)
					comboBox.Items.Add(new BackendAndTargetClass(item.backend, item.target));
				comboBox.SelectedIndex = 0;
			}
			/// <summary>
			/// コンボボックスで選択されているデバイスを取得
			/// </summary>
			/// <param name="comboBox">コンボボックス</param>
			/// <returns>サポートデバイス</returns>
			public static CvDnn.SUPPORT_DEVICES GetItem(ref ComboBox comboBox)
			{
				if ((comboBox.SelectedIndex >= 0) && (comboBox.SelectedItem is BackendAndTargetClass item))
					return new CvDnn.SUPPORT_DEVICES() { backend = item.Backend, target = item.Target };
				return new CvDnn.SUPPORT_DEVICES();
			}
			/// <summary>
			/// コンボボックスを指定された値にする
			/// </summary>
			/// <param name="combo"></param>
			/// <param name="backend"></param>
			/// <param name="target"></param>
			/// <returns></returns>
			public static bool SetItem(ref ComboBox combo, Backend backend, Target target)
			{
				int backend_match_index = -1;
				for (int index = 0; index < combo.Items.Count; index++)
				{
					if (combo.Items[index] is BackendAndTargetClass item)
					{
						if ((item.Backend == backend) && (item.Target == target))
						{
							combo.SelectedIndex = index; ;
							return true;
						}
						if ((item.Backend == backend) && (backend_match_index < 0))
							backend_match_index = index;
					}
				}
				if (backend_match_index >= 0)
				{
					combo.SelectedIndex = backend_match_index;
					return true;
				}
				return false;
			}
		}
		/// <summary>
		/// バックエンドとターゲットのコンボボックス変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbBackendAndtarget_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if ((network != null) && (CbBackendAndtarget.SelectedIndex >= 0) &&
				(CbBackendAndtarget.SelectedItem is BackendAndTargetClass item))
			{
				if (execDetection)
				{   // ネットワーク再読み込み
					ReadNetwork(true);
				}
				else
				{
					// バックエンドの取得
					Backend nowBackend = network.GetBackend();
					// ターゲットの取得
					Target nowTarget = network.GetTarget();
					bool backend_set = false;
					// バックエンドの設定
					if (item.Backend != nowBackend)
					{
						if (network.SetBackend(item.Backend) == false)
						{
							MessageBox.Show(string.Format("バックエンド{0}指定に失敗しました。", item.Backend),
							"バックエンド指定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						backend_set = true;
					}
					// ターゲットの設定
					if ((item.Target != nowTarget) || (backend_set))
					{
						if (network.SetTarget(item.Target) == false)
						{
							MessageBox.Show(string.Format("ターゲット{0}指定に失敗しました。", item.Target),
							"ターゲット指定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
				}
			}
		}
		/// <summary>
		/// 推論実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtDetectionExec_Click(object sender, EventArgs e)
		{
			if ((network != null) &&
				(SelectImage != null) &&
				(float.TryParse(TbConfidence.Text, out float conf_percent)) &&
					(float.TryParse(TbNms.Text, out float nms_percent)))
			{
				float confidence = conf_percent / 100.0F;
				float nms = nms_percent / 100.0F;

				// 結果を消す
				ImagePictureBox.ClearShape();
				TbResult.Text = "";
				TbResult.Refresh();

				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				// 推論実行
				List<DetectionResult> results = network.Predict(ref SelectImage, confidence, nms);

				stopwatch.Stop();
				TbResult.Text = string.Format("処理時間:{0}ms\r\n", stopwatch.ElapsedMilliseconds);

				if ((results != null) && (results.Count > 0))
				{
					// 図形をクリア
					int shapeNum = 1;
					foreach (DetectionResult item in results)
					{
						ImagePictureBox.AddShape(
							new Drawing.Shape.Rectangle(
								string.Format("[{2,3}]{0} {1:p}", classDef.getLabel(item.ClassID), item.Confidence, shapeNum),
								item.Rectangle,
								classDef.getColor(item.ClassID)
								)
							{
								ShowLable = true,
								LineWidth = 2.0F,
								IsEditable = false,
								LabelFill = true,
								LabelColor = classDef.getReverseColor(item.ClassID),
							});

						TbResult.AppendText(string.Format("[{0,3}] {1} {2:p}\r\n", shapeNum, classDef.getLabel(item.ClassID), item.Confidence));

						shapeNum++;
					}
				}
				// 推論実行済み
				execDetection = true;
			}
		}
		/// <summary>
		/// クラス定義のコンボボックスクラス
		/// </summary>
		private class ClassComboItem
		{
			/// <summary>
			/// 名前
			/// </summary>
			public string Name { get; private set; }
			/// <summary>
			/// オブジェクトType
			/// </summary>
			public Type Type { get; private set; }
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="name">名前</param>
			/// <param name="type">オブジェクトType</param>
			public ClassComboItem(string name, Type type)
			{
				Name = name;
				Type = type;
			}
			/// <summary>
			/// 文字列変換
			/// </summary>
			/// <returns>名前</returns>
			public override string ToString()
			{
				return Name;
			}
			/// <summary>
			/// コンボボックスに設定
			/// </summary>
			/// <param name="comboBox">コンボボックス</param>
			/// <returns>コンボボックスの件数</returns>
			public static int MakeComboBox(ref ComboBox comboBox)
			{
				comboBox.Items.Clear();
				Type myType = typeof(ClassDefine.ClassDefineBase);
				string DetectionNameSpace = myType.Namespace;
				// 指定した名前空間のクラスをすべて取得
				List<Type> theList = Assembly.GetExecutingAssembly().GetTypes()
						  .Where(t => (t.Namespace != null) && (t.Namespace.StartsWith(DetectionNameSpace)) &&
						  (t.IsSubclassOf(typeof(ClassDefine.ClassDefineBase))))
						  .ToList();
				foreach (Type type in theList)
				{
					Attribute nameAttr = type.GetCustomAttribute(typeof(DisplayNameAttribute));
					if (nameAttr != null)
					{
						string name = ((DisplayNameAttribute)nameAttr).DisplayName;
						comboBox.Items.Add(new ClassComboItem(name, type));
					}
				}
				if (comboBox.Items.Count > 0)
					comboBox.SelectedIndex = 0;
				return (comboBox.Items.Count);
			}
		}

		/// <summary>
		/// デフォルトの推論モデル定義
		/// </summary>
		/// <param name="baseFolder"></param>
		private void makeDefaultDetectionDefList(string baseFolder)
		{
			if (detectionDefList != null)
			{
				detectionDefList.Clear();
				detectionDefList = null;
			}
			string[] definition = {
				// SSD 300
				"SSD300\tCaffe SSD 300\tCaffe\tCaffeSSD\t" +
					baseFolder + "\t" +
					@"SSD\MSCOCO_SSD_300x300\VGG_coco_SSD_300x300_iter_400000.caffemodel" + "\t" +
					@"SSD\MSCOCO_SSD_300x300\deploy.prototxt" + "\t" +
					"300",
				// SSD 512
				"SSD512\tCaffe SSD 512\tCaffe\tCaffeSSD\t" +
					baseFolder + "\t" +
					@"SSD\MSCOCO_SSD_512x512\VGG_coco_SSD_512x512_iter_360000.caffemodel" + "\t" +
					@"SSD\MSCOCO_SSD_512x512\deploy.prototxt" + "\t" +
					"512",
				// YoloX m
				"YoLoXm ONNX\tYoLoX Middle Onnx\tONNX\tYoLoX\t" +
					baseFolder + "\t" +
					@"YoLoX\yolox_m.onnx" + "\t" +
					"\t" +
					"640",
				// YoLoX s
				"YoLoXs ONNX\tYoLoX Small Onnx\tONNX\tYoLoX\t" +
					baseFolder + "\t" +
					@"YoLoX\yolox_s.onnx" + "\t" +
					"\t" +
					"640",
				};
			string txt = "";
			foreach (string line in definition) txt += line + "\r\n";
			detectionDefList = new DetectionDefine.DetectionDefineList(txt);
		}
		/// <summary>
		/// 推論モデル編集
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtEditModel_Click(object sender, EventArgs e)
		{
			DetectionDefine.DetectionDefineForm detectionDefForm = new DetectionDefine.DetectionDefineForm(ModelBaseFolder, detectionDefList);
			System.Drawing.Point btPoint = PointToScreen(BtEditModel.Location);
			btPoint.X += BtEditModel.Width;
			detectionDefForm.Location = btPoint;
			// イベント追加
			detectionDefForm.SaveListEvent += DetectionDefForm_SaveListEvent;
			if (detectionDefForm.ShowDialog() == DialogResult.OK)
			{
			}
			// イベント削除
			detectionDefForm.SaveListEvent -= DetectionDefForm_SaveListEvent;
			// 解放
			detectionDefForm.Dispose();
		}
		/// <summary>
		/// 保存ボタンが押された
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="list"></param>
		private void DetectionDefForm_SaveListEvent(object sender, DetectionDefine.DetectionDefineList list)
		{
			// 定義リストを新規に作成
			detectionDefList.Clear();
			detectionDefList = null;
			detectionDefList = new DetectionDefine.DetectionDefineList(list);
			// 現在のコンボボックスの選択内容を保存
			string now_detection = null;
			DetectionDefine.DetectionDefine detection = detectionDefList.GetComboItem(ref CbDetectionDef);
			if (detection != null)
			{   // 設定を保存
				now_detection = detection.Name;
			}
			// コンボボックスを再生成
			detectionDefList.MakeComboBox(ref CbDetectionDef, now_detection);
			// 設定を保存する
			config.DetectionDefine = detectionDefList.ToString();
		}
		/// <summary>
		/// 繰り返し実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtExecAll_Click(object sender, EventArgs e)
		{
			if ((LbImageFile.Items.Count > 0) &&
				(float.TryParse(TbConfidence.Text, out float conf_percent)) &&
					(float.TryParse(TbNms.Text, out float nms_percent)) &&
					(int.TryParse(TbRepeatNum.Text, out int repeatNum)))
			{
				float confidence = conf_percent / 100.0F;
				float nms = nms_percent / 100.0F;

				List<string> files = new List<string>();
				int pos = LbImageFile.SelectedIndex;
				if (pos < 0) pos = 0;
				for (int i = 0; i < LbImageFile.Items.Count; i ++)
				{
					files.Add((string)LbImageFile.Items[pos]);
					pos++;
					if (pos >= LbImageFile.Items.Count)
						pos = 0;
				}
				string imageFolder = TbImageFolder.Text;
				DetectionDefine.DetectionDefine detection = detectionDefList.GetComboItem(ref CbDetectionDef);
				Type classDefType = typeof(MSCOCO);
				if (CbClass.SelectedItem is ClassComboItem item)
					classDefType = item.Type;
				CvDnn.SUPPORT_DEVICES backendTarget = BackendAndTargetClass.GetItem(ref CbBackendAndtarget);
				bool isReload = CbReloadModel.Checked;

				BtExecAll.Text = "実行中";
				BtExecAll.Enabled = false;

				await Task.Run(() => { ExecOneSeq(imageFolder,files,detection,classDefType,backendTarget.backend,backendTarget.target,confidence,nms,repeatNum,isReload); });

				BtExecAll.Text = "実行";
				BtExecAll.Enabled = true;

			}
		}

		/// <summary>
		/// ネットワークの読み込み
		/// </summary>
		/// <param name="detection"></param>
		/// <param name="classDef"></param>
		/// <param name="backend"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		private DetectionBase loadNetwork(DetectionDefine.DetectionDefine detection, ClassDefineBase classDef, Backend backend, Target target)
		{
			// モデルファイル
			string modelFile = Path.Combine(detection.BaseDir, detection.ModelFilename);
			// Framework種別
			string configFile = null;
			if (detection.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG)
				configFile = Path.Combine(detection.BaseDir, detection.ConfigFilename);
			// 推論ロジック
			Type detectionType = detection.DetectionType;
			// クラス数
			int classNum = classDef.Count();
			// ネットワークサイズ
			int networkSize = detection.NetworkSize;
			// コンストラクタ
			ConstructorInfo ctor = detectionType.GetConstructor(
							new Type[] { typeof(Framework), typeof(string), typeof(string), typeof(int), typeof(int) });
			if (ctor != null)
			{   // 推論モジュールの生成
				DetectionBase detectionNetwork = (DetectionBase)ctor.Invoke(new object[] {
								detection.Framework.Framework, modelFile, configFile, networkSize ,classNum
							});
				// バックエンドとターゲットの設定
				if ((detectionNetwork.SetBackend(backend)) &&
					(detectionNetwork.SetTarget(target)))
					return detectionNetwork;
				detectionNetwork.Dispose();
			}
			return null;
		}
		/// <summary>
		/// シーケンス実行
		/// </summary>
		/// <param name="imageFolder"></param>
		/// <param name="files"></param>
		/// <param name="detection"></param>
		/// <param name="classDefType"></param>
		/// <param name="backend"></param>
		/// <param name="target"></param>
		/// <param name="confidence"></param>
		/// <param name="nms"></param>
		/// <param name="repeatNum"></param>
		/// <param name="isReload"></param>
		private void ExecOneSeq(string imageFolder, List<string> files,
			DetectionDefine.DetectionDefine detection,
			Type classDefType, Backend backend, Target target,
			float confidence, float nms, int repeatNum, bool isReload)
		{
			// 計測用ストップウォッチ
			Stopwatch stopwatch = new Stopwatch();
			// 推論ロジック
			DetectionBase net = null;
			// 読み込んだ画像
			Bitmap image = null; ;
			// 実行時間
			double first_time = 0.0;
			double proc_time = 0.0;
			int proc_count = 0;
			// クラス定義
			ClassDefineBase classDef = null;
			ConstructorInfo classDefCtor = classDefType.GetConstructor(new Type[] { typeof(bool) });
			bool useBackground = false;
			UseBackgroundAttribute useBackgroundAttribute = detection.DetectionType.GetCustomAttribute<UseBackgroundAttribute>();
			if (useBackgroundAttribute != null)
				useBackground = useBackgroundAttribute.UseBackground;
			classDef = (ClassDefineBase)classDefCtor.Invoke(new object[] { useBackground });


			for (int loop_cnt = 0; loop_cnt < repeatNum; loop_cnt++)
			{
				if ((net == null) || (isReload))
				{
					if (net != null)
					{
						net.Dispose();
						net = null;
					}
					// ネットワークの読み込み
					net = loadNetwork(detection, classDef, backend, target);
				}
				// ネットワークが読み込めていないなら中断
				if (net == null) break;

				string imagePath = Path.Combine(imageFolder, files[0]);
				// 画像を読み込み
				if (image != null)
				{
					image.Dispose();
					image = null;
				}
				// ファイルから読み込み
				using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
				{
					image = new Bitmap(fs);
				}
				if (image == null) break;
				// 最初の推論を実行
				stopwatch.Reset();
				stopwatch.Start();
				// 推論実行
				List<DetectionResult> results = net.Predict(ref image, confidence, nms);
				// 処理時間集計
				stopwatch.Stop();
				first_time += (double)stopwatch.ElapsedMilliseconds;
				// 結果の更新
				UpdateResults(imagePath, results,classDef, stopwatch.ElapsedMilliseconds);

				// 2回目以降
				for(int i = 1; i < files.Count; i ++)
				{
					// 画像を読み込み
					if (image != null)
					{
						image.Dispose();
						image = null;
					}
					imagePath = Path.Combine(imageFolder, files[i]);
					// ファイルから読み込み
					using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
					{
						image = new Bitmap(fs);
					}
					if (image == null) break;
					// 最初の推論を実行
					stopwatch.Reset();
					stopwatch.Start();
					// 推論実行
					List<DetectionResult> results_2nd = net.Predict(ref image, confidence, nms);
					// 処理時間集計
					stopwatch.Stop();
					proc_time += (double)stopwatch.ElapsedMilliseconds;
					proc_count++;
					// 結果の更新
					UpdateResults(imagePath, results_2nd, classDef,stopwatch.ElapsedMilliseconds);
				}
			}
			// 推論ロジックの解放
			if (net != null)
			{
				net.Dispose();
				net = null;
			}
			// 集計結果表示
			updateResultTotal(repeatNum, files.Count, first_time, proc_time, proc_count);
		}
		/// <summary>
		/// 結果の更新
		/// </summary>
		/// <param name="image"></param>
		/// <param name="results"></param>
		/// <param name="classDef"></param>
		private void UpdateResults(string imagePath, List<DetectionResult> results, ClassDefineBase classDef,long ms)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { UpdateResults(imagePath, results,classDef,ms); });
				return;
			}

			TbResult.Text = string.Format("{0} ...{1}\r\n{2}ms",Path.GetFileName(imagePath) ,(results==null)?0:results.Count,ms);
			if (CbResultDraw.Checked)
			{
				// 図形のクリア
				ImagePictureBox.ClearShape();
				Bitmap image = null;
				using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
				{
					image = new Bitmap(fs);
				}
				if (image != null)
				{
					// 画像を追加
					ImagePictureBox.Image = image;
					if ((results != null) && (results.Count > 0))
					{
						// 図形をクリア
						int shapeNum = 1;
						foreach (DetectionResult item in results)
						{
							ImagePictureBox.AddShape(
								new Drawing.Shape.Rectangle(
									string.Format("[{2,3}]{0} {1:p}", classDef.getLabel(item.ClassID), item.Confidence, shapeNum),
									item.Rectangle,
									classDef.getColor(item.ClassID)
									)
								{
									ShowLable = true,
									LineWidth = 2.0F,
									IsEditable = false,
									LabelFill = true,
									LabelColor = classDef.getReverseColor(item.ClassID),
								});

							shapeNum++;
						}
					}
				}
			}
		}
		/// <summary>
		/// 集計結果の表示
		/// </summary>
		/// <param name="repeatNum"></param>
		/// <param name="imageNum"></param>
		/// <param name="first_time"></param>
		/// <param name="proc_time"></param>
		/// <param name="proc_count"></param>
		private void updateResultTotal(int repeatNum,int imageNum, double first_time,double proc_time,int proc_count)
		{
			if (InvokeRequired)
			{
				Invoke((MethodInvoker)delegate { updateResultTotal(repeatNum, imageNum, first_time, proc_time, proc_count); });
				return;
			}
			// 結果時間の表示
			TbResult.Text = string.Format("{0}x{1}={2}回実行\r\n最初:{3}ms\r\nそれ以外:{4}ms",
				repeatNum, imageNum, repeatNum * imageNum,
				first_time / (double)repeatNum,
				proc_time / (double)proc_count);
		}
		/// <summary>
		/// モデル定義の読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItemReadModel_Click(object sender, EventArgs e)
		{
			string folder = config.DetectionDefineDir;
			if ((detectionDefList != null) && (detectionDefList.Count > 0))
				folder = detectionDefList.GetBaseFolder();
			else if (detectionDefList == null)
				detectionDefList = new DetectionDefineList();

			string filename = "modeldef.tsv";
			OpenFileDialog ofd = new OpenFileDialog()
			{
				InitialDirectory = folder,
				FileName = filename,
				Multiselect = false,
				Filter = "CSV/TSVファイル|*.csv;*.tsv|全てのファイル|*.*",
				Title = "モデル定義ファイルを選択してください",
			};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				if (File.Exists(ofd.FileName))
				{
					int read_c = 0;
					bool isTsv = true;
					if (Path.GetExtension(filename) == ".csv")
						isTsv = false;
					using(StreamReader sr = new StreamReader(ofd.FileName,true)) 
					{
						read_c = detectionDefList.Load(sr,isTsv);
					}
					MessageBox.Show(string.Format("{0}件読み込みました。",read_c), "読み込み結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					MessageBox.Show(string.Format("'{0}'が読み込めません",filename), "読み込み結果", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			ofd.Dispose();
		}
		/// <summary>
		/// モデル定義保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItemSaveModel_Click(object sender, EventArgs e)
		{
			string folder = config.DetectionDefineDir;
			if ((detectionDefList != null) && (detectionDefList.Count > 0))
				folder = detectionDefList.GetBaseFolder();
			else
			{
				MessageBox.Show("書き込むデータがありません。" ,"エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string filename = "modeldef.tsv";
			SaveFileDialog sfd = new SaveFileDialog()
			{
				InitialDirectory = folder,
				FileName = filename,
				Filter = "TSVファイル|*.tsv|CSVファイル|*.csv|全てのファイル|*.*",
				CheckFileExists = false,
				Title = "保存するモデル定義ファイル名を指定"
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				bool isTsv = true;
				if (Path.GetExtension(filename) == ".csv")
					isTsv = false;
				int write_c = 0;
				using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
				{
					write_c = detectionDefList.Save(sw, isTsv);
				}
				MessageBox.Show(string.Format("{0}件書き込みました。", write_c), "書き込み結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			sfd.Dispose();
		}
		/// <summary>
		/// About表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItemAbount_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			about.ShowDialog();
			about.Dispose();
		}
	}
}

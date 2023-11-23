using CVSharpDNN.Detection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.AgastFeatureDetector;

namespace CVSharpDNN.DetectionDefine
{
	public class DetectionDefine
	{
		/// <summary>
		/// 名前
		/// </summary>
		public string Name {  get; set; }
		/// <summary>
		/// 説明文
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 基準ディレクトリ変更
		/// </summary>
		/// <param name="old_baseDir"></param>
		/// <param name="filename"></param>
		/// <param name="new_baseDir"></param>
		/// <returns></returns>
		private string BaseDirChange(string old_baseDir,string filename,string new_baseDir)
		{
			if ((filename != null) && (filename.Trim().Length > 0))
			{
				try
				{   // 一旦絶対パスに変換
					string fullpath = Utils.GetAbsolutePath(old_baseDir, filename);
					// 新しい基準ディレクトリの相対パスに変換
					string newFilename = Utils.GetRelativePath(new_baseDir, fullpath);
					// 変換したファイルを設定
					return newFilename;
				}
				catch { }
			}
			return null;
		}

		/// <summary>
		///  基準ディレクトリ
		/// </summary>
		private string baseDir_;
		/// <summary>
		///  基準ディレクトリ
		/// </summary>
		public string BaseDir
		{
			get { return baseDir_;}
			set
			{
				if ((value != null) && (value.Trim().Length != 0))
				{   // 設定値がある
					if ((baseDir_ == null) || (baseDir_.Trim().Length == 0))
					{   // 新規に設定
						ModelFilename = Utils.GetRelativePath(value, ModelFilename);
						ConfigFilename = Utils.GetRelativePath(value, ConfigFilename);
					}
					else
					{   // 基準ディレクトリの変更
						ModelFilename = BaseDirChange(baseDir_,ModelFilename,value);
						ConfigFilename = BaseDirChange(baseDir_,ConfigFilename,value);
					}
					baseDir_ = value;
				}
				else if ((baseDir_ != null) && (baseDir_.Trim().Length != 0))
				{	// nullに設定 ,,, 各ファイル名を絶対パスにする
					ModelFilename = Utils.GetAbsolutePath(baseDir_, ModelFilename);
					ConfigFilename = Utils.GetRelativePath(baseDir_,ConfigFilename);
				}
				baseDir_ = value;
			}
		}
		/// <summary>
		/// フレームワーク
		/// </summary>
		private FrameworkClass framework_;
		/// <summary>
		/// フレームワークの設定
		/// </summary>
		public FrameworkClass Framework
		{
			get { return framework_; }
			set 
			{
				if (framework_ != value)
				{
					framework_ = value;
					// モデルとConfigファイルを消す
					ModelFilename = ConfigFilename = null;
				}
			}
		}

		/// <summary>
		/// モデルファイル名（相対パス）
		/// </summary>
		public string ModelFilename { get; set; }

		/// <summary>
		/// Configファイル名
		/// </summary>
		private string configFilename_ = string.Empty;
		/// <summary>
		/// Configファイル名
		/// </summary>
		public string ConfigFilename { 
			get 
			{	
				if ((Framework != null) && (Framework.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG)) 
					return configFilename_;
				return null;
			}
			set
			{   // コンストラクタ種別がModel&Deployの時のみ設定
				if ((Framework != null) && (Framework.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG))
					configFilename_ = value;
			}
		}
		/// <summary>
		/// ネットワーク入力サイズ
		/// </summary>
		public int NetworkSize { get; set; }

		private Type detectionType_;
		/// <summary>
		/// 推論ロジックのオブジェクトタイプ
		/// </summary>
		public Type DetectionType 
		{
			get { return detectionType_; }
			set 
			{
				detectionType_ = value;
				SetNetworkSizes(detectionType_);
			} 
		}
		/// <summary>
		/// ネットワークサイズ候補
		/// </summary>
		public int[] NetworkSizeArray { get; private set; }

		/// <summary>
		/// ネットワークサイズ候補の取得
		/// </summary>
		/// <param name="detectionType"></param>
		/// <returns></returns>
		private bool SetNetworkSizes(Type detectionType)
		{
			NetworkSizeAttribute networkSizeAttr = detectionType.GetCustomAttribute<NetworkSizeAttribute>();
			if (networkSizeAttr != null)
			{
				NetworkSizeArray = networkSizeAttr.NetworkSize;
				return true;
			}
			return false;
		}

		/// <summary>
		/// ネットワークサイズ候補の取得
		/// </summary>
		/// <param name="detectionTypeString"></param>
		/// <param name="networkSize"></param>
		/// <returns></returns>
		private bool SetNetworkSize(string detectionTypeString, int networkSize)
		{
			// 名前からTypeを探す
			Type myType = typeof(Detection.DetectionBase);
			string DetectionNameSpace = myType.Namespace;
			// 指定した名前空間のクラスをすべて取得
			List<Type> theList = Assembly.GetExecutingAssembly().GetTypes()
					  .Where(t => (t.Namespace != null) && (t.Namespace.StartsWith(DetectionNameSpace)) &&
					  (t.IsSubclassOf(typeof(Detection.DetectionBase))))
					  .ToList();
			foreach (Type type in theList)
			{
				// 表示名の属性
				Attribute dispNameAttr = type.GetCustomAttribute(typeof(DisplayNameAttribute));
				if ((dispNameAttr != null) &&
					(((DisplayNameAttribute)dispNameAttr).DisplayName == detectionTypeString))
				{
					DetectionType = type;
					// ネットワークサイズ
					Attribute networkSizeAttr = type.GetCustomAttribute(typeof(NetworkSizeAttribute));
					if (networkSizeAttr != null)
					{
						NetworkSizeArray = ((NetworkSizeAttribute)networkSizeAttr).NetworkSize;
					}
					if (NetworkSizeArray != null)
					{
						foreach (int size in NetworkSizeArray)
							if (size == networkSize)
							{
								NetworkSize = networkSize;
								return true;
							}
					}
				}
			}
			return false;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="baseDir"></param>
		public DetectionDefine(string baseDir)
		{
			baseDir_ = baseDir;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="description">説明</param>
		/// <param name="detectionType">推論オブジェクトタイプ</param>
		/// <param name="baseDir">基準ディレクトリ</param>
		/// <param name="networkSize">ネットワーク入力サイズ</param>
		public DetectionDefine(string name, string description,FrameworkClass framework, Type detectionType, string baseDir, int networkSize):this(baseDir)
		{
			this.Name = name;
			this.Description = description;
			this.DetectionType = detectionType;
			this.Framework = framework;
			// 基準ディレクトリ
			baseDir_ = baseDir;
			// ネットワークサイズ
			Attribute networkSizeAttr = detectionType.GetCustomAttribute(typeof(NetworkSizeAttribute));
			if (networkSizeAttr != null)
			{
				NetworkSizeArray = ((NetworkSizeAttribute)networkSizeAttr).NetworkSize;
			}
			if (NetworkSizeArray != null)
			{
				foreach(int size in NetworkSizeArray)
					if (size == networkSize)
						NetworkSize = networkSize;
			}
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">名前</param>
		/// <param name="description">説明</param>
		/// <param name="detectionType">推論オブジェクトタイプ</param>
		/// <param name="modelFile">モデルファイル</param>
		/// <param name="configFile">Configファイル</param>
		/// <param name="networkSize">ネットワークサイズ</param>
		public DetectionDefine(string name, string description, FrameworkClass framework, Type detectionType, 
			string baseDir, string modelFile,string configFile, int networkSize) :
			this(name,description,framework, detectionType, baseDir, networkSize)
		{
			this.ModelFilename = modelFile;
			this.ConfigFilename = configFile;
		}
		public DetectionDefine(string name, string description,string frameworkString, string detectionTypeString, 
			string baseDir, string modelFilename, string configFilename, int networkSize):this(baseDir)
		{
			Name = name;
			Description = description;
			this.Framework = new FrameworkClass(frameworkString);
			// 推論オブジェクトのタイプとネットワークサイズ設定
			if (SetNetworkSize(detectionTypeString, networkSize))
			{
				// モデルファイル名
				ModelFilename = modelFilename;
				// Configファイル名
				ConfigFilename = configFilename;
			}
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="parameters">パラメータ文字列</param>
		public DetectionDefine(string[] parameters)
		{
			if ((parameters.Length >= 8) && (int.TryParse(parameters[7],out int networkSize)))
			{
				Name = parameters[0];
				Description = parameters[1];
				this.Framework = new FrameworkClass(parameters[2]);
				baseDir_ = parameters[4];
				if (SetNetworkSize(parameters[3], networkSize))
				{
					ModelFilename = parameters[5];
					if (this.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG)
						ConfigFilename = parameters[6];
				}
			}
		}

		/// <summary>
		/// コピーコンストラクタ
		/// </summary>
		/// <param name="src"></param>
		public DetectionDefine(DetectionDefine src)
		{
			Name = src.Name; Description = src.Description; baseDir_ = src.baseDir_;
			DetectionType = src.DetectionType;
			Framework = src.Framework;
			ModelFilename = src.ModelFilename;
			ConfigFilename = src.ConfigFilename;
			NetworkSize = src.NetworkSize;
		}

		public override string ToString()
		{
			// 表示名の属性
			Attribute dispNameAttr = DetectionType.GetCustomAttribute(typeof(DisplayNameAttribute));
			if (dispNameAttr != null)
			{
				return Name + "\t" + Description + "\t" + 
					Framework.ToString() + "\t" +
					((DisplayNameAttribute)dispNameAttr).DisplayName + "\t" +
					baseDir_ + "\t" + ModelFilename + "\t" + ConfigFilename + "\t" + NetworkSize.ToString();
			}
			return string.Empty;
		}
		public override bool Equals(object obj)
		{
			if (obj is DetectionDefine item)
			{
				if ((Name.Equals(item.Name)) && (DetectionType == item.DetectionType) &&
					(NetworkSize == item.NetworkSize) && (ModelFilename == item.ModelFilename) &&
					(this.Framework == item.Framework))
				{
					if (this.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_ONLY ) return true;
					return (ConfigFilename == item.ConfigFilename);
				}
				return false;
			}
			if (obj is string str)
			{
				return (Name == str);
			}
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}

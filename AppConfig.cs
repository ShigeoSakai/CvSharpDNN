using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN
{
	/// <summary>
	/// アプリケーション設定
	/// </summary>
	public sealed class AppConfig :ApplicationSettingsBase
	{
		/// <summary>
		/// 画像フォルダ
		/// </summary>
		[UserScopedSetting]
		public string ImageFolder
		{
			get
			{
				if ((this["ImageFolder"] != null) && (this["ImageFolder"] is string value))
					return value;
				return Directory.GetCurrentDirectory();
			}
			set
			{
				if ((this["ImageFolder"] == null) || (this["ImageFolder"].Equals(value) == false))
				{
					this["ImageFolder"] = value;
					Save();
				}
			}
		}
		/// <summary>
		/// 推論モデル保存ディレクトリ
		/// </summary>
		[UserScopedSetting]
		public string DetectionDefineDir
		{
			get
			{
				if ((this["DetectionDefineDir"] != null) && (this["DetectionDefineDir"] is string value))
					return value;
				string baseFolder = Directory.GetCurrentDirectory();
				return Path.GetFullPath(Path.Combine(baseFolder, @"..\..\model"));
			}
			set
			{
				if (value != null)
				{ 
					string folder = value;
					if (folder.Last() == '\\') folder = folder.Substring(0, folder.Last() - 1);
					if ((this["DetectionDefineDir"] == null) || (this["DetectionDefineDir"].Equals(folder) == false))
					{
						this["DetectionDefineDir"] = folder;
						Save();
					}
				}
				else if (this["DetectionDefineDir"] != null)
				{
					this["DetectionDefineDir"] = value;
					Save();
				}
			}
		}
		/// <summary>
		/// 推論モデル定義
		/// </summary>
		[UserScopedSetting]
		public string DetectionDefine
		{
			get
			{
				if ((this["DetectionDefine"] != null) && (this["DetectionDefine"] is string value))
					return value;
				return "";
			}
			set
			{
				if ((this["DetectionDefine"] == null) || (this["DetectionDefine"].Equals(value) == false))
				{
					this["DetectionDefine"] = value;
					Save();
				}
			}
		}
		/// <summary>
		/// 選択された推論モデル
		/// </summary>
		[UserScopedSetting]
		public string DetectionSelected
		{
			get
			{
				if ((this["DetectionSelected"] != null) && (this["DetectionSelected"] is string value))
					return value;
				return null;
			}
			set
			{
				if ((this["DetectionSelected"] == null) || (this["DetectionSelected"].Equals(value) == false))
				{
					this["DetectionSelected"] = value;
					Save();
				}
			}
		}
	}
}

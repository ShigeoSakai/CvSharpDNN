using CVSharpDNN.Detection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CVSharpDNN.DetectionDefine.DetectionDefineForm;
using static CVSharpDNN.DetectionDefine.DetectionDefineList;

namespace CVSharpDNN.DetectionDefine
{
	public partial class DetectionDefineControl : UserControl
	{
		/// <summary>
		/// 基準フォルダ
		/// </summary>
		private string baseFolder_ = "";
		/// <summary>
		/// 推論アイテム
		/// </summary>
		private DetectionDefine DetectionItem = null;
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DetectionDefineControl()
		{
			InitializeComponent();
			// フレームワークコンボボックス作成
			FrameworkClass.MakeComboBox(ref CbFramefork);
			// 推論コンボボックス作成
			DetectionComboItem.MakeComboBox(ref CbDetectionKind);
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="baseFolder">基準フォルダ指定</param>
		public DetectionDefineControl(string baseFolder) : this() { baseFolder_ = baseFolder; }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="item"></param>
		public DetectionDefineControl(DetectionDefine item) :this()
		{
			DetectionItem = item;
			// 項目をマッピングする
			MappingItem();

		}
		/// <summary>
		/// ネットワークサイズコンボボックスを作成
		/// </summary>
		/// <param name="comboBox"></param>
		/// <param name="setValues"></param>
		/// <param name="value"></param>
		private bool SetNetworkCombobox(ref ComboBox comboBox,int[] setValues,int value)
		{
			comboBox.Items.Clear();
			int select_index = -1;
			if ((setValues != null) && (setValues.Length > 0))
			{
				for (int index = 0; index < setValues.Length; index++)
				{
					comboBox.Items.Add(setValues[index]);
					if (setValues[index] == value)
						select_index = index;
				}
			}
			if (select_index < 0)
			{
				comboBox.SelectedIndex = 0;
				return false;
			}
			comboBox.SelectedIndex = select_index;
			return true;
		}
		/// <summary>
		/// アイテムをマッピング
		/// </summary>
		private void MappingItem()
		{
			if (DetectionItem != null)
			{
				baseFolder_ = DetectionItem.BaseDir;
				TbName.Text = DetectionItem.Name;
				TbDescription.Text = DetectionItem.Description;
				TbModelFile.Text = DetectionItem.ModelFilename;
				TbConfigFile.Text = DetectionItem.ConfigFilename;
				if (DetectionItem.Framework == null)
				{
					DetectionItem.Framework = (FrameworkClass)CbFramefork.SelectedItem;
				}
				// フレームワークコンボボックス
				CbFramefork.SelectedItem = DetectionItem.Framework;
				// フレームワークによる有効・無効
				TbConfigFile.Enabled = (DetectionItem.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG);
				BtConfigFileSelect.Enabled = (DetectionItem.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG);

				if (DetectionItem.DetectionType == null)
				{
					DetectionComboItem item = DetectionComboItem.GetItem(ref CbDetectionKind);
					if (item != null)
					{
						DetectionItem.DetectionType = item.DetectionType;
					}
				}
				// 推論コンボボックス
				DetectionDefineList.DetectionComboItem.SetItem(ref CbDetectionKind, DetectionItem.DetectionType);
				// ネットワークサイズコンボボックス
				if (SetNetworkCombobox(ref CbNetworkSize, DetectionItem.NetworkSizeArray, DetectionItem.NetworkSize) == false)
					DetectionItem.NetworkSize = (int)CbNetworkSize.SelectedItem;
				// ToolTipを設定
				FileToolTip.SetToolTip(TbConfigFile, TbConfigFile.Text);
				FileToolTip.SetToolTip(TbModelFile, TbModelFile.Text);

			}
		}
		public string DetectionName { get { return TbName.Text; } }
		public string Description { get { return TbDescription.Text; } }
		public FrameworkClass Framework
		{
			get 
			{ if (CbFramefork.SelectedItem is FrameworkClass item)
					return item;
				return null;
			}
		}
		public Type GetDetectionType 
		{ 
			get 
			{
				if (CbDetectionKind.SelectedItem is DetectionDefineList.DetectionComboItem item)
					return item.DetectionType;
				return null;
			} 
		}
		public string ModelFilename {  get { return TbModelFile.Text; } }
		public string ConfigFilename { get { return TbConfigFile.Text;} }
		public int NetworkSize 
		{
			get
			{
				if (CbNetworkSize.SelectedItem is int item)
					return item;
				return 0;
			}
		}
		/// <summary>
		/// フレームワークコンボボックスの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbFramefork_SelectionChangeCommitted(object sender, EventArgs e)
		{
			FrameworkClass item = FrameworkClass.GetComboBox(ref CbFramefork);
			if (item != null)
			{   // Configの有効無効
				TbConfigFile.Enabled = (item.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG);
				BtConfigFileSelect.Enabled = (item.Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_AND_CONFIG);
				// 設定を保存
				if (DetectionItem != null)
					DetectionItem.Framework = item;
				// 更新イベント発行
				OnUpdateEvent();
			}
		}
		/// <summary>
		/// 推論コンボボックスの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbDetectionKind_SelectionChangeCommitted(object sender, EventArgs e)
		{
			DetectionComboItem item =DetectionComboItem.GetItem(ref CbDetectionKind);
			if (item != null)
			{	// ネットワークサイズコンボボックスを変更
				if (CbNetworkSize.SelectedItem is int oldValue)
				{
					SetNetworkCombobox(ref CbNetworkSize,item.NetworkSizes,oldValue);
				}
				// 設定を保存
				if (DetectionItem != null)
					DetectionItem.DetectionType = item.DetectionType;
				// 更新イベント発行
				OnUpdateEvent();
			}
		}
		/// <summary>
		/// ネットワークサイズの更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CbNetworkSize_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (CbNetworkSize.SelectedItem is int value)
			{
				if (DetectionItem != null)
					DetectionItem.NetworkSize = value;
				// 更新イベント発行
				OnUpdateEvent();
			}
		}
		/// <summary>
		/// モデルファイルの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtModelFileSelect_Click(object sender, EventArgs e)
		{
			string fullpath = baseFolder_;
			if ((fullpath == null) || (fullpath.Trim().Length == 0) || 
				(Directory.Exists(fullpath) == false))
			{
				fullpath = Directory.GetCurrentDirectory();
				baseFolder_ = fullpath;
			}
			string folder;
			string filename = null;
			if (TbModelFile.Text.Trim().Length > 0)
			{
				fullpath = Path.Combine(fullpath, TbModelFile.Text);
				folder = Path.GetDirectoryName(fullpath);
				filename = Path.GetFileName(fullpath);
			}
			else
			{
				folder = fullpath;
			}
			string filter = "全てのファイル|*.*";
			if (DetectionItem != null)
			{	// 追加フィルター拡張子
				string fr_name = DetectionItem.Framework.ToString();
				string[] ext = DetectionItem.Framework.GetModelExt();
				if ((fr_name != null) && (ext != null))
				{
					string add_filter = fr_name + "ファイル|";
					foreach (string f_ext in ext)
					{
						add_filter += "*" + f_ext + ";";
						if (filename == null)
							filename = "model" + f_ext;
					}
					filter = add_filter.Substring(0, add_filter.Length - 1) + "|" + filter;
				}
			}
			if (filename == null)
				filename = "model";

			// ダイアログを生成
			OpenFileDialog ofd = new OpenFileDialog()
			{
				InitialDirectory = folder,
				FileName = filename,
				Filter = filter,
				CheckFileExists = true,
				CheckPathExists = true,
				Title = "モデルファイルを選択",
			};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string rel_path = Utils.GetRelativePath(baseFolder_, ofd.FileName);
				TbModelFile.Text = rel_path;
				if (DetectionItem != null)
					DetectionItem.ModelFilename = rel_path;
				// ToolTipを設定
				FileToolTip.SetToolTip(TbModelFile, TbModelFile.Text);

				// 更新イベント発行
				OnUpdateEvent();
			}
			ofd.Dispose();
		}
		/// <summary>
		/// Configファイルの指定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtConfigFileSelect_Click(object sender, EventArgs e)
		{
			string fullpath = baseFolder_;
			if ((fullpath == null) || (fullpath.Trim().Length == 0) ||
				(Directory.Exists(fullpath) == false))
			{
				fullpath = Directory.GetCurrentDirectory();
				baseFolder_ = fullpath;
			}
			string folder;
			string filename = null;
			if (TbConfigFile.Text.Trim().Length > 0)
			{
				fullpath = Path.Combine(fullpath, TbConfigFile.Text);
				folder = Path.GetDirectoryName(fullpath);
				filename = Path.GetFileName(fullpath);
			}
			else
			{
				folder = fullpath;
			}
			string filter = "全てのファイル|*.*";
			if (DetectionItem != null)
			{   // 追加フィルター拡張子
				string fr_name = DetectionItem.Framework.ToString();
				string[] ext = DetectionItem.Framework.GetConfigExt();
				if ((fr_name != null) && (ext != null))
				{
					string add_filter = fr_name + "ファイル|";
					foreach (string f_ext in ext)
					{
						add_filter += "*" + f_ext + ";";
						if (filename == null)
							filename = "config" + f_ext;
					}
					filter = add_filter.Substring(0, add_filter.Length - 1) + "|" + filter;
				}
			}
			if (filename == null)
				filename = "config";
			// ダイアログを生成
			OpenFileDialog ofd = new OpenFileDialog()
			{
				InitialDirectory = folder,
				FileName = filename,
				Filter = filter,
				CheckFileExists = true,
				CheckPathExists = true,
				Title = "Configファイルを選択",
			};
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				string rel_path = Utils.GetRelativePath(baseFolder_, ofd.FileName);
				TbConfigFile.Text = rel_path;
				if (DetectionItem != null)
					DetectionItem.ConfigFilename = rel_path;
				// ToolTipを設定
				FileToolTip.SetToolTip(TbModelFile, TbModelFile.Text);
				// 更新イベント発行
				OnUpdateEvent();
			}
			ofd.Dispose();
		}
		/// <summary>
		/// 削除イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		public delegate void DeleteEventHandler(object sender);
		/// <summary>
		/// 削除イベント
		/// </summary>
		public event DeleteEventHandler DeleteEvent;
		/// <summary>
		/// 削除イベント発行
		/// </summary>
		protected virtual void OnDeleteEvent()
		{
			DeleteEvent?.Invoke(this);
		}
		/// <summary>
		/// 更新イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		public delegate void UpdateEventHandler(object sender);
		/// <summary>
		/// 更新イベント
		/// </summary>
		public event UpdateEventHandler UpdateEvent;
		/// <summary>
		/// 更新イベント発行
		/// </summary>
		protected virtual void OnUpdateEvent()
		{
			UpdateEvent?.Invoke(this);
		}
		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
		{	// 削除イベントを発行
			OnDeleteEvent();
		}
		/// <summary>
		/// データの更新
		/// </summary>
		/// <param name="item"></param>
		public void UpdateDefine(DetectionDefine item)
		{
			DetectionItem = item;
			MappingItem();
		}
		/// <summary>
		/// データが有効かどうか
		/// </summary>
		public bool IsValid
		{
			get
			{
				bool result = false;
				if ((TbName.Text.Trim().Length > 0) && (TbDescription.Text.Trim().Length > 0) &&
					(TbModelFile.Text.Trim().Length > 0) && (Framework != null))
				{ 
					if (File.Exists(Path.Combine(baseFolder_, TbModelFile.Text))) 
					{
						if ((Framework.GetArgumentType() == ARGUMENT_TYPE.MODEL_ONLY) ||
							(File.Exists(Path.Combine(baseFolder_, TbConfigFile.Text))))
							result = true;
					}
				}
				return result;
			}
		}
		/// <summary>
		/// 編集内容を確定させる
		/// </summary>
		public void Refrect()
		{
			if (DetectionItem != null)
			{
				// 名前
				DetectionItem.Name = TbName.Text;
				// 説明
				DetectionItem.Description = TbDescription.Text;
				// 残りの項目は、更新されているはず
			}
		}
		
	}
}

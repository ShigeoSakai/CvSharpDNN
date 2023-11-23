using CVSharpDNN.Detection;
using OpenCvSharp.XImgProc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSharpDNN.DetectionDefine
{
	public partial class DetectionDefineForm : Form
	{
		/// <summary>
		/// リスト保存イベントハンドラ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="list"></param>
		public delegate void SaveListEventHandler(object sender, DetectionDefineList list);
		/// <summary>
		/// リスト保存イベント
		/// </summary>
		public event SaveListEventHandler SaveListEvent;
		/// <summary>
		/// リスト保存イベント発行
		/// </summary>
		/// <param name="list"></param>
		protected virtual void OnSaveListEvent(DetectionDefineList list)
		{
			SaveListEvent?.Invoke(this, list);
		}
		/// <summary>
		/// 推論ロジッククラス
		/// </summary>
		public class DetectionItem
		{
			/// <summary>
			/// 名前
			/// </summary>
			public string Name { get; private set; }
			/// <summary>
			/// 推論ロジックのオブジェクトType
			/// </summary>
			public Type Type { get; private set; }
			/// <summary>
			/// ネットワークサイズ
			/// </summary>
			public int[] NetworkSize { get; private set; }
			/// <summary>
			/// 背景の使用
			/// </summary>
			public bool UseBackground { get; private set; }
			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="name">名前</param>
			/// <param name="type">オブジェクトType</param>
			/// <param name="networkSize">ネットワークサイズ</param>
			/// <param name="useBackground">背景の使用</param>
			public DetectionItem(string name, Type type, int[] networkSize, bool useBackground)
			{
				Name = name;
				Type = type;
				NetworkSize = networkSize;
				UseBackground = useBackground;
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
					if (dispNameAttr != null)
					{
						string name = ((DisplayNameAttribute)dispNameAttr).DisplayName;
						// ネットワークサイズ
						Attribute networkSizeAttr = type.GetCustomAttribute(typeof(NetworkSizeAttribute));
						if (networkSizeAttr != null)
						{
							int[] networkSize = ((NetworkSizeAttribute)networkSizeAttr).NetworkSize;
							// 背景の使用
							bool useBackground = false;
							Attribute backgroundAttr = type.GetCustomAttribute(typeof(UseBackgroundAttribute));
							if (backgroundAttr != null)
								useBackground = ((UseBackgroundAttribute)backgroundAttr).UseBackground;
							// コンボボックスに追加
							comboBox.Items.Add(new DetectionItem(name, type, networkSize, useBackground));
						}
					}
				}
				if (comboBox.Items.Count > 0)
					comboBox.SelectedIndex = 0;
				return comboBox.Items.Count;
			}
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DetectionDefineForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 推論定義リスト
		/// </summary>
		private DetectionDefineList DetectionDefineList = null;
		public DetectionDefineForm(string baseDir,DetectionDefineList list):this()
		{
			// コピーのリストを持つ
			DetectionDefineList = new DetectionDefineList(list);
			TbBaseFolder.Text = baseDir;
			FLPanel.Controls.Clear();
			for(int index = 0; index < DetectionDefineList.Count; index++)
			{
				DetectionDefineControl ctrl = new DetectionDefineControl(DetectionDefineList[index]);
				ctrl.DeleteEvent += DetectionControl_DeleteEvent;
				ctrl.UpdateEvent += DetectionControl_UpdateEvent;
				FLPanel.Controls.Add(ctrl);
			}
		}
		/// <summary>
		/// データ更新イベント
		/// </summary>
		/// <param name="sender"></param>
		private void DetectionControl_UpdateEvent(object sender)
		{
			if (sender is DetectionDefineControl ctrl)
			{	// データは変わっているはずなので...

			}
		}
		/// <summary>
		/// 削除イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <exception cref="NotImplementedException"></exception>
		private void DetectionControl_DeleteEvent(object sender)
		{
			if ((DetectionDefineList != null) && (sender is DetectionDefineControl ctrl))
			{	// リストから削除
				if (DetectionDefineList.Remove(ctrl.DetectionName))
				{   // コントロールから削除
					ctrl.DeleteEvent -= DetectionControl_DeleteEvent;
					ctrl.UpdateEvent -= DetectionControl_UpdateEvent;
					FLPanel.Controls.Remove(ctrl);
				}
			}
		}
		/// <summary>
		/// 保存ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtSave_Click(object sender, EventArgs e)
		{   
			// データのチェック
			foreach(Control ctrl in FLPanel.Controls)
			{
				if (ctrl is DetectionDefineControl item)
				{
					if (!item.IsValid)
					{   // 有効でない項目がある。
						MessageBox.Show("無効な定義があります。", "定義エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						FLPanel.ScrollControlIntoView(ctrl);
						return;
					}
					// 内容を確定させる
					item.Refrect();
				}
			}
			// 保存イベント発行
			OnSaveListEvent(DetectionDefineList);
		}
		/// <summary>
		/// 閉じるボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}
		/// <summary>
		/// 基準フォルダクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtBaseFolderSelect_Click(object sender, EventArgs e)
		{
			string folder = Directory.GetCurrentDirectory();
			if ((TbBaseFolder.Text.Trim().Length > 0) && (Directory.Exists(TbBaseFolder.Text)))
				folder = TbBaseFolder.Text;
			FolderBrowserDialog fbd = new FolderBrowserDialog() 
			{ 
				RootFolder = Environment.SpecialFolder.Desktop,
				SelectedPath = folder,
				ShowNewFolderButton = false,
				Description = "モデルの基準フォルダを指定"
			};
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				TbBaseFolder.Text = fbd.SelectedPath;
				// List内を更新
				if (DetectionDefineList != null)
				{
					DetectionDefineList.UpdateBaseFolder(fbd.SelectedPath);
					// コントロールを更新
					int index = 0;
					foreach(Control ctrl in FLPanel.Controls)
					{
						if (ctrl is DetectionDefineControl item)
						{
							item.UpdateDefine(DetectionDefineList[index]);
							index++;
						}
					}
				}
			}
			fbd.Dispose();
		}
		/// <summary>
		/// 追加ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtAdd_Click(object sender, EventArgs e)
		{
			string folder = Directory.GetCurrentDirectory();
			if ((TbBaseFolder.Text.Trim().Length > 0) && (Directory.Exists(TbBaseFolder.Text)))
				folder = TbBaseFolder.Text;

			// 定義を生成
			DetectionDefine define = new DetectionDefine(folder);
			DetectionDefineList.Add(define);
			// コントロールを生成
			DetectionDefineControl ctrl = new DetectionDefineControl(define);
			// イベント追加
			ctrl.DeleteEvent += DetectionControl_DeleteEvent;
			ctrl.UpdateEvent += DetectionControl_UpdateEvent;
			// コントロールを追加
			FLPanel.Controls.Add(ctrl);
			FLPanel.ScrollControlIntoView(ctrl);
		}
	}
}

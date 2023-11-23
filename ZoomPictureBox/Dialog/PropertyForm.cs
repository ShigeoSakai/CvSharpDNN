using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing.Dialog
{
	/// <summary>
	/// プロパティフォーム
	/// </summary>
	public partial class PropertyForm : Form
	{
		/// <summary>
		/// 値変更イベント
		/// </summary>
		/// <param name="sender">送信元</param>
		/// <param name="name">変更プロパティ名(nullは全体)</param>
		/// <param name="newValue">変更後の値</param>
		/// <param name="oldValue">変更前の値</param>
		public delegate void ChangeValueEventHandler(object sender, string name, object newValue , object oldValue);
		/// <summary>
		/// 値変更イベント
		/// </summary>
		public event ChangeValueEventHandler ChangeValueEvent;
		/// <summary>
		/// 値変更イベント発行
		/// </summary>
		/// <param name="name">変更プロパティ名(nullは全体)</param>
		/// <param name="newValue">変更後の値</param>
		/// <param name="oldValue">変更前の値</param>
		protected virtual void OnChangeValue(string name, object newValue, object oldValue)
		{
			ChangeValueEvent?.Invoke(this,name , newValue, oldValue);
		}
		/// <summary>
		/// 変更記録辞書
		/// </summary>
		/// <remarks>
		/// key ... カテゴリー名::表示名
		///         カテゴリー名がない場合は、"その他"
		///         表示名がない場合は、プロパティ名
		/// value[0] ... 古い値(元に戻す時に使用)
		/// value[1] ... 新しい値
		/// </remarks>
		private Dictionary<string, object[]> updateDictionary = new Dictionary<string, object[]>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PropertyForm()
		{
			InitializeComponent();
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="text">フォーム名称</param>
		/// <param name="shape">図形オブジェクト</param>
		public PropertyForm(string text,Shape.BaseShape shape):this()
		{
			Text = text;
			PropGrid.SelectedObject = shape;
		}
		/// <summary>
		/// OKボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		/// <summary>
		/// 値の変更
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void PropGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			// キーを作成(親ラベル::子ラベル)
			string key = e.ChangedItem.Parent.Label + "::" + e.ChangedItem.Label;
			// 辞書を検索
			if (updateDictionary.ContainsKey(key))
			{   // 既に登録がある場合は、[1]側の内容を更新する
				object old_oldValue = updateDictionary[key][0];
				updateDictionary[key] = new object[] { old_oldValue, e.ChangedItem.Value };
			}
			else
			{	// 新規追加
				updateDictionary.Add(key, new object[] { e.OldValue, e.ChangedItem.Value });
			}
			// 送信するプロパティ名
			string send_property_name = e.ChangedItem.Label;
			// プロパティ名
			PropertyInfo info = GetProperty(key);
			if (info != null)
				send_property_name = info.Name;
			// イベント発行
			OnChangeValue(send_property_name, e.ChangedItem.Value, e.OldValue);
		}
		/// <summary>
		/// キーに一致するプロパティを検索する
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private PropertyInfo GetProperty(string key)
		{
			// キーを分割
			string[] key_strs = key.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
			if (key_strs.Length < 1)
				return null;
			// プロパティ一覧を取得
			PropertyInfo[] infos = PropGrid.SelectedObject.GetType().GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo info in infos)
			{
				// カテゴリー名
				string category_name = "その他";
				// カテゴリー名のカスタムアトリビュートを取得
				CategoryAttribute attr_cat = info.GetCustomAttribute<CategoryAttribute>();
				if (attr_cat != null)
					category_name = attr_cat.Category;
				// カテゴリー名が一致するか
				if ((key_strs.Length == 1) || (category_name == key_strs[0]))
				{	// 表示名
					string display_name = info.Name;
					// 表示名のカスタムアトリビュートを取得
					DisplayNameAttribute attr_name = info.GetCustomAttribute<DisplayNameAttribute>();
					if (attr_name != null)
						display_name = attr_name.DisplayName;
					int key_index = (key_strs.Length == 1) ? 0: 1;
					// 表示名が一致するか
					if (display_name == key_strs[key_index])
						return info;
				}
			}
			return null;
		}
		/// <summary>
		/// 元に戻すボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtRevert_Click(object sender, EventArgs e)
		{
			// 再描画が必要か
			bool isRefresh = false;
			// 辞書の件数チェック
			if (updateDictionary.Count > 0)
			{
				foreach (KeyValuePair<string,object[]> item in updateDictionary)
				{
					// プロパティを取得
					PropertyInfo info = GetProperty(item.Key);
					if (info != null)
					{	// 値を元に戻す
						info.SetValue(PropGrid.SelectedObject, item.Value[0]);
						// 再描画あり
						isRefresh = true;
					}
					else
					{   // プロパティが見つからない....
						Console.WriteLine("Form:{0} Property:{1} Not Found:{2}",
							Text, item.Key, PropGrid.SelectedObject.ToString());
					}
				}
				// 辞書をクリア
				updateDictionary.Clear();
			}
			if (isRefresh)
			{	// プロパティ再表示
				PropGrid.Refresh();
				// イベント発行
				OnChangeValue(null, null, null);
			}
		}
	}
}

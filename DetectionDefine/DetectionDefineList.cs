using CVSharpDNN.Detection;
using Drawing.Shape;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSharpDNN.DetectionDefine
{
	public class DetectionDefineList
	{
		/// <summary>
		/// 定義リスト
		/// </summary>
		private List<DetectionDefine> detectionList_ = new List<DetectionDefine>();
		/// <summary>
		/// 定義リストの取得
		/// </summary>
		public List<DetectionDefine> DetectionList
		{
			get { return detectionList_; }
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DetectionDefineList() { }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="str">設定文字列</param>
		public DetectionDefineList(string str)
		{
			string[] lines = str.Split(new string[] { "\r\n","\n"}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string line in lines)
			{
				detectionList_.Add(new DetectionDefine(line.Split(new char[] { '\t' }, StringSplitOptions.None)));
			}
		}
		public DetectionDefineList(DetectionDefineList src)
		{
			foreach(DetectionDefine detection in src.DetectionList)
			{
				detectionList_.Add(detection);
			}
		}
		/// <summary>
		/// インデクサ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public DetectionDefine this[int index]
		{
			get { if ((index >= 0) && (index < detectionList_.Count)) { return detectionList_[index]; } else { return null;} }
			set { if ((index >= 0) && (index < detectionList_.Count)) detectionList_[index] = value; }
		}
		/// <summary>
		/// 登録件数
		/// </summary>
		public int Count { get { return detectionList_.Count; } }
		/// <summary>
		/// リストに追加
		/// </summary>
		/// <param name="detectionDefine"></param>
		public int Add(DetectionDefine detectionDefine)
		{
			detectionList_.Add(detectionDefine);
			return detectionList_.Count;
		}
		/// <summary>
		/// リストの検索
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DetectionDefine Find(string name)
		{
			foreach(DetectionDefine item in detectionList_)
			{
				if (item.Name == name)
					return item;
			}
			return null;
		}
		public bool Remove(string name)
		{
			DetectionDefine item = Find(name);
			if (item != null)
			{
				detectionList_.Remove(item);
				return true;
			}
			return false;
		}
		public bool RemoveAt(int index)
		{
			if ((index >= 0) && (index <= detectionList_.Count))
			{
				detectionList_.RemoveAt(index);
				return true;
			}
			return false;
		}
		/// <summary>
		/// ファイルストリームから読み込み
		/// </summary>
		/// <param name="streamReader"></param>
		/// <param name="isTsv"></param>
		/// <returns></returns>
		public int Load(StreamReader streamReader,bool isTsv = true)
		{
			// リストをクリア
			Clear();
			char[] sep = new char[] { isTsv? '\t' : ',' };
			while(streamReader.EndOfStream == false)
			{
				string one_line = streamReader.ReadLine();
				if (one_line.StartsWith("#") == false)
				{
					detectionList_.Add(new DetectionDefine(one_line.Split(sep, StringSplitOptions.None)));
				}
			}
			return detectionList_.Count;
		}
		/// <summary>
		/// ファイルストリームに保存
		/// </summary>
		/// <param name="sw"></param>
		/// <param name="isTsv"></param>
		/// <returns></returns>
		public int Save(StreamWriter sw,bool isTsv = true)
		{
			int count = 0;
			foreach (DetectionDefine item in detectionList_)
			{
				string line = item.ToString();
				if (isTsv == false)
					line = line.Replace('\t', ',');
				sw.WriteLine(line);
				count++;
			}
			return count;
		}
		/// <summary>
		/// リストのクリア
		/// </summary>
		public void Clear()
		{
			detectionList_.Clear();
		}
		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string result = "";
			foreach (DetectionDefine item in detectionList_) { result += item.ToString() + "\r\n"; }
			return result;
		}
		/// <summary>
		/// 基準フォルダの更新
		/// </summary>
		/// <param name="folder"></param>
		public void UpdateBaseFolder(string folder)
		{
			if (detectionList_ != null)
			{
				foreach (DetectionDefine item in detectionList_)
				{
					item.BaseDir = folder;
				}
			}
		}
		/// <summary>
		/// 基準フォルダの取得
		/// </summary>
		/// <returns></returns>
		public string GetBaseFolder()
		{
			if ((detectionList_ != null) && (detectionList_.Count > 0))
				return detectionList_[0].BaseDir;
			else
				return null;

		}
		private class DefineListComboItem
		{
			public string Name { get; private set; }
			public DetectionDefine DetectionDefine { get; private set; }
			public DefineListComboItem(string name, DetectionDefine detectionDefine)
			{
				Name = name;
				DetectionDefine = detectionDefine;
			}
			public override string ToString()
			{
				return Name;
			}
		}

		public int MakeComboBox<T>(ref ComboBox combo,T value = null) where T:class
		{
			combo.Items.Clear();
			int select_index = 0;
			for(int index = 0; index < detectionList_.Count; index++)
			{
				combo.Items.Add(new DefineListComboItem(detectionList_[index].Description, detectionList_[index]));
				if ((value != null) && (detectionList_[index].Equals(value)))
					select_index = index;
			}
			if (combo.Items.Count > 0)
				combo.SelectedIndex = select_index;
			return select_index;
		}
		public DetectionDefine GetComboItem(ref ComboBox combo)
		{
			if ((combo.SelectedItem != null) && (combo.SelectedItem is DefineListComboItem item))
				return item.DetectionDefine;
			return null;
		}
		public bool SetComboItem<T>(ref ComboBox combo,T value)
		{
			if (combo.Items.Count > 0)
			{
				for(int index = 0; index <combo.Items.Count; index++)
				{
					if (combo.Items[index] is DefineListComboItem item)
					{
						if (item.Equals(value))
						{
							combo.SelectedIndex =index;
							return true;
						}
					}
				}

			}
			return false;
		}


		/// <summary>
		/// 推論ロジックのコンボボックスアイテム
		/// </summary>
		public class DetectionComboItem
		{
			public Type DetectionType { get; private set; }
			public string DetectionName { get; private set; }
			public int[] NetworkSizes { get; private set; }
			public DetectionComboItem(Type detectionType, string detectionName, int[] networkSizes)
			{
				DetectionType = detectionType;
				DetectionName = detectionName;
				NetworkSizes = networkSizes;
			}
			public override string ToString()
			{
				return DetectionName;
			}
			public static void MakeComboBox(ref ComboBox comboBox)
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
							// コンボボックスに追加
							comboBox.Items.Add(new DetectionComboItem(type, name, networkSize));
						}
					}
				}
				comboBox.SelectedIndex = 0;
			}
			/// <summary>
			/// コンボボックに値を設定
			/// </summary>
			/// <param name="combo"></param>
			/// <param name="detectionType"></param>
			public static void SetItem(ref ComboBox combo,Type detectionType)
			{
				for(int index = 0; index < combo.Items.Count; index ++)
				{
					if (combo.Items[index] is DetectionComboItem item)
					{
						if (item.DetectionType == detectionType)
						{
							combo.SelectedIndex = index; 
							return;
						}
					}
				}
				combo.SelectedIndex = 0;
			}
			public static DetectionComboItem GetItem(ref ComboBox combobox)
			{
				if (combobox.SelectedItem is DetectionComboItem item)
					return item;
				return null;
			}
		}
	}
}

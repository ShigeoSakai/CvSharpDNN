using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CVSharpDNN.Detection
{
	/// <summary>
	/// フレームワークの定義
	/// </summary>
	public enum Framework
	{
		[Description("Caffe"),Argument(ARGUMENT_TYPE.MODEL_AND_CONFIG),
			ModelExt(".caffemodel"),ConfigExt(".prototxt")]
		Caffe = 0,
		[Description("TesorFlow"), Argument(ARGUMENT_TYPE.MODEL_AND_CONFIG),
			ModelExt(".pb"),ConfigExt(".pbtxt")]
		Tesorflow,
		[Description("PyTorch"),Argument(ARGUMENT_TYPE.MODEL_ONLY),
			ModelExt(".t7;.net")]
		PyTorch,
		[Description("Darknet"),Argument(ARGUMENT_TYPE.MODEL_AND_CONFIG),
			ModelExt(".weights"),ConfigExt(".cfg")]
		Darknet,
		[Description("OpenVINO"),Argument(ARGUMENT_TYPE.MODEL_AND_CONFIG),
			ModelExt(".bin"),ConfigExt(".xml")]
		OpenVINO,
		[Description("ONNX"),Argument(ARGUMENT_TYPE.MODEL_ONLY),
			ModelExt(".onnx")]
		ONNX
	}
	/// <summary>
	/// 説明文の属性
	/// </summary>
	public class DescriptionAttribute : Attribute
	{
		public string Text { get; set; }
		public DescriptionAttribute(string text)
		{
			Text = text;
		}
	}
	/// <summary>
	/// 引数の種類
	/// </summary>
	public enum ARGUMENT_TYPE
	{
		/// <summary>
		/// モデルのみ
		/// </summary>
		MODEL_ONLY,
		/// <summary>
		/// モデルとConfig
		/// </summary>
		MODEL_AND_CONFIG
	}
	/// <summary>
	/// 引数の属性
	/// </summary>
	public class ArgumentAttribute : Attribute
	{
		public ARGUMENT_TYPE ArgumentType { get; set; }
		public ArgumentAttribute(ARGUMENT_TYPE argumentType)
		{
			ArgumentType = argumentType;
		}
	}
	/// <summary>
	/// モデルの拡張子属性
	/// </summary>
	public class ModelExtAttribute : Attribute
	{
		public string Extend {  get; set; }
		public ModelExtAttribute(string extend) 
		{
			Extend = extend;
		}
	}
	/// <summary>
	/// Configの拡張子属性
	/// </summary>
	public class ConfigExtAttribute : Attribute
	{
		public string Extend { get; set; }
		public ConfigExtAttribute(string extend)
		{
			Extend = extend;
		}	
	}
	public static class GetAttribute
	{
		private static TYPE getAttributeLocal<TYPE>(this Enum value) where TYPE:Attribute
		{
			// Typeの取得
			Type type = value.GetType();
			// Fieldの取得
			FieldInfo fieldInfo = type.GetField(value.ToString());
			if (fieldInfo != null)
			{
				TYPE result = fieldInfo.GetCustomAttribute(typeof(TYPE), false) as TYPE;
				return result;
			}
			return null;
		}
		private static TYPE[] getAttributesLocal<TYPE>(this Enum value) where TYPE : Attribute
		{
			// Typeの取得
			Type type = value.GetType();
			// Fieldの取得
			FieldInfo fieldInfo = type.GetField(value.ToString());
			if (fieldInfo != null)
			{
				TYPE[] result = fieldInfo.GetCustomAttributes(typeof(TYPE), false) as TYPE[];
				return result;
			}
			return null;
		}
		public static string GetDescription(this Enum value)
		{
			DescriptionAttribute attr = getAttributeLocal<DescriptionAttribute>(value);
			if (attr != null)
				return attr.Text;
			return null;
		}
		public static ARGUMENT_TYPE GetArgumentType(this Enum value)
		{
			ArgumentAttribute attr = getAttributeLocal<ArgumentAttribute>(value);
			if (attr != null)
				return attr.ArgumentType;
			return ARGUMENT_TYPE.MODEL_AND_CONFIG;
		}
		public static string[] GetModelExt(this Enum value)
		{
			ModelExtAttribute attr = getAttributeLocal<ModelExtAttribute>(value);
			if ((attr != null) && (attr.Extend != null))
			{
				return attr.Extend.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			}
			return null;
		}
		public static string[] GetConfigExt(this Enum value)
		{
			ConfigExtAttribute attr = getAttributeLocal<ConfigExtAttribute>(value);
			if ((attr != null) && (attr.Extend != null))
			{
				return attr.Extend.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			}
			return null;
		}
	}
	/// <summary>
	/// フレームワーククラス
	/// </summary>
	public class FrameworkClass
	{
		/// <summary>
		/// フレームワーク
		/// </summary>
		public Framework Framework { get; private set; }
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="framework">フレームワーク</param>
		public FrameworkClass(Framework framework)
		{
			Framework = framework;
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="frameworkName">文字列</param>
		public FrameworkClass(string frameworkName)
		{
			foreach(Framework item in Enum.GetValues(typeof(Framework)))
			{
				// Enum文字列変換で一致か？
				if (item.ToString().Equals(frameworkName))
				{
					Framework = item;
					return;
				}
				// 説明文で一致か？
				if ((item.GetDescription() != null) && (item.GetDescription().Equals(frameworkName)))
				{
					Framework = item;
					return;
				}
			}
		}
		/// <summary>
		/// 引数の種別を取得
		/// </summary>
		/// <returns></returns>
		public ARGUMENT_TYPE GetArgumentType() { return Framework.GetArgumentType(); }
		/// <summary>
		/// モデルの拡張子を取得
		/// </summary>
		/// <returns></returns>
		public string[] GetModelExt() { return Framework.GetModelExt(); }
		/// <summary>
		/// Configの拡張子を取得
		/// </summary>
		/// <returns></returns>
		public string[] GetConfigExt() {  return Framework.GetConfigExt(); }
		/// <summary>
		/// 文字列変換
		/// </summary>
		/// <returns></returns>
		public override string ToString() { return Framework.GetDescription(); }
		/// <summary>
		/// Equalオペレータ
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				if (obj is FrameworkClass frameworkClass)
					return (this.Framework == frameworkClass.Framework);
				if (obj is Framework framework)
					return framework == this.Framework;
				if (obj is string text)
					return this.Framework.ToString().Equals(text) ||
						this.Framework.GetDescription().Equals(text);
			}
			return base.Equals(obj);
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		/// <summary>
		/// フレームワーク一覧を取得
		/// </summary>
		/// <returns></returns>
		public static List<Framework> GetList()
		{
			List<Framework> list = new List<Framework>();
			foreach (Framework item in Enum.GetValues(typeof(Framework)))
				list.Add(item);
			return list;
		}
		/// <summary>
		/// コンボボックスに設定
		/// </summary>
		/// <param name="combo"></param>
		/// <param name="framework"></param>
		public static void MakeComboBox(ref ComboBox combo,Framework? framework = null)
		{
			combo.Items.Clear();
			foreach (Framework item in GetList())
				combo.Items.Add(new FrameworkClass(item));
			if (framework.HasValue)
				combo.SelectedItem = framework.Value;
			else
				combo.SelectedIndex = 0;
		}
		/// <summary>
		/// コンボボックスから値を取得
		/// </summary>
		/// <param name="combo"></param>
		/// <returns></returns>
		public static Framework? GetComboBoxFramework(ref ComboBox combo)
		{
			if (combo.SelectedItem is FrameworkClass item)
				return item.Framework;
			return null;
		}
		/// <summary>
		/// コンボボックスから値を取得
		/// </summary>
		/// <param name="combo"></param>
		/// <returns></returns>
		public static FrameworkClass GetComboBox(ref ComboBox combo)
		{
			if (combo.SelectedItem is FrameworkClass item)
				return item;
			return null;
		}
	}
}

using System;

namespace CVSharpDNN.Detection
{
	/// <summary>
	/// ネットワーク入力サイズ属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal class NetworkSizeAttribute : Attribute
	{
		public int[] NetworkSize;
		public NetworkSizeAttribute(params int[] networkSize) { NetworkSize = networkSize; }
	}
	/// <summary>
	/// 背景を使用するかどうかの属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal class UseBackgroundAttribute : Attribute
	{
		public bool UseBackground;
		public UseBackgroundAttribute(bool useBackGrouund) { UseBackground = useBackGrouund; }
	}
}

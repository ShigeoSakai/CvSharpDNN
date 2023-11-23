using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing.Shape
{
	/// <summary>
	/// 図形名のカスタム属性
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ShapeNameAttribute : Attribute
	{
		public string Name;
		public ShapeNameAttribute(string name) { this.Name = name; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drawing.Dialog
{
	public partial class AxisForm : Form
	{
		public float X
		{
			get
			{
				if (float.TryParse(TbX.Text, out float x))
					return x;
				return float.NaN;
			}
			set { TbX.Text = string.Format("{0:#,0.###}", value);}
		}
		public float Y
		{
			get
			{
				if (float.TryParse(TbY.Text, out float y))
					return y;
				return float.NaN;
			}
			set { TbY.Text = string.Format("{0:#,0.###}", value);}
		}
		public int IntX
		{
			get
			{
				if (float.TryParse(TbX.Text, out float x))
					return (int)x;
				return 0;
			}
			set { TbX.Text = string.Format("{0:#,0}", value); }
		}
		public int IntY
		{
			get
			{
				if (float.TryParse(TbY.Text, out float y))
					return (int)y;
				return 0;
			}
			set { TbY.Text = string.Format("{0:#,0}", value); }
		}
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AxisForm()
		{
			InitializeComponent();
		}
		public AxisForm(string title,int x,int y): this()
		{
			IntX = x;
			IntY = y;
			Text = title;
		}
		public AxisForm(string title, float x, float y) : this()
		{
			X = x;
			Y = y;
			Text = title;
		}
		/// <summary>
		/// OKボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		/// <summary>
		/// キャンセルボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}

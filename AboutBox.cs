using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSharpDNN
{
	/// <summary>
	/// このソフトについて...
	/// </summary>
	public partial class AboutBox : Form
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AboutBox()
		{
			InitializeComponent();
			// バージョン情報取得
			GetVersionInfo();
		}
		/// <summary>
		/// OKボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtOk_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// バージョン情報取得
		/// </summary>
		private void GetVersionInfo()
		{
			System.Reflection.Assembly asm =
					System.Reflection.Assembly.GetExecutingAssembly();
			//バージョンの取得
			Version ver= asm.GetName().Version;
			string ver_str = string.Format("ver {0}.{1}.{2}.{3}", ver.Major,ver.Minor,ver.Build,ver.Revision);
			FileInfo fi = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string build_date = string.Format("Build Date {0,2}/{1,2}/{2,2} {3:00}:{4:00}:{5:00}", 
				(fi.LastWriteTime.Year % 100), fi.LastWriteTime.Month, fi.LastWriteTime.Day,
				fi.LastWriteTime.Hour, fi.LastWriteTime.Minute, fi.LastWriteTime.Second);
			LbInfo.Text = ver_str + "  " + build_date;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CVSharpDNN
{
	public class Utils
	{
		/// <summary>
		/// Win32API 
		/// </summary>
		/// <param name="lpszDest"></param>
		/// <param name="lpszDir"></param>
		/// <param name="lpszFile"></param>
		/// <returns></returns>
		[DllImport("shlwapi.dll",CharSet = CharSet.Auto)]
		private static extern IntPtr PathCombine(
			[Out] StringBuilder lpszDest,
			string lpszDir,
			string lpszFile);
		/// <summary>
		/// Win32API
		/// </summary>
		/// <param name="pszPath"></param>
		/// <param name="pszFrom"></param>
		/// <param name="dwAttrFrom"></param>
		/// <param name="pszTo"></param>
		/// <param name="dwAttrTo"></param>
		/// <returns></returns>
		[DllImport("shlwapi.dll",CharSet = CharSet.Auto)]
		private static extern bool PathRelativePathTo(
			 [Out] StringBuilder pszPath,
			 [In] string pszFrom,
			 [In] System.IO.FileAttributes dwAttrFrom,
			 [In] string pszTo,
			 [In] System.IO.FileAttributes dwAttrTo
		);
		/// <summary>
		/// 相対パスから絶対パスを取得します。
		/// </summary>
		/// <param name="basePath">基準とするパス。</param>
		/// <param name="relativePath">相対パス。</param>
		/// <returns>絶対パス。</returns>
		public static string GetAbsolutePath(string basePath, string relativePath)
		{
			StringBuilder sb = new StringBuilder(2048);
			IntPtr res = PathCombine(sb, basePath, relativePath);
			if (res == IntPtr.Zero)
			{
				throw new Exception("絶対パスの取得に失敗しました。");
			}
			return sb.ToString();
		}
		/// <summary>
		/// 絶対パスから相対パスを取得します。
		/// </summary>
		/// <param name="basePath">基準とするフォルダのパス。</param>
		/// <param name="absolutePath">相対パス。</param>
		/// <returns>絶対パス。</returns>
		public static string GetRelativePath(string basePath, string absolutePath)
		{
			StringBuilder sb = new StringBuilder(260);
			bool res = PathRelativePathTo(sb,
				basePath, System.IO.FileAttributes.Directory,
				absolutePath, System.IO.FileAttributes.Normal);
			if (!res)
			{
				throw new Exception("相対パスの取得に失敗しました。");
			}
			string result = sb.ToString();
			if (result.StartsWith(".\\"))
				result = result.Substring(2);
			return result;
		}
	}
}

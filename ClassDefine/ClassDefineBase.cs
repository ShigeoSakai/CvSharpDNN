using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.ClassDefine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class ClassDefineBase :IDisposable
    {
        /// <summary>
        /// RGBの構造
        /// </summary>
        protected struct RGB
        {
            public int R;
            public int G;
            public int B;
            public RGB(int r, int g, int b)
            {
                R = r; G = g; B = b;
            }
            public Color GetColor()
            {
                return Color.FromArgb(R, G, B);
            }
            public Color ReverseColor()
            {
                return Color.FromArgb(255 - R, 255 - G, 255 - B);
            }
        };

        // 色定義
        protected RGB[] colors;

        // ラベルの配列
        protected String[] labels;

        /// <summary>
        /// 背景の使用
        /// </summary>
        protected bool UseBackground = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="useBackground">背景を使用するか</param>
		public ClassDefineBase(bool useBackground = false)
        {
            UseBackground = useBackground;
        }

        /// <summary>
        /// ラベル名を求める
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>ラベル名</returns>
        public String getLabel(int index)
        {
            int add_index = 0;
            if (UseBackground == false)
                add_index = 1;
            if ((index >= 0) && (index < labels.Length - add_index))
            {
                return labels[index + add_index];
            }
            return "未定義";
        }
		/// <summary>
		/// ラベル数を取得
		/// </summary>
		/// <returns>ラベル数</returns>
		public int Count()
        {
            return labels.Length - ((UseBackground) ? 0:1);
        }
		/// <summary>
		/// 色を取得
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Color getColor(int index)
        {
			int add_index = 0;
			if (UseBackground == false)
				add_index = 1;
			if ((index >= 0) && (index < colors.Length - add_index))
            {
                return colors[index + add_index].GetColor();
            }
            return Color.FromArgb(0, 0, 0);
        }
		/// <summary>
		/// 反転色を取得
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Color getReverseColor(int index)
        {
			int add_index = 0;
			if (UseBackground == false)
				add_index = 1;
			if ((index >= 0) && (index < colors.Length - add_index))
            {
                return colors[index + add_index].ReverseColor();
            }
            return Color.FromArgb(255, 255, 255);
        }
        /// <summary>
        /// 解放
        /// </summary>
		public virtual void Dispose() { }
	}
}

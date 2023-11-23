using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    /// <summary>
    /// マーカー種類
    /// </summary>
    public enum MARKER
    {
        [Description("十字(+)")]
        PLUS,
        [Description("✕印(+)")]
        CROSS,
        [Description("○印(○)")]
        CIRCLE,
        [Description("●印(●)")]
        CIRCLE_FILL,
        [Description("星印(＊)")]
        STAR,
        [Description("点(・)")]
        POINT,
    }
    /// <summary>
    /// ラベル表示位置
    /// </summary>
    public enum LABEL_POSITION
    {
        CENTER = 0x022,
        TOP_LEFT = 0x011,
        TOP_LEFT_INNER = 0x111,
        TOP_CENTER = 0x012,
        TOP_CENTER_INNER = 0x112,
        TOP_RIGHT = 0x013,
        TOP_RIGHT_INNER = 0x113,
        BOTTOM_LEFT = 0x131,
        BOTTOM_LEFT_INNER = 0x031,
        BOTTOM_CENTER = 0x132,
        BOTTOM_CENTER_INNER = 0x032,
        BOTTOM_RIGHT = 0x133,
        BOTTOM_RIGHT_INNER = 0x033,
    }
	/// <summary>
	/// 線の終端形状
	/// </summary>
	public enum LINECAP_SHAPE
	{
		NONE = 0,
		RECTANGLE,
		DIAMOND,
		ARROW,
		CIRCLE
	}
}

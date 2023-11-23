using System.Drawing;
namespace ColorMap.Sequential2
{
	/// <summary>
	/// Pinkカラーマップ
	/// </summary>
	public class PinkColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PinkColorMap()
		{
			colorMap = pink_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] pink_map_ =
		{
			Color.FromArgb(30,0,0),		// 0
			Color.FromArgb(34,6,6),		// 1
			Color.FromArgb(39,12,12),		// 2
			Color.FromArgb(44,19,19),		// 3
			Color.FromArgb(49,25,25),		// 4
			Color.FromArgb(53,28,28),		// 5
			Color.FromArgb(56,31,31),		// 6
			Color.FromArgb(60,34,34),		// 7
			Color.FromArgb(63,36,36),		// 8
			Color.FromArgb(66,38,38),		// 9
			Color.FromArgb(69,41,41),		// 10
			Color.FromArgb(72,43,43),		// 11
			Color.FromArgb(74,45,45),		// 12
			Color.FromArgb(77,46,46),		// 13
			Color.FromArgb(79,48,48),		// 14
			Color.FromArgb(82,50,50),		// 15
			Color.FromArgb(84,52,52),		// 16
			Color.FromArgb(87,53,53),		// 17
			Color.FromArgb(89,55,55),		// 18
			Color.FromArgb(91,56,56),		// 19
			Color.FromArgb(93,58,58),		// 20
			Color.FromArgb(95,59,59),		// 21
			Color.FromArgb(97,61,61),		// 22
			Color.FromArgb(99,62,62),		// 23
			Color.FromArgb(101,63,63),		// 24
			Color.FromArgb(103,65,65),		// 25
			Color.FromArgb(105,66,66),		// 26
			Color.FromArgb(107,67,67),		// 27
			Color.FromArgb(109,68,68),		// 28
			Color.FromArgb(110,70,70),		// 29
			Color.FromArgb(112,71,71),		// 30
			Color.FromArgb(114,72,72),		// 31
			Color.FromArgb(116,73,73),		// 32
			Color.FromArgb(117,74,74),		// 33
			Color.FromArgb(119,75,75),		// 34
			Color.FromArgb(121,77,77),		// 35
			Color.FromArgb(122,78,78),		// 36
			Color.FromArgb(124,79,79),		// 37
			Color.FromArgb(125,80,80),		// 38
			Color.FromArgb(127,81,81),		// 39
			Color.FromArgb(128,82,82),		// 40
			Color.FromArgb(130,83,83),		// 41
			Color.FromArgb(131,84,84),		// 42
			Color.FromArgb(133,85,85),		// 43
			Color.FromArgb(134,86,86),		// 44
			Color.FromArgb(136,87,87),		// 45
			Color.FromArgb(137,88,88),		// 46
			Color.FromArgb(139,89,89),		// 47
			Color.FromArgb(140,90,90),		// 48
			Color.FromArgb(141,91,91),		// 49
			Color.FromArgb(143,92,92),		// 50
			Color.FromArgb(144,93,93),		// 51
			Color.FromArgb(146,94,94),		// 52
			Color.FromArgb(147,94,94),		// 53
			Color.FromArgb(148,95,95),		// 54
			Color.FromArgb(150,96,96),		// 55
			Color.FromArgb(151,97,97),		// 56
			Color.FromArgb(152,98,98),		// 57
			Color.FromArgb(153,99,99),		// 58
			Color.FromArgb(155,100,100),		// 59
			Color.FromArgb(156,100,100),		// 60
			Color.FromArgb(157,101,101),		// 61
			Color.FromArgb(158,102,102),		// 62
			Color.FromArgb(160,103,103),		// 63
			Color.FromArgb(161,104,104),		// 64
			Color.FromArgb(162,105,105),		// 65
			Color.FromArgb(163,105,105),		// 66
			Color.FromArgb(165,106,106),		// 67
			Color.FromArgb(166,107,107),		// 68
			Color.FromArgb(167,108,108),		// 69
			Color.FromArgb(168,109,109),		// 70
			Color.FromArgb(169,109,109),		// 71
			Color.FromArgb(170,110,110),		// 72
			Color.FromArgb(172,111,111),		// 73
			Color.FromArgb(173,112,112),		// 74
			Color.FromArgb(174,112,112),		// 75
			Color.FromArgb(175,113,113),		// 76
			Color.FromArgb(176,114,114),		// 77
			Color.FromArgb(177,115,115),		// 78
			Color.FromArgb(178,115,115),		// 79
			Color.FromArgb(179,116,116),		// 80
			Color.FromArgb(180,117,117),		// 81
			Color.FromArgb(182,118,118),		// 82
			Color.FromArgb(183,118,118),		// 83
			Color.FromArgb(184,119,119),		// 84
			Color.FromArgb(185,120,120),		// 85
			Color.FromArgb(186,120,120),		// 86
			Color.FromArgb(187,121,121),		// 87
			Color.FromArgb(188,122,122),		// 88
			Color.FromArgb(189,123,123),		// 89
			Color.FromArgb(190,123,123),		// 90
			Color.FromArgb(191,124,124),		// 91
			Color.FromArgb(192,125,125),		// 92
			Color.FromArgb(193,125,125),		// 93
			Color.FromArgb(194,127,126),		// 94
			Color.FromArgb(194,128,127),		// 95
			Color.FromArgb(194,130,127),		// 96
			Color.FromArgb(195,131,128),		// 97
			Color.FromArgb(195,133,129),		// 98
			Color.FromArgb(196,134,129),		// 99
			Color.FromArgb(196,136,130),		// 100
			Color.FromArgb(197,137,131),		// 101
			Color.FromArgb(197,139,131),		// 102
			Color.FromArgb(197,140,132),		// 103
			Color.FromArgb(198,141,132),		// 104
			Color.FromArgb(198,143,133),		// 105
			Color.FromArgb(199,144,134),		// 106
			Color.FromArgb(199,145,134),		// 107
			Color.FromArgb(200,147,135),		// 108
			Color.FromArgb(200,148,136),		// 109
			Color.FromArgb(200,149,136),		// 110
			Color.FromArgb(201,151,137),		// 111
			Color.FromArgb(201,152,137),		// 112
			Color.FromArgb(202,153,138),		// 113
			Color.FromArgb(202,155,139),		// 114
			Color.FromArgb(203,156,139),		// 115
			Color.FromArgb(203,157,140),		// 116
			Color.FromArgb(203,158,141),		// 117
			Color.FromArgb(204,160,141),		// 118
			Color.FromArgb(204,161,142),		// 119
			Color.FromArgb(205,162,142),		// 120
			Color.FromArgb(205,163,143),		// 121
			Color.FromArgb(205,164,144),		// 122
			Color.FromArgb(206,166,144),		// 123
			Color.FromArgb(206,167,145),		// 124
			Color.FromArgb(207,168,145),		// 125
			Color.FromArgb(207,169,146),		// 126
			Color.FromArgb(208,170,146),		// 127
			Color.FromArgb(208,171,147),		// 128
			Color.FromArgb(208,173,148),		// 129
			Color.FromArgb(209,174,148),		// 130
			Color.FromArgb(209,175,149),		// 131
			Color.FromArgb(210,176,149),		// 132
			Color.FromArgb(210,177,150),		// 133
			Color.FromArgb(210,178,150),		// 134
			Color.FromArgb(211,179,151),		// 135
			Color.FromArgb(211,180,152),		// 136
			Color.FromArgb(212,181,152),		// 137
			Color.FromArgb(212,182,153),		// 138
			Color.FromArgb(212,184,153),		// 139
			Color.FromArgb(213,185,154),		// 140
			Color.FromArgb(213,186,154),		// 141
			Color.FromArgb(214,187,155),		// 142
			Color.FromArgb(214,188,155),		// 143
			Color.FromArgb(214,189,156),		// 144
			Color.FromArgb(215,190,157),		// 145
			Color.FromArgb(215,191,157),		// 146
			Color.FromArgb(216,192,158),		// 147
			Color.FromArgb(216,193,158),		// 148
			Color.FromArgb(216,194,159),		// 149
			Color.FromArgb(217,195,159),		// 150
			Color.FromArgb(217,196,160),		// 151
			Color.FromArgb(217,197,160),		// 152
			Color.FromArgb(218,198,161),		// 153
			Color.FromArgb(218,199,161),		// 154
			Color.FromArgb(219,200,162),		// 155
			Color.FromArgb(219,201,162),		// 156
			Color.FromArgb(219,202,163),		// 157
			Color.FromArgb(220,203,163),		// 158
			Color.FromArgb(220,204,164),		// 159
			Color.FromArgb(221,205,164),		// 160
			Color.FromArgb(221,206,165),		// 161
			Color.FromArgb(221,207,165),		// 162
			Color.FromArgb(222,208,166),		// 163
			Color.FromArgb(222,209,166),		// 164
			Color.FromArgb(222,209,167),		// 165
			Color.FromArgb(223,210,167),		// 166
			Color.FromArgb(223,211,168),		// 167
			Color.FromArgb(224,212,168),		// 168
			Color.FromArgb(224,213,169),		// 169
			Color.FromArgb(224,214,170),		// 170
			Color.FromArgb(225,215,170),		// 171
			Color.FromArgb(225,216,170),		// 172
			Color.FromArgb(226,217,171),		// 173
			Color.FromArgb(226,218,171),		// 174
			Color.FromArgb(226,219,172),		// 175
			Color.FromArgb(227,220,172),		// 176
			Color.FromArgb(227,220,173),		// 177
			Color.FromArgb(227,221,173),		// 178
			Color.FromArgb(228,222,174),		// 179
			Color.FromArgb(228,223,174),		// 180
			Color.FromArgb(229,224,175),		// 181
			Color.FromArgb(229,225,175),		// 182
			Color.FromArgb(229,226,176),		// 183
			Color.FromArgb(230,227,176),		// 184
			Color.FromArgb(230,227,177),		// 185
			Color.FromArgb(230,228,177),		// 186
			Color.FromArgb(231,229,178),		// 187
			Color.FromArgb(231,230,178),		// 188
			Color.FromArgb(231,231,179),		// 189
			Color.FromArgb(232,232,179),		// 190
			Color.FromArgb(232,232,180),		// 191
			Color.FromArgb(233,233,182),		// 192
			Color.FromArgb(233,233,183),		// 193
			Color.FromArgb(233,233,185),		// 194
			Color.FromArgb(234,234,186),		// 195
			Color.FromArgb(234,234,187),		// 196
			Color.FromArgb(234,234,189),		// 197
			Color.FromArgb(235,235,190),		// 198
			Color.FromArgb(235,235,191),		// 199
			Color.FromArgb(235,235,193),		// 200
			Color.FromArgb(236,236,194),		// 201
			Color.FromArgb(236,236,195),		// 202
			Color.FromArgb(237,237,196),		// 203
			Color.FromArgb(237,237,198),		// 204
			Color.FromArgb(237,237,199),		// 205
			Color.FromArgb(238,238,200),		// 206
			Color.FromArgb(238,238,201),		// 207
			Color.FromArgb(238,238,203),		// 208
			Color.FromArgb(239,239,204),		// 209
			Color.FromArgb(239,239,205),		// 210
			Color.FromArgb(239,239,206),		// 211
			Color.FromArgb(240,240,208),		// 212
			Color.FromArgb(240,240,209),		// 213
			Color.FromArgb(240,240,210),		// 214
			Color.FromArgb(241,241,211),		// 215
			Color.FromArgb(241,241,212),		// 216
			Color.FromArgb(242,242,214),		// 217
			Color.FromArgb(242,242,215),		// 218
			Color.FromArgb(242,242,216),		// 219
			Color.FromArgb(243,243,217),		// 220
			Color.FromArgb(243,243,218),		// 221
			Color.FromArgb(243,243,219),		// 222
			Color.FromArgb(244,244,221),		// 223
			Color.FromArgb(244,244,222),		// 224
			Color.FromArgb(244,244,223),		// 225
			Color.FromArgb(245,245,224),		// 226
			Color.FromArgb(245,245,225),		// 227
			Color.FromArgb(245,245,226),		// 228
			Color.FromArgb(246,246,227),		// 229
			Color.FromArgb(246,246,228),		// 230
			Color.FromArgb(246,246,230),		// 231
			Color.FromArgb(247,247,231),		// 232
			Color.FromArgb(247,247,232),		// 233
			Color.FromArgb(247,247,233),		// 234
			Color.FromArgb(248,248,234),		// 235
			Color.FromArgb(248,248,235),		// 236
			Color.FromArgb(248,248,236),		// 237
			Color.FromArgb(249,249,237),		// 238
			Color.FromArgb(249,249,238),		// 239
			Color.FromArgb(249,249,239),		// 240
			Color.FromArgb(250,250,240),		// 241
			Color.FromArgb(250,250,241),		// 242
			Color.FromArgb(250,250,242),		// 243
			Color.FromArgb(251,251,243),		// 244
			Color.FromArgb(251,251,244),		// 245
			Color.FromArgb(251,251,245),		// 246
			Color.FromArgb(252,252,246),		// 247
			Color.FromArgb(252,252,247),		// 248
			Color.FromArgb(252,252,248),		// 249
			Color.FromArgb(253,253,249),		// 250
			Color.FromArgb(253,253,251),		// 251
			Color.FromArgb(253,253,252),		// 252
			Color.FromArgb(254,254,253),		// 253
			Color.FromArgb(254,254,254),		// 254
			Color.FromArgb(255,255,255),		// 255
		};
	}
}

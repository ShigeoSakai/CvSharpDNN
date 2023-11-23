using System.Drawing;
namespace ColorMap.Sequential
{
	/// <summary>
	/// Greysカラーマップ
	/// </summary>
	public class GreysColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GreysColorMap()
		{
			colorMap = greys_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] greys_map_ =
		{
			Color.FromArgb(255,255,255),		// 0
			Color.FromArgb(254,254,254),		// 1
			Color.FromArgb(254,254,254),		// 2
			Color.FromArgb(253,253,253),		// 3
			Color.FromArgb(253,253,253),		// 4
			Color.FromArgb(252,252,252),		// 5
			Color.FromArgb(252,252,252),		// 6
			Color.FromArgb(251,251,251),		// 7
			Color.FromArgb(251,251,251),		// 8
			Color.FromArgb(250,250,250),		// 9
			Color.FromArgb(250,250,250),		// 10
			Color.FromArgb(249,249,249),		// 11
			Color.FromArgb(249,249,249),		// 12
			Color.FromArgb(248,248,248),		// 13
			Color.FromArgb(248,248,248),		// 14
			Color.FromArgb(247,247,247),		// 15
			Color.FromArgb(247,247,247),		// 16
			Color.FromArgb(247,247,247),		// 17
			Color.FromArgb(246,246,246),		// 18
			Color.FromArgb(246,246,246),		// 19
			Color.FromArgb(245,245,245),		// 20
			Color.FromArgb(245,245,245),		// 21
			Color.FromArgb(244,244,244),		// 22
			Color.FromArgb(244,244,244),		// 23
			Color.FromArgb(243,243,243),		// 24
			Color.FromArgb(243,243,243),		// 25
			Color.FromArgb(242,242,242),		// 26
			Color.FromArgb(242,242,242),		// 27
			Color.FromArgb(241,241,241),		// 28
			Color.FromArgb(241,241,241),		// 29
			Color.FromArgb(240,240,240),		// 30
			Color.FromArgb(240,240,240),		// 31
			Color.FromArgb(239,239,239),		// 32
			Color.FromArgb(239,239,239),		// 33
			Color.FromArgb(238,238,238),		// 34
			Color.FromArgb(237,237,237),		// 35
			Color.FromArgb(237,237,237),		// 36
			Color.FromArgb(236,236,236),		// 37
			Color.FromArgb(235,235,235),		// 38
			Color.FromArgb(234,234,234),		// 39
			Color.FromArgb(234,234,234),		// 40
			Color.FromArgb(233,233,233),		// 41
			Color.FromArgb(232,232,232),		// 42
			Color.FromArgb(231,231,231),		// 43
			Color.FromArgb(231,231,231),		// 44
			Color.FromArgb(230,230,230),		// 45
			Color.FromArgb(229,229,229),		// 46
			Color.FromArgb(229,229,229),		// 47
			Color.FromArgb(228,228,228),		// 48
			Color.FromArgb(227,227,227),		// 49
			Color.FromArgb(226,226,226),		// 50
			Color.FromArgb(226,226,226),		// 51
			Color.FromArgb(225,225,225),		// 52
			Color.FromArgb(224,224,224),		// 53
			Color.FromArgb(224,224,224),		// 54
			Color.FromArgb(223,223,223),		// 55
			Color.FromArgb(222,222,222),		// 56
			Color.FromArgb(221,221,221),		// 57
			Color.FromArgb(221,221,221),		// 58
			Color.FromArgb(220,220,220),		// 59
			Color.FromArgb(219,219,219),		// 60
			Color.FromArgb(218,218,218),		// 61
			Color.FromArgb(218,218,218),		// 62
			Color.FromArgb(217,217,217),		// 63
			Color.FromArgb(216,216,216),		// 64
			Color.FromArgb(215,215,215),		// 65
			Color.FromArgb(215,215,215),		// 66
			Color.FromArgb(214,214,214),		// 67
			Color.FromArgb(213,213,213),		// 68
			Color.FromArgb(212,212,212),		// 69
			Color.FromArgb(211,211,211),		// 70
			Color.FromArgb(210,210,210),		// 71
			Color.FromArgb(209,209,209),		// 72
			Color.FromArgb(208,208,208),		// 73
			Color.FromArgb(207,207,207),		// 74
			Color.FromArgb(207,207,207),		// 75
			Color.FromArgb(206,206,206),		// 76
			Color.FromArgb(205,205,205),		// 77
			Color.FromArgb(204,204,204),		// 78
			Color.FromArgb(203,203,203),		// 79
			Color.FromArgb(202,202,202),		// 80
			Color.FromArgb(201,201,201),		// 81
			Color.FromArgb(200,200,200),		// 82
			Color.FromArgb(200,200,200),		// 83
			Color.FromArgb(199,199,199),		// 84
			Color.FromArgb(198,198,198),		// 85
			Color.FromArgb(197,197,197),		// 86
			Color.FromArgb(196,196,196),		// 87
			Color.FromArgb(195,195,195),		// 88
			Color.FromArgb(194,194,194),		// 89
			Color.FromArgb(193,193,193),		// 90
			Color.FromArgb(193,193,193),		// 91
			Color.FromArgb(192,192,192),		// 92
			Color.FromArgb(191,191,191),		// 93
			Color.FromArgb(190,190,190),		// 94
			Color.FromArgb(189,189,189),		// 95
			Color.FromArgb(188,188,188),		// 96
			Color.FromArgb(187,187,187),		// 97
			Color.FromArgb(186,186,186),		// 98
			Color.FromArgb(184,184,184),		// 99
			Color.FromArgb(183,183,183),		// 100
			Color.FromArgb(182,182,182),		// 101
			Color.FromArgb(181,181,181),		// 102
			Color.FromArgb(179,179,179),		// 103
			Color.FromArgb(178,178,178),		// 104
			Color.FromArgb(177,177,177),		// 105
			Color.FromArgb(176,176,176),		// 106
			Color.FromArgb(175,175,175),		// 107
			Color.FromArgb(173,173,173),		// 108
			Color.FromArgb(172,172,172),		// 109
			Color.FromArgb(171,171,171),		// 110
			Color.FromArgb(170,170,170),		// 111
			Color.FromArgb(168,168,168),		// 112
			Color.FromArgb(167,167,167),		// 113
			Color.FromArgb(166,166,166),		// 114
			Color.FromArgb(165,165,165),		// 115
			Color.FromArgb(164,164,164),		// 116
			Color.FromArgb(162,162,162),		// 117
			Color.FromArgb(161,161,161),		// 118
			Color.FromArgb(160,160,160),		// 119
			Color.FromArgb(159,159,159),		// 120
			Color.FromArgb(157,157,157),		// 121
			Color.FromArgb(156,156,156),		// 122
			Color.FromArgb(155,155,155),		// 123
			Color.FromArgb(154,154,154),		// 124
			Color.FromArgb(153,153,153),		// 125
			Color.FromArgb(151,151,151),		// 126
			Color.FromArgb(150,150,150),		// 127
			Color.FromArgb(149,149,149),		// 128
			Color.FromArgb(148,148,148),		// 129
			Color.FromArgb(147,147,147),		// 130
			Color.FromArgb(146,146,146),		// 131
			Color.FromArgb(145,145,145),		// 132
			Color.FromArgb(143,143,143),		// 133
			Color.FromArgb(142,142,142),		// 134
			Color.FromArgb(141,141,141),		// 135
			Color.FromArgb(140,140,140),		// 136
			Color.FromArgb(139,139,139),		// 137
			Color.FromArgb(138,138,138),		// 138
			Color.FromArgb(137,137,137),		// 139
			Color.FromArgb(136,136,136),		// 140
			Color.FromArgb(135,135,135),		// 141
			Color.FromArgb(134,134,134),		// 142
			Color.FromArgb(132,132,132),		// 143
			Color.FromArgb(131,131,131),		// 144
			Color.FromArgb(130,130,130),		// 145
			Color.FromArgb(129,129,129),		// 146
			Color.FromArgb(128,128,128),		// 147
			Color.FromArgb(127,127,127),		// 148
			Color.FromArgb(126,126,126),		// 149
			Color.FromArgb(125,125,125),		// 150
			Color.FromArgb(124,124,124),		// 151
			Color.FromArgb(123,123,123),		// 152
			Color.FromArgb(122,122,122),		// 153
			Color.FromArgb(120,120,120),		// 154
			Color.FromArgb(119,119,119),		// 155
			Color.FromArgb(118,118,118),		// 156
			Color.FromArgb(117,117,117),		// 157
			Color.FromArgb(116,116,116),		// 158
			Color.FromArgb(115,115,115),		// 159
			Color.FromArgb(114,114,114),		// 160
			Color.FromArgb(113,113,113),		// 161
			Color.FromArgb(112,112,112),		// 162
			Color.FromArgb(111,111,111),		// 163
			Color.FromArgb(110,110,110),		// 164
			Color.FromArgb(109,109,109),		// 165
			Color.FromArgb(108,108,108),		// 166
			Color.FromArgb(107,107,107),		// 167
			Color.FromArgb(106,106,106),		// 168
			Color.FromArgb(105,105,105),		// 169
			Color.FromArgb(104,104,104),		// 170
			Color.FromArgb(102,102,102),		// 171
			Color.FromArgb(101,101,101),		// 172
			Color.FromArgb(100,100,100),		// 173
			Color.FromArgb(99,99,99),		// 174
			Color.FromArgb(98,98,98),		// 175
			Color.FromArgb(97,97,97),		// 176
			Color.FromArgb(96,96,96),		// 177
			Color.FromArgb(95,95,95),		// 178
			Color.FromArgb(94,94,94),		// 179
			Color.FromArgb(93,93,93),		// 180
			Color.FromArgb(92,92,92),		// 181
			Color.FromArgb(91,91,91),		// 182
			Color.FromArgb(90,90,90),		// 183
			Color.FromArgb(89,89,89),		// 184
			Color.FromArgb(88,88,88),		// 185
			Color.FromArgb(87,87,87),		// 186
			Color.FromArgb(86,86,86),		// 187
			Color.FromArgb(85,85,85),		// 188
			Color.FromArgb(84,84,84),		// 189
			Color.FromArgb(83,83,83),		// 190
			Color.FromArgb(82,82,82),		// 191
			Color.FromArgb(80,80,80),		// 192
			Color.FromArgb(79,79,79),		// 193
			Color.FromArgb(78,78,78),		// 194
			Color.FromArgb(76,76,76),		// 195
			Color.FromArgb(75,75,75),		// 196
			Color.FromArgb(73,73,73),		// 197
			Color.FromArgb(72,72,72),		// 198
			Color.FromArgb(71,71,71),		// 199
			Color.FromArgb(69,69,69),		// 200
			Color.FromArgb(68,68,68),		// 201
			Color.FromArgb(66,66,66),		// 202
			Color.FromArgb(65,65,65),		// 203
			Color.FromArgb(64,64,64),		// 204
			Color.FromArgb(62,62,62),		// 205
			Color.FromArgb(61,61,61),		// 206
			Color.FromArgb(59,59,59),		// 207
			Color.FromArgb(58,58,58),		// 208
			Color.FromArgb(56,56,56),		// 209
			Color.FromArgb(55,55,55),		// 210
			Color.FromArgb(54,54,54),		// 211
			Color.FromArgb(52,52,52),		// 212
			Color.FromArgb(51,51,51),		// 213
			Color.FromArgb(49,49,49),		// 214
			Color.FromArgb(48,48,48),		// 215
			Color.FromArgb(47,47,47),		// 216
			Color.FromArgb(45,45,45),		// 217
			Color.FromArgb(44,44,44),		// 218
			Color.FromArgb(42,42,42),		// 219
			Color.FromArgb(41,41,41),		// 220
			Color.FromArgb(40,40,40),		// 221
			Color.FromArgb(38,38,38),		// 222
			Color.FromArgb(37,37,37),		// 223
			Color.FromArgb(35,35,35),		// 224
			Color.FromArgb(34,34,34),		// 225
			Color.FromArgb(33,33,33),		// 226
			Color.FromArgb(32,32,32),		// 227
			Color.FromArgb(31,31,31),		// 228
			Color.FromArgb(30,30,30),		// 229
			Color.FromArgb(29,29,29),		// 230
			Color.FromArgb(27,27,27),		// 231
			Color.FromArgb(26,26,26),		// 232
			Color.FromArgb(25,25,25),		// 233
			Color.FromArgb(24,24,24),		// 234
			Color.FromArgb(23,23,23),		// 235
			Color.FromArgb(22,22,22),		// 236
			Color.FromArgb(20,20,20),		// 237
			Color.FromArgb(19,19,19),		// 238
			Color.FromArgb(18,18,18),		// 239
			Color.FromArgb(17,17,17),		// 240
			Color.FromArgb(16,16,16),		// 241
			Color.FromArgb(15,15,15),		// 242
			Color.FromArgb(13,13,13),		// 243
			Color.FromArgb(12,12,12),		// 244
			Color.FromArgb(11,11,11),		// 245
			Color.FromArgb(10,10,10),		// 246
			Color.FromArgb(9,9,9),		// 247
			Color.FromArgb(8,8,8),		// 248
			Color.FromArgb(6,6,6),		// 249
			Color.FromArgb(5,5,5),		// 250
			Color.FromArgb(4,4,4),		// 251
			Color.FromArgb(3,3,3),		// 252
			Color.FromArgb(2,2,2),		// 253
			Color.FromArgb(1,1,1),		// 254
			Color.FromArgb(0,0,0),		// 255
		};
	}
}

using System.Drawing;
namespace ColorMap.Miscellaneous
{
	/// <summary>
	/// Cubehelixカラーマップ
	/// </summary>
	public class CubehelixColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CubehelixColorMap()
		{
			colorMap = cubehelix_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] cubehelix_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(1,0,1),		// 1
			Color.FromArgb(3,1,3),		// 2
			Color.FromArgb(4,1,4),		// 3
			Color.FromArgb(6,2,6),		// 4
			Color.FromArgb(8,2,8),		// 5
			Color.FromArgb(9,3,9),		// 6
			Color.FromArgb(10,4,11),		// 7
			Color.FromArgb(12,4,13),		// 8
			Color.FromArgb(13,5,15),		// 9
			Color.FromArgb(14,6,17),		// 10
			Color.FromArgb(15,6,19),		// 11
			Color.FromArgb(17,7,21),		// 12
			Color.FromArgb(18,8,23),		// 13
			Color.FromArgb(19,9,25),		// 14
			Color.FromArgb(20,10,27),		// 15
			Color.FromArgb(20,11,29),		// 16
			Color.FromArgb(21,11,31),		// 17
			Color.FromArgb(22,12,33),		// 18
			Color.FromArgb(23,13,35),		// 19
			Color.FromArgb(23,14,37),		// 20
			Color.FromArgb(24,15,39),		// 21
			Color.FromArgb(24,17,41),		// 22
			Color.FromArgb(25,18,43),		// 23
			Color.FromArgb(25,19,45),		// 24
			Color.FromArgb(25,20,47),		// 25
			Color.FromArgb(26,21,48),		// 26
			Color.FromArgb(26,22,50),		// 27
			Color.FromArgb(26,24,52),		// 28
			Color.FromArgb(26,25,54),		// 29
			Color.FromArgb(26,26,56),		// 30
			Color.FromArgb(26,28,57),		// 31
			Color.FromArgb(26,29,59),		// 32
			Color.FromArgb(26,31,60),		// 33
			Color.FromArgb(26,32,62),		// 34
			Color.FromArgb(26,34,63),		// 35
			Color.FromArgb(26,35,65),		// 36
			Color.FromArgb(25,37,66),		// 37
			Color.FromArgb(25,38,67),		// 38
			Color.FromArgb(25,40,69),		// 39
			Color.FromArgb(25,41,70),		// 40
			Color.FromArgb(24,43,71),		// 41
			Color.FromArgb(24,45,72),		// 42
			Color.FromArgb(24,46,73),		// 43
			Color.FromArgb(23,48,74),		// 44
			Color.FromArgb(23,50,74),		// 45
			Color.FromArgb(23,52,75),		// 46
			Color.FromArgb(23,53,76),		// 47
			Color.FromArgb(22,55,76),		// 48
			Color.FromArgb(22,57,77),		// 49
			Color.FromArgb(22,58,77),		// 50
			Color.FromArgb(21,60,77),		// 51
			Color.FromArgb(21,62,78),		// 52
			Color.FromArgb(21,64,78),		// 53
			Color.FromArgb(21,66,78),		// 54
			Color.FromArgb(21,67,78),		// 55
			Color.FromArgb(21,69,78),		// 56
			Color.FromArgb(20,71,78),		// 57
			Color.FromArgb(20,73,78),		// 58
			Color.FromArgb(20,74,77),		// 59
			Color.FromArgb(21,76,77),		// 60
			Color.FromArgb(21,78,77),		// 61
			Color.FromArgb(21,79,76),		// 62
			Color.FromArgb(21,81,76),		// 63
			Color.FromArgb(21,83,75),		// 64
			Color.FromArgb(22,84,75),		// 65
			Color.FromArgb(22,86,74),		// 66
			Color.FromArgb(22,88,73),		// 67
			Color.FromArgb(23,89,73),		// 68
			Color.FromArgb(23,91,72),		// 69
			Color.FromArgb(24,92,71),		// 70
			Color.FromArgb(25,94,70),		// 71
			Color.FromArgb(26,95,69),		// 72
			Color.FromArgb(27,97,68),		// 73
			Color.FromArgb(27,98,67),		// 74
			Color.FromArgb(28,99,66),		// 75
			Color.FromArgb(30,101,66),		// 76
			Color.FromArgb(31,102,65),		// 77
			Color.FromArgb(32,103,64),		// 78
			Color.FromArgb(33,104,63),		// 79
			Color.FromArgb(35,106,61),		// 80
			Color.FromArgb(36,107,60),		// 81
			Color.FromArgb(38,108,59),		// 82
			Color.FromArgb(39,109,58),		// 83
			Color.FromArgb(41,110,58),		// 84
			Color.FromArgb(43,111,57),		// 85
			Color.FromArgb(45,112,56),		// 86
			Color.FromArgb(47,113,55),		// 87
			Color.FromArgb(49,114,54),		// 88
			Color.FromArgb(51,114,53),		// 89
			Color.FromArgb(53,115,52),		// 90
			Color.FromArgb(55,116,51),		// 91
			Color.FromArgb(57,116,51),		// 92
			Color.FromArgb(60,117,50),		// 93
			Color.FromArgb(62,118,49),		// 94
			Color.FromArgb(65,118,49),		// 95
			Color.FromArgb(67,119,48),		// 96
			Color.FromArgb(70,119,48),		// 97
			Color.FromArgb(72,120,47),		// 98
			Color.FromArgb(75,120,47),		// 99
			Color.FromArgb(78,120,47),		// 100
			Color.FromArgb(81,121,46),		// 101
			Color.FromArgb(83,121,46),		// 102
			Color.FromArgb(86,121,46),		// 103
			Color.FromArgb(89,121,46),		// 104
			Color.FromArgb(92,122,46),		// 105
			Color.FromArgb(95,122,47),		// 106
			Color.FromArgb(98,122,47),		// 107
			Color.FromArgb(101,122,47),		// 108
			Color.FromArgb(104,122,48),		// 109
			Color.FromArgb(107,122,48),		// 110
			Color.FromArgb(110,122,49),		// 111
			Color.FromArgb(113,122,50),		// 112
			Color.FromArgb(116,122,50),		// 113
			Color.FromArgb(120,122,51),		// 114
			Color.FromArgb(123,122,52),		// 115
			Color.FromArgb(126,122,53),		// 116
			Color.FromArgb(129,122,55),		// 117
			Color.FromArgb(132,122,56),		// 118
			Color.FromArgb(135,122,57),		// 119
			Color.FromArgb(138,121,59),		// 120
			Color.FromArgb(141,121,60),		// 121
			Color.FromArgb(144,121,62),		// 122
			Color.FromArgb(147,121,64),		// 123
			Color.FromArgb(150,121,65),		// 124
			Color.FromArgb(153,121,67),		// 125
			Color.FromArgb(155,121,69),		// 126
			Color.FromArgb(158,121,71),		// 127
			Color.FromArgb(161,121,74),		// 128
			Color.FromArgb(164,120,76),		// 129
			Color.FromArgb(166,120,78),		// 130
			Color.FromArgb(169,120,81),		// 131
			Color.FromArgb(171,120,83),		// 132
			Color.FromArgb(174,120,86),		// 133
			Color.FromArgb(176,120,88),		// 134
			Color.FromArgb(178,120,91),		// 135
			Color.FromArgb(181,120,94),		// 136
			Color.FromArgb(183,120,96),		// 137
			Color.FromArgb(185,120,99),		// 138
			Color.FromArgb(187,121,102),		// 139
			Color.FromArgb(189,121,105),		// 140
			Color.FromArgb(191,121,108),		// 141
			Color.FromArgb(193,121,111),		// 142
			Color.FromArgb(194,121,114),		// 143
			Color.FromArgb(196,122,117),		// 144
			Color.FromArgb(198,122,120),		// 145
			Color.FromArgb(199,122,124),		// 146
			Color.FromArgb(201,123,127),		// 147
			Color.FromArgb(202,123,130),		// 148
			Color.FromArgb(203,124,133),		// 149
			Color.FromArgb(204,124,136),		// 150
			Color.FromArgb(205,125,140),		// 151
			Color.FromArgb(206,125,143),		// 152
			Color.FromArgb(207,126,146),		// 153
			Color.FromArgb(208,127,149),		// 154
			Color.FromArgb(209,127,153),		// 155
			Color.FromArgb(209,128,156),		// 156
			Color.FromArgb(210,129,159),		// 157
			Color.FromArgb(211,130,162),		// 158
			Color.FromArgb(211,131,165),		// 159
			Color.FromArgb(211,131,169),		// 160
			Color.FromArgb(212,132,172),		// 161
			Color.FromArgb(212,133,175),		// 162
			Color.FromArgb(212,135,178),		// 163
			Color.FromArgb(212,136,181),		// 164
			Color.FromArgb(212,137,184),		// 165
			Color.FromArgb(212,138,186),		// 166
			Color.FromArgb(212,139,189),		// 167
			Color.FromArgb(212,140,192),		// 168
			Color.FromArgb(211,142,195),		// 169
			Color.FromArgb(211,143,197),		// 170
			Color.FromArgb(211,144,200),		// 171
			Color.FromArgb(210,146,203),		// 172
			Color.FromArgb(210,147,205),		// 173
			Color.FromArgb(210,149,207),		// 174
			Color.FromArgb(209,150,210),		// 175
			Color.FromArgb(208,152,212),		// 176
			Color.FromArgb(208,154,214),		// 177
			Color.FromArgb(207,155,216),		// 178
			Color.FromArgb(207,157,218),		// 179
			Color.FromArgb(206,158,220),		// 180
			Color.FromArgb(205,160,222),		// 181
			Color.FromArgb(205,162,224),		// 182
			Color.FromArgb(204,164,226),		// 183
			Color.FromArgb(203,165,227),		// 184
			Color.FromArgb(203,167,229),		// 185
			Color.FromArgb(202,169,230),		// 186
			Color.FromArgb(201,171,231),		// 187
			Color.FromArgb(201,172,233),		// 188
			Color.FromArgb(200,174,234),		// 189
			Color.FromArgb(199,176,235),		// 190
			Color.FromArgb(199,178,236),		// 191
			Color.FromArgb(198,180,237),		// 192
			Color.FromArgb(197,182,238),		// 193
			Color.FromArgb(197,183,239),		// 194
			Color.FromArgb(196,185,239),		// 195
			Color.FromArgb(196,187,240),		// 196
			Color.FromArgb(195,189,241),		// 197
			Color.FromArgb(195,191,241),		// 198
			Color.FromArgb(194,193,242),		// 199
			Color.FromArgb(194,194,242),		// 200
			Color.FromArgb(194,196,242),		// 201
			Color.FromArgb(193,198,243),		// 202
			Color.FromArgb(193,200,243),		// 203
			Color.FromArgb(193,202,243),		// 204
			Color.FromArgb(193,203,243),		// 205
			Color.FromArgb(193,205,243),		// 206
			Color.FromArgb(193,207,243),		// 207
			Color.FromArgb(193,208,243),		// 208
			Color.FromArgb(193,210,243),		// 209
			Color.FromArgb(193,212,243),		// 210
			Color.FromArgb(193,213,243),		// 211
			Color.FromArgb(194,215,242),		// 212
			Color.FromArgb(194,216,242),		// 213
			Color.FromArgb(195,218,242),		// 214
			Color.FromArgb(195,219,242),		// 215
			Color.FromArgb(196,221,241),		// 216
			Color.FromArgb(196,222,241),		// 217
			Color.FromArgb(197,224,241),		// 218
			Color.FromArgb(198,225,241),		// 219
			Color.FromArgb(199,226,240),		// 220
			Color.FromArgb(200,228,240),		// 221
			Color.FromArgb(200,229,240),		// 222
			Color.FromArgb(202,230,239),		// 223
			Color.FromArgb(203,231,239),		// 224
			Color.FromArgb(204,232,239),		// 225
			Color.FromArgb(205,233,239),		// 226
			Color.FromArgb(206,235,239),		// 227
			Color.FromArgb(208,236,238),		// 228
			Color.FromArgb(209,237,238),		// 229
			Color.FromArgb(210,238,238),		// 230
			Color.FromArgb(212,239,238),		// 231
			Color.FromArgb(213,240,238),		// 232
			Color.FromArgb(215,240,238),		// 233
			Color.FromArgb(217,241,238),		// 234
			Color.FromArgb(218,242,238),		// 235
			Color.FromArgb(220,243,239),		// 236
			Color.FromArgb(222,244,239),		// 237
			Color.FromArgb(223,244,239),		// 238
			Color.FromArgb(225,245,240),		// 239
			Color.FromArgb(227,246,240),		// 240
			Color.FromArgb(229,247,240),		// 241
			Color.FromArgb(231,247,241),		// 242
			Color.FromArgb(232,248,242),		// 243
			Color.FromArgb(234,248,242),		// 244
			Color.FromArgb(236,249,243),		// 245
			Color.FromArgb(238,250,244),		// 246
			Color.FromArgb(240,250,245),		// 247
			Color.FromArgb(242,251,246),		// 248
			Color.FromArgb(244,251,247),		// 249
			Color.FromArgb(245,252,248),		// 250
			Color.FromArgb(247,252,249),		// 251
			Color.FromArgb(249,253,250),		// 252
			Color.FromArgb(251,253,252),		// 253
			Color.FromArgb(253,254,253),		// 254
			Color.FromArgb(255,255,255),		// 255
		};
	}
}

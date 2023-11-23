using System.Drawing;
namespace ColorMap.Miscellaneous
{
	/// <summary>
	/// GistEarth�J���[�}�b�v
	/// </summary>
	public class GistEarthColorMap : ColorMapClass
	{
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		public GistEarthColorMap()
		{
			colorMap = gist_earth_map_;
		}
		/// <summary>
		/// �J���[�}�b�v�e�[�u��
		/// </summary>
		protected Color[] gist_earth_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(0,0,43),		// 1
			Color.FromArgb(1,0,56),		// 2
			Color.FromArgb(1,0,67),		// 3
			Color.FromArgb(2,0,78),		// 4
			Color.FromArgb(3,0,88),		// 5
			Color.FromArgb(3,0,99),		// 6
			Color.FromArgb(4,0,110),		// 7
			Color.FromArgb(5,2,115),		// 8
			Color.FromArgb(5,4,116),		// 9
			Color.FromArgb(6,6,116),		// 10
			Color.FromArgb(7,9,116),		// 11
			Color.FromArgb(7,11,116),		// 12
			Color.FromArgb(8,13,116),		// 13
			Color.FromArgb(9,16,117),		// 14
			Color.FromArgb(9,18,117),		// 15
			Color.FromArgb(10,20,117),		// 16
			Color.FromArgb(11,22,117),		// 17
			Color.FromArgb(11,25,117),		// 18
			Color.FromArgb(12,27,117),		// 19
			Color.FromArgb(13,29,118),		// 20
			Color.FromArgb(13,32,118),		// 21
			Color.FromArgb(14,34,118),		// 22
			Color.FromArgb(15,36,118),		// 23
			Color.FromArgb(15,39,118),		// 24
			Color.FromArgb(16,41,119),		// 25
			Color.FromArgb(17,43,119),		// 26
			Color.FromArgb(17,45,119),		// 27
			Color.FromArgb(18,48,119),		// 28
			Color.FromArgb(19,50,119),		// 29
			Color.FromArgb(19,52,119),		// 30
			Color.FromArgb(20,54,120),		// 31
			Color.FromArgb(21,56,120),		// 32
			Color.FromArgb(21,58,120),		// 33
			Color.FromArgb(22,60,120),		// 34
			Color.FromArgb(23,62,120),		// 35
			Color.FromArgb(23,64,121),		// 36
			Color.FromArgb(24,66,121),		// 37
			Color.FromArgb(25,69,121),		// 38
			Color.FromArgb(25,71,121),		// 39
			Color.FromArgb(26,73,121),		// 40
			Color.FromArgb(27,75,121),		// 41
			Color.FromArgb(27,77,122),		// 42
			Color.FromArgb(28,79,122),		// 43
			Color.FromArgb(29,81,122),		// 44
			Color.FromArgb(29,83,122),		// 45
			Color.FromArgb(30,84,122),		// 46
			Color.FromArgb(31,86,123),		// 47
			Color.FromArgb(31,88,123),		// 48
			Color.FromArgb(32,90,123),		// 49
			Color.FromArgb(33,92,123),		// 50
			Color.FromArgb(33,94,123),		// 51
			Color.FromArgb(34,96,123),		// 52
			Color.FromArgb(35,97,124),		// 53
			Color.FromArgb(35,99,124),		// 54
			Color.FromArgb(36,101,124),		// 55
			Color.FromArgb(37,102,124),		// 56
			Color.FromArgb(37,104,124),		// 57
			Color.FromArgb(38,105,125),		// 58
			Color.FromArgb(39,107,125),		// 59
			Color.FromArgb(39,109,125),		// 60
			Color.FromArgb(40,110,125),		// 61
			Color.FromArgb(41,112,125),		// 62
			Color.FromArgb(41,113,125),		// 63
			Color.FromArgb(42,115,126),		// 64
			Color.FromArgb(43,116,126),		// 65
			Color.FromArgb(43,118,126),		// 66
			Color.FromArgb(44,120,126),		// 67
			Color.FromArgb(45,121,126),		// 68
			Color.FromArgb(45,123,127),		// 69
			Color.FromArgb(46,124,127),		// 70
			Color.FromArgb(47,126,127),		// 71
			Color.FromArgb(47,127,127),		// 72
			Color.FromArgb(48,128,126),		// 73
			Color.FromArgb(48,129,125),		// 74
			Color.FromArgb(49,129,123),		// 75
			Color.FromArgb(49,130,122),		// 76
			Color.FromArgb(50,130,121),		// 77
			Color.FromArgb(50,131,120),		// 78
			Color.FromArgb(51,132,119),		// 79
			Color.FromArgb(51,132,117),		// 80
			Color.FromArgb(52,133,116),		// 81
			Color.FromArgb(52,133,115),		// 82
			Color.FromArgb(53,134,114),		// 83
			Color.FromArgb(53,134,112),		// 84
			Color.FromArgb(54,135,111),		// 85
			Color.FromArgb(54,136,110),		// 86
			Color.FromArgb(55,136,109),		// 87
			Color.FromArgb(55,137,108),		// 88
			Color.FromArgb(56,137,106),		// 89
			Color.FromArgb(56,138,105),		// 90
			Color.FromArgb(56,138,104),		// 91
			Color.FromArgb(57,139,103),		// 92
			Color.FromArgb(57,140,101),		// 93
			Color.FromArgb(58,140,100),		// 94
			Color.FromArgb(58,141,99),		// 95
			Color.FromArgb(59,141,98),		// 96
			Color.FromArgb(59,142,97),		// 97
			Color.FromArgb(60,142,95),		// 98
			Color.FromArgb(60,143,94),		// 99
			Color.FromArgb(61,144,93),		// 100
			Color.FromArgb(61,144,92),		// 101
			Color.FromArgb(62,145,90),		// 102
			Color.FromArgb(62,145,89),		// 103
			Color.FromArgb(63,146,88),		// 104
			Color.FromArgb(63,147,87),		// 105
			Color.FromArgb(64,147,85),		// 106
			Color.FromArgb(64,148,84),		// 107
			Color.FromArgb(64,148,83),		// 108
			Color.FromArgb(65,149,82),		// 109
			Color.FromArgb(65,149,81),		// 110
			Color.FromArgb(66,150,79),		// 111
			Color.FromArgb(66,151,78),		// 112
			Color.FromArgb(67,151,77),		// 113
			Color.FromArgb(67,152,76),		// 114
			Color.FromArgb(68,152,74),		// 115
			Color.FromArgb(68,153,73),		// 116
			Color.FromArgb(69,153,72),		// 117
			Color.FromArgb(71,154,71),		// 118
			Color.FromArgb(73,155,70),		// 119
			Color.FromArgb(75,155,70),		// 120
			Color.FromArgb(78,156,71),		// 121
			Color.FromArgb(80,156,71),		// 122
			Color.FromArgb(82,157,72),		// 123
			Color.FromArgb(84,157,72),		// 124
			Color.FromArgb(87,158,73),		// 125
			Color.FromArgb(89,159,74),		// 126
			Color.FromArgb(91,159,74),		// 127
			Color.FromArgb(93,160,75),		// 128
			Color.FromArgb(95,160,75),		// 129
			Color.FromArgb(98,161,76),		// 130
			Color.FromArgb(100,161,77),		// 131
			Color.FromArgb(102,162,77),		// 132
			Color.FromArgb(104,163,78),		// 133
			Color.FromArgb(107,163,78),		// 134
			Color.FromArgb(109,163,79),		// 135
			Color.FromArgb(111,164,79),		// 136
			Color.FromArgb(113,164,80),		// 137
			Color.FromArgb(115,165,81),		// 138
			Color.FromArgb(118,165,81),		// 139
			Color.FromArgb(120,166,82),		// 140
			Color.FromArgb(121,166,82),		// 141
			Color.FromArgb(123,167,82),		// 142
			Color.FromArgb(125,167,82),		// 143
			Color.FromArgb(126,167,83),		// 144
			Color.FromArgb(128,168,83),		// 145
			Color.FromArgb(130,168,83),		// 146
			Color.FromArgb(131,169,84),		// 147
			Color.FromArgb(133,169,84),		// 148
			Color.FromArgb(135,170,84),		// 149
			Color.FromArgb(136,170,85),		// 150
			Color.FromArgb(138,171,85),		// 151
			Color.FromArgb(140,171,85),		// 152
			Color.FromArgb(141,171,86),		// 153
			Color.FromArgb(143,172,86),		// 154
			Color.FromArgb(145,172,86),		// 155
			Color.FromArgb(146,173,87),		// 156
			Color.FromArgb(148,173,87),		// 157
			Color.FromArgb(150,174,87),		// 158
			Color.FromArgb(151,174,88),		// 159
			Color.FromArgb(153,174,88),		// 160
			Color.FromArgb(154,175,88),		// 161
			Color.FromArgb(156,175,88),		// 162
			Color.FromArgb(158,176,89),		// 163
			Color.FromArgb(159,176,89),		// 164
			Color.FromArgb(161,177,89),		// 165
			Color.FromArgb(163,177,90),		// 166
			Color.FromArgb(164,178,90),		// 167
			Color.FromArgb(166,178,90),		// 168
			Color.FromArgb(168,178,91),		// 169
			Color.FromArgb(169,179,91),		// 170
			Color.FromArgb(171,179,91),		// 171
			Color.FromArgb(173,180,92),		// 172
			Color.FromArgb(174,180,92),		// 173
			Color.FromArgb(176,181,92),		// 174
			Color.FromArgb(178,181,93),		// 175
			Color.FromArgb(179,181,93),		// 176
			Color.FromArgb(181,182,93),		// 177
			Color.FromArgb(182,182,94),		// 178
			Color.FromArgb(183,181,94),		// 179
			Color.FromArgb(183,181,94),		// 180
			Color.FromArgb(184,180,95),		// 181
			Color.FromArgb(184,179,95),		// 182
			Color.FromArgb(185,178,95),		// 183
			Color.FromArgb(185,177,95),		// 184
			Color.FromArgb(185,176,96),		// 185
			Color.FromArgb(186,175,96),		// 186
			Color.FromArgb(186,175,96),		// 187
			Color.FromArgb(187,174,97),		// 188
			Color.FromArgb(187,173,97),		// 189
			Color.FromArgb(188,172,97),		// 190
			Color.FromArgb(188,171,98),		// 191
			Color.FromArgb(188,170,98),		// 192
			Color.FromArgb(189,169,98),		// 193
			Color.FromArgb(189,169,99),		// 194
			Color.FromArgb(190,168,99),		// 195
			Color.FromArgb(190,167,99),		// 196
			Color.FromArgb(190,166,100),		// 197
			Color.FromArgb(191,165,100),		// 198
			Color.FromArgb(191,164,100),		// 199
			Color.FromArgb(192,163,101),		// 200
			Color.FromArgb(192,163,103),		// 201
			Color.FromArgb(193,163,105),		// 202
			Color.FromArgb(194,163,108),		// 203
			Color.FromArgb(195,164,110),		// 204
			Color.FromArgb(197,164,113),		// 205
			Color.FromArgb(198,165,115),		// 206
			Color.FromArgb(199,166,118),		// 207
			Color.FromArgb(200,166,120),		// 208
			Color.FromArgb(201,167,123),		// 209
			Color.FromArgb(202,168,125),		// 210
			Color.FromArgb(203,169,127),		// 211
			Color.FromArgb(204,170,130),		// 212
			Color.FromArgb(206,171,132),		// 213
			Color.FromArgb(207,172,135),		// 214
			Color.FromArgb(208,173,137),		// 215
			Color.FromArgb(209,173,140),		// 216
			Color.FromArgb(210,174,142),		// 217
			Color.FromArgb(211,175,145),		// 218
			Color.FromArgb(212,176,147),		// 219
			Color.FromArgb(213,177,150),		// 220
			Color.FromArgb(214,178,152),		// 221
			Color.FromArgb(216,179,154),		// 222
			Color.FromArgb(217,181,157),		// 223
			Color.FromArgb(218,182,159),		// 224
			Color.FromArgb(219,183,162),		// 225
			Color.FromArgb(220,185,164),		// 226
			Color.FromArgb(221,186,167),		// 227
			Color.FromArgb(222,188,169),		// 228
			Color.FromArgb(223,189,172),		// 229
			Color.FromArgb(225,191,175),		// 230
			Color.FromArgb(226,193,178),		// 231
			Color.FromArgb(227,195,181),		// 232
			Color.FromArgb(228,197,184),		// 233
			Color.FromArgb(229,199,187),		// 234
			Color.FromArgb(230,201,190),		// 235
			Color.FromArgb(231,203,193),		// 236
			Color.FromArgb(232,205,196),		// 237
			Color.FromArgb(233,207,199),		// 238
			Color.FromArgb(235,209,202),		// 239
			Color.FromArgb(236,211,205),		// 240
			Color.FromArgb(237,213,208),		// 241
			Color.FromArgb(238,215,211),		// 242
			Color.FromArgb(239,217,214),		// 243
			Color.FromArgb(240,220,217),		// 244
			Color.FromArgb(241,222,220),		// 245
			Color.FromArgb(242,224,223),		// 246
			Color.FromArgb(244,227,226),		// 247
			Color.FromArgb(245,230,229),		// 248
			Color.FromArgb(246,233,232),		// 249
			Color.FromArgb(247,236,235),		// 250
			Color.FromArgb(248,239,238),		// 251
			Color.FromArgb(249,242,241),		// 252
			Color.FromArgb(250,245,244),		// 253
			Color.FromArgb(251,248,247),		// 254
			Color.FromArgb(253,250,250),		// 255
		};
	}
}
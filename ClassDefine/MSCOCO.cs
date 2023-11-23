﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVSharpDNN.ClassDefine
{
	[DisplayName("MS-COCO")]
	public class MSCOCO : ClassDefineBase
    {
        public MSCOCO(bool useBackground = false) :base(useBackground)
        {
            labels = new String[]
            {
                "背景",
                "人",
                "自転車",
                "車両",
                "バイク",
                "飛行機",
                "バス",
                "列車",
                "トラック",
                "ボート",
                "信号機",
                "消火栓",
                "一時停止標識",
                "パーキングメーター",
                "ベンチ",
                "鳥",
                "猫",
                "犬",
                "馬",
                "羊",
                "牛",
                "象",
                "熊",
                "シマウマ",
                "キリン",
                "バックパック",
                "傘",
                "ハンドバッグ",
                "ネクタイ",
                "スーツケース",
                "フリスビー",
                "スキー板",
                "スノーボード",
                "スポーツボール",
                "凧",
                "バット",
                "グローブ",
                "スケートボード",
                "サーフボード",
                "テニスラケット",
                "ボトル",
                "ワイングラス",
                "カップ",
                "フォーク",
                "ナイフ",
                "スプーン",
                "ボウル",
                "バナナ",
                "アップル",
                "サンドイッチ",
                "オレンジ",
                "ブロッコリ",
                "にんじん",
                "ホットドッグ",
                "ピザ",
                "ドーナツ",
                "ケーキ",
                "椅子",
                "ソファー",
                "鉢植え",
                "ベッド",
                "ダイニングテーブル",
                "トイレ",
                "TVモニター",
                "ラップトップ",
                "マウス",
                "リモコン",
                "キーボード",
                "携帯電話",
                "電子レンジ",
                "オーブン",
                "トースター",
                "シンク",
                "冷蔵庫",
                "本",
                "時計",
                "花瓶",
                "はさみ",
                "テディベア",
                "ヘアドライヤー",
                "歯ブラシ",
                };
            // 色の定義
            colors = new RGB[]
                {
                new RGB(0,0,0),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(0,0,255),
                new RGB(7,0,255),
                new RGB(21,0,255),
                new RGB(39,0,255),
                new RGB(62,0,255),
                new RGB(80,0,255),
                new RGB(98,0,255),
                new RGB(116,0,255),
                new RGB(139,0,255),
                new RGB(157,0,255),
                new RGB(175,0,255),
                new RGB(192,0,255),
                new RGB(216,0,255),
                new RGB(234,0,255),
                new RGB(251,0,255),
                new RGB(255,14,255),
                new RGB(255,38,255),
                new RGB(255,55,255),
                new RGB(255,73,255),
                new RGB(255,91,255),
                new RGB(255,115,255),
                new RGB(255,132,255),
                new RGB(255,150,255),
                new RGB(255,168,255),
                new RGB(255,191,255),
                new RGB(255,209,255),
                new RGB(255,227,255),
                new RGB(255,243,255),
                new RGB(255,254,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(255,255,255),
                new RGB(245,245,255),
                new RGB(230,230,255),
                new RGB(212,212,255),
                new RGB(195,195,255),
                new RGB(171,171,255),
                new RGB(153,153,255),
                new RGB(136,136,255),
                new RGB(118,118,255),
                new RGB(94,94,255),
                new RGB(77,77,255),
                new RGB(59,59,255),
                new RGB(41,41,255),
                new RGB(23,23,255),
                };
        }
    }
}
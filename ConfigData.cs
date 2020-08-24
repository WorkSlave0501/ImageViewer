using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ImageViewer
{
    /// <summary>
    /// コンフィグデータクラス
    /// </summary>
    public class ConfigData
    {
        //画像ウィンドウ幅
        public double WindowWidth
        {
            get; set;
        }

        //画像ウィンドウ高さ
        public double WindowHeight
        {
            get; set;
        }

        //画像ウィンドウX位置
        public double WinposiX
        {
            get; set;
        }

        //画像ウィンドウY位置
        public double WinposiY
        {
            get; set;
        }

        //スライドショー切替間隔
        public int SlideInterval
        {
            get; set;
        }

        //画像ファイルパス
        public List<string> List_FilePath
        {
            get; set;
        }

        //ウィンドウ背景
        public string WndBackGrnd
        {
            get; set;
        }

        //再生中のファイルNo
        public int PlayNo
        {
            get; set;
        }

        //コンストラクタ
        public ConfigData()
        {
            //コンフィグデータの読み込み
            WindowWidth = Properties.Configuration.Default.WindowWidth;
            WindowHeight = Properties.Configuration.Default.WindowHeight;
            WinposiX = Properties.Configuration.Default.WinposiX;
            WinposiY = Properties.Configuration.Default.WinposiY;
            SlideInterval = Properties.Configuration.Default.SlideInterval;
            List_FilePath = Properties.Configuration.Default.List_FilePath;
            WndBackGrnd = Properties.Configuration.Default.WndBackGrnd;
            PlayNo = Properties.Configuration.Default.PlayNo;
        }

        /// <summary>
        /// コンフィグデータを保存する
        /// </summary>
        public void fnc_SaveConfig()
        {
            //コンフィグデータの書き込み
            Properties.Configuration.Default.WindowWidth = WindowWidth;
            Properties.Configuration.Default.WindowHeight = WindowHeight;
            Properties.Configuration.Default.WinposiX = WinposiX;
            Properties.Configuration.Default.WinposiY = WinposiY;
            Properties.Configuration.Default.SlideInterval = SlideInterval;
            Properties.Configuration.Default.List_FilePath = List_FilePath;
            Properties.Configuration.Default.WndBackGrnd = WndBackGrnd;
            Properties.Configuration.Default.PlayNo = PlayNo;


            //コンフィグデータを保存
            Properties.Configuration.Default.Save();
        }

        /// <summary>
        /// コンフィグデータをリセットする。
        /// </summary>
        public void fnc_ResetConfig()
        {
            //コンフィグデータをリセットする。
            Properties.Configuration.Default.Reset();
        }
    }
}

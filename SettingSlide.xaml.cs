﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageViewer
{
    /// <summary>
    /// スライドショー設定画面
    /// </summary>
    public partial class SettingSlide : Page
    {
        // ページリスト参照用
        private List<Page> ref_listPage
        {
            get; set;
        }

        // 設定ウィンドウ参照用
        private SettingWindow ref_sWindow
        {
            get;set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        /// <param name="list"></param>
        /// <param name="swnd"></param>
        public SettingSlide(List<Page> list, SettingWindow swnd, int conInterval)
        {
            InitializeComponent();

            // 設定データの値をスライダーの初期値として設定する。
            this.IntervalSlider.Value = conInterval;

            // ページリストを参照
            ref_listPage = list;

            // 設定画面を参照
            ref_sWindow = swnd;
        }

        /// <summary>
        /// その他アイコンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Keyicon(object sender, RoutedEventArgs e)
        {
            // その他設定ページへ遷移する。
            fnc_ToPageShortcutkey();
        }

        /// <summary>
        /// ショートカットキー設定ページへ遷移する。
        /// </summary>
        private void fnc_ToPageShortcutkey()
        {
            // その他設定ページ
            this.NavigationService.Navigate(ref_listPage[1]);
        }

        /// <summary>
        /// クローズアイコンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Closeicon(object sender, RoutedEventArgs e)
        {
            // 設定ページを閉じる。
            fnc_ClosePage();
        }

        /// <summary>
        /// 設定ページを閉じる。
        /// </summary>
        private void fnc_ClosePage()
        {
            // 設定ウィンドウを終了(非表示)にする。
            ref_sWindow.fnc_SwitchVisibility_swnd();
        }

        /// <summary>
        /// データバインディングを行う。
        /// </summary>
        public void fnc_DataBinding(BindData bdata)
        {
            DataContext = bdata;
        }
    }
}

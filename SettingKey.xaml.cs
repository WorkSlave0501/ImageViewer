using System;
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
    public partial class SettingKey : Page
    {
        //ページリスト参照用
        private List<Page> ref_listPage
        {
            get;set;
        }

        //設定ウィンドウ参照用
        private SettingWindow ref_settingWindow
        {
            get; set;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingKey()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="list"></param>
        /// <param name="swnd"></param>
        public SettingKey(List<Page> list, SettingWindow swnd)
        {
            InitializeComponent();

            //ページリスト参照
            ref_listPage = list;

            //設定ウィンドウを参照
            ref_settingWindow = swnd;
        }

        /// <summary>
        /// スライドショーアイコンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Slideicon(object sender, RoutedEventArgs e)
        {
            //スライドショー設定ページへ遷移する。
            fnc_ToPageSlideshow();
        }

        /// <summary>
        /// スライドショー設定ページへ遷移する。
        /// </summary>
        private void fnc_ToPageSlideshow()
        {
            // スライドショー設定ページへ遷移
            this.NavigationService.Navigate(ref_listPage[0]);
        }

        private void hnd_Closeicon(object sender, RoutedEventArgs e)
        {
            //設定ページを閉じる。
            fnc_ClosePage();
        }

        /// <summary>
        /// 設定ページを閉じる。
        /// </summary>
        private void fnc_ClosePage()
        {
            //設定ウィンドウを終了(非表示)にする。
            ref_settingWindow.fnc_SwitchVisibility_swnd();
        }
    }
}

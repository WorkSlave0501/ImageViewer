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
    /// <summary>
    /// その他画面
    /// </summary>
    public partial class SettingVarious : Page
    {
        //ページリスト参照用
        private List<Page> ref_listPage
        {
            get;set;
        }

        //設定ウィンドウ参照用
        private SettingWindow ref_sWindow
        {
            get; set;
        }

        //バインドデータ格納用
        private BindData buf_bindData
        {
            get; set;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public SettingVarious()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="list"></param>
        /// <param name="swnd"></param>
        public SettingVarious(List<Page> list, SettingWindow swnd)
        {
            InitializeComponent();

            //ページリスト参照
            ref_listPage = list;

            //設定ウィンドウを参照
            ref_sWindow = swnd;
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
            ref_sWindow.fnc_SwitchVisibility_swnd();
        }

        /// <summary>
        /// データバインディングを行う。
        /// </summary>
        public void fnc_DataBinding(BindData bdata)
        {
            buf_bindData = bdata;
            DataContext = buf_bindData;
        }

        /// <summary>
        /// BackGroundがチェックされた際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_CheckBg(object sender, RoutedEventArgs e)
        {
            fnc_CheckBg();
        }

        /// <summary>
        /// 背景設定を切り替える。
        /// </summary>
        private void fnc_CheckBg()
        {
            buf_bindData.iwndBackGrnd = "Black";
        }

        /// <summary>
        /// BackGroundのチェックが外された際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_UncheckBg(object sender, RoutedEventArgs e)
        {
            fnc_UncheckBg();
        }

        /// <summary>
        /// 背景設定を切り替える。
        /// </summary>
        private void fnc_UncheckBg()
        {
            buf_bindData.iwndBackGrnd = "Transparent";
        }

        /// <summary>
        /// Volumeがチェックされた際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_CheckVol(object sender, RoutedEventArgs e)
        {
            fnc_CheckVol();
        }

        /// <summary>
        /// 音量設定を切り替える。
        /// </summary>
        private void fnc_CheckVol()
        {
            buf_bindData.Volume = 0.5;
        }

        /// <summary>
        /// Volumeのチェックが外された際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_UncheckVol(object sender, RoutedEventArgs e)
        {
            fnc_UncheckVol();
        }

        /// <summary>
        /// 音量設定を切り替える。
        /// </summary>
        private void fnc_UncheckVol()
        {
            buf_bindData.Volume = 0.0;
        }

        /// <summary>
        /// Topmostがチェックされた際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_CheckTmost(object sender, RoutedEventArgs e)
        {
            fnc_CheckTmost();
        }

        /// <summary>
        /// 最前面表示設定を切り替える。
        /// </summary>
        private void fnc_CheckTmost()
        {
            buf_bindData.tMost = true;
        }

        /// <summary>
        /// Topmostのチェックが外された際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_UncheckTmost(object sender, RoutedEventArgs e)
        {
            fnc_UncheckTmost();
        }

        /// <summary>
        /// 最前面表示設定を切り替える
        /// </summary>
        private void fnc_UncheckTmost()
        {
            buf_bindData.tMost = false;
        }
    }
}

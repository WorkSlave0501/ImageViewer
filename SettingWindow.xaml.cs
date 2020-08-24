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
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : NavigationWindow
    {
        //各ページのリスト
        private List<Page> list_Page
        {
            get; set;
        }

        //スライドショー間隔設定ページ
        private SettingSlide pageSlide
        {
            get; set;
        }

        //キー設定ページ
        private SettingKey pageKey
        {
            get; set;
        }

        //コンストラクタ
        public SettingWindow()
        {
            InitializeComponent();
        }

        //コンストラクタ
        public SettingWindow(BindData bdata, ConfigData condata)
        {
            InitializeComponent();

            //ウィンドウ表示を非表示で初期化
            this.Visibility = Visibility.Hidden;

            //ページリスト作成
            list_Page = new List<Page>();

            //スライドショー設定ページ作成
            pageSlide = new SettingSlide(bdata, list_Page, this);

            //ページをリストへ追加
            list_Page.Add(pageSlide);

            //ショートカットキー設定ページ作成
            pageKey = new SettingKey(list_Page, this);

            //ページをリストへ追加
            list_Page.Add(pageKey);
            
            //1ページ目(スライドショー設定ページ)表示
            Navigate(pageSlide);
        }

        /// <summary>
        /// 左クリック押下時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DragWindow(object sender, MouseButtonEventArgs e)
        {
            //ウィンドウ枠外のドラッグでもウィンドウの移動を可能にする。
            DragMove();
        }

        /// <summary>
        /// 設定ウィンドウの終了処理
        /// </summary>
        public void fnc_CloseWindow()
        {
            //ページリストをクリア
            list_Page.Clear();

            //設定ウィンドウを閉じる。
            this.Close();
        }

        /// <summary>
        /// ウィンドウの表示/非表示を切り替える
        /// </summary>
        public void fnc_SwitchVisibility_swnd()
        {
            //表示を非表示にする
            if (this.Visibility == Visibility.Visible)
            {
                this.Visibility = Visibility.Hidden;
            }
            //非表示を表示にする
            else
            {
                this.Visibility = Visibility.Visible;
            }
        }

        public void fnc_BindingSlide(BindData bdata)
        {
            pageSlide.fnc_DataBinding(bdata);
        }
    }
}

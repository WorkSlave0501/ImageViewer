using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageViewer
{
    /// <summary>
    /// 設定画面
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

        //その他設定ページ
        private SettingVarious pageVarious
        {
            get; set;
        }

        //コンストラクタ
        public SettingWindow(int conInterval)
        {
            InitializeComponent();

            //ページリスト作成
            list_Page = new List<Page>();

            //スライドショー設定ページ作成
            pageSlide = new SettingSlide(list_Page, this, conInterval);

            //ページをリストへ追加
            list_Page.Add(pageSlide);

            //その他設定ページ作成
            pageVarious = new SettingVarious(list_Page, this);

            //ページをリストへ追加
            list_Page.Add(pageVarious);
            
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
            double tmp_Opacity = 0.0;    //プロパティ：Opacity

            // 設定ウィンドウが非表示
            if (sWindow.Opacity == 0.0)
            {
                // 表示
                tmp_Opacity = 1.0;
            }
            // 設定ウィンドウが表示
            else if (sWindow.Opacity != 0.0)
            {
                // 非表示
                tmp_Opacity = 0.0;
            }

            // ウィンドウ表示/非表示アニメーション作成（透明度、アニメーション時間：0.3秒）
            DoubleAnimation animation = new DoubleAnimation(tmp_Opacity, TimeSpan.FromSeconds(0.3));

            // 設定ウィンドウのOpacity値を0.3秒で0⇔1間で変化させる。
            sWindow.BeginAnimation(NavigationWindow.OpacityProperty, animation);
        }

        /// <summary>
        /// データバインディングを行う。
        /// </summary>
        /// <param name="bdata"></param>
        public void fnc_BindToPages(BindData bdata)
        {
            pageSlide.fnc_DataBinding(bdata);
            pageVarious.fnc_DataBinding(bdata);
        }
    }
}

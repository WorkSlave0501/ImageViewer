using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ImageViewer
{
    /// <summary>
    /// メインウィンドウクラス
    /// </summary>
    public partial class MainWindow : Window
    {
        //command用メンバ
        public static RoutedCommand DClickCommand = new RoutedCommand();

        //画像ウィンドウリスト
        private List<ImageWindow> list_iWindow
        {
            get; set;
        }

        //設定ウィンドウ
        private SettingWindow sWindow
        {
            get;set;
        }

        //バインド用データリスト
        private List<BindData> list_bindData
        {
            get; set;
        }

        //コンフィグデータ
        public ConfigData conData
        {
            get; set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //画像ウィンドウリストを作成
            list_iWindow = new List<ImageWindow>();

            //設定データリスト作成
            list_bindData = new List<BindData>();

            //コンフィグデータ読込み
            conData = new ConfigData();

            //前回起動時の状態確認
            fnc_CheckConfig();
        }

        /// <summary>
        /// コンフィグファイルから、前回起動時の状態を確認する。
        /// </summary>
        private void fnc_CheckConfig()
        {
            //前回終了時のコンフィグデータ無し
            if (conData.List_FilePath == null)
            {
                //処理無し
            }
            //前回終了時のコンフィグデータ有り
            else
            {
                // 前回の画像ウィンドウを復元する。
                fnc_ResurrectWindow();
            }
        }

        /// <summary>
        /// 前回終了時ウィンドウの復元処理
        /// </summary>
        private void fnc_ResurrectWindow()
        {
            // バインド用データ作成
            BindData bdata_tmp = new BindData();

            //コンフィグから読み込んだスライドショー間隔を設定データへ代入する。
            bdata_tmp.slideInterval = conData.SlideInterval;

            // バインド用データリストへ追加
            list_bindData.Add(bdata_tmp);

            //設定ウィンドウ作成
            if (sWindow == null)
            {
                sWindow = new SettingWindow(list_bindData[list_bindData.IndexOf(bdata_tmp)], conData);
            }
            else
            {
                //処理無し
            }

            // 画像ウィンドウ作成
            ImageWindow iwnd = new ImageWindow(list_iWindow, list_bindData[list_bindData.IndexOf(bdata_tmp)], conData, sWindow, list_bindData);

            //作成した画像ウィンドウをリストに追加
            list_iWindow.Add(iwnd);

            // 画像表示処理を開始する。
            iwnd.fnc_StartbyConfig();

            // 画像ウィンドウ表示
            iwnd.Show();
        }

        /// <summary>
        /// メニュー：開くクリック時のイベントハンドラ
        /// 画像の読み込み処理を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuOpenFile(object sender, RoutedEventArgs e)
        {
            //再生ファイルを指定し、画像ウィンドウを表示する。
            fnc_OpenFile();
        }

        /// <summary>
        /// 再生ファイルを指定し、画像ウィンドウを表示する。
        /// </summary>
        private void fnc_OpenFile()
        {
            //ダイアログ生成
            var browser = new CommonOpenFileDialog();

            //複数ファイル選択が可能か否か
            browser.Multiselect = true;

            //ファイル形式フィルタ設定
            browser.Filters.Add(new CommonFileDialogFilter("画像ファイル", "*.gif;*.png;*.jpeg;*.jpg;*.bmp"));
            browser.Filters.Add(new CommonFileDialogFilter("動画ファイル", "*.mp4;*.avi;*.mkv;*.mpeg;*.wmv"));
            browser.Filters.Add(new CommonFileDialogFilter("音楽ファイル", "*.mp3"));
            browser.Filters.Add(new CommonFileDialogFilter("すべてのファイル", "*.*"));

            //タイトル設定
            browser.Title = "再生ファイル選択";

            //true:フォルダ選択/false:ファイル選択
            browser.IsFolderPicker = false;

            //ファイルパスの取得に成功
            if (browser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // バインド用データ作成
                BindData bdata_tmp = new BindData();

                //コンフィグから読み込んだスライドショー間隔を設定データへ代入する。
                bdata_tmp.slideInterval = conData.SlideInterval;

                // バインド用データリストへ追加
                list_bindData.Add(bdata_tmp);

                //設定ウィンドウ作成
                if (sWindow == null)
                {
                    sWindow = new SettingWindow(list_bindData[list_bindData.IndexOf(bdata_tmp)], conData);
                }
                else
                {
                    //処理無し
                }

                // 画像ウィンドウ作成
                ImageWindow iwnd = new ImageWindow(list_iWindow, list_bindData[list_bindData.IndexOf(bdata_tmp)], conData, sWindow, list_bindData);

                //作成した画像ウィンドウをリストに追加
                list_iWindow.Add(iwnd);

                // 画像表示処理を開始する。
                if (iwnd.fnc_StartbyOpen(browser))
                {
                    // 画像ウィンドウ表示
                    iwnd.Show();
                }
            }
            //ダイアログのリソース解放
            browser.Dispose();
        }

        /// <summary>
        /// メニュー：開くクリック時のイベントハンドラ
        /// 画像の読み込み処理を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuOpenFolder(object sender, RoutedEventArgs e)
        {
            //再生ファイルを指定し、画像ウィンドウを表示する。
            fnc_OpenFolder();
        }

        /// <summary>
        /// 再生ファイルを指定し、画像ウィンドウを表示する。
        /// </summary>
        private void fnc_OpenFolder()
        {
            //ダイアログ生成
            var browser = new CommonOpenFileDialog();

            //複数ファイル選択が可能か否か
            browser.Multiselect = true;

            //タイトル設定
            browser.Title = "再生ファイル選択";

            //true:フォルダ選択/false:ファイル選択
            browser.IsFolderPicker = true;

            //ファイルパスの取得に成功
            if (browser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // バインド用データ作成
                BindData bdata_tmp = new BindData();

                //コンフィグから読み込んだスライドショー間隔を設定データへ代入する。
                bdata_tmp.slideInterval = conData.SlideInterval;

                // バインド用データリストへ追加
                list_bindData.Add(bdata_tmp);

                //設定ウィンドウ作成
                if (sWindow == null)
                {
                    sWindow = new SettingWindow(list_bindData[list_bindData.IndexOf(bdata_tmp)], conData);
                }
                else
                {
                    //処理無し
                }

                // 画像ウィンドウ作成
                ImageWindow iwnd = new ImageWindow(list_iWindow, list_bindData[list_bindData.IndexOf(bdata_tmp)], conData, sWindow, list_bindData);

                //作成した画像ウィンドウをリストに追加
                list_iWindow.Add(iwnd);

                // 画像表示処理を開始する。
                if (iwnd.fnc_StartbyOpen(browser))
                {
                    // 画像ウィンドウ表示
                    iwnd.Show();
                }
            }
            //ダイアログのリソース解放
            browser.Dispose();
        }

        /// <summary>
        /// メニュー：設定クリック時のイベントハンドラ
        /// 設定画面を開く。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuSetting(object sender, RoutedEventArgs e)
        {
            //設定画面を開く。
            fnc_OpenSetting();
        }

        /// <summary>
        /// 設定画面を開く(表示する)。
        /// </summary>
        private void fnc_OpenSetting()
        {
            //設定ウィンドウが存在する場合
            if (sWindow != null)
            {
                //設定ウィンドウが表示状態
                if (sWindow.Visibility == Visibility.Visible)
                {
                    //ウィンドウをアクティブにする。
                    sWindow.Activate();
                }
                //設定ウィンドウが非表示状態
                else if (sWindow.Visibility == Visibility.Hidden)
                {
                    //ウィンドウを表示させる。
                    sWindow.fnc_SwitchVisibility_swnd();
                }
            }
            else
            {
                //処理無し
            }
        }

        /// <summary>
        /// メニュー：表示/非表示クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuSwVisibility(object sender, RoutedEventArgs e)
        {
            //画像ウィンドウの表示/非表示を切り替える
            fnc_SwitchVisibility();
        }

        /// <summary>
        /// 画像ウィンドウの表示/非表示を切り替える
        /// </summary>
        private void fnc_SwitchVisibility()
        {
            //全画像ウィンドウの表示/非表示切り替えを実施する
            foreach (ImageWindow iwnd in list_iWindow)
            {
                iwnd.fnc_SwitchVisibility_iwnd();
            }
        }

        /// <summary>
        /// メニュー：終了クリック時のイベントハンドラ
        /// プログラムを終了させる。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuEnd(object sender, RoutedEventArgs e)
        {
            //プログラムを終了させる。
            fnc_EndProcess();
        }

        /// <summary>
        /// プログラムを終了させる。
        /// </summary>
        private void fnc_EndProcess()
        {
            //アプリ終了時に画像ウィンドウが存在する場合
            if (list_iWindow.Count != 0)
            {
                foreach (ImageWindow iwnd in list_iWindow)
                {
                    //コンフィグデータを設定する。
                    iwnd.fnc_iWndValueToConfig();

                    //画像ウィンドウを全て終了させる。
                    iwnd.fnc_CloseWindow();
                }

                //コンフィグデータを保存する。
                conData.fnc_SaveConfig();
            }
            //アプリ終了時に画像ウィンドウが存在しない場合
            else
            {
                //コンフィグデータを規定値にリセットする。
                conData.fnc_ResetConfig();
            }

            //バインド用データリストを削除する。
            list_bindData.Clear();

            //画像ウィンドウリストを削除する。
            list_iWindow.Clear();

            //設定ウィンドウを削除する。
            if (sWindow != null)
            {
                sWindow.fnc_CloseWindow();
            }
            else
            {
                //処理無し
            }

            //通知アイコンを削除する。
            tbi.Dispose();

            //常駐処理を終了する。
            this.Close();
        }

        /// <summary>
        /// hnd_IconDClickの有効無効設定。（常に有効）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_CanDClickActivate(object sender, CanExecuteRoutedEventArgs e)
        {
            //常駐アイコンダブルクリック時の
            e.CanExecute = true;
        }

        /// <summary>
        /// 常駐アイコンダブルクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_IconDClick(object sender, ExecutedRoutedEventArgs e)
        {
            //画像ウィンドウをアクティブにする
            fnc_ActivateiWindow();
        }

        /// <summary>
        /// 画像ウィンドウをアクティブにする
        /// </summary>
        private void fnc_ActivateiWindow()
        {
            foreach (ImageWindow iwnd in list_iWindow)
            {
                iwnd.Activate();
            }
        }
    }
}

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
    /// クラス名：メインウィンドウクラス
    /// 概要：タスクトレイに常駐するプログラム。画像ウィンドウ、設定ウィンドウの管理を行う。
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// ファイルOPEN形式の列挙型定義
        /// 0:ファイル
        /// 1:フォルダ
        /// </summary>
        enum OpenType
        {
            FILE,
            FOLDER,
            NONE,
        }

        // ファイルOPEN形式
        private OpenType oType
        {
            get; set;
        }

        // command用メンバ
        public static RoutedCommand DClickCommand = new RoutedCommand();

        // 画像ウィンドウリスト
        public List<ImageWindow> list_iWindow
        {
            get; set;
        }

        // 設定ウィンドウ
        public SettingWindow sWindow
        {
            get;set;
        }

        // コンフィグデータ
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

            // 画像ウィンドウリストを作成
            list_iWindow = new List<ImageWindow>();

            // コンフィグデータ読込み
            conData = new ConfigData();

            // ファイルOPEN形式の初期化
            oType = OpenType.NONE;

            // 前回起動時の状態確認
            fnc_CheckConfig();
        }

        /// <summary>
        /// コンフィグファイルを確認し、前回起動時の状態を復元するか判断する。
        /// </summary>
        private void fnc_CheckConfig()
        {
            // 前回終了時のコンフィグデータ無し
            if (conData.List_FilePath == null)
            {
                // 処理無し
            }
            // 前回終了時のコンフィグデータ有り
            else
            {
                // 前回の画像ウィンドウを復元する。
                fnc_ResurrectiWindow();
            }
        }

        /// <summary>
        /// 前回終了時の画像ウィンドウ復元処理
        /// </summary>
        private void fnc_ResurrectiWindow()
        {
            // 設定ウィンドウ作成
            fnc_MakesWindow();

            // 画像ウィンドウ作成
            ImageWindow iwnd = fnc_MakeiWindow();

            // 画像表示処理を開始する。
            iwnd.fnc_StartbyConfig();

            // 画像ウィンドウ表示
            iwnd.Show();
        }

        /// <summary>
        /// 設定ウィンドウを作成する。
        /// </summary>
        /// <param name="bdata_tmp"></param>
        private void fnc_MakesWindow()
        {
            // 設定ウィンドウ作成
            if (sWindow == null)
            {
                sWindow = new SettingWindow(conData.SlideInterval);
            }
            // 作成済みの場合は追加作成しない。(設定ウィンドウは1個のみ。)
            else
            {
                // 処理無し
            }
        }

        /// <summary>
        /// 画像ウィンドウを作成する。
        /// </summary>
        /// <returns></returns>
        private ImageWindow fnc_MakeiWindow()
        {
            // 画像ウィンドウ作成
            ImageWindow iwnd = new ImageWindow(this);

            // 作成した画像ウィンドウをリストに追加
            list_iWindow.Add(iwnd);

            // データバインディングを行う。
            iwnd.fnc_Binding();

            return iwnd;
        }

        /// <summary>
        /// メニュー：ファイルから開くクリック時のイベントハンドラ
        /// 画像の読み込み処理を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuOpenFile(object sender, RoutedEventArgs e)
        {
            // ファイルOPEN形式をファイルに設定
            oType = OpenType.FILE;

            // ファイルOPEN処理
            fnc_OpenProcess();
        }

        /// <summary>
        /// 再生ファイルを指定し、画像ウィンドウを表示する。
        /// </summary>
        private void fnc_OpenProcess()
        {
            // ファイルダイアログ生成
            CommonOpenFileDialog browser = new CommonOpenFileDialog();

            // ファイルダイアログの設定を行う。
            fnc_DialogSetting(browser);

            // 画像表示開始処理
            fnc_StartProcess(browser);
        }

        /// <summary>
        /// ファイルダイアログの設定を行う。
        /// </summary>
        private void fnc_DialogSetting(CommonOpenFileDialog browser)
        {
            // ダイアログ設定：共通
            fnc_DialogSetting_Common(browser);

            // 選択形式別設定
            switch (oType)
            {
                case OpenType.FILE:
                    // ダイアログ設定：ファイル選択
                    fnc_DialogSetting_File(browser);
                    break;
                case OpenType.FOLDER:
                    // ダイアログ設定：フォルダ選択
                    fnc_DialogSetting_Folder(browser);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ダイアログ設定：共通
        /// </summary>
        /// <param name="browser"></param>
        private void fnc_DialogSetting_Common(CommonOpenFileDialog browser)
        {
            // 複数ファイル選択：許可
            browser.Multiselect = true;
        }

        /// <summary>
        /// ダイアログ設定：ファイル選択
        /// </summary>
        /// <param name="browser"></param>
        private void fnc_DialogSetting_File(CommonOpenFileDialog browser)
        {
            // ダイアログのタイトル設定
            browser.Title = "ファイル選択";

            // フォルダ選択不可（ファイル選択）
            browser.IsFolderPicker = false;

            // ファイル形式フィルタ設定
            browser.Filters.Add(new CommonFileDialogFilter("画像ファイル", "*.jpeg;*.JPEG;*.jpg;*.JPG;*.bmp;*.gif;*.png"));
            browser.Filters.Add(new CommonFileDialogFilter("動画ファイル", "*.mp4;*.avi;*.mkv;*.mpeg;*.wmv"));
            browser.Filters.Add(new CommonFileDialogFilter("音楽ファイル", "*.mp3;*.MP3"));
            browser.Filters.Add(new CommonFileDialogFilter("すべてのファイル", "*.*"));
        }

        /// <summary>
        /// ダイアログ設定：フォルダ選択
        /// </summary>
        /// <param name="browser"></param>
        private void fnc_DialogSetting_Folder(CommonOpenFileDialog browser)
        {
            // ダイアログのタイトル設定
            browser.Title = "フォルダ選択";

            // フォルダ選択:許可
            browser.IsFolderPicker = true;
        }

        /// <summary>
        /// 画像表示開始処理
        /// </summary>
        private void fnc_StartProcess(CommonOpenFileDialog browser)
        {
            // ファイルパスの取得に成功
            if (browser.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // 設定ウィンドウ作成
                fnc_MakesWindow();

                // 画像ウィンドウ作成
                ImageWindow iwnd = fnc_MakeiWindow();

                // 画像表示処理を開始する。
                if (iwnd.fnc_StartbyOpen(browser) == true)
                {
                    // 画像ウィンドウ表示
                    iwnd.Show();
                }
                // 画像表示処理の開始に失敗
                else
                {
                    // 処理無し
                }
            }
            // ファイルパスの取得に失敗
            else
            {
                // 処理無し
            }

            // ダイアログのリソース解放
            browser.Dispose();

            // ファイルOPEN形式を初期化
            oType = OpenType.NONE;
        }

        /// <summary>
        /// メニュー：フォルダーを開くクリック時のイベントハンドラ
        /// 画像の読み込み処理を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuOpenFolder(object sender, RoutedEventArgs e)
        {
            // ファイルOPEN形式をフォルダに設定
            oType = OpenType.FOLDER;

            // ファイルOPEN処理
            fnc_OpenProcess();
        }

        /// <summary>
        /// メニュー：設定クリック時のイベントハンドラ
        /// 設定画面を開く。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuSetting(object sender, RoutedEventArgs e)
        {
            // 設定画面を開く。
            fnc_OpenSetting();
        }

        /// <summary>
        /// 設定画面を開く。
        /// </summary>
        public void fnc_OpenSetting()
        {
            // 設定ウィンドウが存在する場合
            if (sWindow != null)
            {
                // 設定ウィンドウが表示状態
                if (sWindow.Opacity != 0.0)
                {
                    // ウィンドウをアクティブにする。
                    sWindow.Activate();
                }
                // 設定ウィンドウが非表示状態
                else if (sWindow.Opacity == 0.0)
                {
                    // ウィンドウを表示させる。
                    sWindow.fnc_SwitchVisibility_swnd();
                }
            }
            else
            {
                // 処理無し
            }
        }

        /// <summary>
        /// メニュー：表示/非表示クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MenuSwVisibility(object sender, RoutedEventArgs e)
        {
            // 画像ウィンドウの表示/非表示を切り替える
            fnc_SwitchVisibility();
        }

        /// <summary>
        /// 画像ウィンドウの表示/非表示を切り替える
        /// </summary>
        public void fnc_SwitchVisibility()
        {
            // 全画像ウィンドウの表示/非表示切り替えを実施する
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
            // プログラムを終了させる。
            fnc_EndProcess();
        }

        /// <summary>
        /// プログラムを終了させる。
        /// </summary>
        private void fnc_EndProcess()
        {
            // 画像ウィンドウを削除する。
            fnc_DeleteiWindow();

            // 設定ウィンドウを削除する。
            fnc_DeletesWindow();

            // 画像ウィンドウリストを削除する。
            list_iWindow.Clear();

            // 通知アイコンを削除する。
            tbi.Dispose();

            // 常駐処理を終了する。
            this.Close();
        }

        /// <summary>
        /// 画像ウィンドウを削除する。
        /// </summary>
        private void fnc_DeleteiWindow()
        {
            // アプリ終了時に画像ウィンドウが存在する場合
            if (list_iWindow.Count != 0)
            {
                // 全ての画像ウィンドウ
                foreach (ImageWindow iwnd in list_iWindow)
                {
                    // コンフィグデータを設定する。
                    iwnd.fnc_iWndValueToConfig();

                    // 画像ウィンドウを全て終了させる。
                    iwnd.fnc_CloseWindow();
                }

                // コンフィグデータを保存する。
                conData.fnc_SaveConfig();
            }
            // アプリ終了時に画像ウィンドウが存在しない場合
            else
            {
                // コンフィグデータを規定値にリセットする。
                conData.fnc_ResetConfig();
            }
        }

        /// <summary>
        /// 設定ウィンドウを削除する。
        /// </summary>
        private void fnc_DeletesWindow()
        {
            // 設定ウィンドウが有る場合
            if (sWindow != null)
            {
                // 設定ウィンドウを閉じる。
                sWindow.fnc_CloseWindow();
            }
            else
            {
                // 処理無し
            }
        }

        /// <summary>
        /// hnd_IconDClickの有効無効設定。（常に有効）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_CanDClickActivate(object sender, CanExecuteRoutedEventArgs e)
        {
            //常駐アイコンダブルクリック時の処理：有効
            e.CanExecute = true;
        }

        /// <summary>
        /// 常駐アイコンダブルクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_IconDClick(object sender, ExecutedRoutedEventArgs e)
        {
            // 画像ウィンドウをアクティブにする
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Drawing;
using WpfAnimatedGif;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Threading;

namespace ImageViewer
{
    /// <summary>
    /// クラス名：画像ウィンドウ
    /// 各種ファイルの再生・表示を行う。
    /// </summary>
    public partial class ImageWindow : Window
    {
        /// <summary>
        /// ファイル形式の列挙型定義
        /// 0:画像(gif以外)
        /// 1:画像(gif)
        /// 2:動画
        /// 3:音楽
        /// </summary>
        enum ImageType
        {
            IMG,
            GIF,
            MOV,
            MSC,
            NONE,
        }

        // ファイル形式の判定結果
        private ImageType iType
        {
            get; set;
        }

        /// <summary>
        /// 動画再生状態の列挙型定義
        /// 0:再生中
        /// 1:停止
        /// </summary>
        enum PlayState
        {
            PLAY,
            PAUSE,
            STOP,
        }

        // 動画再生状態
        private PlayState movieState
        {
            get; set;
        }

        // 音楽再生時の背景画像パス
        private const string MUSICBG = "pack://application:,,,/Resources/MUSICbckg.png";
        
        // 再生ファイルのパスリスト
        public List<string> list_filePath
        {
            get; set;
        }

        // 再生中のファイルINDEX
        private int playNo
        {
            get; set;
        }

        // スライドショー状態（true:開始/false:停止）
        public bool slideShowState
        {
            get; set;
        }

        // スライドショータイマー
        private System.Windows.Forms.Timer Slideshow_timer
        {
            get; set;
        }

        // バインドデータ
        private BindData bindData
        {
            get; set;
        }

        // メインウィンドウ参照用
        private MainWindow ref_mWindow
        {
            get; set;
        }

        // シークバー位置更新タイマー
        private System.Windows.Forms.Timer SeekBarUpdate_timer
        {
            get; set;
        }

        // タイマー起動間隔（1秒）
        private const int timerInterval = 100;
        
        // 動画経過時間
        private int elapsedMsec
        {
            get; set;
        }

        // タイマ更新フラグ
        private bool timerChanging
        {
            get; set;
        }

        // シークバー操作完了フラグ
        private bool SeekbarChanging
        {
            get; set;
        }

        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageWindow(MainWindow mwnd)
        {
            InitializeComponent();

            // オブジェクト作成
            // ファイルパスのリスト
            list_filePath = new List<string>();

            // バインドデータ
            bindData = new BindData();

            // スライドショータイマー
            Slideshow_timer = new System.Windows.Forms.Timer();

            // スライドショータイマー制御イベントハンドラ作成
            Slideshow_timer.Tick += new EventHandler(hnd_SlideTime);

            // シークバー位置更新タイマー
            SeekBarUpdate_timer = new System.Windows.Forms.Timer();

            // シークバー位置更新タイマー制御イベントハンドラ作成
            SeekBarUpdate_timer.Tick += new EventHandler(hnd_UpdateSeekPosi);

            // 初期化
            // メインウィンドウ
            ref_mWindow = mwnd;

            // 動画再生状態
            movieState = PlayState.STOP;

            // スライドショー状態
            slideShowState = false;

            // 再生中のファイルINDEX
            playNo = 0;

            // 動画経過時間
            elapsedMsec = 0;

            // タイマー更新フラグ
            timerChanging = true;

            // シークバー操作完了フラグ
            SeekbarChanging = false;
        }

        /// <summary>
        /// データバインディングを行う。
        /// </summary>
        public void fnc_Binding()
        {
            // バインド用データをバインディング
            DataContext = bindData;
        }

        /// <summary>
        /// コンフィグデータをウィンドウ情報へ設定する
        /// （前回終了時の状態を復元する。）
        /// </summary>
        private void fnc_ConfigToValue()
        {
            // ウィンドウ情報を設定する。
            this.Height = ref_mWindow.conData.WindowHeight;
            this.Width = ref_mWindow.conData.WindowWidth;
            this.Left = ref_mWindow.conData.WinposiX;
            this.Top = ref_mWindow.conData.WinposiY;
            bindData.iwndBackGrnd = ref_mWindow.conData.WndBackGrnd;
            this.playNo = ref_mWindow.conData.PlayNo;

            // スライドショー間隔を設定する。
            bindData.slideInterval = ref_mWindow.conData.SlideInterval;

            // ファイルパスを設定する。
            if (ref_mWindow.conData.List_FilePath != null)
            {
                // listのコンストラクタにより値渡しで設定する。
                list_filePath = new List<string>(ref_mWindow.conData.List_FilePath);
            }
        }

        /// <summary>
        /// 開くメニューによる画像表示の開始処理
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool fnc_StartbyOpen(CommonOpenFileDialog browser)
        {
            bool emptyPath = true;          //true:空/false:ファイルパス有り

            // フォルダを開く
            if (browser.IsFolderPicker == true)
            {
                fnc_MakePathFromFolders(browser, ref emptyPath);
            }
            // ファイルを開く
            else if (browser.IsFolderPicker == false)
            {
                fnc_MakePathFromFiles(browser, ref emptyPath);
            }

            //パス無し
            if (emptyPath == true)
            {
                return false;
            }

            //表示画像を設定する
            fnc_SetImage();

            return true;
        }

        /// <summary>
        /// フォルダを開く際の、ファイルパスリスト作成処理
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="emptyPath"></param>
        private void fnc_MakePathFromFolders(CommonOpenFileDialog browser, ref bool emptyPath)
        {
            string[] filePath = null;       //フォルダ内のファイルパス

            // ファイルパスをクリアする。
            if (list_filePath != null)
            {
                list_filePath.Clear();
            }

            // 各フォルダ内のファイルパスを取得する。
            foreach (string folder in browser.FileNames)
            {
                // サブフォルダを含めて検索する。
                filePath = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

                // 取得したファイルパスをリストへ格納する
                foreach (string fp in filePath)
                {
                    list_filePath.Add(fp);

                    // パス無し
                    if (string.IsNullOrEmpty(fp))
                    {
                        emptyPath = true;
                    }
                    // パス有り
                    else
                    {
                        emptyPath = false;
                    }
                }
            }
        }

        /// <summary>
        /// ファイルを開く際の、ファイルパスリスト作成処理
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="emptyPath"></param>
        private void fnc_MakePathFromFiles(CommonOpenFileDialog browser, ref bool emptyPath)
        {
            // ファイルパスをクリアする。
            if (list_filePath != null)
            {
                list_filePath.Clear();
            }

            //取得したファイルパスをリストへ格納する
            foreach (string fp in browser.FileNames)
            {
                list_filePath.Add(fp);

                // パス無し
                if (string.IsNullOrEmpty(fp))
                {
                    emptyPath = true;
                }
                // パス有り
                else
                {
                    emptyPath = false;
                }
            }
        }

        /// <summary>
        /// ドラッグアンドドロップされたファイルの受け入れ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Drop(object sender, System.Windows.DragEventArgs e)
        {
            // ドロップされたものがFileDrop形式の場合は、各ファイルのパス文字列を文字列配列に格納する。
            string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

            // ドラッグアンドドロップによる画像表示の開始処理
            fnc_StartbyDrop(files);
        }

        /// <summary>
        /// ドラッグアンドドロップによる画像表示の開始処理
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool fnc_StartbyDrop(string[] files)
        {
            bool emptyPath = true;  // true:空/false:ファイルパス有り

            // 画像ウィンドウを作成する。
            ImageWindow iwnd　= fnc_MakeiWindow();

            // ファイルパスリストをクリアする。
            iwnd.list_filePath.Clear();

            // フォルダがドロップされた場合
            try
            {
                fnc_MakePathFromFolders(files, ref emptyPath, iwnd);
            }
            // ファイルがドロップされた場合
            catch (IOException)
            {
                fnc_MakePathFromFiles(files, ref emptyPath, iwnd);
            }

            // パス無し
            if (emptyPath == true)
            {
                return false;
            }
            else
            {
                // 処理無し
            }

            // 表示画像を設定する
            iwnd.fnc_SetImage();

            // 画面表示
            iwnd.Show();

            return true;
        }

        /// <summary>
        /// 画像ウィンドウを作成する。
        /// </summary>
        private ImageWindow fnc_MakeiWindow()
        {
            // 画像ウィンドウのオブジェクト作成
            ImageWindow iwnd = new ImageWindow(ref_mWindow);

            // 作成した画像ウィンドウをリストに追加
            ref_mWindow.list_iWindow.Add(iwnd);

            // データバインディングを行う。
            iwnd.fnc_Binding();

            return iwnd;
        }

        /// <summary>
        /// フォルダをドロップした際の、ファイルパスリスト作成処理
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="emptyPath"></param>
        private void fnc_MakePathFromFolders(string[] files, ref bool emptyPath, ImageWindow iwnd)
        {
            string[] filePath = null;       // フォルダ内のファイルパス

            // 各フォルダ内のファイルパスを取得する。
            foreach (string folder in files)
            {
                // サブフォルダを含めて検索する。
                filePath = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

                // 取得したファイルパスをリストへ格納する
                foreach (string fp in filePath)
                {
                    iwnd.list_filePath.Add(fp);

                    // パス無し
                    if (string.IsNullOrEmpty(fp))
                    {
                        emptyPath = true;
                    }
                    // パス有り
                    else
                    {
                        emptyPath = false;
                    }
                }
            }
        }

        /// <summary>
        /// ファイルをドロップした際の、ファイルパスリスト作成処理
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="emptyPath"></param>
        private void fnc_MakePathFromFiles(string[] files, ref bool emptyPath, ImageWindow iwnd)
        {
            // 取得したファイルパスをリストへ格納する
            foreach (string fp in files)
            {
                iwnd.list_filePath.Add(fp);

                // パス無し
                if (string.IsNullOrEmpty(fp))
                {
                    emptyPath = true;
                }
                // パス有り
                else
                {
                    emptyPath = false;
                }
            }
        }

        /// <summary>
        /// コンフィグファイルによる画像表示の開始処理
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public bool fnc_StartbyConfig()
        {
            // コンフィグデータをウィンドウ情報へ設定する
            fnc_ConfigToValue();

            // 表示画像を設定する
            fnc_SetImage();

            return true;
        }

        /// <summary>
        /// 画像設定処理
        /// </summary>
        private void fnc_SetImage()
        {
            // 拡縮率を初期化
            bindData.scaleValue = 1.0;

            // ファイル形式判定
            iType = fnc_JdgType(list_filePath[playNo]);

            // ファイル形式に応じた設定処理の呼び出し
            switch (iType)
            {
                // 画像ファイル(gif以外)
                case ImageType.IMG:
                    fnc_SetIMG(list_filePath[playNo]);
                    fnc_HideSeekBar();
                    break;
                // 画像ファイル(gif)
                case ImageType.GIF:
                    fnc_SetGIF(list_filePath[playNo]);
                    fnc_HideSeekBar();
                    break;
                // 動画ファイル
                case ImageType.MOV:
                    fnc_SetMOV(list_filePath[playNo]);
                    fnc_IndicateSeekBar();
                    fnc_MediaStarted();
                    break;
                // 音楽ファイル
                case ImageType.MSC:
                    fnc_SetMSC(list_filePath[playNo]);
                    fnc_IndicateSeekBar();
                    fnc_MediaStarted();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ファイル形式を判定する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private ImageType fnc_JdgType(string filePath)
        {
            // 画像ファイル(gif以外)かどうか
            if (filePath.Contains(".jpeg") == true
              || filePath.Contains(".JPEG") == true
              || filePath.Contains(".jpg") == true
              || filePath.Contains(".JPG") == true
              || filePath.Contains(".bmp") == true
              || filePath.Contains(".BMP") == true
              || filePath.Contains(".png") == true
              || filePath.Contains(".PNG") == true)
            {
                return ImageType.IMG;
            }
            // 画像ファイル(gif)かどうか
            else if (filePath.Contains(".gif") == true)
            {
                return ImageType.GIF;
            }
            // 動画ファイルかどうか
            else if (filePath.Contains(".mp4") == true
              || filePath.Contains(".avi") == true
              || filePath.Contains(".mkv") == true
              || filePath.Contains(".mpeg") == true
              || filePath.Contains(".wmv") == true)
            {
                return ImageType.MOV;
            }
            // 音楽ファイルかどうか
            else if (filePath.Contains(".mp3") == true
              || filePath.Contains(".MP3") == true)
            {
                return ImageType.MSC;
            }
            // 該当無し
            else
            {
                return ImageType.NONE;
            }
        }

        /// <summary>
        /// jpg, bmp, png画像の設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetIMG(string filePath)
        {
            // 画像オブジェクト作成
            BitmapImage bmpImage = new BitmapImage();

            // ※排他制御回避の為、ファイルストリームを用いてファイルOPEN
            using (FileStream stream = File.OpenRead(filePath))
            {
                bmpImage.BeginInit();
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.CreateOptions = BitmapCreateOptions.None;
                bmpImage.StreamSource = stream;
                bmpImage.EndInit();
                stream.Close();
                stream.Dispose();
            }

            // Imageコントロールに画像を設定する。
            img.Source = bmpImage;

            bmpImage.Freeze();
        }

        /// <summary>
        /// gif画像の設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetGIF(string filePath)
        {
            // gif画像パスの更新
            bindData.gifPath = filePath;
        }

        /// <summary>
        /// 動画ファイルの設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetMOV(string filePath)
        {
            // 動画オブジェクト作成
            MediaElement mov = new MediaElement();

            mov.BeginInit();
            mov.Source = new Uri(filePath);
            mov.EndInit();
            
            // MediaElementコントロールに動画を設定する。
            movie.Source = mov.Source;
        }

        /// <summary>
        /// 音楽ファイルの設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetMSC(string filePath)
        {
            // 背景画像の設定
            var uri = new Uri(MUSICBG);
            img.Source = new BitmapImage(uri);

            // 音楽オブジェクト作成
            MediaElement msc = new MediaElement();

            msc.BeginInit();
            msc.Source = new Uri(filePath);
            msc.EndInit();

            // MediaElementコントロールに音楽を設定する。
            movie.Source = msc.Source;
        }

        /// <summary>
        /// ウィンドウの表示/非表示を切り替える
        /// </summary>
        public void fnc_SwitchVisibility_iwnd()
        {
            // 表示を非表示にする
            if (this.Opacity != 0.0)
            {
                fnc_HideWindow();
            }
            // 非表示を表示にする
            else if (this.Opacity == 0.0)
            {
                fnc_IndicateWindow();
            }
            else
            {
                // 処理無し
            }
        }

        /// <summary>
        /// ウィンドウをドラッグ中のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DragWindow(object sender, MouseButtonEventArgs e)
        {
            // ウィンドウ枠外のドラッグでもウィンドウの移動を可能にする。
            DragMove();
        }

        /// <summary>
        /// ウィンドウ左側クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_ClickLeftField(object sender, MouseButtonEventArgs e)
        {
            // 前の画像を表示する。
            fnc_BackImage();
        }

        /// <summary>
        /// 前の画像を表示する。
        /// </summary>
        private void fnc_BackImage()
        {
            // キー操作で再生中断される為、停止にする。
            movieState = PlayState.STOP;

            // 動画経過時間を初期化する。
            elapsedMsec = 0;
            SeekBar.Value = 0;

            // シークバー更新処理を停止する。
            fnc_UpdateSeekPosiManager();

            // 表示中の画像INDEXを戻す。
            fnc_BackpNo();

            // 表示中の画像をクリア
            fnc_ClearImage();

            // 画像表示処理
            fnc_SetImage();
        }

        /// <summary>
        /// 表示中の画像INDEXを戻す。
        /// </summary>
        private void fnc_BackpNo()
        {
            // リストの先頭の場合
            if (playNo == 0)
            {
                // INDEXを末尾に移動させる
                playNo = (list_filePath.Count - 1);
            }
            else
            {
                // INDEXを戻す
                playNo = playNo - 1;
            }
        }

        /// <summary>
        /// ウィンドウ右側クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_ClickRightField(object sender, MouseButtonEventArgs e)
        {
            // 次の画像を表示する。
            fnc_NextImage();
        }

        /// <summary>
        /// 次の画像を表示する。
        /// </summary>
        private void fnc_NextImage()
        {
            // 再生中断される為、停止にする。
            movieState = PlayState.STOP;

            // 動画経過時間を初期化する。
            elapsedMsec = 0;
            SeekBar.Value = 0;

            // シークバー更新処理を停止する。
            fnc_UpdateSeekPosiManager();

            // 表示中の画像INDEXを進める。
            fnc_NextpNo();

            // 表示中の画像をクリア
            fnc_ClearImage();
             
            // 画像表示処理
            fnc_SetImage();
        }

        /// <summary>
        /// 表示中の画像INDEXを進める。
        /// </summary>
        private void fnc_NextpNo()
        {
            // リストの末尾の場合
            if (playNo == (list_filePath.Count - 1))
            {
                // INDEXを先頭に戻す
                playNo = 0;
            }
            else
            {
                // INDEXを進める
                playNo = playNo + 1;
            }
        }

        /// <summary>
        /// 表示画像クリア
        /// </summary>
        private void fnc_ClearImage()
        {
            // 画像(gif以外)のクリア
            img.Source = null;

            // 画像(gif)のクリア
            bindData.gifPath = null;

            // 動画のクリア
            movie.Source = null;

            movie.Close();
        }

        /// <summary>
        /// 要素上にドラッグされてきた時の、ドロップ受け入れ準備処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            // マウスカーソルをコピーにする
            e.Effects = System.Windows.DragDropEffects.Copy;

            // ドラッグされてきたものがFileDrop形式の場合だけ、このイベントを処理済みにする
            e.Handled = e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop); 
        }

        /// <summary>
        /// ホイール操作時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SpinWheel(object sender, MouseWheelEventArgs e)
        {
            fnc_ScaleImage(sender, e);
        }

        /// <summary>
        /// 表示画像の拡大・縮小を行う。
        /// </summary>
        private void fnc_ScaleImage(object sender, MouseWheelEventArgs e)
        {
            double tmp = 0.0;

            // ホイールを上に回す→拡大
            if (e.Delta > 0)
            {
                // 拡縮率が10.0未満なら加算
                if (bindData.scaleValue < BindData.SCALEMAX)
                {
                    // 誤差回避の為、整数に戻して計算
                    tmp = bindData.scaleValue * 10;
                    tmp++;
                    bindData.scaleValue = tmp / 10;
                }
            }
            // ホイールを下に回す→縮小
            else
            {
                // 拡縮率が0.1超過なら加算
                if (bindData.scaleValue > BindData.SCALEMIN)
                {
                    // 誤差回避の為、整数に戻して計算
                    tmp = bindData.scaleValue * 10;
                    tmp--;
                    bindData.scaleValue = tmp / 10;
                }
            }
        }

        /// <summary>
        /// キーボード入力(Down)時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DownKey(object sender, System.Windows.Input.KeyEventArgs e)
        {
            fnc_DKeyControl(sender, e);
        }

        /// <summary>
        /// 入力キーに応じた処理を呼び出す。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fnc_DKeyControl(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                // 左方向キー：前の画像を表示
                case Key.Left:
                    fnc_BackImage();
                    break;
                // 右方向キー：次の画像を表示
                case Key.Right:
                    fnc_NextImage();
                    break;
                // スライドショー開始
                case Key.D1:
                    fnc_PlaySlideshow();
                    break;
                // スライドショー停止
                case Key.D2:
                    fnc_StopSlideshow();
                    break;
                // ウィンドウ背景切替
                case Key.B:
                    fnc_SwitchBackGrnd();
                    break;
                // 音量有無切替
                case Key.M:
                    fnc_SwitchMute();
                    break;
                // 設定ウィンドウ表示
                case Key.S:
                    fnc_OpenSetting();
                    break;
                // 最前面表示切替
                case Key.T:
                    fnc_SwitchMost();
                    break;
                // 動画・音楽の一時停止/再生
                case Key.Space:
                    fnc_PauseMedia();
                    break;
                // Enterキー：表示/非表示を切り替える
                case Key.Enter:
                    ref_mWindow.fnc_SwitchVisibility();
                    break;
                // Escキー：ウィンドウを閉じる
                case Key.Escape:
                    fnc_CloseProcess();
                    break;
                default:
                    break;
            }

            // ルーティングイベント(バブル)の中断
            // ※Escキーの2重動作を回避する為。
            e.Handled = true;
        }

        /// <summary>
        /// ボリューム有無切替
        /// </summary>
        private void fnc_SwitchMute()
        {
            // ボリューム有り→無し
            if (bindData.Volume != 0.0)
            {
                bindData.Volume = 0.0;
                bindData.check_Volume = false;
            }
            // ボリューム無し→有り
            else
            {
                bindData.Volume = 0.5;
                bindData.check_Volume = true;
            }
        }

        /// <summary>
        /// 設定ウィンドウを表示する。
        /// </summary>
        private void fnc_OpenSetting()
        {
            ref_mWindow.fnc_OpenSetting();
        }

        /// <summary>
        /// 動画・音楽の一時停止/再生を行う。
        /// </summary>
        private void fnc_PauseMedia()
        {
            switch (movieState)
            {
                // 再生中：一時停止する
                case PlayState.PLAY:
                    // メディアを一時停止する。
                    movie.Pause();
                    movieState = PlayState.PAUSE;
                    // シークバー位置更新を停止する。
                    fnc_UpdateSeekPosiManager();
                    break;
                // 一時停止,停止：再生する
                case PlayState.PAUSE:
                case PlayState.STOP:
                    // メディアを再生する。
                    movie.Play();
                    movieState = PlayState.PLAY;
                    // シークバー位置更新を開始する。
                    fnc_UpdateSeekPosiManager();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 最前面表示切替
        /// </summary>
        private void fnc_SwitchMost()
        {
            // 最前面表示無効→有効
            if (bindData.tMost == false)
            {
                bindData.tMost = true;
                bindData.check_tMost = true;
            }
            // 最前面表示有効→無効
            else
            {
                bindData.tMost = false;
                bindData.check_tMost = false;
            }
        }

        /// <summary>
        /// 画像ウィンドウ終了処理
        /// </summary>
        private void fnc_CloseProcess()
        {
            // スライドショーを停止させる。
            fnc_StopSlideshow();

            // 画像ウィンドウ終了処理
            fnc_CloseWindow();

            // メインの持つ画像ウィンドウリストから自分を削除する。
            ref_mWindow.list_iWindow.Remove(this);

            // ウィンドウクローズ時にメモリ解放させる。
            GC.Collect();
        }

        /// <summary>
        /// 画像ウィンドウ情報をコンフィグデータに設定する。
        /// </summary>
        public void fnc_iWndValueToConfig()
        {
            // 各種コンフィグデータを設定する。
            ref_mWindow.conData.WindowHeight = this.Height;
            ref_mWindow.conData.WindowWidth = this.Width;
            ref_mWindow.conData.WinposiX = this.Left;
            ref_mWindow.conData.WinposiY = this.Top;
            ref_mWindow.conData.WndBackGrnd = bindData.iwndBackGrnd;
            ref_mWindow.conData.SlideInterval = bindData.slideInterval;
            ref_mWindow.conData.PlayNo = this.playNo;

            // nullならリスト作成
            if (ref_mWindow.conData.List_FilePath == null)
            {
                ref_mWindow.conData.List_FilePath = new List<string>();
            }

            // リスト初期化
            ref_mWindow.conData.List_FilePath.Clear();

            // リスト追加
            foreach (string fp in list_filePath)
            {
                ref_mWindow.conData.List_FilePath.Add(fp);
            }
        }

        /// <summary>
        /// 画像ウィンドウ終了処理
        /// </summary>
        public void fnc_CloseWindow()
        {
            // 表示画像クリア
            fnc_ClearImage();

            // ファイルパスリストを削除する。
            this.list_filePath.Clear();

            // 画像ウィンドウを閉じる。
            this.Close();
        }

        /// <summary>
        /// ダブルクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DClickWindow(object sender, MouseButtonEventArgs e)
        {
            // 全画面と通常画面を切り替える
            fnc_SwitchScreen();
        }

        /// <summary>
        /// 全画面と通常画面を切り替える
        /// </summary>
        private void fnc_SwitchScreen()
        {
            switch (this.WindowState)
            {
                // 通常画面の時、フルスクリーンにする。
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
                // フルスクリーンの時、通常画面にする。
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// スライドショーを開始する。
        /// </summary>
        private void fnc_PlaySlideshow()
        {
            // スライドショー：開始
            slideShowState = true;

            // タイマー処理呼び出し
            fnc_SlideTime(slideShowState);
        }

        /// <summary>
        /// スライドショーを停止する。
        /// </summary>
        private void fnc_StopSlideshow()
        {
            // スライドショー：停止
            slideShowState = false;

            // タイマー処理呼び出し
            fnc_SlideTime(slideShowState);
        }

        /// <summary>
        /// 画像ウィンドウの背景を切り替える
        /// </summary>
        private void fnc_SwitchBackGrnd()
        {
            // 背景を透明→黒に切り替える
            if (bindData.iwndBackGrnd == "Transparent")
            {
                bindData.iwndBackGrnd = "Black";
                bindData.check_iwndBackGrnd = true;
            }
            // 背景を黒→透明に切り替える
            else if (bindData.iwndBackGrnd == "Black")
            {
                bindData.iwndBackGrnd = "Transparent";
                bindData.check_iwndBackGrnd = false;
            }
            else
            {
                //処理無し
            }
        }

        /// <summary>
        /// スライドショー用一定間隔制御
        /// ※スレッド形式のタイマー処理はImageの排他制御により画像変更不可
        /// 　上記理由により、イベント形式のタイマーを採用する
        /// </summary>
        /// <param name="slideShowState"></param>
        private void fnc_SlideTime(bool slideShowState)
        {
            switch (slideShowState)
            {
                // スライドショー状態：再生
                case true:
                    // イベント間隔設定
                    Slideshow_timer.Interval = bindData.slideInterval;
                    Slideshow_timer.Enabled = true;
                    break;
                // スライドショー状態：停止
                case false:
                    Slideshow_timer.Enabled = false;
                    Slideshow_timer.Dispose();
                    break;
            }
        }
        
        /// <summary>
        /// タイマー制御イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SlideTime(object sender, EventArgs e)
        {
            // スライド間隔をリアルタイムに更新する。
            Slideshow_timer.Interval = bindData.slideInterval;

            // 動画再生中ではない時
            if (movieState == PlayState.STOP)
            {
                //次の画像を表示する。
                fnc_NextImage();
            }
            // 動画再生中は画像送りしない。
            else
            {
                //処理無し
            }
        }

        /// 動画再生終了時のイベントハンドラ
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MediaEnded(object sender, RoutedEventArgs e)
        {
            // 動画再生状態を停止にする。
            fnc_MediaEnded();
        }

        /// <summary>
        /// 動画再生状態を停止にする。
        /// ※ユーザー操作により次の画像に移動させた場合
        /// 本イベントは発生しない。
        /// </summary>
        private void fnc_MediaEnded()
        {
            // メディアの再生を停止する
            movie.Stop();

            // 再生状態を停止にする。
            movieState = PlayState.STOP;

            // シークバー位置更新停止
            fnc_UpdateSeekPosiManager();
        }

        /// <summary>
        /// 動画再生状態を再生にする。
        /// </summary>
        private void fnc_MediaStarted()
        {
            // メディアの再生を開始する。
            movie.Play();

            // 再生状態を再生にする。
            movieState = PlayState.PLAY;

            // 動画経過時間を初期化する。
            elapsedMsec = 0;
            SeekBar.Value = 0;
        }

        /// <summary>
        /// ウィンドウアクティブ時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Activated(object sender, EventArgs e)
        {
            // アクティブウィンドウを設定ウィンドウの対象にする。
            fnc_SettingTarget();
        }

        /// <summary>
        /// アクティブウィンドウを設定ウィンドウの対象にする。
        /// </summary>
        private void fnc_SettingTarget()
        {
            // アクティブになった画像ウィンドウをデータバインディング対象にする。
            ref_mWindow.sWindow.fnc_BindToPages(bindData);
        }

        /// <summary>
        /// シークバー位置更新処理の実施制御
        /// </summary>
        /// <param name="movieState"></param>
        private void fnc_UpdateSeekPosiManager()
        {
            switch (movieState)
            {
                // 動画再生状態：再生
                case PlayState.PLAY:
                    // イベント間隔設定
                    SeekBarUpdate_timer.Interval = timerInterval;
                    SeekBarUpdate_timer.Enabled = true;
                    break;
                // 動画再生状態：一時停止
                case PlayState.PAUSE:
                    SeekBarUpdate_timer.Enabled = false;
                    break;
                // 動画再生状態：停止
                case PlayState.STOP:
                    SeekBarUpdate_timer.Enabled = false;
                    SeekBarUpdate_timer.Dispose();
                    // 動画経過時間を初期化する。
                    elapsedMsec = 0;
                    SeekBar.Value = 0;
                    break;
            }
        }

        /// <summary>
        /// シークバー位置更新タイマーのイベントハンドラ。
        /// 動画経過時間に合わせてシークバーを動かす
        /// 周期：100msec
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_UpdateSeekPosi(object sender, EventArgs e)
        {
            double temp;

            // シークバー操作中ではない
            if (SeekbarChanging == false)
            {
                // 動画経過時間を+1秒
                elapsedMsec += timerInterval;

                // シークバーの位置更新
                // シークバーの位置 = シークバー最大値 * ( 動画経過時間(msec) / 動画のトータル時間(msec) )
                temp = (SeekBar.Maximum * (elapsedMsec / SeekBar.Maximum));
                SeekBar.Value = temp;

                // タイマー更新フラグON
                timerChanging = true;
            }
        }

        /// <summary>
        /// シークバーの値が変化した際のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SeekBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            fnc_UpdateMoviePosi();
        }

        /// <summary>
        /// スライダーを動かした位置に合わせて動画の再生箇所を更新する
        /// </summary>
        private void fnc_UpdateMoviePosi()
        {
            // クリックによるシークバー操作後の位置更新
            elapsedMsec = (int)SeekBar.Value;

            //    シークバー操作中ではない
            // && タイマー更新操作ではない
            if ( SeekbarChanging == false 
              && timerChanging == false )
            {
                // シークバーの現在値をmsecに変換
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, elapsedMsec);
                
                // 動画の再生位置を設定
                movie.Position = ts;
            }

            // タイマー更新フラグを初期化
            timerChanging = false;
        }

        /// <summary>
        /// シークバー操作開始時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SeekBarDragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            // シークバー操作中フラグ：ON
            SeekbarChanging = true;
            // タイマー更新フラグ：OFF
            timerChanging = false;
        }

        /// <summary>
        /// シークバー操作完了時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SeekBarDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            // シークバー操作中フラグ：ON
            SeekbarChanging = false;
            // シークバー操作後の位置で更新
            elapsedMsec = (int)SeekBar.Value;
        }

        /// <summary>
        /// シークバーのキー入力イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_SeekBarKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            fnc_UnableKey(e);
        }

        /// <summary>
        /// シークバーのキー入力動作を無効化する。
        /// ※←or→キー入力時、シークバー位置の移動が発生し、次or前の画像表示が出来なくなる為。
        /// </summary>
        /// <param name="e"></param>
        private void fnc_UnableKey(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Left)

                e.Handled = true;
        }

        /// <summary>
        /// シークバーを表示にする。
        /// </summary>
        private void fnc_IndicateSeekBar()
        {
            SeekBar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// シークバーを非表示にする。
        /// </summary>
        private void fnc_HideSeekBar()
        {
            SeekBar.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// ウィンドウ起動時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_ContentRendered(object sender, EventArgs e)
        {
            fnc_IndicateWindow();
        }

        /// <summary>
        /// ウィンドウ表示アニメーション
        /// </summary>
        private void fnc_IndicateWindow()
        {
            Storyboard IndicateWindow = this.FindResource("IndicateWindow") as Storyboard;
            IndicateWindow.Begin();
        }

        /// <summary>
        /// ウィンドウ終了アニメーション
        /// </summary>
        private void fnc_HideWindow()
        {
            Storyboard HideWindow = this.FindResource("HideWindow") as Storyboard;
            HideWindow.Begin();
        }

        /// <summary>
        /// 読込み完了時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MediaOpened(object sender, RoutedEventArgs e)
        {
            // シークバーの最大値設定
            fnc_SetSeekBarMax();

            // シークバー位置更新開始
            fnc_UpdateSeekPosiManager();
        }

        /// <summary>
        /// メディアファイルの再生時間を取得する。
        /// ※MediaOpenedイベント発生後のみ取得可能。
        /// </summary>
        private void fnc_SetSeekBarMax()
        {
            // メディアファイルの再生時間(ms)を取得する。
            SeekBar.Maximum = movie.NaturalDuration.TimeSpan.TotalMilliseconds;
        }
    }
}

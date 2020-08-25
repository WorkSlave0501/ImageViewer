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

namespace ImageViewer
{
    /// <summary>
    /// ImageWindow.xaml の相互作用ロジック
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

        //ファイル形式の判定結果
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
            PLAYING,
            STOP,
        }

        //動画再生状態
        private PlayState movieState
        {
            get; set;
        }

        //ファイルパス
        public List<string> list_filePath
        {
            get; set;
        }

        //再生中のファイルINDEX
        private int playNo
        {
            get; set;
        }

        //画像ウィンドウリスト参照用
        public List<ImageWindow> ref_list_iWindow
        {
            get; set;
        }

        //バインド用データリスト参照用
        public List<BindData> ref_list_bindData
        {
            get; set;
        }

        //スライドショー状態（true:開始/false:停止）
        public bool slideShowState
        {
            get; set;
        }

        //タイマーイベント処理
        private System.Windows.Forms.Timer timer
        {
            get; set;
        }

        //バインド用データ参照用
        private BindData ref_bindData
        {
            get; set;
        }

        //コンフィグデータ参照用
        private ConfigData ref_conData
        {
            get;set;
        }

        //設定ウィンドウ参照用
        private SettingWindow ref_sWindow
        {
            get; set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageWindow(List<ImageWindow> iwnd, BindData bdata, ConfigData condata, SettingWindow swnd, List<BindData> list_bdata)
        {
            InitializeComponent();

            //ファイルパスのリストを作成
            list_filePath = new List<string>();

            //画像ウィンドウリストを作成
            ref_list_iWindow = iwnd;

            //再生中のファイルINDEXを初期化
            playNo = 0;

            //スライドショー状態を停止で初期化
            slideShowState = false;

            //タイマーイベント用オブジェクト作成
            timer = new System.Windows.Forms.Timer();

            //動画再生状態を停止で初期化
            movieState = PlayState.STOP;

            //バインド用データを参照
            ref_bindData = bdata;

            //バインド用データリストを参照
            ref_list_bindData = list_bdata;

            //コンフィグデータ参照
            ref_conData = condata;

            //設定ウィンドウ参照
            ref_sWindow = swnd;

            //コンフィグデータをウィンドウ情報へ設定する
            fnc_ConfigToValue();

            //コードバインディング
            this.DataContext = ref_bindData;
        }

        /// <summary>
        /// コンフィグデータをウィンドウ情報へ設定する
        /// （前回終了時の状態を復元する。）
        /// </summary>
        private void fnc_ConfigToValue()
        {
            //ウィンドウ情報を設定する。
            this.Height = ref_conData.WindowHeight;
            this.Width = ref_conData.WindowWidth;
            this.Left = ref_conData.WinposiX;
            this.Top = ref_conData.WinposiY;
            ref_bindData.iwndBackGrnd = ref_conData.WndBackGrnd;
            this.playNo = ref_conData.PlayNo;

            //スライドショー間隔を設定する。
            ref_bindData.slideInterval = ref_conData.SlideInterval;

            //ファイルパスを設定する。
            if (ref_conData.List_FilePath != null)
            {
                //listのコンストラクタにより値渡しで設定する。
                list_filePath = new List<string>(ref_conData.List_FilePath);
            }
        }

        /// <summary>
        /// 開くメニューによる画像表示の開始処理
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool fnc_StartbyOpen(CommonOpenFileDialog browser)
        {
            bool emptyPath = true;  //true:空/false:ファイルパス有り
            string[] filePath = null;      //フォルダ内のファイルパス

            //フォルダを開く
            if (browser.IsFolderPicker == true)
            {
                //各フォルダ内のファイルパスを取得する。
                foreach (string folder in browser.FileNames)
                {
                    //サブフォルダを含めて検索する。
                    filePath = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

                    //取得したファイルパスをリストへ格納する
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
            //ファイルを開く
            else if (browser.IsFolderPicker == false)
            {
                //ファイルパスをクリアする。
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

                //パス無し
                if (emptyPath == true)
                {
                    return false;
                }
            }

            //表示画像を設定する
            fnc_SetImage();

            return true;
        }

        /// <summary>
        /// 画像設定処理
        /// </summary>
        private void fnc_SetImage()
        {
            //拡縮率を初期化
            ref_bindData.scaleValue = 1.0;

            // ファイル形式判定
            iType = fnc_JdgType(list_filePath[playNo]);

            //ファイル形式に応じた設定処理の呼び出し
            switch (iType)
            {
                //画像ファイル(gif以外)
                case ImageType.IMG:
                    fnc_SetIMG(list_filePath[playNo]);
                    break;
                //画像ファイル(gif)
                case ImageType.GIF:
                    fnc_SetGIF(list_filePath[playNo]);
                    break;
                //動画ファイル
                case ImageType.MOV:
                    fnc_SetMOV(list_filePath[playNo]);
                    break;
                //音楽ファイル
                case ImageType.MSC:
                    fnc_SetMSC(list_filePath[playNo]);
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
            //画像ファイル(gif以外)かどうか
            if (filePath.Contains(".jpeg") == true
              || filePath.Contains(".jpg") == true
              || filePath.Contains(".bmp") == true
              || filePath.Contains(".png") == true)
            {
                return ImageType.IMG;
            }
            //画像ファイル(gif)かどうか
            else if (filePath.Contains(".gif") == true)
            {
                return ImageType.GIF;
            }
            //動画ファイルかどうか
            else if (filePath.Contains(".mp4") == true
              || filePath.Contains(".avi") == true
              || filePath.Contains(".mkv") == true
              || filePath.Contains(".mpeg") == true
              || filePath.Contains(".wmv") == true)
            {
                return ImageType.MOV;
            }
            //音楽ファイルかどうか
            else if (filePath.Contains(".mp3") == true)
            {
                return ImageType.MSC;
            }
            //該当無し
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
            //画像作成
            BitmapImage bmpImage = new BitmapImage();

            //※排他制御回避の為、ファイルストリームを用いてファイルOPEN
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
            img.Source = bmpImage;

            bmpImage.Freeze();
        }

        /// <summary>
        /// gif画像の設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetGIF(string filePath)
        {
            //gif画像パスのプロパティ更新
            ref_bindData.gifPath = filePath;
        }

        /// <summary>
        /// 動画ファイルの設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetMOV(string filePath)
        {
            //動画読込み用オブジェクト作成
            MediaElement mov = new MediaElement();

            mov.BeginInit();
            mov.Source = new Uri(filePath);
            mov.EndInit();
            
            //動画設定
            movie.Source = mov.Source;
        }

        /// <summary>
        /// 音楽ファイルの設定処理
        /// </summary>
        /// <param name="filePath"></param>
        private void fnc_SetMSC(string filePath)
        {
            //動画読込み用オブジェクト作成
            MediaElement msc = new MediaElement();

            msc.BeginInit();
            msc.Source = new Uri(filePath);
            msc.EndInit();

            //動画設定
            movie.Source = msc.Source;
        }

        /// <summary>
        /// ウィンドウの表示/非表示を切り替える
        /// </summary>
        public void fnc_SwitchVisibility_iwnd()
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

        /// <summary>
        /// ウィンドウをドラッグ中のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DragWindow(object sender, MouseButtonEventArgs e)
        {
            //ウィンドウ枠外のドラッグでもウィンドウの移動を可能にする。
            DragMove();
        }

        /// <summary>
        /// ウィンドウ左側クリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_ClickLeftField(object sender, MouseButtonEventArgs e)
        {
            //前の画像を表示する。
            fnc_BackImage();
        }

        /// <summary>
        /// 前の画像を表示する。
        /// </summary>
        private void fnc_BackImage()
        {
            //キー操作で再生中断される為、停止にする。
            movieState = PlayState.STOP;

            //表示中の画像INDEXを戻す。
            fnc_BackpNo();

            //表示中の画像をクリア
            fnc_ClearImage();

            //画像表示処理
            fnc_SetImage();
        }

        /// <summary>
        /// 表示中の画像INDEXを戻す。
        /// </summary>
        private void fnc_BackpNo()
        {
            if (playNo == 0)
            {
                //リストの先頭の場合、再生中INDEXを末尾に移動させる
                playNo = (list_filePath.Count - 1);
            }
            else
            {
                //再生中INDEXを戻す
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
            //次の画像を表示する。
            fnc_NextImage();
        }

        /// <summary>
        /// 次の画像を表示する。
        /// </summary>
        private void fnc_NextImage()
        {
            //キー操作で再生中断される為、停止にする。
            movieState = PlayState.STOP;

            //表示中の画像INDEXを進める。
            fnc_NextpNo();

            //表示中の画像をクリア
            fnc_ClearImage();

            //画像表示処理
            fnc_SetImage();
        }

        /// <summary>
        /// 表示中の画像INDEXを進める。
        /// </summary>
        private void fnc_NextpNo()
        {
            if (playNo == (list_filePath.Count - 1))
            {
                //リストの最後尾の場合、再生中INDEXを先頭に戻す
                playNo = 0;
            }
            else
            {
                //再生中INDEXを進める
                playNo = playNo + 1;
            }
        }

        /// <summary>
        /// 表示画像クリア
        /// </summary>
        private void fnc_ClearImage()
        {
            //画像(gif以外)のクリア
            img.Source = null;

            //画像(gif)のクリア
            ref_bindData.gifPath = null;

            //動画のクリア
            movie.Source = null;
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
            bool emptyPath = true;  //true:空/false:ファイルパス有り
            string[] filePath = null;      //フォルダ内のファイルパス

            // バインド用データ作成
            BindData bdata_tmp = new BindData();

            //コンフィグから読み込んだスライドショー間隔を設定データへ代入する。
            bdata_tmp.slideInterval = ref_conData.SlideInterval;

            // バインド用データリストへ追加
            ref_list_bindData.Add(bdata_tmp);

            // 画像ウィンドウのオブジェクト作成
            ImageWindow iwnd = new ImageWindow(ref_list_iWindow, ref_list_bindData[ref_list_bindData.IndexOf(bdata_tmp)], ref_conData, ref_sWindow, ref_list_bindData);
            
            //作成した画像ウィンドウをリストに追加
            ref_list_iWindow.Add(iwnd);

            //ファイルパスリストをクリアする。
            iwnd.list_filePath.Clear();

            //フォルダがドロップされた場合
            try
            {
                //各フォルダ内のファイルパスを取得する。
                foreach (string folder in files)
                {
                    //サブフォルダを含めて検索する。
                    filePath = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

                    //取得したファイルパスをリストへ格納する
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
            //ファイルがドロップされた場合
            catch ( IOException )
            {
                //取得したファイルパスをリストへ格納する
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

            //パス無し
            if (emptyPath == true)
            {
                return false;
            }

            //表示画像を設定する
            iwnd.fnc_SetImage();

            //画面表示
            iwnd.Show();

            return true;
        }

        /// <summary>
        /// コンフィグファイルによる画像表示の開始処理
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public bool fnc_StartbyConfig()
        {
            //表示画像を設定する
            fnc_SetImage();

            return true;
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
                //拡縮率が10.0未満なら加算
                if (ref_bindData.scaleValue < BindData.SCALEMAX)
                {
                    //誤差回避の為、整数に戻して計算
                    tmp = ref_bindData.scaleValue * 10;
                    tmp++;
                    ref_bindData.scaleValue = tmp / 10;
                }
            }
            // ホイールを下に回す→縮小
            else
            {
                //拡縮率が0.1超過なら加算
                if (ref_bindData.scaleValue > BindData.SCALEMIN)
                {
                    //誤差回避の為、整数に戻して計算
                    tmp = ref_bindData.scaleValue * 10;
                    tmp--;
                    ref_bindData.scaleValue = tmp / 10;
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
                //左方向キー：前の画像を表示
                case Key.Left:
                    fnc_BackImage();
                    break;
                //右方向キー：次の画像を表示
                case Key.Right:
                    fnc_NextImage();
                    break;
                //スライドショー開始
                case Key.D1:
                    fnc_PlaySlideshow();
                    break;
                //スライドショー停止
                case Key.D2:
                    fnc_StopSlideshow();
                    break;
                //ウィンドウ背景切替
                case Key.B:
                    fnc_SwitchBackGrnd();
                    break;
                //音量有無切替
                case Key.M:
                    fnc_VolumeSwitch();
                    break;
                //設定ウィンドウ表示
                case Key.S:
                    fnc_OpenSetting();
                    break;
                //Spaceキー：表示/非表示を切り替える
                case Key.Space:
                    //画像ウィンドウ終了処理
                    fnc_DodgeOyaFla();
                    break;
                //Escキー：ウィンドウを閉じる
                case Key.Escape:
                    //画像ウィンドウ終了処理
                    fnc_CloseProcess();
                    break;
                default:
                    break;
            }

            //ルーティングイベント(バブル)の中断
            //※Escキーの2重動作を回避する為。
            e.Handled = true;
        }

        /// <summary>
        /// ボリューム有無切替
        /// </summary>
        private void fnc_VolumeSwitch()
        {
            //ボリューム有り→無し
            if (movie.Volume != 0.0)
            {
                movie.Volume = 0.0;
            }
            //ボリューム無し→有り
            else
            {
                movie.Volume = 0.5;
            }
        }

        /// <summary>
        /// 全画像ウィンドウを非表示にする
        /// </summary>
        private void fnc_DodgeOyaFla()
        {
            //全画像ウィンドウの表示/非表示を切り替える。
            foreach (ImageWindow iwnd in ref_list_iWindow)
            {
                iwnd.fnc_SwitchVisibility_iwnd();
            }
        }

        /// <summary>
        /// 設定ウィンドウを表示する。
        /// </summary>
        private void fnc_OpenSetting()
        {
            //設定ウィンドウが表示状態
            if (ref_sWindow.Visibility == Visibility.Visible)
            {
                //ウィンドウをアクティブにする。
                ref_sWindow.Activate();
            }
            //設定ウィンドウが非表示状態
            else if (ref_sWindow.Visibility == Visibility.Hidden)
            {
                //ウィンドウを表示させる。
                ref_sWindow.fnc_SwitchVisibility_swnd();
            }
        }

        /// <summary>
        /// 画像ウィンドウ終了処理
        /// </summary>
        private void fnc_CloseProcess()
        {
            //スライドショーを停止させる。
            fnc_StopSlideshow();

            //画像ウィンドウ終了処理
            fnc_CloseWindow();

            //メインの持つ画像ウィンドウリストから自分を削除する。
            ref_list_iWindow.Remove(this);
            
            //ウィンドウクローズ時にメモリ解放させる。
            GC.Collect();
        }

        /// <summary>
        /// 画像ウィンドウ情報をコンフィグデータに設定する。
        /// </summary>
        public void fnc_iWndValueToConfig()
        {
            //各種コンフィグデータを設定する。
            ref_conData.WindowHeight = this.Height;
            ref_conData.WindowWidth = this.Width;
            ref_conData.WinposiX = this.Left;
            ref_conData.WinposiY = this.Top;
            ref_conData.WndBackGrnd = ref_bindData.iwndBackGrnd;
            ref_conData.SlideInterval = ref_bindData.slideInterval;
            ref_conData.PlayNo = this.playNo;

            //nullならリスト作成
            if (ref_conData.List_FilePath == null)
            {
                ref_conData.List_FilePath = new List<string>();
            }

            //リスト初期化
            ref_conData.List_FilePath.Clear();

            //リスト追加
            foreach (string fp in list_filePath)
            {
                ref_conData.List_FilePath.Add(fp);
            }
        }

        /// <summary>
        /// 画像ウィンドウ終了処理
        /// </summary>
        public void fnc_CloseWindow()
        {
            //表示画像クリア
            fnc_ClearImage();

            //ファイルパスリストを削除する。
            this.list_filePath.Clear();

            //画像ウィンドウを閉じる。
            this.Close();
        }

        /// <summary>
        /// ダブルクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_DClickWindow(object sender, MouseButtonEventArgs e)
        {
            //全画面と通常画面を切り替える
            fnc_SwitchScreen();
        }

        /// <summary>
        /// 全画面と通常画面を切り替える
        /// </summary>
        private void fnc_SwitchScreen()
        {
            switch (this.WindowState)
            {
                //通常画面の時、フルスクリーンにする。
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
                //フルスクリーンの時、通常画面にする。
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
            //スライドショー：開始
            slideShowState = true;

            //タイマー処理呼び出し
            fnc_SlideTime(slideShowState);
        }

        /// <summary>
        /// スライドショーを停止する。
        /// </summary>
        private void fnc_StopSlideshow()
        {
            //スライドショー：停止
            slideShowState = false;

            //タイマー処理呼び出し
            fnc_SlideTime(slideShowState);
        }

        /// <summary>
        /// 画像ウィンドウの背景を切り替える
        /// </summary>
        private void fnc_SwitchBackGrnd()
        {
            if (ref_bindData.iwndBackGrnd == "Transparent")
            {
                ref_bindData.iwndBackGrnd = "Black";
            }
            else if (ref_bindData.iwndBackGrnd == "Black")
            {
                ref_bindData.iwndBackGrnd = "Transparent";
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
                case true:
                    timer.Tick += new EventHandler(hnd_SlideTime);
                    timer.Interval = ref_bindData.slideInterval;
                    timer.Enabled = true;
                    break;
                case false:
                    timer.Enabled = false;
                    timer.Dispose();
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
            //スライド間隔をリアルタイムに更新する。
            timer.Interval = ref_bindData.slideInterval;

            //動画再生中は画像送りしない
            if (movieState == PlayState.STOP)
            {
                //次の画像を表示する。
                fnc_NextImage();
            }
            else
            {
                //処理無し
            }
        }

        /// <summary>
        /// 動画再生終了時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MediaEnded(object sender, RoutedEventArgs e)
        {
            //動画再生状態を停止にする。
            fnc_MediaEnded();
        }

        /// <summary>
        /// 動画再生状態を停止にする。
        /// </summary>
        private void fnc_MediaEnded()
        {
            //動画再生状態を停止にする。
            movieState = PlayState.STOP;
        }

        /// <summary>
        /// 動画再生時（読込み完了時）のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_MediaStarted(object sender, RoutedEventArgs e)
        {
            //動画再生状態を再生にする。
            fnc_MediaStarted();
        }

        /// <summary>
        /// 動画再生状態を再生にする。
        /// </summary>
        private void fnc_MediaStarted()
        {
            //動画再生状態を再生にする。
            movieState = PlayState.PLAYING;
        }

        /// <summary>
        /// ウィンドウアクティブ時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hnd_Activated(object sender, EventArgs e)
        {
            fnc_SettingTarget();
        }

        /// <summary>
        /// アクティブウィンドウを設定ウィンドウの対象にする。
        /// </summary>
        private void fnc_SettingTarget()
        {
            ref_sWindow.fnc_BindingSlide(ref_bindData);
        }
    }
}

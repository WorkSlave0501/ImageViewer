using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ImageViewer
{
    public class BindData : INotifyPropertyChanged
    {
        // プロパティ値更新時のイベントハンドラ
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        // 拡縮率の最大・最小値
        public const double SCALEMAX = 10.0;
        public const double SCALEMIN = 0.2;

        // スライドショーの切替間隔
        public int slideInterval
        {
            get; set;
        }

        // gif画像のファイルパス
        private string _gifPath;
        // プロパティ
        public string gifPath
        {
            get
            {
                return _gifPath;
            }
            set
            {
                _gifPath = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("gifPath");
            }
        }

        // 画像サイズの拡縮率
        private double _scaleValue;
        // プロパティ
        public double scaleValue
        {
            get
            {
                return _scaleValue;
            }
            set
            {
                _scaleValue = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("scaleValue");
            }
        }

        // ウィンドウ背景
        private string _iwndBackGrnd;
        // プロパティ
        public string iwndBackGrnd
        {
            get
            {
                return _iwndBackGrnd;
            }
            set
            {
                _iwndBackGrnd = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("iwndBackGrnd");
            }
        }

        // 音量
        private double _Volume;
        // プロパティ
        public double Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                _Volume = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("Volume");
            }
        }

        // 最前面表示設定
        private bool _tMost;
        // プロパティ
        public bool tMost
        {
            get
            {
                return _tMost;
            }
            set
            {
                _tMost = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("tMost");
            }
        }

        // チェック：ウィンドウ背景
        private bool _check_iwndBackGrnd;
        // プロパティ
        public bool check_iwndBackGrnd
        {
            get
            {
                return _check_iwndBackGrnd;
            }
            set
            {
                _check_iwndBackGrnd = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("check_iwndBackGrnd");
            }
        }

        // チェック：音量
        private bool _check_Volume;
        // プロパティ
        public bool check_Volume
        {
            get
            {
                return _check_Volume;
            }
            set
            {
                _check_Volume = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("check_Volume");
            }
        }

        // チェック：最前面表示設定
        private bool _check_tMost;
        // プロパティ
        public bool check_tMost
        {
            get
            {
                return _check_tMost;
            }
            set
            {
                _check_tMost = value;
                // Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("check_tMost");
            }
        }
        // コンストラクター
        public BindData()
        {
            _gifPath = null;
            _scaleValue = 1.0;
            _iwndBackGrnd = "Transparent";
            _Volume = 0.5;
            _tMost = false;
            _check_iwndBackGrnd = false;
            _check_Volume = true;
            _check_tMost = false;
        }
    }
}

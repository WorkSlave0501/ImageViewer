using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ImageViewer
{
    public class BindData : INotifyPropertyChanged
    {
        //gif画像のファイルパス
        private string _gifPath;
        public string gifPath
        {
            get
            {
                return _gifPath;
            }
            set
            {
                _gifPath = value;
                //Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("gifPath");
            }
        }

        //スライドショーの切替間隔
        public int slideInterval
        {
            get; set;
        }

        //画像サイズの拡縮率
        private double _scaleValue;
        //プロパティ
        public double scaleValue
        {
            get
            {
                return _scaleValue;
            }
            set
            {
                _scaleValue = value;
                //Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("scaleValue");
            }
        }

        //ウィンドウ背景
        private string _iwndBackGrnd;
        //プロパティ
        public string iwndBackGrnd
        {
            get
            {
                return _iwndBackGrnd;
            }
            set
            {
                _iwndBackGrnd = value;
                //Viewへプロパティ値の変更を通知する。
                NotifyPropertyChanged("iwndBackGrnd");
            }
        }

        //プロパティ値更新時のイベントハンドラ
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        //拡縮率の最大・最小値
        public const double SCALEMAX = 10.0;
        public const double SCALEMIN = 0.2;

        //コンストラクター
        //※slideIntervalの初期値はコンフィグで設定
        public BindData()
        {
            gifPath = null;
            scaleValue = 1.0;
            iwndBackGrnd = "Transparent";
        }
    }
}

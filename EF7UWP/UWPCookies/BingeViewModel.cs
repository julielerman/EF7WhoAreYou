
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI.Xaml;

namespace CookieCounterApp
{
    public class BingeViewModel : INotifyPropertyChanged
    {
        public delegate void BingeNotedHandler(object sender);
        public delegate void NomVisibleHandler(object sender);

        private int _clickCount;
        private bool _playing;
        private Visibility _startControlsVisibility = Visibility.Visible;
        private Visibility _stopControlsVisibility = Visibility.Collapsed;

        public event PropertyChangedEventHandler PropertyChanged;
        public event BingeNotedHandler BingeCompleted;
     

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        protected void OnBingeCompleted()
        {
            if (BingeCompleted != null)
            {
                BingeCompleted(this);
            }
        }

        public int ClickCount
        {
            get
            {
                return _clickCount;
            }
            set
            {
                _clickCount = value;
                OnPropertyChanged("ClickCount");
            }
        }

        internal void StoreBinge(bool worthIt)
        {
            BingeService.RecordBinge(_clickCount, worthIt);
            StartControlsVisibility = Visibility.Visible;
            StopControlsVisibility = Visibility.Collapsed;
            OnBingeCompleted();
        }

        public Visibility StartControlsVisibility
        {
            get
            {
                return _startControlsVisibility;
            }
            set
            {
                _startControlsVisibility = value;
                OnPropertyChanged("StartControlsVisibility");
            }
        }
        public Visibility StopControlsVisibility
        {
            get
            {
                return _stopControlsVisibility;
            }
            set
            {
                _stopControlsVisibility = value;
                OnPropertyChanged("StopControlsVisibility");

            }
        }

        public bool Playing
        {
            get
            {
                return _playing;
            }
            set
            {
                _playing = value;
                OnPropertyChanged("Playing");
            }
        }

        public void StartNewBinge()
        {
            ClickCount = 0;
            Playing = true;
            StartControlsVisibility = Visibility.Collapsed;
            StopControlsVisibility = Visibility.Visible;

      
        }

        public void HandleClick()
        {
            if (Playing)
            {
                ClickCount++;
            
            }
        }

    }
}


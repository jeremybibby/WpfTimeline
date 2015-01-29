using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infusion.WpfTimelineControl.ViewModels
{
    public class MainViewModel : Screen
    {
        #region instance variables

        private TimeSpan _currentPosition;
        private int _zoomLevel;
        public ObservableCollection<TimelineItemViewModel> _serverTimeline;
        public ObservableCollection<TimelineItemViewModel> _clientTimeline;

        #endregion instance variables


        #region constructor

        public MainViewModel()
        {
            // setup our 2 collections
            ServerTimeline = new ObservableCollection<TimelineItemViewModel>();
            ClientTimeline = new ObservableCollection<TimelineItemViewModel>();

            // finally setup design time stuff if we're not connected to real data
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) SetupDesignTimeData();
        }

        #endregion constructor


        #region properties

        public TimeSpan CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                if (value != _currentPosition)
                {
                    _currentPosition = value;
                    NotifyOfPropertyChange(() => CurrentPosition);
                }
            }
        }

        public int ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                if (value != _zoomLevel)
                {
                    _zoomLevel = value;
                    NotifyOfPropertyChange(() => ZoomLevel);
                }
            }
        }

        public ObservableCollection<TimelineItemViewModel> ServerTimeline
        {
            get { return _serverTimeline; }
            set
            {
                if (value != _serverTimeline)
                {
                    _serverTimeline = value;
                    NotifyOfPropertyChange(() => ServerTimeline);
                }
            }
        }

        public ObservableCollection<TimelineItemViewModel> ClientTimeline
        {
            get { return _clientTimeline; }
            set
            {
                if (value != _clientTimeline)
                {
                    _clientTimeline = value;
                    NotifyOfPropertyChange(() => ClientTimeline);
                }
            }
        }

        #endregion properties


        #region helper methods

        private static Random _rand = new Random();
        private void SetupDesignTimeData()
        {
            this.CurrentPosition = TimeSpan.FromSeconds(_rand.Next(5));
            this.ZoomLevel = _rand.Next(4);
            
        }

        #endregion helper methods

    }
}

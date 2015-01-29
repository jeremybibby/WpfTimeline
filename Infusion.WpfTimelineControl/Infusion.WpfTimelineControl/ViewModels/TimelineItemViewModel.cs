using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infusion.WpfTimelineControl.ViewModels
{
    public class TimelineItemViewModel : PropertyChangedBase
    {
        #region instance variables

        public string _itemName;
        public TimeSpan _startTime;
        public TimeSpan _duration;

        #endregion instance variables


        #region properties

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (value != _itemName)
                {
                    _itemName = value;
                    NotifyOfPropertyChange(() => ItemName);
                }
            }
        }

        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    NotifyOfPropertyChange(() => StartTime);
                }
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    NotifyOfPropertyChange(() => Duration);
                }
            }
        }

        #endregion properties



        #region virtual methods

        public virtual void SetupDesignTimeData()
        {
            
        }

        #endregion virtual methods
    }
}

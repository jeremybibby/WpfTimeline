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



        #region virtual methods

        public virtual void SetupDesignTimeData()
        {
            
        }

        #endregion virtual methods
    }
}

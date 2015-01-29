using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infusion.WpfTimelineControl.Utilities;

namespace Infusion.WpfTimelineControl.ViewModels
{
    [Export]
    public class ShellViewModel : Screen
    {
        private Conductor<IScreen> _conductor;
        private IEventAggregator _eventAgg;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator agg)
        {
            _eventAgg = agg;

            _conductor = new Conductor<IScreen> { Parent = this };
            _conductor.ConductWith(this);
            _conductor.AttachToPropertyChanged(() => _conductor.ActiveItem, () => NotifyOfPropertyChange(() => MainActiveItem));
            _conductor.ActivateItem(new MainViewModel());
        }

        public IScreen MainActiveItem
        {
            get { return _conductor.ActiveItem; }
        }
    }
}

using Caliburn.Micro;
using Infusion.WpfTimelineControl.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace Infusion.WpfTimelineControl.Utilities
{
    public class AppBootstrapper : BootstrapperBase
    {
        #region instance variables

        private CompositionContainer _container;
        private AggregateCatalog _catalog;

        #endregion instance variables


        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {

            _catalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());
            _container = new CompositionContainer(_catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<CompositionContainer>(_container);

            _container.Compose(batch);

        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<System.Reflection.Assembly> SelectAssemblies()
        {
            var result = new List<System.Reflection.Assembly>();
            result.Add(Assembly.GetExecutingAssembly());

            return result;
        }


        
    }
}

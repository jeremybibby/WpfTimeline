using Infusion.WpfTimelineControl.Utilities;
using Infusion.WpfTimelineControl.Views;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Infusion.WpfTimelineControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILog _log;
        private AppBootstrapper _bootstrapper;
        
        public App()
        {
            // setup log
            log4net.Config.XmlConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(App));

            // exception handling
            InitializeExceptionHandling();

            // setup main window
            ShellView mw = new ShellView();
            // show the main window
            mw.Show();


            // setup teh bootstrapper
            _bootstrapper = new AppBootstrapper();

        }

        #region helper methods

        private void InitializeExceptionHandling()
        {
            DispatcherUnhandledException += HandleDispatcherException;
            AppDomain.CurrentDomain.UnhandledException += HandleDomainException;
        }

        private string GetAssemblyString()
        {
            try
            {
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                return string.Format("{0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception exc)
            {
                _log.Error("Exception in GetAssemblyString", exc);
                return "Unknown";
            }
        }

        /// <summary>
        /// Called when a WPF dispatcher exception is thrown and is unhandled
        /// </summary>
        private void HandleDispatcherException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // log and restart
            string assemblyInfo = GetAssemblyString();

            if (Debugger.IsAttached) Debugger.Break();

            if (e.Exception is System.OutOfMemoryException)
            {
                Current.Shutdown(-1);
                _log.Fatal("Out of Memory exception. Closing application " + assemblyInfo + ".", e.Exception);
            }
            else
            {
                _log.Fatal("Dispatcher unhandled exception in " + assemblyInfo + ". Continuing Execution.", e.Exception);
                e.Handled = true;
            }



        }

        /// <summary>
        /// Called when an exception happens in the app domain and is unhandled
        /// </summary>
        private void HandleDomainException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log it
            var ex = e.ExceptionObject as Exception;
            string assemblyInfo = GetAssemblyString();
            if (e.IsTerminating)
            {
                _log.Fatal("Domain Unhandled Exception in " + assemblyInfo + ". Forced Terminate.", ex);
            }
            else
            {
                _log.Error("Domain Unhandled Exception in " + assemblyInfo + ". May be able to continue.", ex);
            }

            Environment.Exit(1);
        }

        #endregion helper methods

    }
}


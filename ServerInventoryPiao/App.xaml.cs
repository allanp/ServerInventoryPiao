using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ServerInventoryPiao.Views;
using ServerInventoryPiao.ViewModels;
using ServerInventoryPiao.Controllers;

namespace ServerInventoryPiao
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IMainView _mainView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainView = new MainWindow();
            _mainView.DataContext = new MainViewModel(new DataCenterRepository());
            _mainView.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (_mainView != null)
                _mainView.Close();
        }
    }
}

using System.Windows;
using ServerInventoryPiao.Controllers;
using ServerInventoryPiao.ViewModels;
using ServerInventoryPiao.Views;

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

            _mainView.DataContext = null;
            _mainView = null;
        }
    }
}

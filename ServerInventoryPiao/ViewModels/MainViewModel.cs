using System.Windows.Input;
using ServerInventoryPiao.Controllers;

namespace ServerInventoryPiao.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string TitlePropertyName = "Title";
        public const string DataCentersPropertyName = "DataCenters";
        
        private MainController _mainController;

        private string _title;
        private DataCenterListViewModel _datacenters;
        
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged(TitlePropertyName);
                }
            }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        
        public DataCenterListViewModel DataCenters
        {
            get
            {
                return _datacenters;
            }
            set
            {
                if (_datacenters != value)
                {
                    _datacenters = null;
                    _datacenters = value;
                    RaisePropertyChanged(DataCentersPropertyName);
                }
            }
        }

        public MainViewModel(DataCenterRepository dataCenterRepository)
        {
            _mainController = new MainController(dataCenterRepository);

            LoadCommand = _mainController.LoadCommand;
            SaveCommand = _mainController.SaveCommand;
            
            _mainController.OnDataCentersLoaded += delegate
            {
                DataCenters = new DataCenterListViewModel(_mainController.LoadDataCenter());
            };

            Title = _mainController.Title;
        }
    }
}
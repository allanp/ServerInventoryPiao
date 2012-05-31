using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ServerInventoryPiao.Controllers;

namespace ServerInventoryPiao.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public const string TitlePropertyName = "Title";

        private DataCenterRepository _dataCenterRepository;

        private MainController _mainController;

        private string _title;

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

        public ICommand AboutCommand{get;set;}
        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public DataCenterListViewModel DataCenters { get; set; }




        public MainViewModel(DataCenterRepository dataCenterRepository)
        {
            _dataCenterRepository = dataCenterRepository;

            _mainController = new MainController(_dataCenterRepository);

            LoadCommand = _mainController.LoadCommand;
            SaveCommand = _mainController.SaveCommand;
            AboutCommand = _mainController.AboutCommand;

            DataCenters = new DataCenterListViewModel(_mainController.LoadDataCenter());
            DataCenters.AddNewCommand = _mainController.AddCommand;
            DataCenters.RemoveCommand = _mainController.RemoveCommand;

            Title = _mainController.Title;
        }
    }
}

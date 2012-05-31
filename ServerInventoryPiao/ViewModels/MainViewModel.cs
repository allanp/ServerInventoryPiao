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

        private static DataCenterRepository _dataCenterRepository;

        public static readonly MainController MC = new MainController(_dataCenterRepository);

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

        public MainViewModel()
        {
            LoadCommand = MC.LoadCommand;
            SaveCommand = MC.SaveCommand;
            AboutCommand = MC.AboutCommand;

            DataCenters = new DataCenterListViewModel(MC.LoadDataCenter());
            DataCenters.AddNewCommand = MC.AddCommand;
            DataCenters.RemoveCommand = MC.RemoveCommand;

            Title = MC.Title;
        }
    }
}

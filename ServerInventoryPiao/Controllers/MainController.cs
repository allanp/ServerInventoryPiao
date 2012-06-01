using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ServerInventoryPiao.ViewModels;
using System.IO;
using System.Windows.Resources;
using System.Xml;
using ServerInventoryPiao.Models;
using System.Xml.Linq;
using System.Windows;
using ServerInventoryPiao.Views;

namespace ServerInventoryPiao.Controllers
{
    public class MainController
    {
        public const string DefaultXmlFileName = "datacenters.xml";

        private IFileDialogView _fileDialog = null;
        private DataCenterRepository _dataCenterRepository;

        public string Title { get; set; }
        public string XmlFileName { get; set; }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public EventHandler OnDataCentersLoaded;
        
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private MainController()
        {
            Title = "Server Inventory";
            LoadCommand = new RelayCommand(OnLoad, CanLoad);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public MainController(DataCenterRepository dataCenterRepository) : this()
        {
            this._dataCenterRepository = dataCenterRepository;
        }

        internal List<DataCenterModel> LoadDataCenter()
        {
            return _dataCenterRepository.GetDataCenters();
        }

        public void OnLoad()
        {
            if (_fileDialog == null)
                _fileDialog = new FileDialogView(null);

            try
            {
                _fileDialog.Mode = Mode.Open;
                _fileDialog.DefaultExt = "xml";

                if (_fileDialog.ShowDialog() == true && _fileDialog.FileNames != null && _fileDialog.FileNames.Length > 0)
                {
                    this._dataCenterRepository = new DataCenterRepository(_fileDialog.FileNames[0]);
                    if (OnDataCentersLoaded != null)
                        OnDataCentersLoaded(this, EventArgs.Empty);
                }
            }
            finally
            {
                _fileDialog.Mode = Mode.None;
            }
        }
        public bool CanLoad()
        {
            // always can load
            return true; 
        }

        public void OnSave()
        {
            this._dataCenterRepository.Save();
        }
        public bool CanSave()
        {
            return this._dataCenterRepository != null;
        }
    }
}

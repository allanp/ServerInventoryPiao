using System;
using System.Collections.Generic;
using System.Windows.Input;
using ServerInventoryPiao.Models;
using ServerInventoryPiao.Views;

namespace ServerInventoryPiao.Controllers
{
    public class MainController
    {
        public const string DefaultXmlFileName = "datacenters.xml";

        private const string CompanyString = "software solutions";
        private const string TravSys = "Travsys Servers Inventory";

        private IFileDialogView _fileDialog = null;
        private DataCenterRepository _dataCenterRepository;

        private string _loadedFileName = string.Empty;

        public string Title
        {
            get
            {
                return _loadedFileName == string.Empty ?
                    string.Format("{0} - {1}", TravSys, CompanyString) :
                    string.Format("{0} \"{1}\" is loaded.", TravSys, _loadedFileName);
            }
        }
        public string XmlFileName { get; set; }

        public ICommand LoadCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public EventHandler OnDataCentersLoaded;
        
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private MainController()
        {
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
                    _loadedFileName = _fileDialog.FileNames[0];

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

        public object[] LoadedFileName { get; set; }
    }
}

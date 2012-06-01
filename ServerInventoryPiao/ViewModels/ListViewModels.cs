using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerInventoryPiao.Models;
using System.Collections.ObjectModel;
using ServerInventoryPiao.Controllers;
using System.Windows.Input;

namespace ServerInventoryPiao.ViewModels
{
    public class DataCenterListViewModel : ListViewModelBase<DataCenterModel, DataCenterViewModel>
    {
        public DataCenterListViewModel(List<DataCenterModel> datacenters)
        {
            if (datacenters == null)
            {
                this.Items = new ObservableCollection<DataCenterViewModel>();
            }
            else
            {
                this.Items = ConvertToListViewMode(datacenters, (model) => new DataCenterViewModel(model));
            }

            AddNewCommand = new RelayCommand(() =>
                Items.Add(new DataCenterViewModel(new DataCenterModel())));

            RemoveCommand = new RelayCommand<DataCenterViewModel>(
                datacenter => this.Items.Remove(datacenter), datacenter => this.Items.Contains(datacenter));
        }

        public const string AddNewCommandPropertyName = "AddNewCommand";
        public const string RemoveCommandPropertyName = "RemoveCommand";
        private ICommand _addNewCommand;
        private ICommand _removeCommand;
        public ICommand AddNewCommand
        {
            get { return _addNewCommand; }
            set
            {
                if (_addNewCommand != value)
                {
                    _addNewCommand = value;
                    RaisePropertyChanged(AddNewCommandPropertyName);
                }
            }
        }
        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
            set
            {
                if (_removeCommand != value)
                {
                    _removeCommand = value;
                    RaisePropertyChanged(RemoveCommandPropertyName);
                }
            }
        }
    }

    public class RackListViewModel : ListViewModelBase<RackModel, RackViewModel>
    {
        public RackListViewModel(List<RackModel> racks)
        {
            if (racks == null)
            {
                this.Items = new ObservableCollection<RackViewModel>();
            }
            else
            {
                this.Items = ConvertToListViewMode(racks, (model) => new RackViewModel(model));
            }
        }
    }

    public class DeviceListViewModel : ListViewModelBase<DeviceModel, DeviceViewModel>
    {
        public DeviceListViewModel(List<DeviceModel> devices)
        {
            if (devices == null)
            {
                this.Items = new ObservableCollection<DeviceViewModel>();
            }
            else
            {
                this.Items = ConvertToListViewMode(devices, (model) => new DeviceViewModel(model));
            }
        }
    }

    public class PersonListViewModel : ListViewModelBase<Person, PersonViewModel>
    {
        public PersonListViewModel(List<Person> people)
        {
            if (people == null)
            {
                this.Items = new ObservableCollection<PersonViewModel>();
            }
            else
            {
                this.Items = ConvertToListViewMode(people, (model) => new PersonViewModel(model));
            }
        }
    }
}
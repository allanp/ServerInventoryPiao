using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerInventoryPiao.Models;
using System.Windows.Input;
using ServerInventoryPiao.Controllers;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ModelBaseViewModel<Model> : ViewModelBase<Model> where Model : ModelBase
    {
        public const string IdPropertyName = "Id";
        public const string NamePropertyName = "Name";

        protected ModelBaseViewModel(Model model)
            : base(model)
        {
        }

        public string Id
        {
            get { return _model.Id; }
            set
            {
                if (_model.Id != value)
                {
                    _model.Id = value;
                    RaisePropertyChanged(IdPropertyName);
                }
            }
        }
        public string Name
        {
            get { return _model.Name; }
            set
            {
                if (_model.Name != value)
                {
                    _model.Name = value;
                    RaisePropertyChanged(NamePropertyName);
                }
            }
        }
    }

    public class DataCenterViewModel : ModelBaseViewModel<DataCenterModel>
    {
        public const string AddressPropertyName = "Address";
        public const string PhonePropertyName = "Phone";
        public const string ContactPeoplePropertyName = "ContactPeople";
        public const string RacksPropertyName = "Racks";

        public DataCenterViewModel(DataCenterModel model)
            : base(model)
        {
            ContactPeople = new PersonListViewModel(model.ContactPeople);
            AddNewPersonCommand = new RelayCommand(() =>
            {
                this.ContactPeople.Add(new PersonViewModel(new Person()));
                RaisePropertyChanged(ContactPeoplePropertyName);
            });
            RemovePersonCommand = new RelayCommand<PersonViewModel>((person) =>
            {
                if (this.ContactPeople.Remove(person))
                    RaisePropertyChanged(ContactPeoplePropertyName);
            }, (person) => this.ContactPeople.Contains(person));


            Racks = new RackListViewModel(model.Racks);
            AddNewRackCommand = new RelayCommand(() =>
            {
                this.Racks.Add(new RackViewModel(new RackModel()));
                RaisePropertyChanged(RacksPropertyName);
            });
            RemoveCommand = new RelayCommand<RackViewModel>((rack) =>
            {
                if (this.Racks.Remove(rack))
                    RaisePropertyChanged(RacksPropertyName);
            }, (rack) => this.Racks.Contains(rack));
        }

        public string Address
        {
            get { return _model.Address; }
            set
            {
                if (_model.Address != value)
                {
                    _model.Address = value;
                    RaisePropertyChanged(AddressPropertyName);
                }
            }
        }
        public string Phone
        {
            get { return _model.Phone; }
            set
            {
                if (_model.Phone != value)
                {
                    _model.Phone = value;
                    RaisePropertyChanged(PhonePropertyName);
                }
            }
        }

        private PersonListViewModel _contactPeople;
        private RackListViewModel _racks;

        public PersonListViewModel ContactPeople
        {
            get { return _contactPeople; }
            set
            {
                if (_contactPeople != value)
                {
                    _contactPeople = value;
                    RaisePropertyChanged(ContactPeoplePropertyName);
                }
            }
        }
        public RackListViewModel Racks
        {
            get
            {
                return _racks;
            }
            set
            {
                if (_racks != value)
                {
                    _racks = value;
                    RaisePropertyChanged(RacksPropertyName);
                }
            }
        }

        public const string AddNewRackCommandPropertyName = "AddNewRackCommand";
        private ICommand _addNewRackCommand;
        public ICommand AddNewRackCommand
        {
            get { return _addNewRackCommand; }
            set
            {
                if (_addNewRackCommand != value)
                {
                    _addNewRackCommand = value;
                    RaisePropertyChanged(AddNewRackCommandPropertyName);
                }
            }
        }

        public const string RemoveCommandPropertyName = "RemoveCommand"; 
        private ICommand _removeCommand;
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

        public const string AddNewPersonCommandPropertyName = "AddNewPersonCommand";
        public const string RemovePersonCommandPropertyName = "RemovePersonCommand";
        private ICommand _addNewPersonCommand;
        private ICommand _removePersonCommand;
        public ICommand AddNewPersonCommand
        {
            get { return _addNewPersonCommand; }
            set
            {
                if (_addNewPersonCommand != value)
                {
                    _addNewPersonCommand = value;
                    RaisePropertyChanged(AddNewPersonCommandPropertyName);
                }
            }
        }
        public ICommand RemovePersonCommand
        {
            get { return _removePersonCommand; }
            set
            {
                if (_removePersonCommand != value)
                {
                    _removePersonCommand = value;
                    RaisePropertyChanged(RemovePersonCommandPropertyName);
                }
            }
        }
    }

    public class RackViewModel : ModelBaseViewModel<RackModel>
    {
        public const string FloorPropertyName = "Floor";
        public const string PositionPropertyName = "Position";
        public const string DevicesPropertyName = "Devices";

        public RackViewModel(RackModel model)
            : base(model)
        {
            Devices = new DeviceListViewModel(model.Devices);

            AddNewCommand = new RelayCommand(() =>
                {
                    this.Devices.Add(new DeviceViewModel(new DeviceModel()));
                    RaisePropertyChanged(DevicesPropertyName);
                });

            RemoveCommand = new RelayCommand<DeviceViewModel>(
                (device) =>
                {
                    if(this.Devices.Remove(device))
                        RaisePropertyChanged(DevicesPropertyName);
                }, (device) => this.Devices.Contains(device));
        }

        public string Floor
        {
            get { return _model.Floor; }
            set
            {
                if (_model.Floor != value)
                {
                    _model.Floor = value;
                    RaisePropertyChanged(FloorPropertyName);
                }
            }
        }
        public string Position
        {
            get { return _model.Position; }
            set
            {
                if (_model.Position != value)
                {
                    _model.Position = value;
                    RaisePropertyChanged(PositionPropertyName);
                }
            }
        }

        private DeviceListViewModel _devices;
        public DeviceListViewModel Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                if (_devices != value)
                {
                    _devices = value;
                    RaisePropertyChanged(DevicesPropertyName);
                }
            }
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

    public class DeviceViewModel : ModelBaseViewModel<DeviceModel>
    {
        public const string IPAddressPropertyName = "IPAddress";
        public const string StatusPropertyName = "Status";

        public DeviceViewModel(DeviceModel model)
            : base(model)
        {
        }

        public string IPAddress
        {
            get { return _model.IPAddress; }
            set
            {
                if (_model.IPAddress != value)
                {
                    _model.IPAddress = value;
                    RaisePropertyChanged(IPAddressPropertyName);
                }
            }
        }
        public string Status
        {
            get { return _model.Status; }
            set
            {
                if (_model.Status != value)
                {
                    _model.Status = value;
                    RaisePropertyChanged(StatusPropertyName);
                }
            }
        }
    }

    public class PersonViewModel : ViewModelBase<Person>
    {
        public const string FirstNamePropertyName = "FirstName";
        public const string LastNamePropertyName = "LastName";
        public const string PhonePropertyName = "Phone";
        public const string EmailPropertyName = "Email";

        public PersonViewModel(Person person) : base(person)
        {
        }

        public string FirstName
        {
            get { return _model.FirstName; }
            set
            {
                if (_model.FirstName != value)
                {
                    _model.FirstName = value;
                    RaisePropertyChanged(FirstNamePropertyName);
                }
            }
        }
        public string LastName
        {
            get { return _model.LastName; }
            set
            {
                if (_model.LastName != value)
                {
                    _model.LastName = value;
                    RaisePropertyChanged(LastNamePropertyName);
                }
            }
        }
        public string Phone
        {
            get { return _model.Phone; }
            set
            {
                if (_model.Phone != value)
                {
                    _model.Phone = value;
                    RaisePropertyChanged(PhonePropertyName);
                }
            }
        }
        public string Email
        {
            get { return _model.Email; }
            set
            {
                if (_model.Email != value)
                {
                    _model.Email = value;
                    RaisePropertyChanged(EmailPropertyName);
                }
            }
        }

    }
}

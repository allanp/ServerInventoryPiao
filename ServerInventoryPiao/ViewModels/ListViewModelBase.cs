using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ListViewModelBase<ViewModel> : ViewModelBase
    {
        public const string AddNewCommandPropertyName = "AddNewCommand";
        public const string RemoveCommandPropertyName = "RemoveCommand";
        public const string ItemsPropertyName = "Items";
        public const string SelectedItemsPropertyName = "SelectedItems";

        private ICommand _addNewCommand;
        private ICommand _removeCommand;
        private IEnumerable<ViewModel> _collection;
        private ObservableCollection<ViewModel> _selectedItems;

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

        public IEnumerable<ViewModel> Items
        {
            get { return _collection; }
            set
            {
                if (_collection != value)
                {
                    _collection = value;
                    RaisePropertyChanged(ItemsPropertyName);
                }
            }
        }

        public ObservableCollection<ViewModel> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                if (_selectedItems != value)
                {
                    _selectedItems = value;
                    RaisePropertyChanged(SelectedItemsPropertyName);
                }
            }
        }
    }
}

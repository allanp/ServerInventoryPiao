using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ListViewModelBase<ViewModel> : ViewModelBase, IEnumerable<ViewModel>
    {
        public const string AddNewCommandPropertyName = "AddNewCommand";
        public const string RemoveCommandPropertyName = "RemoveCommand";
        public const string ItemsPropertyName = "Items";
        public const string SelectedItemsPropertyName = "SelectedItems";

        private ICommand _addNewCommand;
        private ICommand _removeCommand;
        private IEnumerable<ViewModel> _collection;

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


        public const string SelectedItemPropertyName = "SelectedItem";
        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged(SelectedItemPropertyName);
                }
            }
        }

        public IEnumerator<ViewModel> GetEnumerator()
        {
            return Items != null ? Items.GetEnumerator() : null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items != null ? Items.GetEnumerator() : null;
        }
    }
}

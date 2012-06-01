using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq.Expressions;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ListViewModelBase<Model, ViewModel> : ViewModelBase, IEnumerable<ViewModel>
    {
        public const string ItemsPropertyName = "Items";
        public const string SelectedItemsPropertyName = "SelectedItems";

        private ObservableCollection<ViewModel> _collection;

        public ObservableCollection<ViewModel> Items
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

        public bool Contains(ViewModel viewModel)
        {
            return this.Items.Contains(viewModel);
        }

        public void Add(ViewModel viewModel)
        {
            if (this.Items == null)
                this.Items = new ObservableCollection<ViewModel>();
            this.Items.Add(viewModel);
        }

        public bool Remove(ViewModel viewModel)
        {
            return this.Items.Remove(viewModel);
        }

        public int Count
        {
            get { return this.Items == null ? 0 : this.Items.Count; }
        }


        protected static ObservableCollection<ViewModel> ConvertToListViewMode(IEnumerable<Model> models, Func<Model, ViewModel> convertor)
        {
            if (models == null) throw new ArgumentNullException("models");
            if (convertor == null) throw new ArgumentNullException("convertor");

            try
            {
                List<ViewModel> viewModels = new List<ViewModel>();
                foreach (var m in models)
                    viewModels.Add(convertor(m));
                return new ObservableCollection<ViewModel>(viewModels);
            }
            catch
            {
#if !Release
                throw;
#endif
            }
            // return null;
        }
    }
}

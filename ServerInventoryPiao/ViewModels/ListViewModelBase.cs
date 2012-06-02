using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Specialized;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ListViewModelBase<Model, ViewModel> : ViewModelBase, IEnumerable<ViewModel>, INotifyCollectionChanged
    {
        public const string ItemsPropertyName = "Items";
        
        private ObservableCollection<ViewModel> _collection;

        public ObservableCollection<ViewModel> Items
        {
            get
            {
                return _collection;
            }
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
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, viewModel));
            }
        }

        public bool Remove(ViewModel viewModel)
        {
            bool result = this.Items.Remove(viewModel);
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, viewModel));
            }
            return result;
        }

        public int Count
        {
            get { return this.Items == null ? 0 : this.Items.Count; }
        }


        protected static ObservableCollection<ViewModel> ConvertToListViewMode(List<Model> models, Converter<Model, ViewModel> converter)
        {
            if (models == null) throw new ArgumentNullException("models");
            if (converter == null) throw new ArgumentNullException("converter");

            try
            {
                return new ObservableCollection<ViewModel>(models.ConvertAll<ViewModel>(converter));

                //List<ViewModel> viewModels = new List<ViewModel>();
                //foreach (var m in models)
                //    viewModels.Add(getViewModel(m));

                //return new ObservableCollection<ViewModel>(viewModels);
            }
            catch
            {
#if !Release
                throw;
#endif
            }
            // return null;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}

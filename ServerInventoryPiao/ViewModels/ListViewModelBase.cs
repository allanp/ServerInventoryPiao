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


        protected static ObservableCollection<ViewModel> ConvertToListViewMode(List<Model> models, Func<Model, ViewModel> convertor, Func<ViewModel, Model> reverse)
        {
            if (models == null) throw new ArgumentNullException("models");
            if (convertor == null) throw new ArgumentNullException("convertor");

            try
            {
                List<ViewModel> viewModels = new List<ViewModel>();
                foreach (var m in models)
                    viewModels.Add(convertor(m));

                var collection = new ObservableCollection<ViewModel>(viewModels);

                collection.CollectionChanged += (sender, e) =>
                {
                    if (Object.ReferenceEquals(sender, collection))
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                models.Add(reverse((ViewModel)e.NewItems[0]));
                                break;
                            case NotifyCollectionChangedAction.Move:
                                models = (from vm in viewModels select reverse(vm)).ToList();
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                models.Remove(reverse((ViewModel)e.OldItems[0]));
                                break;
                            case NotifyCollectionChangedAction.Replace:
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                models = (from vm in viewModels select reverse(vm)).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                };
                return collection;
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

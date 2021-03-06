﻿using System.ComponentModel;

namespace ServerInventoryPiao.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class ViewModelBase<Model> : ViewModelBase
    {
        protected Model _model;
        protected ViewModelBase(Model model)
        {
            _model = model;
        }

        internal Model ModelInternal
        {
            get
            {
                return _model;
            }
        }
    }

}

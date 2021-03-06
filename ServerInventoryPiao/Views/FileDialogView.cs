﻿using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;

namespace ServerInventoryPiao.Views
{
    public class FileDialogView : IFileDialogView, INotifyPropertyChanged
    {
        public const string DialogResultPropertyName = "DialogResult";
        public const string IsEnabledPropertyName = "IsEnabled";
        public const string DataContextPropertyName = "DataContext";
        
        public const string DefaultExtPropertyName = "DefaultExt";
        public const string FilterPropertyName = "Filter"; 
        public const string FileNamesPropertyName = "FileNames";
        public const string IsVisiblePropertyName = "IsVisible";
        
        private Window _owner;

        private string _defaultExt;
        private string _filter;
        private bool? _dialogResult;
        private string[] _fileNames;
        private object _dataContext;
        private bool _isEnabled;
        private bool _isVisible;

        private FileDialog dialog;

        public string[] FileNames
        {
            get
            {
                return _fileNames;
            }
            set
            {
                if (_fileNames != value)
                {
                    _fileNames = value;

                    RaisePropertyChanged(FileNamesPropertyName);

                    if (FileNamesChanged != null)
                        FileNamesChanged(this, FileNames);
                }
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RaisePropertyChanged(IsVisiblePropertyName);
                }
            }
        }

        public string DefaultExt
        {
            get { return _defaultExt; }
            set
            {
                if (_defaultExt != value)
                {
                    _defaultExt = value;
                    RaisePropertyChanged(DefaultExtPropertyName);
                }
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    RaisePropertyChanged(FilterPropertyName);
                }
            }
        }

        public event Action<object, string[]> FileNamesChanged;

        public FileDialogView(Window owner)
        {
            _owner = owner;
            IsEnabled = true;
        }

        public bool? ShowDialog()
        {
            if (!IsEnabled)
                return null;
            
            try
            {
                switch (Mode)
                {
                    case Mode.None:
                        return null;
                    case Mode.Open:
                        dialog = new OpenFileDialog();
                        dialog.Filter = Filter;
                        break;
                    case Mode.Save:
                        dialog = new SaveFileDialog();
                        dialog.FileName = "default";
                        break;
                    default:
                        return null;
                }

                dialog.DefaultExt = DefaultExt;

                IsVisible = true;

                DialogResult = dialog.ShowDialog(_owner);

                if (Closing != null)
                    Closing(this, new CancelEventArgs(DialogResult != true));

                IsVisible = false;

                if (DialogResult == true)
                {
                    FileNames = dialog.FileNames;
                }

                if (Closed != null)
                    Closed(this, EventArgs.Empty);
            }
            finally
            {
                if (dialog != null)
                {
                    dialog = null;
                }
            }
            return DialogResult;
        }

        public bool? DialogResult
        {
            get
            {
                return _dialogResult;
            }
            protected set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    RaisePropertyChanged(DialogResultPropertyName);
                }
            }
        }

        public void Close()
        {
            if (dialog == null)
                return;

            if (!IsVisible)
                return;
        }

        public event CancelEventHandler Closing;

        public event EventHandler Closed;

        public object DataContext
        {
            get
            {
                return _dataContext;
            }
            set
            {
                if (_dataContext != value)
                {
                    _dataContext = value;
                    RaisePropertyChanged(DataContextPropertyName);
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    RaisePropertyChanged(IsEnabledPropertyName);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Mode Mode
        {
            get;
            set;
        }


        public void Show()
        {
            DialogResult = ShowDialog();
        }

        public void Hide()
        {
            // 
        }
    }
}

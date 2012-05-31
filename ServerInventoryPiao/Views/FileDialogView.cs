using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;

namespace ServerInventoryPiao.Views
{
    public interface IDialogView : IView
    {
        bool IsVisible { get; }
        bool? ShowDialog();
        bool? DialogResult { get; }
        void Close();
        event CancelEventHandler Closing;
        event EventHandler Closed;
    }

    public enum Mode { None, Open, Save }

    public interface IFileDialogView : IDialogView
    {
        Mode Mode { get; set; }
        string DefaultExt { get; set; }
        string[] FileNames { get; set; }
        event Action<object, string[]> FileNamesChanged;
    }

    public class FileDialogView : IFileDialogView, INotifyPropertyChanged
    {
        public const string DialogResultPropertyName = "DialogResult";
        public const string IsEnabledPropertyName = "IsEnabled";
        public const string DataContextPropertyName = "DataContext";
        
        public const string DefaultExtPropertyName = "DefaultExt"; 
        public const string FileNamesPropertyName = "FileNames";
        public const string IsVisiblePropertyName = "IsVisible";
        
        private Window _owner;

        private string _defaultExt;
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
                        break;
                    case Mode.Save:
                        dialog = new SaveFileDialog();
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

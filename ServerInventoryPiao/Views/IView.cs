using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ServerInventoryPiao.Views
{
    public interface IView
    {
        object DataContext { get; set; }

        void Show();

        void Hide();

        void Close();
    }

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

}

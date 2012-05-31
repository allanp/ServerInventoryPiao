using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerInventoryPiao.Views
{
    public interface IView
    {
        object DataContext { get; set; }

        void Show();

        void Hide();

        void Close();
    }
}

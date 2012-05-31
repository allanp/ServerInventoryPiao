using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerInventoryPiao.Views
{
    public interface IMainView : IView
    {
        double Height { get; set; }
        double Width { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerInventoryPiao.Views
{
    /// <summary>
    /// Interaction logic for MasterTreeView.xaml
    /// </summary>
    public partial class MasterTreeView : UserControl
    {
        public MasterTreeView()
        {
            InitializeComponent();
        }

        private void TreeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.Focus();
                // e.Handled = true;
            }
        }
    }
}

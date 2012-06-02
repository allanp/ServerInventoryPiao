using System.Windows.Controls;
using System.Windows.Input;

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
                item.Focus();
        }
    }
}

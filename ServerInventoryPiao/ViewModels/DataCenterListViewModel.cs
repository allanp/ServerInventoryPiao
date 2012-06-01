using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerInventoryPiao.Models;
using System.Collections.ObjectModel;
using ServerInventoryPiao.Controllers;

namespace ServerInventoryPiao.ViewModels
{
    public class DataCenterListViewModel : ListViewModelBase<DataCenterModel>
    {
        public DataCenterListViewModel(List<DataCenterModel> datacenters)
        {
            if (datacenters == null) throw new ArgumentNullException("datacenters");

            this.Items = datacenters;
            this.SelectedItem = null;
        }

    }
}

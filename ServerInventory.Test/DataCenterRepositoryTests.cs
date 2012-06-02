using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerInventoryPiao.Controllers;
using ServerInventoryPiao.Models;
using ServerInventoryPiao.Views;

namespace ServerInventory.Test
{
    [TestClass]
    public class DataCenterRepositoryTests
    {
        [TestMethod]
        public void WriteDataTest()
        {
            DataCenterRepository repo = new DataCenterRepository(string.Empty);

            DataCenterModel dc = new DataCenterModel();
            dc.Id = "0";
            dc.Name = "datacenter_ams";
            dc.ContactPeople = new List<Person>()
            {
                new Person(){ FirstName = "John", LastName = "Smith", Email = "john.smith@email.com", Phone = "01-234-5678" }
            };
            dc.Racks = new List<RackModel>()
            {
                new RackModel(){ Id = "1", Name = "rack_ams0", Floor = "3", Position = "b", Devices = new List<DeviceModel>(){
                    new DeviceModel(){ Id = "1000", Name = "computer1000", Status = "active", IPAddress = "10.0.0.1"},
                    new DeviceModel(){ Id = "2000", Name = "computer2000", Status = "active", IPAddress = "10.0.0.2"}
                }
                },
                new RackModel(){ Id = "2", Name = "rack_ams1", Floor = "3", Position = "d", Devices = new List<DeviceModel>(){
                    new DeviceModel(){ Id = "1001", Name = "computer1001", Status = "active", IPAddress = "10.0.3.1"},
                    new DeviceModel(){ Id = "2001", Name = "computer2001", Status = "active", IPAddress = "10.0.3.2"}
                }
                }
            };
            dc.Phone = "01-234-5678";
            dc.Address = "First street 12, Amsterdam, 1101AH";
            repo.AddDataCenter(dc);

            repo.Save();
        }

        [TestMethod]
        public void LoadDataTest()
        {
            var dialog = new FileDialogView(null){ Mode = Mode.Open, DefaultExt = "*.xml" };
            if (dialog.ShowDialog() == true && dialog.FileNames != null && dialog.FileNames.Length > 0)
            {
                DataCenterRepository repo = new DataCenterRepository(dialog.FileNames[0]);
                var dcs = repo.GetDataCenters();

                dcs.ToArray();
            }
        }
    }
}

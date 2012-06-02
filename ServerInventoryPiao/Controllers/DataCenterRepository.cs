using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using ServerInventoryPiao.Models;
using System.Xml.Linq;
using System.Windows.Resources;
using System.Windows;
using ServerInventoryPiao.Views;

namespace ServerInventoryPiao.Controllers
{
    public class DataCenterModelEventArgs : EventArgs
    {
        public DataCenterModelEventArgs(DataCenterModel datacenter)
        {
            this.DataCenter = datacenter;
        }

        public DataCenterModel DataCenter { get; private set; }
    }

    public class DataCenterRepository
    {
        List<DataCenterModel> _datacenters;

        public event EventHandler<DataCenterModelEventArgs> DataCenterAdded;
        public event EventHandler<DataCenterModelEventArgs> DataCenterRemoved;

        public DataCenterRepository() : this(string.Empty)
        {
        }

        public DataCenterRepository(string dataFileName)
        {
            this._datacenters = LoadDataCenter(dataFileName);
        }

        public List<DataCenterModel> GetDataCenters()
        {
            return new List<DataCenterModel>(_datacenters);
        }

        public bool ConatinsDataCenter(DataCenterModel datacenter)
        {
            // if (datacenter == null) throw new ArgumentNullException("datacenter");

            return _datacenters.Contains(datacenter);
        }

        public void AddDataCenter(DataCenterModel datacenter)
        {
            if (datacenter == null)
                throw new ArgumentNullException("datacenter");

            if (!_datacenters.Contains(datacenter))
            {
                _datacenters.Add(datacenter);

                if (this.DataCenterAdded != null)
                    this.DataCenterAdded(this, new DataCenterModelEventArgs(datacenter));
            }
        }

        public bool RemoveDataCenter(DataCenterModel datacenter)
        {
            if (datacenter == null) throw new ArgumentNullException("datacenter");

            bool removed = _datacenters != null && _datacenters.Remove(datacenter);
            if (removed && DataCenterRemoved != null)
            {
                DataCenterRemoved(this, new DataCenterModelEventArgs(datacenter));
            }
            return removed;
        }

        public void Save()
        {
            FileDialogView dialogView = new FileDialogView(null)
            {
                DefaultExt = "xml",
                Filter = "|*.xml",
                IsEnabled = true,
                Mode = Mode.Save
            };

            if (dialogView.ShowDialog() == true && dialogView.FileNames != null && dialogView.FileNames.Length > 0)
            {
                string filename = dialogView.FileNames[0];

                WriteTo(this, filename);
            }
        }

        private static List<DataCenterModel> LoadDataCenter(string xmlFileName)
        {
            if (xmlFileName == null || xmlFileName == string.Empty)
                return new List<DataCenterModel>();

            using (Stream stream = File.OpenRead(xmlFileName))
            {
                using (XmlReader xmlReader = new XmlTextReader(stream))
                {
                    return (from datacenter in XDocument.Load(xmlReader).Element("datacenters").Elements("datacenter")
                            select new DataCenterModel()
                            {
                                Id = (string)datacenter.Attribute("id"),
                                Name = (string)datacenter.Attribute("name"),
                                Address = (string)datacenter.Attribute("address"),
                                Phone = (string)datacenter.Attribute("phone"),
                                ContactPeople = (from person in datacenter.Element("contactpeople").Elements("person")
                                                 select new Person()
                                                 {
                                                     FirstName = (string)person.Attribute("firstname"),
                                                     LastName = (string)person.Attribute("lastname"),
                                                     Phone = (string)person.Attribute("phone"),
                                                     Email = (string)person.Attribute("email")
                                                 }).ToList(),
                                Racks = (from rack in datacenter.Element("racks").Elements("rack")
                                         select new RackModel()
                                         {
                                             Id = (string)rack.Attribute("id"),
                                             Name = (string)rack.Attribute("name"),
                                             Floor = (string)rack.Attribute("floor"),
                                             Position = (string)rack.Attribute("position"),
                                             Devices = (from device in rack.Element("devices").Elements("device")
                                                        select new DeviceModel()
                                                        {
                                                            Id = (string)device.Attribute("id"),
                                                            Name = (string)device.Attribute("name"),
                                                            IPAddress = (string)device.Attribute("ipaddress"),
                                                            Status = (string)device.Attribute("status")
                                                        }).ToList()
                                         }).ToList()
                            }).ToList();
                }
            }
        }

        private static void WriteTo(DataCenterRepository repository, string filename)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (filename == null) throw new ArgumentNullException("filename");

            if (repository._datacenters == null || repository._datacenters.Count == 0)
                return;

            if (!File.Exists(filename))
            {
                using (File.Create(filename)) { }
            }

            using (Stream stream = new MemoryStream() )
            {
                using (XmlWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    var datacenters = repository.GetDataCenters();

                    XDocument doc = new XDocument(datacenters == null ? new XElement("") :
                                                                new XElement("datacenters",
                                                                 from datacenter in datacenters
                                                                 orderby datacenter.Id
                                                                 select
                                                                 datacenter == null ? new XElement("") :
                                                                 new XElement("datacenter",
                                                                     new XAttribute("id", datacenter.Id ?? ""),
                                                                     new XAttribute("name", datacenter.Name ?? ""),
                                                                     new XAttribute("phone", datacenter.Phone ?? ""),
                                                                     new XAttribute("address", datacenter.Address ?? ""),
                                                                     new XElement("contactpeople",
                                                                         from person in datacenter.ContactPeople
                                                                         orderby person.LastName
                                                                         select
                                                                         person == null ? new XElement("") :
                                                                         new XElement("person",
                                                                             new XAttribute("firstname", person.FirstName ?? ""),
                                                                             new XAttribute("lastname", person.LastName ?? ""),
                                                                             new XAttribute("phone", person.Phone ?? ""),
                                                                             new XAttribute("email", person.Email ?? ""))),
                                                                     datacenter.Racks == null ? new XElement("") :
                                                                     new XElement("racks",
                                                                         from rack in datacenter.Racks
                                                                         orderby rack.Id
                                                                         select
                                                                         rack == null ? new XElement("") :
                                                                         new XElement("rack",
                                                                             new XAttribute("id", rack.Id ?? ""),
                                                                             new XAttribute("name", rack.Name ?? ""),
                                                                             new XAttribute("floor", rack.Floor ?? ""),
                                                                             new XAttribute("position", rack.Position ?? ""),
                                                                             rack.Devices == null ? new XElement("devices") :
                                                                             new XElement("devices",
                                                                                 from device in rack.Devices
                                                                                 orderby device.Id
                                                                                 select
                                                                                 device == null ? new XElement("") :
                                                                                 new XElement("device",
                                                                                     new XAttribute("id", device.Id ?? ""),
                                                                                     new XAttribute("name", device.Name ?? ""),
                                                                                     new XAttribute("ipaddress", device.IPAddress ?? ""),
                                                                                     new XAttribute("status", device.Status ?? ""))))))));
                    doc.Save(filename);
                }
            }
        }
    }
}

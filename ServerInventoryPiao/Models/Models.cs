using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerInventoryPiao.Models
{
    /*
        We have a datacenter,  a datacenter can have a street address, a telephone number, contact persons etc.
        A rack has a floor, a position , a name, etc.
        A computer has a name, a ip address, etc.
    */
    public partial class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public abstract class ModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ModelBase()
        {
            Id = "Unknown Id";
            Name = "Undefined Name";
        }
    }

    public partial class DataCenterModel : ModelBase
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Person> ContactPeople { get; set; }
        public List<RackModel> Racks { get; set; }
    }

    public class RackModel : ModelBase
    {
        public string Floor { get; set; }
        public string Position { get; set; }
        public List<DeviceModel> Devices { get; set; }
    }

    public partial class DeviceModel : ModelBase
    {
        public string IPAddress { get; set; }
        public string Status { get; set; }
    }
}

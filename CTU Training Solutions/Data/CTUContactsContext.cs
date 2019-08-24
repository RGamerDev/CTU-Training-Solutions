using CTU_Training_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTU_Training_Solutions.Data
{
    /// <summary>
    /// Context data which contains Contact information and their processes
    /// </summary>
    class CTUContactsContext
    {
        public List<CTUContact> Contacts;

        public CTUContactsContext()
        {
            Contacts = new List<CTUContact>()
            {
                new CTUContact{Name = "Richard" , Ip = "192.168.1.54"},
            };
        }
    }
}

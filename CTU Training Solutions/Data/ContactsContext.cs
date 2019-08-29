using CTU_Training_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTU_Training_Solutions.Data
{
    /// <summary>
    /// Context data which contains Contact information and their processes
    /// </summary>
    public class ContactsContext
    {
        public ObservableCollection<Contact> Contacts;

        public ContactsContext()
        {
            Contacts = new ObservableCollection<Contact>()
            {
                new Contact{Name = "Richard", Ip = "192.168.1.54"},
                new Contact{Name = "Gerhard", Ip = "192.168.1.54"},
                new Contact{Name = "Mikhail", Ip = "192.168.1.54"},
                new Contact{Name = "Donovan", Ip = "192.168.1.54"},
                new Contact{Name = "Leonard", Ip = "192.168.1.54"},
                new Contact{Name = "Marko", Ip = "192.168.1.54"},
                new Contact{Name = "Taahira", Ip = "192.168.1.54"},
                new Contact{Name = "Cindy", Ip = "192.168.1.54"},
                new Contact{Name = "Egon", Ip = "192.168.1.54"},
                new Contact{Name = "Chubbs", Ip = "192.168.1.54"},
                new Contact{Name = "Grayson", Ip = "192.168.1.54"},
            };
        }
    }
}

using CTU_Training_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Filters;
using Windows.Web.Http;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace CTU_Training_Solutions.Data
{
    /// <summary>
    /// Context data which contains list of gallery page objects and processes
    /// </summary>
    class EventContext
    {
        private ObservableCollection<CTUEvent> Events;

        public EventContext()
        {
            Events = new ObservableCollection<CTUEvent>()
            {
                new CTUEvent
                {   Name = "Potchefstroom Student Workshop Design Faculty",
                    Description = "You are invited! Come and join us for a student workshop on CTU's design tracks at a Potchefstroo...",
                    Date = DateTime.Parse("24 August 2019 09:00"),
                    Link = new Uri("https://ctutraining.ac.za/upcoming-events/potchefstroom-student-workshop-design-faculty/")
                },
                new CTUEvent
                {   Name = "Career One2One Saturday",
                    Description = "Stay Calm! Book your ONE2ONE session now!...",
                    Date = DateTime.Parse("31 August 2019 09:00"),
                    Link = new Uri("https://ctutraining.ac.za/upcoming-events/one2onesession/")
                },
                new CTUEvent
                {   Name = "Student Workshop Business/Management Facility",
                    Description = "You are invited! Come and join us for a student workshop on CTU's business/management tracks at a...",
                    Date = DateTime.Parse("14 September 2019 09:00 "),
                    Link = new Uri("https://ctutraining.ac.za/upcoming-events/student-workshop-business-management-facility-2/")
                },
            };
        }

        public void AddNews(ListView news, Func<CTUEvent, (string Name, string Description, DateTime Date, Uri link)> expression)
        {
            foreach (var ev in Events)
            {
                string Name, Description;
                DateTime date;
                Uri uri;
                (Name, Description, date, uri) = expression.Invoke(ev);

                news.Items.Add( new ListViewItem() { Content = $"{Name}\n{Description}\n{date.ToString()}\n{uri.ToString()}" });
            }
        }
    }
}

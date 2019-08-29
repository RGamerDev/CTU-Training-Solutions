using CTU_Training_Solutions.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace CTU_Training_Solutions.Data
{
    /// <summary>
    /// Context data which contains list of gallery page objects and processes
    /// </summary>
    class EventContext
    {
        private ObservableCollection<Event> Events;

        public EventContext()
        {
            Events = new ObservableCollection<Event>()
            {
                new Event
                {   Name = "Potchefstroom Student Workshop Design Faculty",
                    Description = "You are invited! Come and join us for a student workshop on CTU's design tracks at a Potchefstroo...",
                    Date = DateTime.Parse("24 August 2019 09:00"),
                    Link = new HyperlinkButton{Content = "https://ctutraining.ac.za/upcoming-events/potchefstroom-student-workshop-design-faculty/", NavigateUri = new Uri("https://ctutraining.ac.za/upcoming-events/potchefstroom-student-workshop-design-faculty/") }
                },
                new Event
                {   Name = "Career One2One Saturday",
                    Description = "Stay Calm! Book your ONE2ONE session now!...",
                    Date = DateTime.Parse("31 August 2019 09:00"),
                    Link = new HyperlinkButton{Content = "https://ctutraining.ac.za/upcoming-events/one2onesession/", NavigateUri = new Uri("https://ctutraining.ac.za/upcoming-events/one2onesession/") }
                },
                new Event
                {   Name = "Student Workshop Business/Management Facility",
                    Description = "You are invited! Come and join us for a student workshop on CTU's business/management tracks at a...",
                    Date = DateTime.Parse("14 September 2019 09:00 "),
                    Link = new HyperlinkButton{Content = "https://ctutraining.ac.za/upcoming-events/student-workshop-business-management-facility-2/", NavigateUri = new Uri("https://ctutraining.ac.za/upcoming-events/student-workshop-business-management-facility-2/") }
                },
            };
        }

        public void AddNews(ListView news, Func<Event, (string Name, string Description, DateTime Date, HyperlinkButton link)> expression)
        {
            foreach (var ev in Events)
            {
                string Name, Description;
                DateTime date;
                HyperlinkButton uri;
                (Name, Description, date, uri) = expression.Invoke(ev);

                news.Items.Add( new ListViewItem() { Content = $"{Name}\n{Description}\n{date.ToString()}\n{uri.NavigateUri.ToString()}" });
            }
        }
    }
}

using System;
using Windows.UI.Xaml.Controls;

namespace CTU_Training_Solutions.Models
{
    /// <summary>
    /// Class for CTUEvent objects
    /// </summary>
    public class Event
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public HyperlinkButton Link { get; set; }
    }
}

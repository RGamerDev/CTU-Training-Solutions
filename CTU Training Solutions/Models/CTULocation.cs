using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CTU_Training_Solutions.Models
{
    /// <summary>
    /// Class for CTULocation objects
    /// </summary>
    public class CTULocation
    {
        public Geopoint Location { get; set; }
        public string Title { get; set; }
    }
}

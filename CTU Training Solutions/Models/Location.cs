using Windows.Devices.Geolocation;

namespace CTU_Training_Solutions.Models
{
    /// <summary>
    /// Class for CTULocation objects
    /// </summary>
    public class Location
    {
        public Geopoint Coordinates { get; set; }
        public string Title { get; set; }
    }
}

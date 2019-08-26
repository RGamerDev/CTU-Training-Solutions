using CTU_Training_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace CTU_Training_Solutions.Data
{
    /// <summary>
    /// Context data for locations of CTU campusses and process for adding mapicons to the map
    /// </summary>
    public class LocationsContext
    {
        public List<Location> Locations; 

        public LocationsContext()
        {
            Locations = new List<Location>()
            {
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.184465, Longitude = 28.012944 } ), Title = "CTU Training Solutions Auckland Park" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.142969, Longitude = 27.870000 } ), Title = "CTU Training Solutions Roodepoort" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.101114, Longitude = 28.063133 } ), Title = "CTU Training Solutions Sandton" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.184714, Longitude = 28.251969 } ), Title = "CTU Training Solutions Boksburg" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.661544, Longitude = 27.976214 } ), Title = "CTU Training Solutions Vereeniging" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -23.904665, Longitude = 29.454342 } ), Title = "CTU Training Solutions Polokwane" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -25.444504, Longitude = 30.964961 } ), Title = "CTU Training Solutions Nelspruit"},
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -29.819194, Longitude = 31.011655 } ), Title = "CTU Training Solutions Durban" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -33.945626, Longitude = 25.570697 } ), Title = "CTU Training Solutions Port Elizabeth" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -33.930799, Longitude = 18.859399 } ), Title = "CTU Training Solutions Stellenbosch" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -33.979510, Longitude = 18.465435 } ), Title = "CTU Training Solutions Cape Town" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -26.711260, Longitude = 27.107393 } ), Title = "CTU Training Solutions Potchefstroom" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -25.783044, Longitude = 28.278999 } ), Title = "CTU Training Solutions Pretoria" },
                new Location{Coordinates = new Geopoint( new BasicGeoposition { Latitude = -29.082900, Longitude = 26.157272 } ), Title = "CTU Training Solutions Bloemfontein" },
            };
        }

        public void AddMapIcons(MapControl mc, Func<Location, string> title, Func<Location, Geopoint> geopoint)
        {
            foreach (var item in Locations)
            {
                MapIcon mapIcon = new MapIcon()
                {
                    Title = title.Invoke(item),
                    Location = new Geopoint(new BasicGeoposition { Latitude = geopoint.Invoke(item).Position.Latitude, Longitude = geopoint.Invoke(item).Position.Longitude })
                };

                mc.MapElements.Add(mapIcon);
            }
        }
    }
}

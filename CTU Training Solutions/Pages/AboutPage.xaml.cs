using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Maps;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using CTU_Training_Solutions.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        MessageDialog msg;
        CTULocationsContext CTU;

        public AboutPage()
        {
            this.InitializeComponent();
        }

        async private void Show_current_location(object sender, RoutedEventArgs e)
        {
            var accessState = await Geolocator.RequestAccessAsync();

            switch (accessState)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default };
                    Geoposition pos = await geolocator.GetGeopositionAsync();
                    Maps.Center = new Geopoint(new BasicGeoposition
                    {
                        Latitude = pos.Coordinate.Latitude,
                        Longitude = pos.Coordinate.Longitude
                    });
                    Maps.ZoomLevel = 17.5;
                    MapIcon mapIcon = new MapIcon();
                    mapIcon.Title = "Current location";
                    mapIcon.Location = Maps.Center;
                    Maps.MapElements.Add(mapIcon);
                    break;

                case GeolocationAccessStatus.Denied:
                case GeolocationAccessStatus.Unspecified:
                    msg = new MessageDialog("Location access is disabled, enable in the Windows settings");
                    await msg.ShowAsync();
                    break;
                default:
                    break;
            }
        }

        private void Maps_Loaded(object sender, RoutedEventArgs e)
        {
            Maps.Center = new Geopoint(new BasicGeoposition { Latitude = -29.060881, Longitude = 24.906847 });
            Maps.ZoomLevel = 6;
            CTU = new CTULocationsContext();
            CTU.AddMapIcons(Maps, a => a.Title, b => b.Location);
        }
    }
}

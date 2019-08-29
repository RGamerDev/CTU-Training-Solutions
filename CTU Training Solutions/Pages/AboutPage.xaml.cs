using CTU_Training_Solutions.Data;
using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {

        #region Fields
        MessageDialog msg;
        LocationsContext CTU; 
        #endregion

        public AboutPage()
        {
            this.InitializeComponent();
        }

        #region Event handlers

        /// <summary>
        /// Event handler for showing app's current location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    MainPage.SendNotification("Location", $"Your current location is Latitude: {Maps.Center.Position.Latitude.ToString()} and longitude: {Maps.Center.Position.Longitude.ToString()}");
                    break;

                case GeolocationAccessStatus.Denied:
                case GeolocationAccessStatus.Unspecified:
                    msg = new MessageDialog("Location access is disabled, enable in the Windows settings");
                    await msg.ShowAsync();
                    break;
            }
        }

        /// <summary>
        /// Eventhandler which runs when the mapcontrol has loaded 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maps_Loaded(object sender, RoutedEventArgs e)
        {
            Maps.Center = new Geopoint(new BasicGeoposition { Latitude = -29.060881, Longitude = 24.906847 });
            Maps.ZoomLevel = 6;

            //List of campus locations
            CTU = new LocationsContext();

            //Adding map icons to the Maps control
            CTU.AddMapIcons(Maps, a => a.Title, b => b.Coordinates);
        }
        #endregion

        async private void Update(object sender, RoutedEventArgs e)
        {
            Progress.IsActive = true;
            await Task.Delay(3000);

            if (true)
            {
                MainPage.SendNotification("Update", "This app is currently up to date");
            }
            else
            {
                MainPage.SendNotification("Update", "This app is currently not up to date please update via our website!");
            }

            Progress.IsActive = false;
        }
    }
}

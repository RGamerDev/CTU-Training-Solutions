using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(LandingPage));
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {
                case "Home":
                    ContentFrame.Navigate(typeof(LandingPage));
                    break;
                case "Gallery":
                    ContentFrame.Navigate(typeof(GalleryPage));
                    break;
                case "People":
                    ContentFrame.Navigate(typeof(PeoplePage));
                    break;
                case "About":
                    ContentFrame.Navigate(typeof(AboutPage));
                    break;
            }
        }

        /// <summary>
        /// Method for creating and displaying a toast notification
        /// </summary>
        /// <param name="title">Title of the toast</param>
        /// <param name="msg">Toast content</param>
        public static void SendNotification(string title, string msg)
        {
            ToastContent toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveText()
                            {
                            Text = msg
                            }
                        }
                    }
                }
            };

            var toast = new ToastNotification(toastContent.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}

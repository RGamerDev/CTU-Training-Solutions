using CTU_Training_Solutions.Data;
using System;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Windows.Web.Syndication;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {

        #region Fields
        Uri Uri = new Uri("https://www.facebook.com/v4.0/dialog/oauth?client_id=2809518512451486&redirect_uri=https://www.facebook.com/connect/login_success.html");
        Uri SID = new Uri(WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString());
        private readonly string[] requested_permissions =
        {
            "public_profile",
            "email",
            "user_friends",
            "publish_actions"
        }; 
        #endregion

        public LandingPage()
        {
            this.InitializeComponent();
        }

        #region Event handlers
        /// <summary>
        /// Method for logging in facebook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri StartUri = new Uri("https://www.facebook.com/v4.0/dialog/oauth?client_id=2809518512451486&redirect_uri=https://www.facebook.com/connect/login_success.html&display=popup&response_type=token");
                Uri EndUri = new Uri("https://www.facebook.com/connect/login_success.html");

                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                        WebAuthenticationOptions.None,
                                                        StartUri,
                                                        EndUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    string Name = await GetFacebookUserNameAsync(WebAuthenticationResult.ResponseData.ToString());
                    SessionContext.Username = Name;
                    MainPage.SendNotification($"Hello {SessionContext.Username}", $"{SessionContext.Username} has logged in successfully");
                    WebAuthenticationResult.ResponseData.ToString();
                    txtLogin.Text = $"Username: {SessionContext.Username}";
                    btnlogin.Visibility = Visibility.Collapsed;
                    DisplayNews();
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    MainPage.SendNotification("Error", "Response status is an error");
                }
                else
                {
                    MainPage.SendNotification("Error", "Error occurred while attempting to login");
                }
            }
            catch (Exception Error)
            {
                MainPage.SendNotification("Error", Error.Message);
            }

            
        }
        #endregion

        #region Methods

        /// <summary>
        /// This function extracts access_token from the response returned from web authentication broker
        /// and uses that token to get user information using facebook graph api. 
        /// </summary>
        /// <param name="webAuthResultResponseData">responseData returned from AuthenticateAsync result.</param>
        private async Task<string> GetFacebookUserNameAsync(string webAuthResultResponseData)
        {
            //Get Access Token first
            string responseData = webAuthResultResponseData.Substring(webAuthResultResponseData.IndexOf("access_token"));
            String[] keyValPairs = responseData.Split('&');
            string access_token = null;
            string expires_in = null;
            for (int i = 0; i < keyValPairs.Length; i++)
            {
                String[] splits = keyValPairs[i].Split('=');
                switch (splits[0])
                {
                    case "access_token":
                        access_token = splits[1]; //you may want to store access_token for further use. Look at Scenario5 (Account Management).
                        break;
                    case "expires_in":
                        expires_in = splits[1];
                        break;
                }
            }

            //Request User info.
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(new Uri("https://graph.facebook.com/me?access_token=" + access_token));
            JsonObject value = JsonValue.Parse(response).GetObject();
            string facebookUserName = value.GetNamedString("name");
            return facebookUserName;
        }

        /// <summary>
        /// Displays news after logging in
        /// </summary>
        async private void DisplayNews()
        {
            SyndicationClient client = new SyndicationClient();
            SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri("https://www.colcampus.com/feeds/announcements/enrollment_Lf9zD5wfVWRRIzMEWuirHT9uwjApueeWYcZNJOSm.atom"));

            foreach (SyndicationItem item in feed.Items)
            {
                News.Items.Add(new ListViewItem() { Content = item.Title.Text });
            }
        }
        #endregion
    }
}

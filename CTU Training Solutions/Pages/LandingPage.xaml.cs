using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using winsdkfb;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
using Windows.Web.Http.Filters;
using Windows.Web.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using CTU_Training_Solutions.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{
    

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {

        #region Fields
        EventContext evc;
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
            String FacebookURL = "https://www.facebook.com/dialog/oauth?client_id=2809518512451486&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=read_stream&display=popup&response_type=token";

            Uri StartUri = new Uri(FacebookURL);
            Uri callback = new Uri("https://developers.facebook.com/apps/2809518512451486/fb-login/settings/");

            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri);

            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                string Name = await GetFacebookUserNameAsync(WebAuthenticationResult.ResponseData.ToString());
                SessionContext.Username = Name;
                MainPage.SendNotification($"Hello {SessionContext.Username}", $"{SessionContext.Username} has logged in successfully");
                WebAuthenticationResult.ResponseData.ToString();
                txtLogin.Text = $"Username: {SessionContext.Username}";
                btnlogin.Visibility = Visibility.Collapsed;
            }
            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                MainPage.SendNotification("Error", "Error occurred while attempting to login");
            }

            DisplayNews();
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
        private void DisplayNews()
        {
            evc = new EventContext();
            evc.AddNews(News, a =>(a.Name, a.Description, a.Date, a.Link));
        }
        #endregion
    }
}

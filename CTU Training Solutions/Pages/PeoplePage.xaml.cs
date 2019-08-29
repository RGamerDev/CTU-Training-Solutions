using CTU_Training_Solutions.Data;
using CTU_Training_Solutions.Models;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PeoplePage : Page
    {
        #region Fields
        DataTransferManager data = DataTransferManager.GetForCurrentView();
        private MessageWebSocket messageWebSocket;
        private Task connectTask;
        public string message;
        public ContactsContext context;
        #endregion

        public PeoplePage()
        {
            this.InitializeComponent();
            data.DataRequested += DataTransferManager_DataRequested;
            context = new ContactsContext();
        }

        #region Event Handlers
        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetText("Hello world!");
            request.Data.Properties.Title = "Share Example";
            request.Data.Properties.Description = "A demonstration on how to share";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            foreach (Contact item in context.Contacts)
            {
                Master.Items.Add(item.Name);
            }

            this.messageWebSocket = new MessageWebSocket();

            // In this example, we send/receive a string, so we need to set the MessageType to Utf8.
            this.messageWebSocket.Control.MessageType = SocketMessageType.Utf8;

            this.messageWebSocket.MessageReceived += WebSocket_MessageReceived;
            this.messageWebSocket.Closed += WebSocket_Closed;

            try
            {
                connectTask = this.messageWebSocket.ConnectAsync(new Uri("wss://echo.websocket.org")).AsTask();
            }
            catch (Exception ex)
            {
                WebErrorStatus webErrorStatus = WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add additional code here to handle exceptions.
            }
        }


        /// <summary>
        /// When a message is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void WebSocket_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (DataReader dataReader = args.GetDataReader())
                {
                    dataReader.UnicodeEncoding = UnicodeEncoding.Utf8;
                    message = dataReader.ReadString(dataReader.UnconsumedBufferLength);
                    //this.messageWebSocket.Dispose();
                }
            }
            catch (Exception ex)
            {
                WebErrorStatus webErrorStatus = WebSocketError.GetStatus(ex.GetBaseException().HResult);
                // Add additional code here to handle exceptions.
            }
        }

        private void WebSocket_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            // Add additional code here to handle the WebSocket being closed.
        }

        private void send(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();

            //connectTask.ContinueWith(_ => this.SendMessageUsingMessageWebSocketAsync("Hello, World!"));
        }

        private void Master_ItemClick(object sender, ItemClickEventArgs e)
        {
            frame.Navigate(typeof(PeopleDetailsPage), e.ClickedItem.ToString());
        }

        #endregion

        #region Method
        private async Task SendMessageUsingMessageWebSocketAsync(string message)
        {
            using (var dataWriter = new DataWriter(this.messageWebSocket.OutputStream))
            {
                dataWriter.WriteString(message);
                await dataWriter.StoreAsync();
                dataWriter.DetachStream();
            }
        } 
        #endregion
    }
}

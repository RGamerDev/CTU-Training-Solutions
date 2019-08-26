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
using Windows.Networking.Sockets;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PeoplePage : Page
    {
        DataTransferManager data = DataTransferManager.GetForCurrentView();
        private MessageWebSocket messageWebSocket;
        private Task connectTask;
        public string message;

        public PeoplePage()
        {
            this.InitializeComponent();
            data.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetText("Hello world!");
            request.Data.Properties.Title = "Share Example";
            request.Data.Properties.Description = "A demonstration on how to share";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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

        private async Task SendMessageUsingMessageWebSocketAsync(string message)
        {
            using (var dataWriter = new DataWriter(this.messageWebSocket.OutputStream))
            {
                dataWriter.WriteString(message);
                await dataWriter.StoreAsync();
                dataWriter.DetachStream();
            }
        }

        private void WebSocket_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (DataReader dataReader = args.GetDataReader())
                {
                    dataReader.UnicodeEncoding = UnicodeEncoding.Utf8;
                    message = dataReader.ReadString(dataReader.UnconsumedBufferLength);
                    txtbl.Text = message;
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
    }
}

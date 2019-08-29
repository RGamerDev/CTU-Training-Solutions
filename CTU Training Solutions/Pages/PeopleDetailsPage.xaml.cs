﻿using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CTU_Training_Solutions.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PeopleDetailsPage : Page
    {
        private string context;
        public PeopleDetailsPage()
        {
            this.InitializeComponent();
        }

        private void SendMsg(object sender, RoutedEventArgs e)
        {
            ChatWindow.Items.Add($"{context}: {Txt.Text}");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            context = e.Parameter.ToString();
            Person.Text = context;
        }
    }
}

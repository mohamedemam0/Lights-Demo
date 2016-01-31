using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightsDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //App.BluetoothManager.MessageReceived += BluetoothManager_MessageReceived;
            App.BluetoothManager.ExceptionOccured += BluetoothManager_ExceptionOccured;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.BluetoothManager.Disconnect();
        }

        #region Bluetooth Connection Methods, Events and Exceptions
        private async void BluetoothManager_ExceptionOccured(object sender, Exception ex)
        {
            var md = new MessageDialog(ex.Message, "We've got a problem with bluetooth...");
            md.Commands.Add(new UICommand("Ok"));
            md.DefaultCommandIndex = 0;
            var result = await md.ShowAsync();
        }
        public async static void SendMessage(string cmd)
        {
            var res = await App.BluetoothManager.SendMessageAsync(cmd);
        }
        //private void BluetoothManager_MessageReceived(object sender, string message)
        //{
        //    //System.Diagnostics.Debug.WriteLine(message);
        //}

        #endregion

        #region Connect and Disconnect Buttons

        private async void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            await App.BluetoothManager.EnumerateDevicesAsync(GetElementRect((FrameworkElement)sender));
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point(0, 0));
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private void DisConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            App.BluetoothManager.Disconnect();
        }
        #endregion

        private void TurnOnBtn_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("1");
        }

        private void TurnOffBtn_Click(object sender, RoutedEventArgs e)
        {
            SendMessage("0");
        }
        
    }
}

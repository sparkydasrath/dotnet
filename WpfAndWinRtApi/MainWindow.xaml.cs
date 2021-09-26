using System;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace WpfAndWinRtApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleClick(object sender, RoutedEventArgs e)
        {
            string title = "featured picture of the day";
            string content = "beautiful scenery";
            string image = "https://picsum.photos/360/180?image=104";
            string logo = "https://picsum.photos/64?image=883";
            ShowNotification(title, content, image, logo);
        }

        private void ShowNotification(string title, string content, string image, string logo)
        {
            // string xmlString = $@"<toast><visual><binding template = 'ToastGeneric'><text>{title}</text><text>{content}</text><image src=>'{image}'</image></binding></visual></toast>";
            // XmlDocument toastXml = new();
            // toastXml.LoadXml(xmlString);
            // ToastNotification toast = new ToastNotification(toastXml);
            // ToastNotificationManager.CreateToastNotifier().Show(toast);

            string xmlString =
                $@"<toast><visual>
                   <binding template='ToastGeneric'>
                   <text>{title}</text>
                   <text>{content}</text>
                   <image src='{image}'/>
                   <image src='{logo}' placement='appLogoOverride' hint-crop='circle'/>
                   </binding>
              </visual></toast>";

            XmlDocument toastXml = new();
            toastXml.LoadXml(xmlString);

            ToastNotification toast = new(toastXml);

            try
            {
                ToastNotifier x = ToastNotificationManager.CreateToastNotifier();
                x.Show(toast);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
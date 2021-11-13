using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyNetMQHost host = new MyNetMQHost();
            MyNetMQClient client = new MyNetMQClient();

            client.Publish("Hello world!");
        }
    }
}
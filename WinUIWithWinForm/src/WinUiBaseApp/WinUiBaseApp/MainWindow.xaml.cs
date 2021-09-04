using Microsoft.UI.Xaml;
using WinFormsLibrary1;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUiBaseApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>z
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";

            Class1 c1 = new();
            c1.Print();

            Form1 f1 = new ();
            f1.Activate();
        }

        public string MyText { get; set; }
    }
}
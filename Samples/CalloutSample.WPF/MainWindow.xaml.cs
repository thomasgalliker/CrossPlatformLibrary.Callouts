using System.Windows;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

namespace CalloutSample.WPF
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Button_OnClick_SimpleCallout(object sender, RoutedEventArgs e)
        {
            var callout = SimpleIoc.Default.GetInstance<ICallout>();
            callout.Show("Simple Callout", "Message with some text.");
        }

        private void Button_OnClick_ContentCallout(object sender, RoutedEventArgs e)
        {
        }
    }
}
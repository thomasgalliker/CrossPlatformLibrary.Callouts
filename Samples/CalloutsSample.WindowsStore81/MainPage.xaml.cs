using System.Diagnostics;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

namespace CalloutsSample.WindowsStore81
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
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
            var callout = SimpleIoc.Default.GetInstance<ICallout>();

            var okButtonConfig = new ButtonConfig("I Agree", () => { Debug.WriteLine("Agreed!"); }, isEnabled: false);
            var cancelButtonConfig = new ButtonConfig("I Decline", () => { Debug.WriteLine("Disagreed!"); });
            var buttonConfigs = new[] { okButtonConfig, cancelButtonConfig };

            var panel = new StackPanel();
            panel.Children.Add(new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap, });

            var checkBox = new CheckBox { Content = "Agree" };
            checkBox.Checked += (o, args) => { okButtonConfig.IsEnabled = true; };
            checkBox.Unchecked += (o, args) => { okButtonConfig.IsEnabled = false; };
            panel.Children.Add(checkBox);

            callout.Show("Content Callout", panel, buttonConfigs);
        }
    }
}
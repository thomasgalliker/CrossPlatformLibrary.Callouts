using System.Windows;
using System.Windows.Controls;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

using Microsoft.Phone.Controls;

namespace CalloutsSample.WindowsPhone8
{
    public partial class MainPage : PhoneApplicationPage
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

            var panel = new StackPanel();

            panel.Children.Add(new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap, });

            var checkBox = new CheckBox { Content = "Agree" };
            checkBox.Checked += (o, args) => { };
            panel.Children.Add(checkBox);

            var buttonConfigs = new[] { new ButtonConfig("I Agree", () => { }), new ButtonConfig("I Decline") };

            callout.Show("Content Callout", panel, buttonConfigs);
        }
    }
}
using System.Windows;
using System.Windows.Controls;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

using Microsoft.Phone.Controls;

namespace CalloutsSample.WindowsPhone8
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly ICallout callout;
        private StackPanel panel;

        public MainPage()
        {
            this.InitializeComponent();
            this.callout = SimpleIoc.Default.GetInstance<ICallout>();
        }

        private void Button_OnClick_SimpleCallout(object sender, RoutedEventArgs e)
        {
            this.callout.Show("Simple Callout", "Message with some text.");
        }

        private void Button_OnClick_ContentCallout(object sender, RoutedEventArgs e)
        {
            // Create a reusable UI control
            if (this.panel == null)
            {
                this.panel = new StackPanel();
                this.panel.Children.Add(new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap, });

                var checkBox = new CheckBox { Content = "Agree" };
                checkBox.Checked += (o, args) => { };
                this.panel.Children.Add(checkBox);
            }

            var buttonConfigs = new[] { new ButtonConfig("I Agree", () => { }), new ButtonConfig("I Decline") };

            this.callout.Show("Content Callout", this.panel, buttonConfigs);
        }
    }
}
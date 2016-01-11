using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

namespace CalloutsSample.WindowsPhone81
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
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
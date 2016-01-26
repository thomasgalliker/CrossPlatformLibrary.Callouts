using System.Windows;
using System.Windows.Controls;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

namespace CalloutSample.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ICallout callout;

        public MainWindow()
        {
            this.InitializeComponent();

            this.callout = SimpleIoc.Default.GetInstance<ICallout>();
        }

        private void Button_OnClick_SimpleCallout(object sender, RoutedEventArgs e)
        {
            this.callout.Show(
                this.CaptionTextBox.Text,
                "Message with some text.",
                new [] {new ButtonConfig("OK") },
                this.FullScreenCheckBox.IsChecked.Value);
        }

        private void Button_OnClick_ContentCallout(object sender, RoutedEventArgs e)
        {
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap, });

            var checkBox = new CheckBox { Content = "Agree" };
            checkBox.Checked += (o, args) => { };
            panel.Children.Add(checkBox);

            var buttonConfigs = new[] { new ButtonConfig("I Agree", () => { }), new ButtonConfig("I Decline") };

            this.callout.Show(
                this.CaptionTextBox.Text,
                panel,
                buttonConfigs,
                this.FullScreenCheckBox.IsChecked.Value);
        }
    }
}
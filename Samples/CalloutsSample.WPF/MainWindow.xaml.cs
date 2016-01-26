using System.Diagnostics;
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
            var okButtonConfig = new ButtonConfig("I Agree", () => { Debug.WriteLine("Agreed!"); }, isEnabled: false);
            var cancelButtonConfig = new ButtonConfig("I Decline", () => { Debug.WriteLine("Disagreed!"); });
            var buttonConfigs = new[] { okButtonConfig, cancelButtonConfig };

            var panel = new StackPanel();
            panel.Children.Add(new TextBlock { Text = "This is a short message.", TextWrapping = TextWrapping.Wrap, });
            
            var checkBox = new CheckBox { Content = "Agree" };
            checkBox.Checked += (o, args) => { okButtonConfig.IsEnabled = true; };
            checkBox.Unchecked += (o, args) => { okButtonConfig.IsEnabled = false; };
            panel.Children.Add(checkBox);

            this.callout.Show(
                this.CaptionTextBox.Text,
                panel,
                buttonConfigs,
                this.FullScreenCheckBox.IsChecked.Value);
        }
    }
}
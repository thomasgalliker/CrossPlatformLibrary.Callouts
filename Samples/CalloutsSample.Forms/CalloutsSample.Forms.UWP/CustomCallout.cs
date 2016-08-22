using CrossPlatformLibrary.Callouts;
using Windows.UI.Xaml.Controls;

namespace CalloutsSample.Forms.UWP
{
    public class CustomCallout : ICustomCallout
    {
        public object GetContent(ButtonConfig okButtonConfig)
        {
            var stackpanel = new StackPanel();
            
            ////var label = new TextBlock();
            ////label.Text = "Do you agree?";
            ////stackpanel.Children.Add(label);

            ////var checkBox = new CheckBox();
            ////checkBox.Checked += (o, args) => { okButtonConfig.IsEnabled = checkBox.IsChecked.HasValue && checkBox.IsChecked.Value; };
            ////stackpanel.Children.Add(checkBox);
 
            return null;
        }
    }
}
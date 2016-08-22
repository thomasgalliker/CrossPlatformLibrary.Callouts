using System.Diagnostics;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

using Xamarin.Forms;

namespace CalloutsSample.Forms
{
    class DemoPage : ContentPage
    {
        public DemoPage()
        {
            this.Title = "CrossPlatformLibrary.Callouts Demo";

            var simpleCalloutButton = new Button
            {
                Text = "Show simple callout"
            };
            simpleCalloutButton.Clicked += this.SimpleCalloutButton_Clicked;

            var contentCalloutButton = new Button
            {
                Text = "Show content callout"
            };
            contentCalloutButton.Clicked += this.ContentCalloutButton_Clicked;

            var stackPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    simpleCalloutButton,
                    contentCalloutButton
                }
            };

            this.Content = stackPanel;
        }

        private void SimpleCalloutButton_Clicked(object sender, System.EventArgs e)
        {
            var callout = SimpleIoc.Default.GetInstance<ICallout>();

            var buttonConfigs = new[] { new ButtonConfig("OK") };

            callout.Show("Simple Callout", "Message with some text.", buttonConfigs);
        }

        private void ContentCalloutButton_Clicked(object sender, System.EventArgs e)
        {
            var callout = SimpleIoc.Default.GetInstance<ICallout>();
   
            var okButtonConfig = new ButtonConfig("I Agree", () => { Debug.WriteLine("AGREED!"); }, isEnabled: false);
            var cancelButtonConfig = new ButtonConfig("I Decline");
            var buttonConfigs = new[] { okButtonConfig, cancelButtonConfig };

            var customCallout = SimpleIoc.Default.GetInstance<ICustomCallout>();
            var customCalloutContent = customCallout.GetContent(okButtonConfig);

            callout.Show("Content Callout", customCalloutContent, buttonConfigs);
        }
    }
}

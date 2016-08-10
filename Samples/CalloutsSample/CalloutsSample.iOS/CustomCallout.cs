using CoreGraphics;

using CrossPlatformLibrary.Callouts;

using UIKit;

namespace CalloutsSample.Forms.iOS
{
    public class CustomCallout : ICustomCallout
    {
        public object GetContent(ButtonConfig okButtonConfig)
        {
            var panel = new UIView();
            panel.Frame = new CGRect(0, 0, 200, 50);

            var label = new UILabel();
            label.Text = "Do you agree?";
            label.SizeToFit();
            label.AdjustsFontSizeToFitWidth = true;
            panel.Add(label);

            var uiSwitch = new UISwitch();
            uiSwitch.Frame = new CGRect(200, 0, 27, 27);
            uiSwitch.ValueChanged += (o, args) => { okButtonConfig.IsEnabled = uiSwitch.On; };
            panel.Add(uiSwitch);
 
            return panel;
        }
    }
}
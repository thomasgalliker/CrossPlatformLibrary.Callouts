using CoreGraphics;

using CrossPlatformLibrary.Callouts;

using UIKit;

namespace CalloutsSample.Forms.iOS
{
    public class CustomCallout : ICustomCallout
    {
        public object GetContent(ButtonConfig okButtonConfig)
        {
            var label = new UILabel();
            label.Text = "A custom iOS UILabel with some text...";
            label.SizeToFit();
            label.AdjustsFontSizeToFitWidth = true;
            return label;
        }
    }
}
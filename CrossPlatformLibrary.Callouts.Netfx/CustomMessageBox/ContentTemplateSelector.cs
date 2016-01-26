using System.Windows;
using System.Windows.Controls;

namespace CrossPlatformLibrary.Callouts.CustomMessageBox
{
    public class ContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ObjectTemplate { get; set; }

        public DataTemplate StringTemplate { get; set; }

        public override DataTemplate SelectTemplate(object content, DependencyObject container)
        {
            var stringContent = content as TextAndImage;
            if (stringContent != null)
            {
                return this.StringTemplate;
            }
            else
            {
                return this.ObjectTemplate;
            }
        }
    }
}
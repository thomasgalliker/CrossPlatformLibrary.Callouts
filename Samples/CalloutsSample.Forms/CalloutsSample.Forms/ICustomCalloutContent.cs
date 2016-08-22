using CrossPlatformLibrary.Callouts;

namespace CalloutsSample.Forms
{
    public interface ICustomCallout
    {
        object GetContent(ButtonConfig okButtonConfig);
    }
}

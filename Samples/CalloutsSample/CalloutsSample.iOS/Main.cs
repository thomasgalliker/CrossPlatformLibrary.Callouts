using CrossPlatformLibrary.IoC;

using UIKit;

namespace CalloutsSample.Forms.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            SimpleIoc.Default.Register<ICustomCallout, CustomCallout>(); // TODO: Register by convention!

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
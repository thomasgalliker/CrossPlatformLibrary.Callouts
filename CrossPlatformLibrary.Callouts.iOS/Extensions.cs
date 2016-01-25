using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#if __UNIFIED__
using CoreGraphics;
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace CrossPlatformLibrary.Callouts
{
    public static class Extensions
    {
        internal static void PresentInternal(this UIApplication app, UIViewController controller)
        {
            var topViewController = GetTopViewController(app);
            if (controller.PopoverPresentationController != null)
            {
#if __UNIFIED__
                var cgRect = new CGRect(topViewController.View.Bounds.Width / (nfloat)2, topViewController.View.Bounds.Bottom, (nfloat)0, (nfloat)0);
#else
                var cgRect = new RectangleF(topViewController.View.Bounds.Width / 2, topViewController.View.Bounds.Bottom, 0, 0);
#endif
                controller.PopoverPresentationController.SourceView = topViewController.View;
                controller.PopoverPresentationController.SourceRect = cgRect;
                controller.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Unknown;
            }

            topViewController.PresentViewController(controller, animated: false, completionHandler: () => { });
        }


        private static UIViewController GetTopViewController(this UIApplication app)
        {
            UIViewController uiViewController = app.KeyWindow.RootViewController;
            while (uiViewController.PresentedViewController != null)
            {
                uiViewController = uiViewController.PresentedViewController;
            }
            return uiViewController;
        }

    }
}
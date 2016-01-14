using System.Collections.Generic;

using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using CrossPlatformLibrary.Dispatching;

using Guards;

using Java.Lang;
using Java.Lang.Reflect;
using Java.Util;

using Exception = System.Exception;
using Object = System.Object;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Callout implementation for Android.
    /// </summary>
    public class Callout : CalloutBase
    {
        private readonly IDispatcherService dispatcherService;

        public Callout(IDispatcherService dispatcherService)
        {
            Guard.ArgumentNotNull(() => dispatcherService);

            this.dispatcherService = dispatcherService;
        }

        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            var currentActivity = GetActivity();
            var alert = new AlertDialog.Builder(currentActivity);
            alert.SetTitle(caption);
            alert.SetCancelable(false);

            var stringContent = content as string;
            if (stringContent != null)
            {
                alert.SetMessage(stringContent);
            }

            var viewContent = content as View;
            if (viewContent != null)
            {
                alert.SetView(viewContent);
            }

            foreach (var buttonConfig in buttonConfigs)
            {
                var config = buttonConfig;
                alert.SetPositiveButton(buttonConfig.Text, (senderAlert, args) => { config.Action(); });
                ////alert.SetNegativeButton(buttonConfig.Text, (senderAlert, args) => { config.Action(); });
                ////alert.SetNeutralButton(buttonConfig.Text, (senderAlert, args) => { config.Action(); });
            }

            // dispatch the alert to the UI thread
            this.dispatcherService.CheckBeginInvokeOnUI(() => alert.Create().Show());
        }

        private static Activity GetActivity()
        {
            Class activityThreadClass = Class.ForName("android.app.ActivityThread");
            Java.Lang.Object activityThread = activityThreadClass.GetMethod("currentActivityThread").Invoke(null);
            var activitiesField = activityThreadClass.GetDeclaredField("mActivities");
            activitiesField.Accessible = true;
            Android.Util.ArrayMap activities = (Android.Util.ArrayMap)activitiesField.Get(activityThread);
            foreach (Java.Lang.Object activityRecord in activities.Values())
            {
                try
                {
                    Class activityRecordClass = activityRecord.Class;
                    Field pausedField = activityRecordClass.GetDeclaredField("paused");
                    pausedField.Accessible = true;
                    if (!pausedField.GetBoolean(activityRecord))
                    {
                        Field activityField = activityRecordClass.GetDeclaredField("activity");
                        activityField.Accessible = true;
                        Activity activity = (Activity)activityField.Get(activityRecord);
                        return activity;
                    }
                }
                catch (Exception)
                {

                }

            }

            return null;
        }
    }
}
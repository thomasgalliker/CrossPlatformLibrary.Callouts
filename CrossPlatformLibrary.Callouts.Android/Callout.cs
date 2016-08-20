using System;

using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;

using CrossPlatformLibrary.Dispatching;

using Guards;

using Java.Lang;
using Java.Lang.Reflect;

using Exception = System.Exception;
using Object = Java.Lang.Object;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    ///     Callout implementation for Android.
    /// </summary>
    public class Callout : CalloutBase
    {
        private readonly IDispatcherService dispatcherService;

        public Callout(IDispatcherService dispatcherService)
        {
            Guard.ArgumentNotNull(() => dispatcherService);

            this.dispatcherService = dispatcherService;
            this.MaxNumberOfButtons = 3;
        }

        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            ButtonConfig positiveButton = null;
            ButtonConfig negativeButton = null;
            ButtonConfig neutralButton = null;

            if (buttonConfigs.Length < this.MinNumberOfButtons || buttonConfigs.Length > this.MaxNumberOfButtons)
            {
                throw new ArgumentException(string.Format("'buttonConfigs' supports a minimum of {0} and a maximum of {1} buttons", this.MinNumberOfButtons, this.MaxNumberOfButtons));
            }

            var currentActivity = GetActivity();
            var alertBuilder = new AlertDialog.Builder(currentActivity);
            alertBuilder.SetTitle(caption);
            alertBuilder.SetCancelable(isFullScreen);

            var stringContent = content as string;
            if (stringContent != null)
            {
                alertBuilder.SetMessage(stringContent);
            }

            var viewContent = content as View;
            if (viewContent != null)
            {
                alertBuilder.SetView(viewContent);
            }

            AlertDialog alertDialog = null;

            if (buttonConfigs.Length >= 1)
            {
                positiveButton = buttonConfigs[0];
                alertBuilder.SetPositiveButton(positiveButton.Text, (senderAlert, args) => { positiveButton.Action(); });
                positiveButton.EnabledChanged += (sender, isEnabled) => { UpdateEnabledChanged(alertDialog, DialogButtonType.Positive, isEnabled); };
            }
            if (buttonConfigs.Length >= 2)
            {
                negativeButton = buttonConfigs[1];
                alertBuilder.SetNegativeButton(negativeButton.Text, (senderAlert, args) => { negativeButton.Action(); });
                negativeButton.EnabledChanged += (sender, isEnabled) => { UpdateEnabledChanged(alertDialog, DialogButtonType.Negative, isEnabled); };
            }
            if (buttonConfigs.Length == 3)
            {
                neutralButton = buttonConfigs[2];
                alertBuilder.SetNeutralButton(neutralButton.Text, (senderAlert, args) => { neutralButton.Action(); });
                neutralButton.EnabledChanged += (sender, isEnabled) => { UpdateEnabledChanged(alertDialog, DialogButtonType.Neutral, isEnabled); };
            }

            // dispatch the alert to the UI thread
            this.dispatcherService.CheckBeginInvokeOnUI(
                () =>
                    {
                        alertDialog = alertBuilder.Create();
                        alertDialog.Show();

                        if (positiveButton != null)
                        {
                            UpdateEnabledChanged(alertDialog, DialogButtonType.Positive, positiveButton.IsEnabled);
                        }
                        if (negativeButton != null)
                        {
                            UpdateEnabledChanged(alertDialog, DialogButtonType.Negative, negativeButton.IsEnabled);
                        }
                        if (neutralButton != null)
                        {
                            UpdateEnabledChanged(alertDialog, DialogButtonType.Neutral, neutralButton.IsEnabled);
                        }
                    });
        }

        private static void UpdateEnabledChanged(AlertDialog alertDialog, DialogButtonType dialogButtonType, bool isEnabled)
        {
            var button = alertDialog.GetButton((int)dialogButtonType);
            button.Enabled = isEnabled;
        }

        /// <summary>
        ///     Gets the current activity.
        ///     The inspiration for this code came from here:
        ///     http://stackoverflow.com/questions/11411395/how-to-get-current-foreground-activity-context-in-android
        /// </summary>
        private static Activity GetActivity()
        {
            Class activityThreadClass = Class.ForName("android.app.ActivityThread");
            Object activityThread = activityThreadClass.GetMethod("currentActivityThread").Invoke(null);
            var activitiesField = activityThreadClass.GetDeclaredField("mActivities");
            activitiesField.Accessible = true;
            ArrayMap activities = (ArrayMap)activitiesField.Get(activityThread);
            foreach (Object activityRecord in activities.Values())
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
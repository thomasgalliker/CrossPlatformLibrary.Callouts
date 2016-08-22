﻿using System.Diagnostics;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

using Xamarin.Forms;

namespace CalloutsSample.Forms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SimpleCalloutButtonClicked(object sender, System.EventArgs e)
        {
            var callout = SimpleIoc.Default.GetInstance<ICallout>();

            var buttonConfigs = new[] { new ButtonConfig("OK") };

            callout.Show("Simple Callout", "Message with some text.", buttonConfigs);
        }

        private void ContentCalloutButtonClicked(object sender, System.EventArgs e)
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
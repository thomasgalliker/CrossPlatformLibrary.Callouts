// -----------------------------------------------------------------------
// <copyright>
// See https://github.com/evanwon/WPFCustomMessageBox
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace CrossPlatformLibrary.Callouts.CustomMessageBox
{
    internal partial class CustomMessageBoxWindow : Window
    {
        private readonly bool isModal;
        private bool isButtonClickToClose;

        public CustomMessageBoxWindow(string caption, object content, ButtonConfig[] buttonConfigs, bool isModal = false)
        {
            this.InitializeComponent();
            this.Title = caption;

            var stringContent = content as string;
            if (stringContent != null)
            {
                this.ContentControl.Content = new TextAndImage(
                    messageBoxText: stringContent, 
                    messageBoxImage: this.GetImageSource(MessageBoxImage.None));
            }
            else
            {
                this.ContentControl.Content = content;
            }

            this.isModal = isModal;
            this.DisplayButtons(buttonConfigs);
        }


        private void DisplayButtons(ButtonConfig[] buttonConfigs)
        {
            this.ButtonConfigItems.ItemsSource = buttonConfigs;
        }

        private ImageSource GetImageSource(MessageBoxImage image)
        {
            Icon icon;

            switch (image)
            {
                case MessageBoxImage.Exclamation:       // Enumeration value 48 - also covers "Warning"
                    icon = SystemIcons.Exclamation;
                    break;
                case MessageBoxImage.Error:             // Enumeration value 16, also covers "Hand" and "Stop"
                    icon = SystemIcons.Hand;
                    break;
                case MessageBoxImage.Information:       // Enumeration value 64 - also covers "Asterisk"
                    icon = SystemIcons.Information;
                    break;
                case MessageBoxImage.Question:
                    icon = SystemIcons.Question;
                    break;
                default:
                    return null;
            }

            return icon.ToImageSource();
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            var buttonConfig = (ButtonConfig)((Button)sender).Tag;
            buttonConfig.Action();

            this.DismissDialog();
        }

        private void DismissDialog()
        {
            // Indicate the OnClosing method that the close was initiated by a button click
            this.isButtonClickToClose = true;
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.isModal && !this.isButtonClickToClose)
            {
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }

        #region Nasty code to disable the close button of the Window
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIdEnableItem, uint uEnable);

        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint SC_CLOSE = 0xF060;
        const int WM_SHOWWINDOW = 0x00000018;

        /// <summary>
        /// This code hides the close button if the message box is shown as not closable.
        /// Source: http://stackoverflow.com/questions/17962429/disable-close-button-in-title-bar-of-a-wpf-window-c
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            if (this.isModal)
            {
                var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                if (hwndSource != null)
                {
                    hwndSource.AddHook(this.hwndSourceHook);
                }
            }
        }

        private IntPtr hwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWWINDOW)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
            return IntPtr.Zero;
        }
        #endregion
    }
}

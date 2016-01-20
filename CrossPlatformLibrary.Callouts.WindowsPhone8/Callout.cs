using System;
using System.Windows;

using Guards;

using Microsoft.Phone.Controls;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Callout implementation for Windows Phone 8.
    /// With kind support of: http://windowsapptutorials.com/windows-phone/general/how-to-create-a-custom-messagebox-in-windows-phone-application-3/
    /// </summary>
    public class Callout : CalloutBase
    {
        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            ButtonConfig leftButton = null;
            ButtonConfig rightButton = null;

            if (buttonConfigs.Length < 1 || buttonConfigs.Length > 2)
            {
                throw new ArgumentException("'buttonConfigs' only supports ");
            }

            //var hyperlinkButton = content as HyperlinkButton;
            //if (hyperlinkButton != null)
            //{
            //    TiltEffect.SetIsTiltEnabled(hyperlinkButton, true);
            //}

            var messageBox = new CustomMessageBox
            {
                Caption = caption,
                IsFullScreen = isFullScreen
            };

            if (buttonConfigs.Length >= 1)
            {
                leftButton = buttonConfigs[0];
                messageBox.LeftButtonContent = leftButton.Text;
            }
            if (buttonConfigs.Length == 2)
            {
                rightButton = buttonConfigs[1];
                messageBox.RightButtonContent = rightButton.Text;
            }
            
            var stringContent = content as string;
            if (stringContent != null)
            {
                messageBox.Message = stringContent;
                messageBox.Content = null;
            }
            else
            {
                messageBox.Message = null;
                messageBox.Content = content;
            }

            messageBox.Dismissed += (s1, e1) =>
                {
                    switch (e1.Result)
                    {
                        case CustomMessageBoxResult.LeftButton:
                            if (leftButton != null)
                            {
                                leftButton.Action();
                            }
                            break;
                        case CustomMessageBoxResult.RightButton:
                            if (rightButton != null)
                            {
                                rightButton.Action();
                            }
                            break;
                        case CustomMessageBoxResult.None:
                            break;
                        default:
                            break;
                    }

                    messageBox.Content = null; // This is to avoid ArgumentException 'Value does not fall within the expected range'
                };

            Deployment.Current.Dispatcher.BeginInvoke(() => { messageBox.Show(); });
        }
    }
}
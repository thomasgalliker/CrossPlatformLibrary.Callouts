using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CrossPlatformLibrary.Callouts
{
    public sealed partial class ContentDialog : UserControl
    {
        public ContentDialog()
        {
            this.InitializeComponent();
            this.primaryButton.Visibility = Visibility.Collapsed;
            this.secondaryButton.Visibility = Visibility.Collapsed;
            this.ParentPopup.IsOpen = false;
            this.primaryButton.Click += this.PrimaryButton_Click;
            this.secondaryButton.Click += this.SecondaryButton_Click;
        }

        private void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnPrimaryButtonClick();
            this.ParentPopup.IsOpen = false;
        }

        private void SecondaryButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnSecondaryButtonClick();
            this.ParentPopup.IsOpen = false;
        }

        private void OnLayoutUpdated(object sender, object e)
        {
            if (this.ParentPopup.HorizontalOffset == 0 && this.ParentPopup.VerticalOffset == 0)
            {
                double ActualHorizontalOffset = this.ParentPopup.HorizontalOffset;
                double ActualVerticalOffset = this.ParentPopup.VerticalOffset;

                double NewHorizontalOffset = (Window.Current.Bounds.Width - this.gdChild.ActualWidth) / 2;
                double NewVerticalOffset = (Window.Current.Bounds.Height - this.gdChild.ActualHeight) / 2;

                if (ActualHorizontalOffset != NewHorizontalOffset || ActualVerticalOffset != NewVerticalOffset)
                {
                    this.ParentPopup.HorizontalOffset = NewHorizontalOffset;
                    this.ParentPopup.VerticalOffset = NewVerticalOffset;
                }
            }
        }

        public object ContentWrapper
        {
            get
            {
                return this.content.Content;
            }
            set
            {
                this.content.Content = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title.Text;
            }
            set
            {
                this.title.Text = value;
            }
        }

        public string PrimaryButtonText
        {
            get
            {
                return this.primaryButton.Content as string;
            }
            set
            {
                this.primaryButton.Content = value;
                this.primaryButton.Visibility = Visibility.Visible;
            }
        }

        public string SecondaryButtonText
        {
            get
            {
                return this.secondaryButton.Content as string;
            }
            set
            {
                this.secondaryButton.Content = value;
                this.secondaryButton.Visibility = Visibility.Visible;
            }
        }

        public bool IsPrimaryButtonEnabled
        {
            get
            {
                return this.primaryButton.IsEnabled;
            }
            set
            {
                this.primaryButton.IsEnabled = value;
            }
        }

        public bool IsSecondaryButtonEnabled
        {
            get
            {
                return this.secondaryButton.IsEnabled;
            }
            set
            {
                this.secondaryButton.IsEnabled = value;
            }
        }

        public event EventHandler PrimaryButtonClick;

        public event EventHandler SecondaryButtonClick;

        public void ShowAsync()
        {
            this.ParentPopup.IsOpen = true;
        }

        private void OnPrimaryButtonClick()
        {
            this.PrimaryButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void OnSecondaryButtonClick()
        {
            this.SecondaryButtonClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
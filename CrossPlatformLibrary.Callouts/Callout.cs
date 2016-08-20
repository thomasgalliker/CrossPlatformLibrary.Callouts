namespace CrossPlatformLibrary.Callouts
{
    public class Callout : ICallout
    {
        public void Show(string caption, object content)
        {
            throw new NotImplementedInReferenceAssemblyException();
        }

        public void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            throw new NotImplementedInReferenceAssemblyException();
        }
    }
}
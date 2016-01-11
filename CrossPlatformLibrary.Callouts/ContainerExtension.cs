using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Callouts
{
    public class ContainerExtension : IContainerExtension
    {
        public void Initialize(ISimpleIoc container)
        {
            container.RegisterWithConvention<ICallout>();
        }
    }
}

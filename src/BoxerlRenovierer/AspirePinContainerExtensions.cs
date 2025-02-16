using Aspire.Hosting;
using Aspire.Hosting.Lifecycle;

namespace BoxerlRenovierer;

public static class AspirePinContainerExtensions
{
    public static void WithContainerVersionPinning(this IDistributedApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        builder.Services.AddLifecycleHook<PinContainerVersionsLifecycleHook>();
    }
}
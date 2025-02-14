using Aspire.Hosting;
using Aspire.Hosting.Lifecycle;

namespace Zyrrio.Aspire.PinContainers;

public static class AspirePinContainerExtensions
{
    public static void WithContainerVersionPinning(this IDistributedApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        builder.Services.AddLifecycleHook<PinContainerVersionsLifecycleHook>();
    }
}
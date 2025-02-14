using YamlDotNet.RepresentationModel;

namespace Zyrrio.Aspire.PinContainers;

internal record ComposeYml
{
    public required Dictionary<string, ComposeService> Services { get; init; }
}
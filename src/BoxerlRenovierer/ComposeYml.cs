using YamlDotNet.RepresentationModel;

namespace BoxerlRenovierer;

internal record ComposeYml
{
    public required Dictionary<string, ComposeService> Services { get; init; }
}
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Lifecycle;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Zyrrio.Aspire.PinContainers;

public class PinContainerVersionsLifecycleHook : IDistributedApplicationLifecycleHook
{
    public Task BeforeStartAsync(DistributedApplicationModel appModel,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(appModel);

        if (!File.Exists("compose.yml"))
            throw new InvalidOperationException(
                "Pin of ContainerVersions was requested, but no compose.yml file was found.");

        var pinnedContainerVersions = GetPinnedVersionsFromComposeYml();

        foreach (var containerResource in appModel.GetContainerResources())
        {
            var containerName = containerResource.Name;
            var containerImage = containerResource.Annotations.OfType<ContainerImageAnnotation>().FirstOrDefault();

            if (containerImage == null) continue;
            if (!pinnedContainerVersions.TryGetValue(containerName, out var pinnedContainerImage)) continue;

            containerImage.Image = pinnedContainerImage.Name;
            containerImage.Registry = pinnedContainerImage.Registry;
            if (pinnedContainerImage.Sha256 == null)
            {
                containerImage.Tag = pinnedContainerImage.Tag;
            }
            containerImage.SHA256 = pinnedContainerImage.Sha256;
        }
        
        return Task.CompletedTask;
    }

    private Dictionary<string, ContainerImage> GetPinnedVersionsFromComposeYml()
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();

        var composeYml = deserializer.Deserialize<ComposeYml>(
            new StreamReader(new FileStream("compose.yml", FileMode.Open, FileAccess.Read, FileShare.Read)));

        return composeYml.Services.ToDictionary(s => s.Key, s => ContainerImage.Parse(s.Value.Image));
    }
}
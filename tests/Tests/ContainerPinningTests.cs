using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class ContainerPinningTests
{
    [Fact]
    public async Task ContainerVersionsApplied()
    {
        var testingBuilder = await DistributedApplicationTestingBuilder.CreateAsync<Program>();
        using var distributedApplication = await testingBuilder.BuildAsync();
        await distributedApplication.StartAsync();
        
        var applicationModel = distributedApplication.Services.GetRequiredService<DistributedApplicationModel>();
        var containerImage = applicationModel.Resources.OfType<ContainerResource>()
            .SelectMany(cr => cr.Annotations.OfType<ContainerImageAnnotation>())
            .FirstOrDefault();

        Assert.Equal("mailhog", containerImage.Image);
        Assert.Equal("docker.io/mailhog", containerImage.Registry);
        Assert.Equal("8d76a3d4ffa32a3661311944007a415332c4bb855657f4f6c57996405c009bea", containerImage.SHA256);
        Assert.Null(containerImage.Tag);
    }
}
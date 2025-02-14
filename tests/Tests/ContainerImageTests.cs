﻿using Zyrrio.Aspire.PinContainers;

namespace Tests;

public class ContainerImageTests
{
    public static IEnumerable<object[]> ContainerImages =>
    [
        ["redis", new ContainerImage{Name = "redis"}],
        ["docker.io/mailhog/mailhog",  new ContainerImage{Registry = "docker.io/mailhog", Name = "mailhog"}],
        ["docker.io/mailhog/mailhog:v1.0.0",  new ContainerImage{Registry = "docker.io/mailhog", Name = "mailhog", Tag = "v1.0.0"}],
        ["docker.io/mailhog/mailhog:v1.0.0@sha256:8d76a3d4ffa32a3661311944007a415332c4bb855657f4f6c57996405c009bea",  new ContainerImage{Registry = "docker.io/mailhog", Name = "mailhog", Tag = "v1.0.0", Sha256 = "8d76a3d4ffa32a3661311944007a415332c4bb855657f4f6c57996405c009bea"}],
        [":", new ContainerImage{Name = ""}],
        ["redis@sha256:123", new ContainerImage{ Name = "redis", Sha256 = "123"}]
    ];
    
    [Theory, MemberData(nameof(ContainerImages))]
    public void CanParseContainerImageNames(string imageName, ContainerImage expectedParseResult)
    {
        var containerImage = ContainerImage.Parse(imageName);
        Assert.Equal(expectedParseResult, containerImage);
    }
}
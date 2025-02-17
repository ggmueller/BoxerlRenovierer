# BoxerlRenovierer

This package enables you to redefine Dotnet Aspire Container references with
`compose.yml` files.

The idea here is to minimally compatible with docker-compose.yml files so
that tools that already support these files can be used to
pin and update the referenced containers used in the Aspire configuration.

## Usage

### Add reference

Add nuget package to your Aspire project:

```bash
dotnet add package BoxerlRenovierer
```

Ensure that you have set `IsAspireProjectResource` to false on the reference, like:

```xml
<PackageReference Include="BoxerlRenovierer" Version="1.0.0-pre.1" IsAspireProjectResource="false"/>
```

See https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/dotnet-aspire-sdk#project-references why this is necessary.

### Add compose.yml

Add a minimalistic compose.yml to your project, that reference your Aspire containers by name.

For this Aspire container definition

```csharp
builder.AddContainer("smtp", "docker.io/mailhog/mailhog");
```

It can be

```yml
version: '3.3'
services:
  smtp:
    image: docker.io/mailhog/mailhog:1.0.1@sha256:8d76a3d4ffa32a3661311944007a415332c4bb855657f4f6c57996405c009bea
```

See how the image references the concrete version and sha256 tag of the container.
This reference can be updated by tools, eg. by Renovate

### Enable BoxerlRenovierer

Add one line of configuration to your Aspire host.

```csharp
builder.WithContainerVersionPinning();
```

This enables all containers that are referenced by the container.yml to be managed by BoxerlRenovierer

# References

Aspire Content on my Blog (in German): https://zyrr.io/tags/aspire
Nuget-Package: https://www.nuget.org/packages/BoxerlRenovierer
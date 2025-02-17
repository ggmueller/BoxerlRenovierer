namespace BoxerlRenovierer;

public record ContainerImage
{
    public string? Registry { get; init; }
    public required string Name { get; init; }
    public string? Tag { get; init; }
    public string? Sha256 { get; init; }

    public static ContainerImage Parse(string image)
    {
        var imageValue = image.AsSpan();
        var nameRange = imageValue[..];
        var sha256Range = default(ReadOnlySpan<char>);
        var tagRange = default(ReadOnlySpan<char>);
        var registryRange = default(ReadOnlySpan<char>);
        var tagSeparatorPosition = imageValue.IndexOf(':');
        var sha256SeparatorPosition = imageValue.IndexOf("@sha256:", StringComparison.Ordinal);
        var registrySeparatorPosition = imageValue.LastIndexOf('/');

        if (sha256SeparatorPosition > -1)
        {
            sha256Range = nameRange[(sha256SeparatorPosition + 8)..];
            nameRange = nameRange[..sha256SeparatorPosition];
        }

        if (tagSeparatorPosition > -1 &&
            (sha256SeparatorPosition == -1 || tagSeparatorPosition < sha256SeparatorPosition))
        {
            tagRange = nameRange[(tagSeparatorPosition + 1)..];
            nameRange = nameRange[..tagSeparatorPosition];
        }

        if (registrySeparatorPosition > -1)
        {
            registryRange = nameRange[..registrySeparatorPosition];
            nameRange = nameRange[(registrySeparatorPosition + 1)..];
        }

        return new ContainerImage
        {
            Name = new string(nameRange),
            Registry = registryRange.IsEmpty ? null : new string(registryRange),
            Sha256 = sha256Range.IsEmpty ? null : new string(sha256Range),
            Tag = tagRange.IsEmpty ? null : new string(tagRange)
        };
    }
}
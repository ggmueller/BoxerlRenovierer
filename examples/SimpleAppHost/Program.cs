using BoxerlRenovierer;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddContainer("smtp", "docker.io/mailhog/mailhog");

builder.WithContainerVersionPinning();

builder.Build().Run();

public partial class Program
{
}
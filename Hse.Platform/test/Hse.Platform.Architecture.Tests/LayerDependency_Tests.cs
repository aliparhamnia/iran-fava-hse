using System.Linq;
using System.Reflection;
using Hse.Platform.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Hse.Platform.Architecture;

public class LayerDependency_Tests
{
    [Fact]
    public void Domain_Should_Not_Reference_EfCore_Or_Blazor()
    {
        var names = ReferencedAssemblyNames(typeof(PlatformDomainModule).Assembly);

        names.ShouldNotContain("Microsoft.EntityFrameworkCore");
        names.ShouldNotContain("Microsoft.AspNetCore.Components");
        names.ShouldNotContain("MudBlazor");
    }

    [Fact]
    public void Application_Should_Not_Reference_Blazor()
    {
        var names = ReferencedAssemblyNames(typeof(PlatformApplicationModule).Assembly);

        names.ShouldNotContain("Microsoft.AspNetCore.Components");
        names.ShouldNotContain("MudBlazor");
        names.ShouldNotContain("Hse.Platform.Blazor");
        names.ShouldNotContain("Hse.Platform.Blazor.Client");
    }

    [Fact]
    public void ApplicationContracts_Should_Not_Reference_EfCore_Or_Blazor()
    {
        var names = ReferencedAssemblyNames(typeof(PlatformApplicationContractsModule).Assembly);

        names.ShouldNotContain("Microsoft.EntityFrameworkCore");
        names.ShouldNotContain("Hse.Platform.EntityFrameworkCore");
        names.ShouldNotContain("Microsoft.AspNetCore.Components");
    }

    [Fact]
    public void EntityFrameworkCore_May_Reference_Domain_But_Not_Blazor()
    {
        var names = ReferencedAssemblyNames(typeof(PlatformEntityFrameworkCoreModule).Assembly);

        names.ShouldContain("Hse.Platform.Domain");
        names.ShouldNotContain("Hse.Platform.Blazor");
        names.ShouldNotContain("MudBlazor");
    }

    private static string[] ReferencedAssemblyNames(Assembly assembly)
    {
        return assembly.GetReferencedAssemblies()
            .Select(x => x.Name)
            .Where(x => x != null)
            .Cast<string>()
            .ToArray();
    }
}

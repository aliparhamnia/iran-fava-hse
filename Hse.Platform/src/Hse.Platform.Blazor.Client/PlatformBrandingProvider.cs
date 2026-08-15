using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Hse.Platform.Localization;

namespace Hse.Platform.Blazor.Client;

[Dependency(ReplaceServices = true)]
public class PlatformBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PlatformResource> _localizer;

    public PlatformBrandingProvider(IStringLocalizer<PlatformResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}

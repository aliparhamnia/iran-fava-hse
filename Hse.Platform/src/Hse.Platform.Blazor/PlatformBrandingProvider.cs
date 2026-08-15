using Microsoft.Extensions.Localization;
using Hse.Platform.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hse.Platform.Blazor;

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

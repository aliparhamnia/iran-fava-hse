using Hse.Platform.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Hse.Platform.Blazor.Client;

public abstract class PlatformComponentBase : AbpComponentBase
{
    protected PlatformComponentBase()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}

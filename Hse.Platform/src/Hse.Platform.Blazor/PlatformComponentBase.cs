using Hse.Platform.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Hse.Platform.Blazor;

public abstract class PlatformComponentBase : AbpComponentBase
{
    protected PlatformComponentBase()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}

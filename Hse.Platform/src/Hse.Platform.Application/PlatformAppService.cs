using Hse.Platform.Localization;
using Volo.Abp.Application.Services;

namespace Hse.Platform;

/* Inherit your application services from this class.
 */
public abstract class PlatformAppService : ApplicationService
{
    protected PlatformAppService()
    {
        LocalizationResource = typeof(PlatformResource);
    }
}

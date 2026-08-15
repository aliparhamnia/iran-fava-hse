using Volo.Abp.Modularity;

namespace Hse.Platform;

public abstract class PlatformApplicationTestBase<TStartupModule> : PlatformTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}

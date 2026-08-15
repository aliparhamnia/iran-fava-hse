using Volo.Abp.Modularity;

namespace Hse.Platform;

[DependsOn(
    typeof(PlatformDomainModule),
    typeof(PlatformTestBaseModule)
)]
public class PlatformDomainTestModule : AbpModule
{

}

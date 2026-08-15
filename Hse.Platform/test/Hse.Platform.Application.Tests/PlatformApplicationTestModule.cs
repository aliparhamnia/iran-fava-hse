using Volo.Abp.Modularity;

namespace Hse.Platform;

[DependsOn(
    typeof(PlatformApplicationModule),
    typeof(PlatformDomainTestModule)
)]
public class PlatformApplicationTestModule : AbpModule
{

}

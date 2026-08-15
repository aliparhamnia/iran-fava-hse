using Hse.Platform.Samples;
using Xunit;

namespace Hse.Platform.EntityFrameworkCore.Domains;

[Collection(PlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<PlatformEntityFrameworkCoreTestModule>
{

}

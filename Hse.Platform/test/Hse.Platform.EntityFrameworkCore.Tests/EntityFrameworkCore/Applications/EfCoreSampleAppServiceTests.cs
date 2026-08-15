using Hse.Platform.Samples;
using Xunit;

namespace Hse.Platform.EntityFrameworkCore.Applications;

[Collection(PlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<PlatformEntityFrameworkCoreTestModule>
{

}

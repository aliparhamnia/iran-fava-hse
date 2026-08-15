using Xunit;

namespace Hse.Platform.EntityFrameworkCore;

[CollectionDefinition(PlatformTestConsts.CollectionDefinitionName)]
public class PlatformEntityFrameworkCoreCollection : ICollectionFixture<PlatformEntityFrameworkCoreFixture>
{

}

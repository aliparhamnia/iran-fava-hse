using System.Threading.Tasks;

namespace Hse.Platform.Data;

public interface IPlatformDbSchemaMigrator
{
    Task MigrateAsync();
}

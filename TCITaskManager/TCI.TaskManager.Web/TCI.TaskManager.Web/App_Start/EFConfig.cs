using System.Data.Entity;
using TCI.TaskManager.Web.Data;

namespace TCI.TaskManager.Web
{
    public class EFConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }
    }
}
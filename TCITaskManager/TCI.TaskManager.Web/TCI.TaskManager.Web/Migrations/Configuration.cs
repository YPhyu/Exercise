namespace TCI.TaskManager.Web.Migrations
{
    using System.Data.Entity.Migrations;
    using TCI.TaskManager.Web.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}

namespace TestPracticeMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TestPracticeMVC.Models.InventoryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestPracticeMVC.Models.InventoryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(x => x.Name,
                new Models.Users() { Name = "admin", Password = "admin123", Role = "admin" },
                new Models.Users() { Name = "salesman1", Password = "salesman123", Role = "salesman" },
                new Models.Users() { Name = "salesman2", Password = "salesman123", Role = "salesman" }
            );
        }
    }
}

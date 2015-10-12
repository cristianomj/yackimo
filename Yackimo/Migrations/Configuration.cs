namespace Yackimo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Yackimo.Models;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<Yackimo.Models.YackimoDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Yackimo.Models.YackimoDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            for (int i = 0; i < 1000; i++)
            {
                context.Products.AddOrUpdate(p => p.Name,
                    new Product
                    {
                        Name = i.ToString(),
                        Category = "Category",
                        Description = "Description",
                        DataCreated = DateTime.Now,
                        UserId = 5
                    });
            }            
        }
    }
}

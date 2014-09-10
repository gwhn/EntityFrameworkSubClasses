namespace EntityFrameworkSubClasses.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkSubClasses.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameworkSubClasses.MyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.As.AddOrUpdate(
                x => x.Name,
                new A {Name = "A1", B = new B {Name = "B1"}},
                new Aa
                {
                    Name = "Aa1",
                    SubName = "Aa1.1",
                    B = new Bb {Name = "Bb1", SubName = "Bb1.1", D = new Dd {Name = "Dd1", SubName = "Dd1.1"}},
                    C = new Cc {Name = "Cc1", SubName = "Cc1.1"}
                }
                );

        }
    }
}

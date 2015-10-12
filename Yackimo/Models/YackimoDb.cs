using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Yackimo.Models
{
    public class YackimoDb : DbContext
    {
        public YackimoDb()
            : base("name=DefaultConnection")
        { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TradeRoutes> TradeRoutes { get; set; }
        public DbSet<RouteProducts> RouteProducts { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
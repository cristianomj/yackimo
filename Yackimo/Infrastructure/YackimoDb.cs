 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Yackimo.Entities;

namespace Yackimo.Infrastructure
{
    public class YackimoDb : DbContext
    {
        public YackimoDb()
            : base("name=DefaultConnection")
        { }
        
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bag> Bags { get; set; }

        public DbSet<ChatInbox> ChatInboxes { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Trade> Trades { get; set; }

        #region Model Definitions
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOptional(c => c.Parent)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasOptional(u => u.ChatInbox)
                .WithMany()
                .HasForeignKey(u => u.ChatInboxId);

            modelBuilder.Entity<UserProfile>()
                .HasOptional(u => u.Bag)
                .WithMany()
                .HasForeignKey(u => u.BagId);

            modelBuilder.Entity<Trade>()
                .HasMany(t => t.Products)
                .WithMany(t => t.Trades);
        }
        #endregion
    }
}
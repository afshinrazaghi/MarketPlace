using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Context
{
    public class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options)
            : base(options)
        {
        }
        #region account
        public DbSet<User> Users { get; set; }
        #endregion

        #region contacts
        public DbSet<ContactUs> ContactUses { get; set; }
        #endregion

        #region siteSetting
        public DbSet<SiteSetting> SiteSettings { get; set; }
        #endregion

        #region on model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Cascade;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}

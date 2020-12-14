using DataLayer.Migrations;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MyCmsContex : DbContext, IDisposable
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsComment> NewsComments { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NewsComment>().HasRequired(n => n.News).WithMany(c => c.NewsComment).HasForeignKey(nc => nc.NewsID).WillCascadeOnDelete(true);
        }
    }
}

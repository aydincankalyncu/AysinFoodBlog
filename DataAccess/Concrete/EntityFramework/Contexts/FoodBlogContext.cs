using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class FoodBlogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AysinFoodBlog;Trusted_Connection=true;");
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<FoodComment> FoodComments { get; set; }
        public DbSet<Info> Infos { get; set; }
    }
}

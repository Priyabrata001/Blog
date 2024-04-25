using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blogs.Models;

namespace Blogs.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext (DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<Blogs.Models.Blog> Blogs { get; set; } = default!;
        public DbSet<Blogs.Models.User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(t => t.UserType)
                .HasDefaultValue("User");

        }
    }
}

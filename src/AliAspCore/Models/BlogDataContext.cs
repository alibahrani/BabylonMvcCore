using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AliAspCore.Models
{
    public class BlogDataContext :  DbContext
    {
        public DbSet<Post> posts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {
            Database.EnsureCreated();  

        }
    }

    
}

using AliAspCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AliAspCore.Models
{
    public class Special
    {
        public int id { get; set; }

        public string Key { get; internal set; }
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public int Price { get; internal  set; }


    }
    public class SpeciaDataContext : DbContext
    {
        public DbSet<Special> Specials { get; set; }

        public SpeciaDataContext(DbContextOptions<SpeciaDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public IEnumerable<Special> GetMonthlySpecials()
        {
            return Specials.ToArray() ;

        }
    }
}

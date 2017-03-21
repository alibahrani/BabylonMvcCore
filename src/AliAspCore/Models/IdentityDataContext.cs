using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AliAspCore.Models
{
    public class IdentityDataContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrService.Models
{
    public class HrDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public HrDbContext(DbContextOptions<HrDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

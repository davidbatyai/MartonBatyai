using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MindenMancs.Models;

namespace MindenMancs.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MindenMancs.Models.Animals> Animals { get; set; }
        public DbSet<MindenMancs.Models.Shelters> Shelters { get; set; }
    }
}

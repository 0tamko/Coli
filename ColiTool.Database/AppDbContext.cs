using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ColiTool.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //define DbSets (tables) here
        public DbSet<Entities.TestEntity> TestEntities { get; set; }
        public DbSet<Entities.CanMessage> CanMessages { get; set; }
        public DbSet<Entities.Configuration> Configurations { get; set; }
    }
}

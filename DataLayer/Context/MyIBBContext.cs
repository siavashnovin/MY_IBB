using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MyIBBContext:DbContext
    {
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageComment> PageComments { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }

    }
}

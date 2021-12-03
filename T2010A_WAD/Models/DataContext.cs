using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using T2010A_WAD.Areas.Admin.Models;
namespace T2010A_WAD.Models
{
    public class DataContext : DbContext
    {
        public DataContext(): base("T2010A") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
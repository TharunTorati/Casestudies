using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TruYumWebApiCaseStudy.Models
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options):base(options)
        {

        }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}

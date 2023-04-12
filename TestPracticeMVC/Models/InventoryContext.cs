using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestPracticeMVC.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() : base("ShoppingCart")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
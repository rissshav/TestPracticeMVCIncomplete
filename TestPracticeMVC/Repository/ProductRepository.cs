using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TestPracticeMVC.Models;

namespace TestPracticeMVC.Repository
{
    public class ProductRepository<T> : IProductRepository<T> where T : class
    {
        public InventoryContext _context = null;
        public DbSet<T> table = null;
        public ProductRepository()
        {
            this._context = new InventoryContext();
            table = _context.Set<T>();
        }
        public ProductRepository(InventoryContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public void Add(T Products)
        {
            table.Add(Products);
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object ProductId)
        {
            return table.Find(ProductId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T Products)
        {
            table.Attach(Products);
            _context.Entry(Products).State = EntityState.Modified;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPracticeMVC.Models;
using TestPracticeMVC.Repository;

namespace TestPracticeMVC.Controllers
{
    [Authorize(Roles = "admin,salesman")]
    public class SalesmanController : Controller
    {
        InventoryContext db = new InventoryContext();
        private IProductRepository<Products> _productRepository = null;
        public SalesmanController()
        {
            this._productRepository = new ProductRepository<Products>();
        }
        public SalesmanController(IProductRepository<Products> productRepository)
        {
            this._productRepository = productRepository;
        }
        public ActionResult Index()
        {
            var modal = _productRepository.GetAll();
            return View(modal);
        }
        public ActionResult AddToCart(Guid id)
        {
            Products data = _productRepository.GetById(id);
            db.Carts.Add(new Cart { ProductId = data.ProductId, ProductName = data.Name, ProductPrice = data.Price , ProductDescription = data.Description, Image = data.Image  });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
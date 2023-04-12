using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestPracticeMVC.Models;
using TestPracticeMVC.Repository;

namespace TestPracticeMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        InventoryContext db = new InventoryContext();
        private IProductRepository<Products> _productRepository = null;
        public AdminController()
        {
            this._productRepository = new ProductRepository<Products>();
        }
        public AdminController(IProductRepository<Products> productRepository)
        {
            this._productRepository = productRepository;
        }
        public ActionResult Index(string search, int? page)
        {
            /*var model = _productRepository.GetAll();
            return View(model);*/
            return View(db.Products.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1,5));
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file, Products model)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);


            string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
            model.Image = "~/Content/Images/" + _filename;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                    _productRepository.Add(model);
                    _productRepository.Save();
                    file.SaveAs(path);
                    ViewBag.msg = "Student Added";
                    ModelState.Clear();
                    return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Invalid File Type";
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditProduct(Guid? id)
        {
            Products model = _productRepository.GetById(id);
            Session["ImgPath"] = model.Image;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(HttpPostedFileBase file, Products model)
        {

                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);


                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), _filename);
                    model.Image = "~/Content/Images/" + _filename;

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                      if(file.ContentLength < 10000)
                        {
                            string OldImgPath = Request.MapPath(Session["ImgPath"].ToString());
                            _productRepository.Update(model);
                            _productRepository.Save();
                            file.SaveAs(path);
                            if (System.IO.File.Exists(OldImgPath))
                            {
                                System.IO.File.Delete(OldImgPath);
                            }
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.msg = "File should be less than 1 mb";
                        }
                    }
                    else
                    {
                        ViewBag.msg = "Invalid File Type";
                    }
                }

                model.Image = Session["ImgPath"].ToString();
                db.Entry(model).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                /*_productRepository.Update(model);
                _productRepository.Save();*/  

            return View();
        }
        [HttpGet]
        public ActionResult DeleteProduct(Guid id)
        {
            Products model = _productRepository.GetById(id);
            return View(model);
        }
        [HttpGet, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
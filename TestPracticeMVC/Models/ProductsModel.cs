using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestPracticeMVC.Models
{
    [NotMapped]
    public class ProductsModel
    {
        public List<Products> Products { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
using System.Collections.Generic;
using Web2023Project.Model;
using Web2023Project.Models;

namespace Web2023Project.Website.Dao
{
    public class HomeViewModel
    {
        public List<Products> listProduct_new { get; set; }
        public List<Products> listProduct_hot { get; set; }
        public List<Products> listProduct_sale { get; set; }
    }
}
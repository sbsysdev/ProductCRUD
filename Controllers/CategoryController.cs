using ProductCRUD.Data;
using ProductCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace ProductCRUD.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Categories";

            Connection connection = new Connection();
            DataSet ds = connection.Query("sp_read_all_category");
            List<Category> categories = null;

            if (ds == null) return View(categories);

            categories = ds.Tables[0].AsEnumerable().Select(row => new Category {
                Id = row.Field<int>(0),
                Name = row.Field<string>(1),
                Products = row.Field<int>(2),
                Created = row.Field<DateTime>(3),
            }).ToList();

            return View(categories);
        }

        public ActionResult Details(int id)
        {
            ViewBag.Title = "Category Detail";

            Category category = null;
            List<Product> products = null;

            Param categoryIdParam = new Param
            {
                Key = "category_id",
                Value = id
            };

            Connection connection = new Connection();

            // Category detail
            DataSet categoryDS = connection.Query("sp_read_category_by_id", new List<Param>{ categoryIdParam });

            if (categoryDS == null || categoryDS.Tables[0].AsEnumerable().Count() != 1) return View((category, products));

            category = categoryDS.Tables[0].AsEnumerable().Select(row => new Category
            {
                Id = row.Field<int>(0),
                Name = row.Field<string>(1),
                Products = row.Field<int>(2),
                Created = row.Field<DateTime>(3),
            }).ElementAt(0);

            ViewBag.Title = $"{category.Name} details";

            // Category products
            DataSet productsDS = connection.Query("sp_read_product_category_by_id", new List<Param> { categoryIdParam });

            if (productsDS == null || productsDS.Tables[0].AsEnumerable().Count() == 0) return View((category, products));

            products = (from row in productsDS.Tables[0].AsEnumerable() select new Product
            {
                Id = row.Field<int>(0),
                Category = row.Field<int>(1),
                Name = row.Field<string>(2),
                Price = row.Field<double>(3),
                Stock = row.Field<int>(4),
                Created = row.Field<DateTime>(5),
            }).ToList();

            return View((category, products));
        }

        public ActionResult Edit(int id) {
            ViewBag.Title = "Category Edit";

            return View(id);
        }
    }
}

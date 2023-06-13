using ProductCRUD.Data;
using ProductCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Tracing;
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

            Debug.WriteLine(ds.Tables[0].Rows[0][3]);

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

            return View(id);
        }

        public ActionResult Edit(int id) {
            ViewBag.Title = "Category Edit";

            return View(id);
        }
    }
}

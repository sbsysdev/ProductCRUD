using System;
using System.Collections.Generic;
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

            int[] categories = { 1, 2, 3 };

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

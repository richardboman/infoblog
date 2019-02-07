using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult _CategoryPartialView()
        {
            var ctx = new ApplicationDbContext();
            var model = ctx.Category.ToList();
            return PartialView("_CategoryPartialView", model);
        }
    }
}
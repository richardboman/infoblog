using Infoblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infoblog.Controllers
{
    public class ScienceController : Controller
    {
        // GET: Science
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowSciencePosts()
        {
            var ctx = new ApplicationDbContext();
            var viewModel = new ScienceViewModel();
            viewModel.SciencePosts = ctx.SciencePost.ToList();

            return View(viewModel);
        }

        public void AddSciencePost(ScienceModel model)
        {
            var sciencePost = new ScienceModel()
            {
                Title = model.Title,
                Content = model.Content,
                Author = model.Author
            };

            var ctx = new ApplicationDbContext();
            // "SciencePost" nedan är namnet på tabellen.
            ctx.SciencePost.Add(sciencePost);
            ctx.SaveChanges();
        }
    }
}
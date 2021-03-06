﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infoblog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infoblog.Controllers
{
    [Authorize]
    public class PostController : Controller
    { 
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowPost()
        {
            var ctx = new ApplicationDbContext();
            var pvm = new FormalPostViewModel();
            var cat = new CategoryModel();
            pvm.Posts = ctx.Post.OrderByDescending(p => p.Date).ToList();
                return View(pvm);
        }


        public ActionResult ShowFilteredPost()
        {
            var ctx = new ApplicationDbContext();
            var pvm = new FormalPostViewModel();
            var cat = new CategoryModel();
            var value = int.Parse(Request["CategoryFilter"]);


            pvm.Posts = ctx.Post.Where(p=>p.CategoryId==value).OrderByDescending(p => p.Date).ToList();
            return View("ShowPost", pvm);
        }


        public ActionResult EditPost(PostViewModel post)
        {
            return View(post);
        }

        [HttpPost]
        [ActionName("EditPost")]
        public virtual ActionResult SaveEdit(PostViewModel post)
        {
            var ctx = new ApplicationDbContext();
            var postToEdit = ctx.Post.SingleOrDefault(p => p.Id == post.Id);
            postToEdit.Content = post.Content;
            postToEdit.Title = post.Title;
            postToEdit.File = post.File;
            ctx.SaveChanges();
            return RedirectToAction("ShowPost", "Post");
        }

        public ActionResult EditInformalPost(PostViewModel post)
        {
            return View(post);
        }

        [HttpPost]
        [ActionName("EditInformalPost")]
        public virtual ActionResult SaveInformalEdit(PostViewModel post)
        {
            var ctx = new ApplicationDbContext();
            var postToEdit = ctx.InformalPost.SingleOrDefault(p => p.Id == post.Id);
            postToEdit.Content = post.Content;
            postToEdit.Title = post.Title;
            postToEdit.File = post.File;
            ctx.SaveChanges();
            return RedirectToAction("ShowInformalPost", "Post");
        }

        public ActionResult RemovePost(PostViewModel postmodel)
        {
            var ctx = new ApplicationDbContext();
            var post = ctx.Post.SingleOrDefault(p => p.Id == postmodel.Id);
            if (post != null)
            {
                var postToRemove = ctx.Post.Find(postmodel.Id);
                ctx.Post.Remove(postToRemove);
                ctx.SaveChanges();
            }

            return RedirectToAction("ShowPost", "Post");
        }

        public ActionResult RemoveInformalPost(PostViewModel postmodel)
        {
            var ctx = new ApplicationDbContext();
            var post = ctx.InformalPost.SingleOrDefault(p => p.Id == postmodel.Id);
            if (post != null)
            {
                var postToRemove = ctx.InformalPost.Find(postmodel.Id);
                ctx.InformalPost.Remove(postToRemove);
                ctx.SaveChanges();
            }

            return RedirectToAction("ShowInformalPost", "Post");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WritePost(FormalPostViewModel postmodel)
        { 
            var ctx = new ApplicationDbContext();
            var cat = new CategoryModel();
            var value = Request["Category"];
            var userId = User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(userId);
            var post = new FormalPostModel

            {
                Title = postmodel.Title,
                Content = postmodel.Content,
                Author = User.Identity.GetUserName(),
                Date = DateTime.Now,
                CategoryId = int.Parse(value)
            };
            var notification = new SendEmailController();

            if (ModelState.IsValid)
            {
                if (postmodel.File != null && postmodel.File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(postmodel.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                    post.FilePath = "~/UploadedFiles/" + fileName;
                    postmodel.File.SaveAs(path);
                }
                ctx.Post.Add(post);
                ctx.SaveChanges();
                notification.EmailWhenPost(post);
            }
            else
            {
                postmodel.Posts = ctx.Post.OrderByDescending(p => p.Date).ToList();
                return View("ShowPost", postmodel);
            }

            return RedirectToAction("ShowPost");
        }

        public ActionResult ShowInformalPost()
        {
            var ctx = new ApplicationDbContext();
            var pvm = new InformalPostViewModel();
            pvm.InformalPosts = ctx.InformalPost.OrderByDescending(p => p.Date).ToList();

            return View(pvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WriteInformalPost(InformalPostViewModel postmodel)
        {
            var ctx = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var value = Request["Category"];
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindById(userId);
            var informalpost = new InformalPostModel
            {
                Title = postmodel.Title,
                Content = postmodel.Content,
                Author = User.Identity.GetUserName(),
                Date = DateTime.Now,
                CategoryId = int.Parse(value)
            };

            if (ModelState.IsValid)
            {
                if (postmodel.File != null && postmodel.File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(postmodel.File.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles/"), fileName);
                    informalpost.FilePath = "~/UploadedFiles/" + fileName;
                    postmodel.File.SaveAs(path);
                }
                ctx.InformalPost.Add(informalpost);
                ctx.SaveChanges();
            }
            else
            {
                postmodel.InformalPosts = ctx.InformalPost.OrderByDescending(p => p.Date).ToList();
                return View("ShowInformalPost", postmodel);
            }

            return RedirectToAction("ShowInformalPost");
        }
    }
}

   
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AliAspCore.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AliAspCore.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db; 
        public BlogController(BlogDataContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        [Route("")]
        public IActionResult Index(int page = 0)
        {
            var pageSize = 2;
            var totalPosts = _db.posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage  = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;


            var posts = _db.posts
                            .OrderByDescending(x => x.Posted)
                            .Skip(pageSize * page)
                            .Take(pageSize)
                            .ToArray();

            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                 return PartialView(posts);

            return View(posts);

        }

        [Route("blog/{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _db.posts.FirstOrDefault(x => x.Key == key);
            return View(post);
        }

        [Authorize]
        [HttpGet, Route("create")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize]
        [HttpPost, Route("create")]
        public IActionResult Create(Post post)
        {

            if (!ModelState.IsValid)
            {
                return View(); 
            }
            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            _db.posts.Add(post);
            _db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
            {
                year = post.Posted.Year,
                month = post.Posted.Month,
                key = post.Key 

            });
        }


    }
}

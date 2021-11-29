using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2010A_WAD.Models;

namespace T2010A_WAD.Controllers
{
    public class CategoriesController : Controller
    {
        private DataContext context = new DataContext();
        // GET: Categories
        public ActionResult Index()
        {
            ViewData["Page-Title"] = "Categories Page";
            ViewBag.PageTitle = "Demo Title for Categories Page";
            // var category = new Category() {Id=1,CategoryNam e="Fashion" };
            // ViewBag.Category = category;
            var list = context.Categories.ToList();
       
            return View(list); // passing data by model
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // khi dữ liệu gửi lên thỏa mãn yêu cầu (yêu cầu theo Model) -> lưu vào DB
                context.Categories.Add(category);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(category);// trở lại giao diện kèm dữ liệu vừa nhập
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            // Dựa vào id để tìm category
            Category category = context.Categories.Find(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
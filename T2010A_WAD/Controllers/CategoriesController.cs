using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2010A_WAD.Models;
using System.IO;

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
        public ActionResult Create([Bind(Include = "Id,CategoryName,CategoryImage")]Category category,HttpPostedFileBase CategoryImage)
        {
            if (ModelState.IsValid)
            {
                string catImg = "~/Uploads/default.jpg"; // gia tri mac dinh khi ko chon anh 
                try
                {
                    if(CategoryImage != null)
                    {
                        // up image len va set gia tri lai cho catImg
                        string fileName = Path.GetFileName(CategoryImage.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        CategoryImage.SaveAs(path);
                        catImg = "~/Uploads/" + fileName;
                    }
                    
                }catch(Exception e) { 
                }finally{
                    category.CategoryImage = catImg;
                }
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
        public ActionResult Edit([Bind(Include = "Id,CategoryName,CategoryImage")] Category category,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                
               // string catImg = category.CategoryImage; // gia tri mac dinh khi ko chon anh 
                try
                {
                    if (Image != null)
                    {
                        // up image len va set gia tri lai cho catImg
                        string fileName = Path.GetFileName(Image.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                        Image.SaveAs(path);
                        category.CategoryImage = "~/Uploads/" + fileName;
                    }
                }
                catch (Exception e)
                {
                }
                
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
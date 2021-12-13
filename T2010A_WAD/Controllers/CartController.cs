using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2010A_WAD.Models;
namespace T2010A_WAD.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int? id,int? qty)
        {
            var product = db.Products.Find(id);
            if(product != null)
            {
                CartItem item = new CartItem() {Product=product,Qty=(int)qty };// gia sử thêm 1 sp với qty là 1
                Cart cart = Session["Cart"] as Cart;
                if(cart == null) // khi ma chưa có sp nào trong gio hàng
                {
                    cart = new Cart() { GrandTotal = 0, Items = new List<CartItem>() };
                }
                cart.AddToCart(item);
                Session["Cart"] = cart; // thêm lại vào session
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}
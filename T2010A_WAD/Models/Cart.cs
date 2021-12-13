using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2010A_WAD.Models
{
    public class Cart
    {
        public int GrandTotal { get; set; }
        public List<CartItem> Items { get; set; }

        public void CalculateGrandTotal()
        {
            int grand = 0;
            foreach(var i in Items)
            {
                grand += i.Qty * i.Product.Price;
            }
            GrandTotal = grand;
        }

        public void AddToCart(CartItem item)
        {
            // kiểm tra xem sp đã có trong giỏ hàng chưa, nếu có rồi chỉ thêm số lượng
            // nếu chưa có mới thêm nguyên item vào giỏ hàng
            bool flag = false;
            foreach(var i in Items)
            {
                if(item.Product.Id == i.Product.Id)
                {
                    flag = true;
                    i.Qty += item.Qty;
                }
            }
            if (!flag)
            {
                Items.Add(item);
            }
            CalculateGrandTotal();
        }

    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Qty { get; set; }
    }
}
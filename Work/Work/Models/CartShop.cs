using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Work.Models
{
    public class CartShop
    {
        public int orderID { get; set; }
        public int userID { get; set; }
        public int deliveryID { get; set; }
        public int paymentID { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public string shipAddress { get; set; }

        public SortedList<string, bill> orderedProduct { get; set; }

        public int cartItem()
        {
            return (int)orderedProduct.Values.Sum(s => s.quantity);
        }

        public CartShop()
        {
            this.orderID = 0;
            this.userID = 0;
            this.deliveryID = 0;
            this.paymentID = 0;
            this.shipAddress = "";
            this.orderDate = DateTime.Now;
            this.deliveryDate = DateTime.Now.AddDays(3);
            this.orderedProduct = new SortedList<string, bill>();
        }

        public bool IsEmpty()
        {
            return orderedProduct.Keys.Count == 0;
        }

        public void addItem(string productID)
        {
            if (orderedProduct.Keys.Contains(productID))
            {
                bill b = orderedProduct.Values[orderedProduct.IndexOfKey(productID)];
                b.quantity++;
            }
            else
            {
                bill b = new bill();
                b.productID = productID;
                b.quantity = 1;
                product p = Common.GetProductById(productID);
                b.price = p.price;
                b.discount = p.sale;
                orderedProduct.Add(productID, b);
            }
        }

        public void deleteItem(string productID)
        {
            if (orderedProduct.Keys.Contains(productID))
            {
                orderedProduct.Remove(productID);
            }
        }

        public void decrease(string productID)
        {
            if (orderedProduct.Keys.Contains(productID))
            {
                bill b = orderedProduct.Values[orderedProduct.IndexOfKey((string)productID)];
                if (b.quantity > 1)
                {
                    b.quantity--;
                }
                else
                {
                    deleteItem(productID);
                }
            }
        }

        public long moneyOfOneProduct(bill b)
        {
            return (long)((b.quantity * b.price) - (b.quantity * b.price * b.discount));
        }

        public long totalOfCartShop()
        {
            long total = 0;
            foreach (bill b in orderedProduct.Values)
            {
                total += moneyOfOneProduct(b);
            }
            return total;
        }
    }
    }
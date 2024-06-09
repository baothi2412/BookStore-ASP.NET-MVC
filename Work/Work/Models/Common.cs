using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Work.Models
{
    public class Common
    {
        static DbContext cn = new DbContext("name=BookStore1Entities2");
        public static product GetProductById(String productID)
        {
            return new DbContext("name=BookStore1Entities2").Set<product>().Find(productID);
        }

        public static List<product> GetProducts()
        {
            return new DbContext("name=BookStore1Entities2").Set<product>().ToList();
        }

        public static List<category> GetCategories()
        {
            return new DbContext("name=BookStore1Entities2").Set<category>().ToList();
        }

        public static string GetNameOfProductByID(string productID)
        {
            var product = cn.Set<product>().Find(productID);
            return product != null ? product.productName : null;
        }

        public static string GetImgOfProductByID(string productID)
        {
            var product = cn.Set<product>().Find(productID);
            return product != null ? product.avatar : null;
        }


        public static List<order> GetOrders()
        {
            return new DbContext("name=BookStore1Entities2").Set<order>().ToList();
        }

        public static List<payment> GetPayments()
        {
            return new DbContext("name=BookStore1Entities2").Set<payment>().ToList();
        }

        public static int GetOrderCount()
        {
            return (int)GetOrders().Count;
        }

        public static List<order> GetOrders(int id)
        {
            return new DbContext("name=BookStore1Entities2").Set<order>().Where(x => x.userID == id).OrderByDescending(x => x.orderID).ToList();
        }
    }
}
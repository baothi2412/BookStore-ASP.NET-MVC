using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Work.Models;

namespace Work.App_Start
{
    public class SessionConfig
    {
        public static CartShop GetCartLits()
        {
            return (CartShop)HttpContext.Current.Session["cartshop"];
        }
        public static void SetOrder(order order)
        {
            HttpContext.Current.Session["ordernew"] = order;
        }
        public static order GetOrder()
        {
            return (order)HttpContext.Current.Session["ordernew"];
        }
        public static void SetUser(user user)
        {
            HttpContext.Current.Session["userinfo"] = user;
        }

        public static user GetUser()
        {
            return (user)HttpContext.Current.Session["userinfo"];
        }
    }
}
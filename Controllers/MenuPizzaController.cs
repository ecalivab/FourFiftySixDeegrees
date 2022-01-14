using FortyFiftySixDeegrees.DataAccess;
using FortyFiftySixDeegrees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FortyFiftySixDeegrees.Helper;
using System.Data.Entity;

namespace FortyFiftySixDeegrees.Controllers
{
    public class MenuPizzaController : Controller
    {
        readonly private PizzaContext db;
        public MenuPizzaController() 
        {
            db = new PizzaContext();
        }
            
        // GET: Menu
        public ActionResult ListMenuPizza()
        {
                     
            List <Pizza> Menu = db.Pizzas.ToList();
            Order currentOrder = new Order();
            Tuple<List<Pizza>, Order> tuple = new Tuple<List<Pizza>, Order>(Menu, currentOrder);
            return View(tuple);
        }


        [HttpPost]
        public ActionResult SendOrder([Bind(Include = "OrderID,CustomerName,TelephoneNumber,Data,TotalAmount,OrderDetails")] Order mOrder) 
        {
            mOrder.Ready = false; //The order is recently sent so it is not ready. 

            if (ModelState.IsValid)
            {
                db.Orders.Add(mOrder);
                db.SaveChanges();
                return Json(new { success = true, responseText = "Your order was sent succesfully!" }, JsonRequestBehavior.AllowGet);
            }
           
            return Json(new { success = false, responseText = "Something went wrong! Try again!" }, JsonRequestBehavior.AllowGet);
        }
      
    }
   
}
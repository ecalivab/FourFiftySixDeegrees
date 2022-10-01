using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FortyFiftySixDeegrees.DataAccess;
using FortyFiftySixDeegrees.Models;
using FortyFiftySixDeegrees.Helper;

namespace FortyFiftySixDeegrees.Controllers
{
    public class PizzaDBController : Controller
    {
        private PizzaContext db = new PizzaContext();

        // PizzaDB/OrderControl to access the order list to edit

        // GET: PizzaDB
        public ActionResult Index()
        {
            return View(db.Pizzas.ToList());
        }

        // GET: PizzaDB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: PizzaDB/Create
        public ActionResult Create()
        {
            return View(new Pizza());
        }

        // POST: PizzaDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PizzaId,Name,Price,URL,DescriptionEN,DescriptionFI")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pizza);
        }

        // GET: PizzaDB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: PizzaDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PizzaId,Name,Price,URL,DescriptionEN,DescriptionFI")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: PizzaDB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: PizzaDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Orders
        public ActionResult OrderControl(bool filter = false)
        {
            List<Order> mOrder = new List<Order>();
            if (filter) 
            {
                mOrder = db.Orders.Include(m => m.OrderDetails).Where(m => m.Ready == false).ToList();
                return View(mOrder);
            }

            mOrder = db.Orders.Include(m => m.OrderDetails).ToList();
            return View(mOrder);
        }

        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(m => m.OrderDetails).FirstOrDefault(m => m.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: PizzaDB/EditOrder/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "OrderID,CustomerName,TelephoneNumber,Data,TotalAmount,Ready,OrderDetails")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderControl", "PizzaDB");
            }
            return View(order);
        }

        // GET: PizzaDB/DeleteOrder/5
        [HttpGet]
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(m => m.OrderDetails).FirstOrDefault(m => m.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            db.OrderDetails.RemoveRange(db.OrderDetails.Where(m => m.OrderParent.OrderID == id));
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("OrderControl", "PizzaDB");
        }

        [HttpPost]
        public JsonResult UpdateOrderDetails(int? OrderID, int? parentID, double price, OperationEdit op)
        {
            if (OrderID == null || parentID == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            OrderDetails orderDetail = db.OrderDetails.Find(OrderID);
            Order order = db.Orders.Find(parentID);
            if (order == null || orderDetail == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            switch (op)
            {
                case OperationEdit.ADD:
                    orderDetail.Quantity++;
                    order.TotalAmount += price;
                    break;
                case OperationEdit.REMOVE:
                    orderDetail.Quantity--;
                    order.TotalAmount -= price;
                    break;

            }
            db.Entry(orderDetail).State = EntityState.Modified;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true, newQuantity = orderDetail.Quantity, pid = parentID, id = OrderID, newAmount = order.TotalAmount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrderDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name");
            ViewBag.parentOrder = id;
            return View(new OrderDetails());
        }

        // POST
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderDetail([Bind(Include = "OrderDetailsID,PizzaId,Quantity")] OrderDetails orderDetails, int? parentOrder)
        {
            if (ModelState.IsValid)
            {
                Order currentOrder = db.Orders.Include(m => m.OrderDetails).FirstOrDefault(m => m.OrderID == parentOrder);
                Pizza pizza = db.Pizzas.Find(orderDetails.PizzaId); //This is used to find the price of the pizza and added to the total amount
                currentOrder.OrderDetails.Add(orderDetails);
                currentOrder.TotalAmount += (pizza.Price * orderDetails.Quantity);
                db.Entry(currentOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderControl", "PizzaDB");
            }

            ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name", orderDetails.PizzaId);
            return View(orderDetails);
        }

        //Ready Update of the Database
        public JsonResult UpdateOrderReady(int? OrderId, bool isReady)
        {
            if (OrderId == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            Order currentOrder = db.Orders.Include(m => m.OrderDetails).FirstOrDefault(m => m.OrderID == OrderId);
            currentOrder.Ready = isReady;
            db.Entry(currentOrder).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true, ready = isReady }, JsonRequestBehavior.AllowGet);
        }
    }
}

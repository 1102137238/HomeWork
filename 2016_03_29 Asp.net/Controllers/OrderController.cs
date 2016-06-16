using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using _2016_03_29_Asp.net.Models;

namespace _2016_03_29_Asp.net.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(Models.Orders orders)
        {
            //Models.OrderService orderService = new Models.OrderService();
            //ViewBag.Data = orderService.GetOrderById("111");

            

            Models.EmployeeService empService = new Models.EmployeeService();
            ViewBag.empInfo = empService.GetEmployeeArray();

            Models.CompanyService companyService = new Models.CompanyService();
            ViewBag.companyInfo = companyService.GetCompanyArray();

            Models.OrderService orderService = new Models.OrderService();
            ViewBag.orders = orderService.GetOrders(orders);

            return View();
       }


        [HttpPost()]
        public JsonResult DeleteOrder(string OrderID)
        {

            try
            {
                OrderService ordersService = new OrderService();
                ordersService.DeleteOrderDetailById(OrderID);
                ordersService.DeleteOrderById(OrderID);

                return this.Json(true);
            }catch (Exception){
                return this.Json(false);
            }
        }





    }
}
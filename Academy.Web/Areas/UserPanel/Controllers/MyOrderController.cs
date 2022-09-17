using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Order;

namespace Academy.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class MyOrderController : Controller
    {
        private IOrderService _orderService;
        public MyOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {

            return View(_orderService.GetOrdersForUser(User.Identity.Name));
        }

        public IActionResult ShowOrder(int id, bool finaly=false,string Type="")
        {
            Order order = _orderService.GetOrderForUserPanel(User.Identity.Name,id);
            if (order==null)
            {
                return NotFound();
            }
            ViewBag.finaly = finaly;
            ViewBag.discountType = Type;
            return View(order);
        }


        public IActionResult FinalyOrder(int id) 
        {
            if (_orderService.IsFinallyOrder(User.Identity.Name,id))
            {
                //اینجا باید پیامیرا نمایش بدیم که فاکتورپرداخت موفقیت آمیز بوده هر جور دویت داری میشه پیاده کرد و پیام را نمایش داد
                return Redirect("/UserPanel/MyOrder/ShowOrder/"+id+"?finaly=true");
            }
            return View();
        }

        public IActionResult UseDiscount(int orderId,string code) 
        {
            var discountType = _orderService.UseDiscount(orderId,code);
            return Redirect("/UserPanel/MyOrder/Showorder/"+orderId+"?Type="+discountType.ToString());
        }
    }
}

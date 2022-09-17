using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Order;

namespace Academy.Web.Pages.Admin.manageDiscount
{
    
    public class EditDiscountModel : PageModel
    {
        private IOrderService _orderService;
        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Discount Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _orderService.GetDiscountById(id);
        }

        public IActionResult OnPost(string stDate = "", string edDate = "")
        {
            //قبل از اینکه ولید بودن چک بشه باید تاریخ را چک کنیم واینکه به میلادی تبدیل کنیم
            if (stDate != "")
            {
                string[] std = stDate.Split("/");
                Discount.StartDate = new DateTime(int.Parse(std[0]), int.Parse(std[1]), int.Parse(std[2]), new PersianCalendar());
            }
            if (edDate != "")
            {
                string[] edd = edDate.Split("/");
                Discount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]), new PersianCalendar());
            }
            if (!ModelState.IsValid)
                return Page();


            _orderService.AddDiscount(Discount);
            return RedirectToAction("Index");
        }
    }
}

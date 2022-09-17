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
    public class CreateDiscountModel : PageModel
    {
        private IOrderService _orderService;
        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [BindProperty]
        public Discount Discount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string stDate="",string edDate="") 
        {
            //قبل از اینکه ولید بودن چک بشه باید تاریخ را چک کنیم واینکه به میلادی تبدیل کنیم
            if (stDate!=null)
            {
                string[] std = stDate.Split("/");
                Discount.StartDate = new DateTime(int.Parse( std[0]),int.Parse(std[1]),int.Parse(std[2]),new PersianCalendar());
            }
            if (edDate!=null)
            {
                string[] edd = edDate.Split("/");
                Discount.EndDate = new DateTime(int.Parse(edd[0]), int.Parse(edd[1]), int.Parse(edd[2]),new PersianCalendar());
            }

            if (!ModelState.IsValid )
                return Page();

            //چک کنیک کد تخفیف تکراری نباشه
            if (_orderService.IsExistDiscount(Discount.DiscountCode))
            {
                ViewData["IsExistCode"] = true;
                return Page();
            }


            _orderService.AddDiscount(Discount);
            return RedirectToPage("Index");

        }

        //admin/discount/creatediscount?handler=checkcode
        //admin/discount/creatediscount/checkcode
        //public IActionResult OnGetCheckCode(string code) 
        //{
        //    return Content(_orderService.IsExistDiscount(code).ToString());
        //}
    }
}

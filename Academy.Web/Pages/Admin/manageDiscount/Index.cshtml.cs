using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Order;

namespace Academy.Web.Pages.Admin.manageDiscount
{
    public class IndexModel : PageModel
    {

        private IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Discount> Discounts { get; set; }
        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}

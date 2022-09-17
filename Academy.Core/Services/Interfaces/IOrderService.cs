using System;
using System.Collections.Generic;
using System.Text;
using Academy.Core.DTOs.Order;
using Academy.DataLayer.Entities.Order;
using Academy.DataLayer.Entities.Order;

namespace Academy.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        Order GetOrderByOrderId(int orderId);

        void UpdatePriceOrder(int orderId);
        void UpdateOrder(Order order);

        Order GetOrderForUserPanel(string userName,int orderId);

        bool IsFinallyOrder(string userName,int orderId);

        List<Order> GetOrdersForUser(string userName);

        //برای اون دکمه که اگر کاربر خریده بود درس را نشان نده اگر نخریده بود نمایش بده
        bool IsUserInCourse(string userName,int courseId);

        #region Discount
        DiscountUseType UseDiscount(int orderId, string code);
        void UpdateDiscount(Discount discount);
        void AddDiscount(Discount discount);
        List<Discount> GetAllDiscounts();
        Discount GetDiscountById(int disountId);

        bool IsExistDiscount(string code);
        #endregion
    }
}

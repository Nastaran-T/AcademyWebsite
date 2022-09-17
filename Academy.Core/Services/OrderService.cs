using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Core.DTOs.Order;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Context;
using Academy.DataLayer.Entities.Course;
using Academy.DataLayer.Entities.Order;
using Academy.DataLayer.Entities.User;
using Academy.DataLayer.Entities.Wallet;
using Academy.DataLayer.Entities.Order;

namespace Academy.Core.Services
{
    public class OrderService : IOrderService
    {
        private AcademyContext _context;
        private IUserService _userService;
        public OrderService(AcademyContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public int AddOrder(string userName,int courseId)
        {
            //میتونستم از سرویس هم استفاده نکنم ولی سرویس را اینجکت کردم
            int userId = _userService.GetUserIdByUserName(userName);

            //چک کنم این یوزر فاکتوری داره که باز مونده باشه
            Order order = _context.Orders.SingleOrDefault(o=>o.UserId==userId && !o.IsFinaly);
            //میتونستیم مثل سرویس هم صدا بزنم مثل یوزر
            Course course = _context.Courses.Find(courseId);
            if (order==null) 
            {
                //این یوزر فاکتوری نداره پس یدونه براش ایجاد میشه
                order = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    //جمع فاکتور از قیمت هر دوره یا کالا بدست میاد 
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId=courseId,
                            Price=course.CoursePrice,
                            Count=1
                        }
                    }
                    
                    
                };
              
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                //یعنی کاربر فاکتور دارد که باز است
                //باید چک کنیم که آیا محصول انتخاب شده الان،تی فاکتور باز مونده هست یا نه 
                //اگر هست باید به تعدادش یکی اضافه بشه
                //اگر نه باید به دیتیل اضافه کنیم یدونه جدید

                OrderDetail orderDetail = _context.OrderDetails.FirstOrDefault(o=>o.OrderId==order.OrderId && o.CourseId==courseId);

                if (orderDetail!=null) 
                {
                    orderDetail.Count += 1;
                    _context.OrderDetails.Update(orderDetail);

                    order.OrderSum = orderDetail.Count * orderDetail.Price;
                    _context.Orders.Update(order);

                }
                else
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId=order.OrderId,
                        Count=1,
                        CourseId=courseId,
                        Price=course.CoursePrice
                    };
                   
                    _context.OrderDetails.Add(orderDetail);
                }
                _context.SaveChanges();

                UpdatePriceOrder(order.OrderId);

            }
            return order.OrderId;
        }

        public Order GetOrderForUserPanel(string userName, int orderId)
        {
            //یوزر نیم هم این فاکتور را چک میکنیم که یه وقت آی دی را دستکاری نکنه فاکتور های دیگه نمایان بشه

            int userId = _userService.GetUserIdByUserName(userName);
            Order order = _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course)
                .FirstOrDefault(o=>o.UserId==userId && o.OrderId==orderId);
            return order;
        }

        public List<Order> GetOrdersForUser(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
         return _context.Orders.Where(o=>o.UserId==userId).ToList();
           
        }

        public bool IsFinallyOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            var order = _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course).FirstOrDefault(o=>o.OrderId==orderId && o.UserId==userId);

            if (order==null || order.IsFinaly)
            {
                return false;
            }
            if (_userService.BalanceWalletUser(userName) >= order.OrderSum)
            {
                order.IsFinaly = true;
                Wallet wallet = new Wallet() 
                {
                    Amount=order.OrderSum,
                    CreateDate=DateTime.Now,
                    IsPay=true,
                    TypeId=2,
                    UserID=userId,
                    Description="فاکتور شماره#"+orderId

                }; 
                _userService.AddWallet(wallet);
                _context.Orders.Update(order);
                

                //درس های هر فاکتور را باید اضافه کنیم به جدول که بدونیم هر یوزر چه درس های را خرید کرده
                foreach (var item in order.OrderDetails)
                {
                    _context.userCourses.Add(new UserCourse()
                    {
                        CourseId = item.CourseId,
                        UserId=userId
                    }) ;
                }
                _context.SaveChanges();
                return true;

            }
            return false;
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public void UpdatePriceOrder(int orderId)
        {
            Order order = _context.Orders.Find(orderId);
            order.OrderSum = _context.OrderDetails.Where(o => o.OrderId == orderId).Sum(o=>o.Price);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }


        public bool IsUserInCourse(string userName, int courseId) 
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _context.userCourses.Any(u=>u.UserId==userId && u.CourseId==courseId);
        }

        #region Discount
        //سرویس اعمال کد تخفیف به فاکتور
        public DiscountUseType UseDiscount(int orderId, string code)
        {
            var discount = _context.Discounts.SingleOrDefault(d=>d.DiscountCode==code);

            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate < DateTime.Now)
                return DiscountUseType.ExpireDate;

            if (discount.EndDate != null && discount.EndDate >= DateTime.Now)
                return DiscountUseType.ExpireDate;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;


            //اینجا باید مقدار درصد را روی فاکتور اعمال کنیم
            var Order = GetOrderByOrderId(orderId);

            //چک کنیم که کاربر فقط یه بار از کد تخفیف استفاده کنه
            if (_context.UserDiscountCodes.Any(u => u.UserId == Order.UserId && u.DiscountId == discount.DiscountId))
                return DiscountUseType.UserUsed;

            Order.OrderSum = (Order.OrderSum) * (discount.DiscountPercent) / 100;

            UpdateOrder(Order);

            discount.UsableCount -= 1;
            UpdateDiscount(discount);

            _context.UserDiscountCodes.Add(new UserDiscountCode() 
            {
                DiscountId=discount.DiscountId,
                UserId=Order.UserId
            });
            _context.SaveChanges();
            return DiscountUseType.Success;
           
            

            

            


        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
        }



        #endregion

        #region Admin_ManageDiscount
        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount GetDiscountById(int disountId)
        {
            return _context.Discounts.Find(disountId);
        }

        //برای اینکه چک کنیم که کد تکراری ادد نشه
        public bool IsExistDiscount(string code)
        {
            return _context.Discounts.Any(d=>d.DiscountCode==code);
        }

        #endregion
    }
}

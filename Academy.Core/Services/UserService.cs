using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Academy.Core.Convertor;
using Academy.Core.DTOs;
using Academy.Core.Generator;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Context;
using Academy.DataLayer.Entities.User;
using Academy.DataLayer.Entities.Wallet;
using Academy.Core.Security;
//using static Academy.Core.DTOs.UserPanelViewModel;

namespace Academy.Core.Services
{
    public class UserService : IUserService
    {
        private AcademyContext _context;

        public UserService(AcademyContext context)
        {
            _context = context;
        }

        #region Account_Register
        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public int AddorUpdateUser(User user)
        {
            //Add
            _context.Add(user);
            _context.SaveChanges();
            return user.UserID;

            //_context.Users.AddOrUpdate(ref user,u=>u.UserID);
            //var newEntry = _context.ChangeTracker.Entries<User>()
            //    .FirstOrDefault(u => u.Entity.UserID == user.UserID);
            //return user.UserID;
        }
        #endregion


        #region Account_ActiveAccount
        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);

            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            //کد فعالسازی ما یکبار مصرف است و با هر بار فعالسازی ایمیل کد عوض میشه مورد دیگر برای تعریف این خط این است که 
            //اگر کاربر رمز عبور خود را فراموش کند باید مجدد به ایمیل خود برود و در این مواقع هم ما به کد فعالسازی نیاز داریم

            /*مورد دیگه ام اینکه برای این ما این کد را یکبار مصرف کردیم
            * که اگر احیانا این کاربر توسط مدیر غیر فعال شد به دلایلی 
            * دوباره نره ایمیل فعالسازی که قبل فرستادیم بزنه وفعال بشه بخاطر همین یکبار مصرف است 
            */

            user.ActiveCode = NameGenerator.CodeGenarator();
            _context.SaveChanges();
            return true;


        }

        #endregion


        #region Account_Login
        /*در اینجا میشد به جای ویو مدل
         * فقط ایمیل و پسورد چک کنیم
         * هر دو درسته
         */
        /*نکته دیگه ای که وجود دارد اینه که چرا برگشت متد یک یوزر است
         * برای اینکه من میخوام از طریق همین ایمیل و پسورد یوزر مورد نظر پیدا کنم
         * که موارد دیگه ای را براساس اون بررسی کنم
         * مثل بررسی اینکه ایا یوزر عمل فعالسازی را انجام داده است یا نه..
         */
        public User LoginUser(LoginViewModel login)
        {
            string HashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Password == HashPassword && u.Email == email);

        }
        #endregion

        #region Account_Resetpassword
        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();

        }
        #endregion

        #region Account_ForgotPassword
        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);

        }
        #endregion

        #region Area_Userpanel_InformationUser
        public User GetUserByUserName(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }
        public InformationUserViewModel GetInformationUserPanel(string username)
        {
            User user = GetUserByUserName(username);

            InformationUserViewModel informationUser = new InformationUserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = BalanceWalletUser(username)
            };

            return informationUser;
        }
        #endregion

        #region Area_Userpanel_SidBar_Partial
        public SideBarDataViewModel GetSidebarDataUserPanel(string username)
        {
            //شبیه کد بالا هم میشه نوشت این مدل دیگه شه

            return _context.Users.Where(u => u.UserName == username).Select(u => new SideBarDataViewModel()
            {
                UserName = u.UserName,
                RegisterDate = u.RegisterDate,
                ImageName = u.UserAvatar

            }).Single();
        }
        #endregion

        #region Area_Userpanel_EditProfile
        public EditUserPanelViewModel GetDataforEditProfile(string username)
        {
            return _context.Users.Where(u => u.UserName == username).Select(u => new EditUserPanelViewModel()
            {
                UserName = u.UserName,
                Email = u.Email,
                AvatarName = u.UserAvatar

            }).Single();

        }


        public void EditProfile(string userName, EditUserPanelViewModel profile)
        {
            //اگر مخالف نال باشه یعنی کاربر فایلی انتخاب کرده است
            if (profile.UserAvatar != null)
            {
                string pathFile = "";
                //اگر مخالف عکس پیشفرض بود بنابراین عکسی انتخاب شده قبلا پس باید پاک شود
                if (profile.AvatarName != "Default.png")
                {
                    pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AvatorUser", profile.AvatarName);

                    if (File.Exists(pathFile))
                    {
                        File.Delete(pathFile);
                    }
                }

                //اگر عکس پیش فرض بود که هیچی مراحل ذخیره سازی را طی میکنه
                //به عکس جدید اسم میدیم
                //  string imageName
                profile.AvatarName = NameGenerator.CodeGenarator() + Path.GetExtension(profile.UserAvatar.FileName);
                pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AvatorUser", profile.AvatarName);
                //حالا عکس ذخیره میکنیم
                using (var stream = new FileStream(pathFile, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

            }

            var user = GetUserByUserName(userName);
            //TODO  checking username dont repeat...me
            user.UserName = profile.UserName;

            //TODO Checking Email If Change by user Sending email for active code and active account
            user.Email = profile.Email;
            user.UserAvatar = profile.AvatarName;


            UpdateUser(user);
        }
        #endregion

        #region Area_Userpanel_ChangePassword
        public bool CompareOldPassword(string username, string oldPassword)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashPassword);
        }


        public void ChangeUserPassword(string userName, string newPassword)
        {
            User user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }
        #endregion

        #region Area_Userpanel_Wallet
        public int BalanceWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);
            var Enter = _context.Wallets.Where(w => w.UserID == userId && w.TypeId == 1 && w.IsPay).Select(w => w.Amount).ToList();
            var Exit = _context.Wallets.Where(w => w.UserID == userId && w.TypeId == 2 && w.IsPay).Select(w => w.Amount).ToList();
            return (Enter.Sum() - Exit.Sum());

            //در خط بالا ابتدا از سام استفاده نکردیم چونکه اگر واریزی یا برداشتی وجود نداشت بهمون خطا میداد ما لیستی از اون هارو واکشی میکنیم بعد سام ذقیقا مثل کد 
        }

        public List<WalletViewModel> GetWalletUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _context.Wallets.Where(w => w.IsPay && w.UserID == userId).Select(w => new WalletViewModel()
            {
                Amount = w.Amount,
                DateTime = w.CreateDate,
                Description = w.Description,
                Type = w.TypeId

            }).ToList();
        }

        public int ShargeWallet(string userName, int amount, string description, bool isPay = false)
        {
            int userId = GetUserIdByUserName(userName);
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                TypeId = 1,
                IsPay = isPay,
                UserID = userId
            };
            return AddWallet(wallet);

        }

        public int AddWallet(Wallet wallet)
        {
            _context.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public Wallet GetWalletById(int WalletId)
        {
            return _context.Wallets.Find(WalletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Update(wallet);
            _context.SaveChanges();
        }

        #endregion

        #region Adminpanel
        public UserForAdminViewModel GetAllUser(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            //کل کاربران را بهمون نمایش بده و قابلیت فیلتر هم داشته باشه براساس ایمیل و نام کاربری
            IQueryable<User> result = _context.Users;
            if (!string.IsNullOrEmpty(filterEmail))
            {
                //contain() هم میشه
                result = result.Where(u => EF.Functions.Like(u.Email, "%" + filterEmail + "%"));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => EF.Functions.Like(u.UserName, "%" + filterUserName + "%"));
            }

            //paging
            int take = 20; //در هر صفحه چند تا کاربر باشه
            int skip = (pageId - 1) * take;

            //براینکه تعداد کل صفحه و صفحه فعلی داشته باشیم
            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;

        }
        #endregion

        #region Adminpanel_CreateUser
        public int AddUserFromAdmin(CreateUserViewModel userViewModel)
        {
            User user = new User();
            user.UserName = userViewModel.UserName;
            user.Email = userViewModel.Email;
            user.IsActive = true;
            user.Password = PasswordHelper.EncodePasswordMd5(userViewModel.Password);
            user.RegisterDate = DateTime.Now;
            user.ActiveCode = NameGenerator.CodeGenarator();
            #region SaveImage
            if (userViewModel.UserAvatar != null)
            {
                string imagePath = "";
                user.UserAvatar = NameGenerator.CodeGenarator() + Path.GetExtension(userViewModel.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AvatorUser", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    userViewModel.UserAvatar.CopyTo(stream);
                }
            }
            #endregion
            return AddorUpdateUser(user);
        }
        #endregion





        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName).UserID;
        }






        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }




        public InformationUserViewModel GetInformationUserPanel(int userId)
        {
            User user = GetUserById(userId);

            InformationUserViewModel informationUser = new InformationUserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = BalanceWalletUser(user.UserName)
            };
            return informationUser;
        }



       

       

       

        

      

       

        public UserForAdminViewModel GetAllDeleteUser(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => EF.Functions.Like(u.Email, "%" + filterEmail + "%"));
            }
            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => EF.Functions.Like(u.UserName, "%" + filterUserName + "%"));
            }

            //paging
            int take = 20;
            int skip = (pageId - 1) * take;

            //براینکه تعداد کل صفحه و صفحه فعلی داشته باشیم
            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;

        }

       

        public EditUserViewModel GetUserForEditMode(int Userid)
        {
            return _context.Users.Where(u => u.UserID == Userid).Select(u => new EditUserViewModel
            {
                UserId = u.UserID,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.Password,
                Avatar = u.UserAvatar,
                //این جا از لی زی لود انتیتی استفاده کردیم چون جدول یوزر با جدول UserRole رابطه دارد
                RoleUser = u.UserRoles.Select(r => r.RoleID).ToList()
            }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {

            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            if (editUser.Password != user.Password)
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {

                //Delete old Image
                if (editUser.Avatar != "Default.png")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.Avatar);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }


                //save new Image
                user.UserAvatar = NameGenerator.CodeGenarator() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AvatorUser", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User GetUserById(int UserId)
        {
            return _context.Users.Find(UserId);
        }


    }
}

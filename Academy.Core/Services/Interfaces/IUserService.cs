using System;
using System.Collections.Generic;
using System.Text;
using Academy.Core.DTOs;
using Academy.DataLayer.Entities.User;
using Academy.DataLayer.Entities.Wallet;
//using static Academy.Core.DTOs.UserPanelViewModel;

namespace Academy.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddorUpdateUser(User user);

        //این متد برای چک کردن کاربر که میتونه لاگین کنه یا نه
        //یه راه اینه که ورودی اینا باشه 
        //User LoginUser(string email,string password);
        User LoginUser(LoginViewModel login );
        User GetUserById(int UserId);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        bool ActiveAccount(string activeCode);
        int GetUserIdByUserName(string userName);

        #region UserPanel
        InformationUserViewModel GetInformationUserPanel(string username);
        InformationUserViewModel GetInformationUserPanel(int userId);
        SideBarDataViewModel GetSidebarDataUserPanel(string username);
        EditUserPanelViewModel GetDataforEditProfile(string username);
        void EditProfile(string userName,EditUserPanelViewModel profile);
        bool CompareOldPassword(string username,string oldPassword);
        void ChangeUserPassword(string userName,string newPassword);
        #endregion

        #region Wallet
        int BalanceWalletUser(string userName);
       List<WalletViewModel> GetWalletUser(string userName);

        //int گذاشتیم برای اینکه در قسمت درگاه پرداخت میخواییم آی دی تراکنش را داشته باشیم
        int ShargeWallet(string userName,int amount,string description,bool isPay=false);
        int AddWallet(Wallet wallet);
        void UpdateWallet(Wallet wallet);
        Wallet GetWalletById(int WalletId);
        #endregion

        #region AdminPanel
        //filtering براش اعمال شده
        UserForAdminViewModel GetAllUser(int pageId=1,string filterEmail="",string filterUserName="");
        UserForAdminViewModel GetAllDeleteUser(int pageId=1,string filterEmail="",string filterUserName="");
        int AddUserFromAdmin(CreateUserViewModel userViewModel);
        EditUserViewModel GetUserForEditMode(int Userid);
        void EditUserFromAdmin(EditUserViewModel editUser);
        #endregion
    }
}

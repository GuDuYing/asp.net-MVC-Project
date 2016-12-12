using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.ComManage;
using Common.CryptHelper;

namespace Service.ComManage
{
    class UserService : RepositoryBase<T_User>, IUserService
    {
        /// <summary>
        /// 用户登录，根据用户名和密码获取用户信息
        /// </summary>
        /// <param name="Account">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public T_User UserLogin(string Account, string pwd)
        {
            try
            {
                var entity = this.Get(e => e.UserEmail == Account);

                if (entity != null && new AESCrypt().Decrypt(entity.UserPwd) == pwd)
                {
                    return entity;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public string GetUserName(int uid)
        {
            try
            {
                string userName = Get(e => e.UID == uid).UserName;
                if (userName != null && userName != "")
                {
                    return userName;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string UserRegister(T_User user)
        {
            try
            {
                //user.UID = new Random().Next(1, 1000);
                user.UserStatu = "0";
                user.UserType = null;
                user.UserPwd = new AESCrypt().Encrypt(user.UserPwd);
                if(this.Save(user))
                {
                    return user.UserName;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }








    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService.SysManage;

namespace Service.SysManage
{
    class UserManage:RepositoryBase<T_User>,IUserManage
    {
        /// <summary>
        /// 管理用户登录验证
        /// add yuangang by 2016-05-12
        /// </summary>
        /// <param name="email">用户名</param>
        /// <param name="password">加密密码（AES）</param>
        /// <returns></returns>
        public T_User UserLogin(string email, string password)
        {
            var entity = this.Get(p => p.UserEmail == email);

            //因为我们用的是AES的动态加密算法，也就是没有统一的密钥，那么两次同样字符串的加密结果是不一样的，所以这里要通过解密来匹配
            //而不能通过再次加密输入的密码来匹配
            if (entity != null && new Common.CryptHelper.AESCrypt().Decrypt(entity.UserPwd) == password)
            {
                return entity;
            }
            return null;
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin(int userId)
        {
            //这里我们还没有做用户角色 所以先返回个True，后面我们做角色的时候再回来修改
            return true;
        }

        /// <summary>
        /// 根据用户ID获取用户名
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        public string GetUserName(int Id)
        {
            var query = this.LoadAll(c => c.UID == Id);
            if (query == null || !query.Any())
            {
                return "";
            }
            return query.First().UserName;
        }
    }
}

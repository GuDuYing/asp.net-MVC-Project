using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService.ComManage
{
    /// <summary>
    /// 用户信息服务接口
    /// </summary>
    public interface IUserService: IRepository<T_User>
    {
        //用户登录获取用户信息
        T_User UserLogin(string Account, string pwd);

        string GetUserName(int uid);

        //用户注册
        string UserRegister(T_User user);
    }
}

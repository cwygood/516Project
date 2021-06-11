using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<User_Info> GetAllUser();
        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User_Info GetUserById(long id);
    }
}

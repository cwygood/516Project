using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Common.Db;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class UserRepository : CusDbContext, IUserRepository
    {
        public UserRepository(IDbHelper context) : base(context)
        {

        }
        public IEnumerable<User_Info> GetAllUser()
        {
            //return this.QueryAll<User_Info>();//EF查询
            return this.QueryAll<User_Info>("select * from user_info");
            //return this.QueryAdoAsync<User_Info>("select * from user_info ", null);//ADO.net 查询
        }

        public User_Info GetUserById(long id)
        {
            return this.QueryFirstOrDefault<User_Info>("select * from user_info where id=@id", new { id = id });
            //return this.QueryFirstOrDefault<User_Info>(f => f.Id == id);//EF查询
            //return this.QueryFirstOrDefaultAsync<User_Info>("select * from user_info where id=@id", new Dictionary<string, object>() { { "@id", id } });//ADO.net 查询
        }

        public User_Info GetUserInfo(string userCode)
        {
            return this.QueryFirstOrDefault<User_Info>("select id,code,name,password,status,locktime from user_info where code=@code", new { code = userCode });
        }
    }
}

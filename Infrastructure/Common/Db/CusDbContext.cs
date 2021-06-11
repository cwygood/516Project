using Domain.Interfaces;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Common.Db
{
    public class CusDbContext
    {
        public IDbHelper DbHelper { get; set; }
        public CusDbContext(IDbHelper context)
        {
            this.DbHelper = context;
        }

        protected IEnumerable<T> QueryAll<T>() where T : class
        {
            return this.DbHelper.QueryAll<T>();
        }
        protected IEnumerable<T> QueryAll<T>(string sql)
        {
            return this.DbHelper.QueryAll<T>(sql);
        }
        protected IEnumerable<T> Query<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return this.DbHelper.Query<T>(expression);
        }
        protected T QueryFirstOrDefault<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return this.DbHelper.QueryFirstOrDefault<T>(expression);
        }
        protected T QueryFirstOrDefault<T>(string sql, object param)
        {
            return this.DbHelper.QueryFirstOrDefault<T>(sql, param);
        }

        protected IEnumerable<T> QueryAdoAsync<T>(string sql, Dictionary<string,object> param)
        {
            return this.DbHelper.QueryAll<T>(sql, param);
        }

        protected T QueryFirstOrDefaultAsync<T>(string sql, Dictionary<string, object> param)
        {
            return this.DbHelper.QueryAll<T>(sql, param).FirstOrDefault();
        }
    }
}

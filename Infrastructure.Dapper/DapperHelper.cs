using Dapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Dapper
{
    public class DapperHelper : IDbHelper
    {
        private readonly DapperDbContext _context;
        public DapperHelper(DapperDbContext context)
        {
            this._context = context;
        }
        [Obsolete]
        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }
        [Obsolete]
        public IEnumerable<T> QueryAll<T>() where T : class
        {
            throw new NotImplementedException();
        }
        [Obsolete]
        public T QueryFirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryAll<T>(string sql)
        {
            return this._context.DbConnection().Query<T>(sql);
        }
        public T QueryFirstOrDefault<T>(string sql, object param)
        {
            return this._context.DbConnection().QueryFirstOrDefault<T>(sql, param);
        }
        [Obsolete]
        public IEnumerable<T> QueryAll<T>(string sql, Dictionary<string, object> param)
        {
            throw new NotImplementedException();
        }
    }
}

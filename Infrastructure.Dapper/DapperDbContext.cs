﻿using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Dapper
{
    public class DapperDbContext
    {
        private readonly string _connectionString;
        public DapperDbContext(IOptions<DatabaseConfiguration> mySqlOptions)
        {
            this._connectionString = mySqlOptions.Value.ConnectionString;
        }

        public MySqlConnection DbConnection()
        {
            if (string.IsNullOrWhiteSpace(this._connectionString))
            {
                throw new Exception("ConnectionString is Null");
            }
            else
            {
                return new MySqlConnection(this._connectionString);
            }
        }
    }
}

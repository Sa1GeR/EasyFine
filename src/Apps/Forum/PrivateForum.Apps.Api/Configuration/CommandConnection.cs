using PrivateForum.App.Web.Configuration;
using PrivateForum.Core;
using Microsoft.Extensions.Options;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace PrivateForum.App.Web
{
    public class CommandConnection : IConnectionFactory
    {
        private DateTime startTime;
        
        public DbConnection Connection { get; private set; }

        public DbTransaction Transaction { get; private set; }

        public CommandConnection(IOptions<DbConfiguration> configuration)
        {
            this.startTime = DateTime.Now;
            this.Connection = new SqlConnection(configuration.Value.ConnectionString);
            this.Transaction = null;
        }

        public void Begin()
        {
            this.Transaction = this.Connection.BeginTransaction(System.Data.IsolationLevel.Snapshot);
        }

        public void Commit()
        {
            this.Transaction.Commit();
        }

        public void Rollback()
        {
            this.Transaction.Rollback();
        }

        public void Dispose()
        {
            this.Connection.Close();
        }
        
    }
}
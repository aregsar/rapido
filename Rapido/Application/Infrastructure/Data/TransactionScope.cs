using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace Rapido.Infrastructure.Data
{
    public class TransactionScope : IDisposable
    {

        public DbTransaction Transaction { get; private set; }

        private DbConnection Connection;

        private IRepository _repo;

        public TransactionScope(IRepository repo)
        {
            _repo = repo;
        }

        public DbTransaction BeginTransaction()
        {
            Connection = _repo.Connection();

            Transaction = Connection.BeginTransaction();

            return Transaction;
        }

        public void Dispose()
        {
            try
            {
                if (Transaction != null)Transaction.Dispose();
            }
            finally
            {
                if (Connection != null) Connection.Dispose();
            }
        }
    }
}
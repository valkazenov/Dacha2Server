using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dacha2Server.Helpers;

namespace Dacha2Server.Models
{
    public class DatabaseDisposableContainer : IDisposable
    {
        public DatabaseDisposableContainer()
        {
            Connection = null;
            Transaction = null;
            DisposeNeed = false;
        }

        public void Dispose()
        {
            if (!DisposeNeed) return;
            if (Connection != null) Connection.Dispose();
            if (Transaction != null) Transaction.Dispose();
        }

        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        public bool DisposeNeed { get; set; }
    }

    public delegate void ModelTransactionAction(SqlConnection connection, SqlTransaction transaction);

    public class BaseModel
    {
        public BaseModel()
        {
            Transaction = null;
        }

        protected DatabaseDisposableContainer ConditionalBeginTransaction()
        {
            var result = new DatabaseDisposableContainer();
            if (Transaction == null)
            {
                result.Connection = CommonHelper.CreateConnection();
                result.Transaction = result.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
                result.DisposeNeed = true;
            }
            else
            {
                result.Connection = Transaction.Connection;
                result.Transaction = Transaction;
            }
            return result;
        }

        protected void ConditionalCommitTransaction(DatabaseDisposableContainer container)
        {
            if (container.DisposeNeed)
            {
                container.Transaction.Commit();
                container.Connection.Close();
            }
        }

        protected void ConditionalRollBackTransaction(DatabaseDisposableContainer container)
        {
            if (container.DisposeNeed)
            {
                container.Transaction.Rollback();
                container.Connection.Close();
            }
        }

        protected void ExecuteAction(ModelTransactionAction action)
        {
            using (var container = ConditionalBeginTransaction())
            {
                try
                {
                    action(container.Connection, container.Transaction);
                    ConditionalCommitTransaction(container);
                }
                catch
                {
                    ConditionalRollBackTransaction(container);
                    throw;
                }
            }
        }

        protected virtual void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        protected virtual void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public virtual void Load()
        {
            ExecuteAction(InternalLoad);
        }

        public virtual void Save()
        {
            ExecuteAction(InternalSave);
        }

        protected SqlCommand CreateDbCommand(SqlConnection connection, SqlTransaction transaction)
        {
            var result = connection.CreateCommand();
            result.Transaction = transaction;
            return result;
        }

        protected void ExecuteWriteCommand(SqlConnection connection, SqlTransaction transaction, string text, Action<SqlCommand> beforeExecuteAction = null)
        {
            using (var command = CreateDbCommand(connection, transaction))
            {
                command.CommandText = text;
                beforeExecuteAction?.Invoke(command);
                command.ExecuteNonQuery();
            }
        }

        protected void ExecuteReadCommand(SqlConnection connection, SqlTransaction transaction, string text, Action<SqlDataReader> action, Action<SqlCommand> beforeExecuteAction = null)
        {
            using (var command = CreateDbCommand(connection, transaction))
            {
                command.CommandText = text;
                beforeExecuteAction?.Invoke(command);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        action(reader);
                }
            }
        }

        public string CreateResult(params object[] values)
        {
            return String.Join(";", values.Select(v => v.ToString()));
        }

        public void FromString(string value)
        {
            var parts = value.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            InternalFromString(parts);
        }

        protected virtual void InternalFromString(string[] parts)
        {
            throw new NotImplementedException();
        }

        public SqlTransaction Transaction { get; set; }
    }

}
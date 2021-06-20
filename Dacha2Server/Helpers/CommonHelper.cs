using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Dacha2Server.Helpers
{
    public class CommonHelper
    {
        public static SqlConnection CreateConnection()
        {
            var result = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString);
            result.Open();
            return result;
        }

        public static T DatabaseAction<T>(Func<SqlTransaction, T> action)
        {
            using (var connection = CommonHelper.CreateConnection())
            using (var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var result = action(transaction);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static int DateTimeToUnixTime(DateTime value)
        {
            return (int)(value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
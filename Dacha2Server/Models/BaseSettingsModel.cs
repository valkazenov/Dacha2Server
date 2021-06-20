using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dacha2Server.Models
{
    public class BaseSettingsModel: BaseModel
    {
        public BaseSettingsModel() : base()
        {
            Applied = 0;
        }

        public virtual void SetApllied()
        {
            ExecuteAction(InternalSetApllied);
        }

        protected virtual void InternalSetApllied(SqlConnection connection, SqlTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Applied.ToString();
        }

        public int Applied { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dacha2Server.Models
{
    public class BasementSettingsModel: BaseSettingsModel
    {
        public BasementSettingsModel(): base()
        {
            BoilerEnabled = 0;
            FanWorkMode = 0;
            FanDisableInterval = 0;
            FanEnableInterval = 0;
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteReadCommand(connection, transaction, "select BoilerEnabled,FanWorkMode,FanDisableInterval,FanEnableInterval,Applied from BasementSettings",
                r =>
                {
                    BoilerEnabled = (int)r["BoilerEnabled"];
                    FanWorkMode = (int)r["FanWorkMode"];
                    FanDisableInterval = (int)r["FanDisableInterval"];
                    FanEnableInterval = (int)r["FanEnableInterval"];
                    Applied = (int)r["Applied"];
                });
        }

        protected override void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            var text =
                "update BasementSettings set " +
                "  BoilerEnabled=@BoilerEnabled, " +
                "  FanWorkMode=@FanWorkMode, " +
                "  FanDisableInterval=@FanDisableInterval, " +
                "  FanEnableInterval=@FanEnableInterval, " +
                "  Applied=0";
            ExecuteWriteCommand(connection, transaction, text, 
                c =>
                {
                    c.Parameters.AddWithValue("@BoilerEnabled", BoilerEnabled);
                    c.Parameters.AddWithValue("@FanWorkMode", FanWorkMode);
                    c.Parameters.AddWithValue("@FanDisableInterval", FanDisableInterval);
                    c.Parameters.AddWithValue("@FanEnableInterval", FanEnableInterval);
                });
        }

        protected override void InternalSetApllied(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteWriteCommand(connection, transaction, "update BasementSettings set Applied=1");
        }

        public override string ToString()
        {
            return CreateResult(BoilerEnabled, FanWorkMode, FanDisableInterval, FanEnableInterval,
                base.ToString());
        }

        protected override void InternalFromString(string[] parts)
        {
            BoilerEnabled = int.Parse(parts[0]);
            FanWorkMode = int.Parse(parts[1]);
            FanDisableInterval = int.Parse(parts[2]);
            FanEnableInterval = int.Parse(parts[3]);
        }

        public int BoilerEnabled { get; set; }
        public int FanWorkMode { get; set; }
        public int FanDisableInterval { get; set; }
        public int FanEnableInterval { get; set; }
    }
}
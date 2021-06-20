using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Dacha2Server.Helpers;

namespace Dacha2Server.Models
{
    public class WateringSettingsModel: BaseSettingsModel
    {
        public WateringSettingsModel()
        {
            ZoneModes = new int[4];
            for (var i = 0; i < 4; i++)
                ZoneModes[i] = 0;
            MinRainPercent = 0;
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteReadCommand(connection, transaction, "select ZoneMode1,ZoneMode2,ZoneMode3,ZoneMode4,MinRainPercent,Applied from WateringSettings",
                r =>
                {
                    ZoneModes[0] = (int)r["ZoneMode1"];
                    ZoneModes[1] = (int)r["ZoneMode2"];
                    ZoneModes[2] = (int)r["ZoneMode3"];
                    ZoneModes[3] = (int)r["ZoneMode4"];
                    MinRainPercent = (int)r["MinRainPercent"];
                    Applied = (int)r["Applied"];
                });
        }

        protected override void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            var text =
                "update WateringSettings set " +
                "  ZoneMode1=@ZoneMode1, " +
                "  ZoneMode2=@ZoneMode2, " +
                "  ZoneMode3=@ZoneMode3, " +
                "  ZoneMode4=@ZoneMode4, " +
                "  MinRainPercent=@MinRainPercent, " +
                "  Applied=0";
            ExecuteWriteCommand(connection, transaction, text,
                c =>
                {
                    c.Parameters.AddWithValue("@ZoneMode1", ZoneModes[0]);
                    c.Parameters.AddWithValue("@ZoneMode2", ZoneModes[1]);
                    c.Parameters.AddWithValue("@ZoneMode3", ZoneModes[2]);
                    c.Parameters.AddWithValue("@ZoneMode4", ZoneModes[3]);
                    c.Parameters.AddWithValue("@MinRainPercent", MinRainPercent);
                });
        }

        protected override void InternalSetApllied(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteWriteCommand(connection, transaction, "update WateringSettings set Applied=1");
        }

        public override string ToString()
        {
            return CreateResult(ZoneModes[0], ZoneModes[1], ZoneModes[2], ZoneModes[3], CommonHelper.DateTimeToUnixTime(DateTime.Now), MinRainPercent,
                base.ToString());
        }

        protected override void InternalFromString(string[] parts)
        {
            ZoneModes[0] = int.Parse(parts[0]);
            ZoneModes[1] = int.Parse(parts[1]);
            ZoneModes[2] = int.Parse(parts[2]);
            ZoneModes[3] = int.Parse(parts[3]);
            MinRainPercent = int.Parse(parts[4]);
        }

        public int[] ZoneModes { get; set; }
        public int MinRainPercent { get; set; }
    }
}
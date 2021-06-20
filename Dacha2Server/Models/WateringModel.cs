using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using Dacha2Server.Helpers;

namespace Dacha2Server.Models
{
    public class WateringModel: BaseLoadModel
    {
        public WateringModel() : base()
        {
            Working = 0;
            Zone = 0;
            StartTime = 0;
            StopTime = 0;
            TankLevel = 0;
            OutsideTemperature = 0;
            OutsideHumidity = 0;
            RainPercent = 0;
            CurrentTime = 0;
            SupplyVoltage = 0;
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteReadCommand(connection, transaction, "select Working,Zone,StartTime,StopTime,TankLevel,OutsideTemperature,OutsideHumidity,RainPercent,CurrentTime,SupplyVoltage,LastSyncTime from WateringData",
                r =>
                {
                    Working = (int)r["Working"];
                    Zone = (int)r["Zone"];
                    StartTime = (int)r["StartTime"];
                    StopTime = (int)r["StopTime"];
                    TankLevel = (int)r["TankLevel"];
                    OutsideTemperature = (double)r["OutsideTemperature"];
                    OutsideHumidity = (double)r["OutsideHumidity"];
                    RainPercent = (int)r["RainPercent"];
                    CurrentTime = (int)r["CurrentTime"];
                    SupplyVoltage = (int)r["SupplyVoltage"];
                    LastSyncTime = (int)r["LastSyncTime"];
                });
        }

        protected override void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            var text =
                "update WateringData set " +
                "  Working=@Working, " +
                "  Zone=@Zone, " +
                "  StartTime=@StartTime, " +
                "  StopTime=@StopTime, " +
                "  TankLevel=@TankLevel, " +
                "  OutsideTemperature=@OutsideTemperature, " +
                "  OutsideHumidity=@OutsideHumidity, " +
                "  RainPercent=@RainPercent, " +
                "  CurrentTime=@CurrentTime, " +
                "  SupplyVoltage=@SupplyVoltage, " +
                "  LastSyncTime=@LastSyncTime";
            ExecuteWriteCommand(connection, transaction, text,
                c =>
                {
                    c.Parameters.AddWithValue("@Working", Working);
                    c.Parameters.AddWithValue("@Zone", Zone);
                    c.Parameters.AddWithValue("@StartTime", StartTime);
                    c.Parameters.AddWithValue("@StopTime", StopTime);
                    c.Parameters.AddWithValue("@TankLevel", TankLevel);
                    c.Parameters.AddWithValue("@OutsideTemperature", OutsideTemperature);
                    c.Parameters.AddWithValue("@OutsideHumidity", OutsideHumidity);
                    c.Parameters.AddWithValue("@RainPercent", RainPercent);
                    c.Parameters.AddWithValue("@CurrentTime", CurrentTime);
                    c.Parameters.AddWithValue("@SupplyVoltage", SupplyVoltage);
                    c.Parameters.AddWithValue("@LastSyncTime", LastSyncTime);
                });
        }

        public override string ToString()
        {
            return CreateResult(Working, Zone, StartTime, StopTime, TankLevel, OutsideTemperature, OutsideHumidity, RainPercent, CurrentTime, SupplyVoltage,
                base.ToString());
        }

        protected override void InternalFromString(string[] parts)
        {
            Working = int.Parse(parts[0]);
            Zone = int.Parse(parts[1]);
            StartTime = int.Parse(parts[2]);
            StopTime = int.Parse(parts[3]);
            TankLevel = int.Parse(parts[4]);
            OutsideTemperature = double.Parse(parts[5], CultureInfo.InvariantCulture);
            OutsideHumidity = double.Parse(parts[6], CultureInfo.InvariantCulture);
            RainPercent = int.Parse(parts[7]);
            CurrentTime = int.Parse(parts[8]);
            SupplyVoltage = int.Parse(parts[9]);
        }

        public int Working { get; set; }
        public int Zone { get; set; }
        public int StartTime { get; set; }
        public int StopTime { get; set; }
        public int TankLevel { get; set; }
        public double OutsideTemperature { get; set; }
        public double OutsideHumidity { get; set; }
        public int RainPercent { get; set; }
        public int CurrentTime { get; set; }
        public int SupplyVoltage { get; set; }
    }
}
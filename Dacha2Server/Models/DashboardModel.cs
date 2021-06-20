using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace Dacha2Server.Models
{
    public class DashboardModel : BaseLoadModel
    {
        public DashboardModel() : base()
        {
            BasementTemperature = 0;
            BasementHumidity = 0;
            BasementFanWorking = 0;
            BasementFanSeconds = 0;
            BasementBoilerWorking = 0;
            BasementBoilerAmperage = 0;
            WateringWorking = 0;
            WateringZone = 0;
            WateringStartTime = 0;
            WateringStopTime = 0;
            WateringTankLevel = 0;
            OutsideTemperature = 0;
            OutsideHumidity = 0;
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            int basementSyncTime = 0; int wateringSyncTime = 0;
            ExecuteReadCommand(connection, transaction, "select Temperature,Humidity,FanWorking,FanSeconds,BoilerWorking,BoilerAmperage,LastSyncTime from BasementData",
                r =>
                {
                    BasementTemperature = (double)r["Temperature"];
                    BasementHumidity = (double)r["Humidity"];
                    BasementFanWorking = (int)r["FanWorking"];
                    BasementFanSeconds = (int)r["FanSeconds"];
                    BasementBoilerWorking = (int)r["BoilerWorking"];
                    BasementBoilerAmperage = (double)r["BoilerAmperage"];
                    basementSyncTime = (int)r["LastSyncTime"];
                });
            ExecuteReadCommand(connection, transaction, "select Working,Zone,StartTime,StopTime,TankLevel,OutsideTemperature,OutsideHumidity,LastSyncTime from WateringData",
                r =>
                {
                    WateringWorking = (int)r["Working"];
                    WateringZone = (int)r["Zone"];
                    WateringStartTime = (int)r["StartTime"];
                    WateringStopTime = (int)r["StopTime"];
                    WateringTankLevel = (int)r["TankLevel"];
                    OutsideTemperature = (double)r["OutsideTemperature"];
                    OutsideHumidity = (double)r["OutsideHumidity"];
                    wateringSyncTime = (int)r["LastSyncTime"];
                });
            LastSyncTime = Math.Min(basementSyncTime, wateringSyncTime);
        }

        public override string ToString()
        {
            return CreateResult(BasementTemperature, BasementHumidity, BasementFanWorking, BasementFanSeconds, BasementBoilerWorking, BasementBoilerAmperage,
                WateringWorking, WateringZone, WateringStartTime, WateringStopTime, WateringTankLevel, OutsideTemperature, OutsideHumidity,
                base.ToString());
        }

        public double BasementTemperature { get; set; }
        public double BasementHumidity { get; set; }
        public int BasementFanWorking { get; set; }
        public int BasementFanSeconds { get; set; }
        public int BasementBoilerWorking { get; set; }
        public double BasementBoilerAmperage { get; set; }
        public int WateringWorking { get; set; }
        public int WateringZone { get; set; }
        public int WateringStartTime { get; set; }
        public int WateringStopTime { get; set; }
        public int WateringTankLevel { get; set; }
        public double OutsideTemperature { get; set; }
        public double OutsideHumidity { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace Dacha2Server.Models
{
    public class BasementModel : BaseLoadModel
    {
        public BasementModel() : base()
        {
            Temperature = 0;
            Humidity = 0;
            BoilerWorking = 0;
            BoilerAmperage = 0;
            FanWorking = 0;
            FanSeconds = 0;
            SupplyVoltage = 0;
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteReadCommand(connection, transaction, "select Temperature,Humidity,FanWorking,FanSeconds,BoilerWorking,BoilerAmperage,SupplyVoltage,LastSyncTime from BasementData",
                r =>
                {
                    Temperature = (double)r["Temperature"];
                    Humidity = (double)r["Humidity"];
                    BoilerWorking = (int)r["BoilerWorking"];
                    BoilerAmperage = (double)r["BoilerAmperage"];
                    FanWorking = (int)r["FanWorking"];
                    FanSeconds = (int)r["FanSeconds"];
                    SupplyVoltage = (int)r["SupplyVoltage"];
                    LastSyncTime = (int)r["LastSyncTime"];
                });
        }

        protected override void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            var text =
                "update BasementData set " +
                "  Temperature=@Temperature, " +
                "  Humidity=@Humidity, " +
                "  BoilerWorking=@BoilerWorking, " +
                "  BoilerAmperage=@BoilerAmperage, " +
                "  FanWorking=@FanWorking, " +
                "  FanSeconds=@FanSeconds, " +
                "  SupplyVoltage=@SupplyVoltage, " +
                "  LastSyncTime=@LastSyncTime";
            ExecuteWriteCommand(connection, transaction, text,
                c =>
                {
                    c.Parameters.AddWithValue("@Temperature", Temperature);
                    c.Parameters.AddWithValue("@Humidity", Humidity);
                    c.Parameters.AddWithValue("@BoilerWorking", BoilerWorking);
                    c.Parameters.AddWithValue("@BoilerAmperage", BoilerAmperage);
                    c.Parameters.AddWithValue("@FanWorking", FanWorking);
                    c.Parameters.AddWithValue("@FanSeconds", FanSeconds);
                    c.Parameters.AddWithValue("@SupplyVoltage", SupplyVoltage);
                    c.Parameters.AddWithValue("@LastSyncTime", LastSyncTime);
                });
        }

        public override string ToString()
        {
            return CreateResult(Temperature, Humidity, BoilerWorking, BoilerAmperage, FanWorking, FanSeconds, SupplyVoltage,
                base.ToString());
        }

        protected override void InternalFromString(string[] parts)
        {
            Temperature = double.Parse(parts[0], CultureInfo.InvariantCulture);
            Humidity = double.Parse(parts[1], CultureInfo.InvariantCulture);
            BoilerWorking = int.Parse(parts[2]);
            BoilerAmperage = double.Parse(parts[3], CultureInfo.InvariantCulture);
            FanWorking = int.Parse(parts[4]);
            FanSeconds = int.Parse(parts[5]);
            SupplyVoltage = int.Parse(parts[6]);
        }

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public int BoilerWorking { get; set; }
        public double BoilerAmperage { get; set; }
        public int FanWorking { get; set; }
        public int FanSeconds { get; set; }
        public int SupplyVoltage { get; set; }
    }
}
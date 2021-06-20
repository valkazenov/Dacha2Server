using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace Dacha2Server.Models
{
    [DebuggerDisplay("WeekDay={WeekDay}, StartHour={StartHour}, StartMinute={StartMinute}, StopHour={StopHour}, StopMinute={StopMinute}")]
    public class WateringTuneTime
    {
        public int WeekDay { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int StopHour { get; set; }
        public int StopMinute { get; set; }
    }

    public class WateringTuneSettingsModel : BaseSettingsModel
    {
        public WateringTuneSettingsModel()
        {
            ZoneNumber = -1;
            Times = new List<WateringTuneTime>();
        }

        protected override void InternalLoad(SqlConnection connection, SqlTransaction transaction)
        {
            Times.Clear(); Applied = 1;
            ExecuteReadCommand(connection, transaction, "select WeekDay,StartHour,StartMinute,StopHour,StopMinute,Applied from WateringTuneSettings where ZoneNumber=@ZoneNumber order by WeekDay,StartHour,StartMinute",
                r =>
                {
                    var time = new WateringTuneTime();
                    time.WeekDay = (int)r["WeekDay"];
                    time.StartHour = (int)r["StartHour"];
                    time.StartMinute = (int)r["StartMinute"];
                    time.StopHour = (int)r["StopHour"];
                    time.StopMinute = (int)r["StopMinute"];
                    Times.Add(time);
                    Applied = (int)r["Applied"];
                },
                c =>
                {
                    c.Parameters.AddWithValue("@ZoneNumber", ZoneNumber);
                });
        }

        protected override void InternalSave(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteWriteCommand(connection, transaction, "delete from WateringTuneSettings where ZoneNumber=@ZoneNumber",
                c =>
                {
                    c.Parameters.AddWithValue("@ZoneNumber", ZoneNumber);
                });
            var text =
                "insert into WateringTuneSettings(ZoneNumber,WeekDay,StartHour,StartMinute,StopHour,StopMinute,Applied) " +
                "  values(@ZoneNumber,@WeekDay,@StartHour,@StartMinute,@StopHour,@StopMinute,0)";
            foreach (var time in Times)
            {
                ExecuteWriteCommand(connection, transaction, text,
                    c =>
                    {
                        c.Parameters.AddWithValue("@ZoneNumber", ZoneNumber);
                        c.Parameters.AddWithValue("@WeekDay", time.WeekDay);
                        c.Parameters.AddWithValue("@StartHour", time.StartHour);
                        c.Parameters.AddWithValue("@StartMinute", time.StartMinute);
                        c.Parameters.AddWithValue("@StopHour", time.StopHour);
                        c.Parameters.AddWithValue("@StopMinute", time.StopMinute);
                    });
            }
        }

        protected override void InternalSetApllied(SqlConnection connection, SqlTransaction transaction)
        {
            ExecuteWriteCommand(connection, transaction, "update WateringTuneSettings set Applied=1 where ZoneNumber=@ZoneNumber",
                c =>
                {
                    c.Parameters.AddWithValue("@ZoneNumber", ZoneNumber);
                });
        }

        public override string ToString()
        {
            var list = new List<object>();
            list.Add(Times.Count);
            foreach (var time in Times)
            {
                list.Add(time.WeekDay);
                list.Add(time.StartHour);
                list.Add(time.StartMinute);
                list.Add(time.StopHour);
                list.Add(time.StopMinute);
            }
            list.Add(base.ToString());
            return CreateResult(list.ToArray());
        }

        protected override void InternalFromString(string[] parts)
        {
            Times.Clear();
            ZoneNumber = int.Parse(parts[0]);
            var count = int.Parse(parts[1]);
            var counter = 2;
            for (var i = 0; i < count; i++)
            {
                var time = new WateringTuneTime();
                time.WeekDay = int.Parse(parts[counter]); counter++;
                time.StartHour = int.Parse(parts[counter]); counter++;
                time.StartMinute = int.Parse(parts[counter]); counter++;
                time.StopHour = int.Parse(parts[counter]); counter++;
                time.StopMinute = int.Parse(parts[counter]); counter++;
                Times.Add(time);
            }
        }

        public int ZoneNumber { get; set; }
        public List<WateringTuneTime> Times { get; set; }
    }
}
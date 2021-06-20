using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dacha2Server.Helpers;

namespace Dacha2Server.Models
{
    public class BaseLoadModel: BaseModel
    {
        public BaseLoadModel(): base()
        {
            LastSyncTime = 0;
        }

        public override string ToString()
        {
            return LastSyncTime.ToString();
        }

        public override void Save()
        {
            LastSyncTime = CommonHelper.DateTimeToUnixTime(DateTime.Now);
            base.Save();
        }

        public int LastSyncTime { get; set; }
    }
}
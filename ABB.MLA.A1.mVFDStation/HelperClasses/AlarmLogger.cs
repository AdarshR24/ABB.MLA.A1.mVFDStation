using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ABB.MLA.A1.mVFDStation.HelperClasses;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class AlarmLogger
    {
        DBConnect db;

        public string StationName { get; set; }
        public AlarmLogger(DBConnect db,string StationName) 
        {
            this.StationName = StationName;
            this.db = db;
        }

        private string statusMessage;

        public string JigNumber { get; set; }

        public string StatusMessage
        {
            get { return statusMessage; }
            set { 
                if(StatusMessage != value)
                {
                    statusMessage = value;
                }
            }
        }


        public void LogAlarm()
        {

        }
    }
}

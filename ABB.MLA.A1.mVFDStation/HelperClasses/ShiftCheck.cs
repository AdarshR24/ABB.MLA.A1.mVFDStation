using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{


    public class ShiftCheck
    {
        public string PrevShift { get; set; }
        public string PrevDate { get; set; }

        public string CurrentShift { get; set; }    

        private double currenttime;
        // get current shift

        DBConnect db;
        public ShiftCheck(DBConnect db)
        {
            this.db = db;
             CheckShift();
        }

        public void GetShiftDetails()
        {
            
        }

        public void CheckShift()
        {
            GetShiftDetails();
            if (DateTime.Now.ToString("dd-MM-yyyy") == PrevDate)
            {

                if (PrevShift == "GENERAL")
                {
                    currenttime = Convert.ToDouble(DateTime.Now.ToString("HH.mm"));
                    if (currenttime >= 8.30 && currenttime < 18.00)
                    {
                        CurrentShift = "GENERAL";
                        return;
                    }
                    else
                    {
                        //ShiftChange sc = new ShiftChange(db);
                        //sc.ShowDialog();
                        //CurrentShift = sc.RunningShift;

                    }
                }
                else if (PrevShift == "FIRST")
                {
                    currenttime = Convert.ToDouble(DateTime.Now.ToString("HH.mm"));
                    if (currenttime >= 6.00 && currenttime < 15.00)
                    {
                        CurrentShift = "FIRST";

                        return;
                    }
                    else
                    {
                        //ShiftChange sc = new ShiftChange(db);
                        //sc.ShowDialog();
                        //CurrentShift = sc.RunningShift;

                    }

                }
                else if (PrevShift == "SECOND")
                {
                    currenttime = Convert.ToDouble(DateTime.Now.ToString("HH.mm"));
                    if (currenttime >= 15.00 && currenttime < 23.30)
                    {
                        CurrentShift = "SECOND";

                        return;
                    }
                    else
                    {
                        //ShiftChange sc = new ShiftChange(db);
                        //sc.ShowDialog();
                        //CurrentShift = sc.RunningShift;

                    }
                }
                else if (PrevShift == "THIRD")
                {
                    currenttime = Convert.ToDouble(DateTime.Now.ToString("HH.mm"));
                    if (currenttime >= 23.30 || currenttime < 6.00)
                    {
                        CurrentShift = "THIRD";

                        return;
                    }
                    else
                    {
                        //ShiftChange sc = new ShiftChange(db);
                        //sc.ShowDialog();
                        //CurrentShift = sc.RunningShift;
                    }

                }
            }
            else
            {
                //ShiftChange sc = new ShiftChange(db);
                //sc.ShowDialog();
                //CurrentShift = sc.RunningShift;
            }
        }

       

    }

}

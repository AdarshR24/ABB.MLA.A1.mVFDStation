using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.MLA.A1.mVFDStation.ErrorHandlers
{
    public class OverAllErrors :INotifyPropertyChanged

    {

        public event EventHandler ErrorListUpdated;
        public OverAllErrors() 
        {
            OverallErrorList = new List<string>();
        }




        private bool _PLCConnectionError = false;

        public bool PLCConnectionError
        {
            get { return _PLCConnectionError; }
            set
            {
                if (_PLCConnectionError != value)
                {
                    _PLCConnectionError = value;
                    AddToList("ERROR: PLC Communication/Connection Failed |", value, OverallErrorList);
                }

            }
        }

        private bool _DBConnectionError = false;


        public bool DBConnectionError
        {
            get { return _DBConnectionError; }
            set
            {

                if (_DBConnectionError != value)
                {
                    _DBConnectionError = value;
                    AddToList("ERROR: Local Database Connection Failed |", value, OverallErrorList);

                }

            }
        }

        private List<string> _overallerrorlist;

        public List<string> OverallErrorList
        {
            get { return _overallerrorlist; }
            set
            {
                if (_overallerrorlist != value)
                {
                    _overallerrorlist = value;
                    OnPropertyChanged(nameof(OverallErrorList));

                }
            }
        }


        private void AddToList(string Error, bool Condition, List<string> Listname)
        {
            int index = Listname.IndexOf(Error);
            if (index > -1 && !Condition)
            {
                Listname.Remove(Error);
            }
            else if (index < 0 && Condition)
            {
                Listname.Add(Error);
            }
            ErrorListUpdated?.Invoke(this, EventArgs.Empty);

        }

        public string GetErrorString()
        {
            string errorString = "";
            if (OverallErrorList.Count > 0)
            {
                foreach (string error in OverallErrorList)
                {
                    errorString = errorString+ error;
                }
            }
            return errorString;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}


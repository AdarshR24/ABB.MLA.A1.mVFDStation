using ABB.MLA.A1.mVFDStation.ErrorHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ABB.MLA.A1.mVFDStation.Model
{
    public class ErrorHandler 
    {
        public OverAllErrors OverallStationErrors;
            
        private static readonly Lazy<ErrorHandler> _instance =
            new Lazy<ErrorHandler>(() => new ErrorHandler());

        public static ErrorHandler Instance => _instance.Value;

        // Private constructor prevents instantiation
        private ErrorHandler()
        {
            OverallStationErrors = new OverAllErrors();
        }

        

       

    }
}

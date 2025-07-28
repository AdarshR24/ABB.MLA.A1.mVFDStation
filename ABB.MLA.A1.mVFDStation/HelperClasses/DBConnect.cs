using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using ABB.MLA.A1.mVFDStation.Model;

namespace ABB.MLA.A1.mVFDStation.HelperClasses
{
    public class DBConnect
    {
        private readonly string _connectionString;

        private bool _myError;

        public bool MyError
        {
            get { return _myError; }
            set 
            { 
                if (_myError != value)
                {
                    _myError = value;
                     ErrorHandler.Instance.OverallStationErrors.DBConnectionError = value;
                }
                
            }
        }

        public int MyException { get; private set; }
        public string MyExceptionMessage { get; private set; }

        public DBConnect()
        {
            _connectionString = App.ConnectionString;
        }

        public void Update(string query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MyError = false;
                }
            }
            catch (Exception ex)
            {
                MyError = true;
                MyExceptionMessage = ex.Message;
                MessageBox.Show(ex.Message);
                Console.WriteLine("");
            }
        }

        public DataTable Select(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    da.Fill(dt);
                    MyError = false;
                }
            }
            catch (Exception ex)
            {
                MyError = true;
                MyExceptionMessage = ex.Message;
                MessageBox.Show(ex.Message);
                throw;

            }

            return dt;
        }
    }
}

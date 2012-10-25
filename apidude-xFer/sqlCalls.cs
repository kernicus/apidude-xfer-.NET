//  apiDude-xFer is a shell program for managing data between mongodb and sql. 
//Copyright (C) 2012  apidude - Kern patton

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace apidude_xFer
{
    public class sqlCalls
    {
        public sqlCalls()
        {
        }

        #region props
        private string _error = string.Empty;

        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        private string _ConnString = string.Empty;

        public string ConnString
        {
            get { return _ConnString; }
            set { _ConnString = value; }
        }
        #endregion

        public static DataTable getRecords(string sp, string Connstring)
        {
            DataTable dt = new DataTable();

            SqlConnection oSqlConn = new SqlConnection(Connstring);
            SqlCommand com = new SqlCommand();

            try
            {
                com.Connection = oSqlConn;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = sp;
                com.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                com.Connection.Close();
            }
            catch (Exception ex)
            {
               string error = "1: " + ex.Message.ToString();
            }
            finally
            {
                if (com.Connection.State == ConnectionState.Open)
                {
                    com.Connection.Close();
                }
            }

            return dt;


        }


    }
}

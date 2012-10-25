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

namespace apidude_xFer
{
    #region xFerLogMessage
    public class xFerLogMessage
    {
        public xFerLogMessage(string message)
        {
            _Message = message;
        }

        #region props
        private string _Message = string.Empty;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private DateTime _dt = new DateTime();

        public DateTime Dt
        {
            get { return _dt; }
            set { _dt = value; }
        }

        #endregion

    }
    #endregion


    public class xFerLog
    {

        public xFerLog(string savePath)
        {
        }


        #region props
        private string _savePath = string.Empty;

        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        private List<xFerLogMessage> _messages = new List<xFerLogMessage>();

        public List<xFerLogMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }


        #endregion

        #region method: openLog
        public void openLog()
        {
            try
            {
                string file = xFerFiles.getFile(_savePath + DateTime.Now.ToShortDateString() +  "_Log.txt");

                if (file.Length > 0)
                {
                    //CONVERT TO OBJECT
                    _messages = LitJson.JsonMapper.ToObject<List<xFerLogMessage>>(file);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("failed to load log");
            }
        }

        public void saveLog()
        {
            string val = LitJson.JsonMapper.ToJson(_messages);
            string file = xFerFiles.createFile(val, _savePath + DateTime.Now.ToShortDateString() + "_Log.txt");           

        }
        #endregion
    }
}

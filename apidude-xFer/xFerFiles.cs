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
using System.IO;

namespace apidude_xFer
{
    public class xFerFiles
    {



        public static string createFile(string file, string filePath)
        {
            StringBuilder ret = new StringBuilder();
            try
            {
                //Creating a new stream-writer and opening the file.
                TextWriter tsw = new StreamWriter(filePath);

                //Writing text to the file.
                tsw.WriteLine(file);

                //Close the file.
                tsw.Close();

            }
            catch (Exception ex)
            {
                ret.Append(ex.Message.ToString());
            }


            return ret.ToString();

        }

        public static string getFile(string filePath)
        {
            StringBuilder ret = new StringBuilder();
            try
            {
                // Delete the file if it exists.
                if (File.Exists(filePath))
                {
                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            ret.AppendLine(s);
                        }
                    }
                }
            }
            catch
            {
            }


            return ret.ToString();

        }


        #region method: openAllFiles
        public static string[] openAllFiles(string filePath)
        {
            StringBuilder ret = new StringBuilder();
            // Delete the file if it exists.
            if (Directory.Exists(filePath))
            {
                // Open the stream and read it back.
                string[] files = Directory.GetFiles(filePath);
                return files;
            }
            else
            {

                return null;
            }
        }


        #endregion

    }

}

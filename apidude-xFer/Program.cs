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
    class Program
    {
        static void Main(string[] args)
        {
            //CHECK TO SEE IF ARGS ARE NULL OR NOT
            string path = "C:\\xFer\\";
            xFerCommands xComm = new xFerCommands(path);
            bool crap = true;

            if (args != null)
            {
                if (args.Length > 0)
                {
                    crap = false;
                    string packageName = string.Empty;
                    string xFerName = string.Empty;
                    string xFerParams = string.Empty;
                    List<xFerParamater> _params = new List<xFerParamater>();
                    for (int i = 0; i < args.Length; i++) // Loop through array
                    {
                        string argument = args[i];
                        if (argument == "-p")
                        {
                            packageName = args[i + 1];
                        }
                        else if (argument == "-xfer")
                        {
                            xFerName = args[i + 1];
                        }
                        else
                        {
                            if (argument == "-params")
                            {
                                if (args.Length > i)
                                {
                                    xFerParams = args[i + i];
                                }
                            }
                        }


                    }

                    if (packageName != string.Empty)
                    {
                        List<xFerParamater> _prams = new List<xFerParamater>();
                        _prams.Add(new xFerParamater("packageName", packageName));
                        if (xFerName != string.Empty)
                        {
                            _prams.Add(new xFerParamater("xFerName", xFerName));
                        }

                        if (xFerParams != string.Empty)
                        {
                            _prams.Add(new xFerParamater("params", xFerParams));
                        }


                        xComm.run(_prams);

                    }

                }
            }

            if (crap)
            {
                Console.WriteLine("*******************************************************");
                Console.WriteLine("***********Welcome to the apidude-xFer shell***********");
                Console.WriteLine("apidude-xFer  Copyright (C) "+DateTime.Now.Year + " Kern Patton");
                Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY; for details type 'show w'.");
                Console.WriteLine("This is free software, and you are welcome to redistribute it");
                Console.WriteLine("under certain conditions; type 'show c' for details.");
                Console.WriteLine("*******************************************************");
            }

            while (crap)
            {
                string r = Console.ReadLine();

                if (r == "exit")
                {
                    Console.WriteLine("exiting");
                    crap = false;
                }
                else if (r.StartsWith("xFer."))
                {
                    //MAKE SURE () was passed
                    if (r.IndexOf("(") > -1 && r.IndexOf(")") > -1)
                    {

                        xComm.main(r);
                    }

                }

                switch (r.ToLower())
                {
                    case "exit":
                        crap = false;
                        Console.WriteLine("exiting");
                        break;
                    case "db":

                        break;
                }

            }

        }
    }
}

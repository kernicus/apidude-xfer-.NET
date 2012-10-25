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
using System.Text.RegularExpressions;
using System.Data;

namespace apidude_xFer
{

    public class xFerCommands
    {
        public xFerCommands(string path)
        {
            initMe(path);
        }

        #region props
        private string _initPath = string.Empty;

        List<xFerPackage> _xFerPackages = new List<xFerPackage>();
        List<xFerDb> _dbs = new List<xFerDb>();

        //private List<xFerParamater> _props = new List<xFerParamater>();

        //public List<xFerParamater> Props
        //{
        //    get { return _props; }
        //    set { _props = value; }
        //}
        #endregion

        #region method: init
        private void initMe(string path)
        {
            //EMPTY PREVIOUS
            _xFerPackages.Clear();
            _initPath = path;
            string[] files = xFerFiles.openAllFiles(path);

            foreach (string file in files)
            {
                if (file.StartsWith(path + "package_"))
                {
                    string sLoad = xFerFiles.getFile(file);
                    if (sLoad.Length > 0)
                    {
                        try
                        {
                            xFerPackage _x = new xFerPackage();
                            _x = LitJson.JsonMapper.ToObject<xFerPackage>(sLoad);
                            
                            if (_x != null)
                            {
                                _xFerPackages.Add(_x);
                                Console.WriteLine("Loaded Package: " + _x.PackageName);

                                foreach (xFer xf in _x.XFers)
                                {
                                    Console.WriteLine("--" + xf.XFerName);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("failed to load file " + file);
                        }
                    }
                }
            }
            //GET Saved DBs

            string[] dbFiles = xFerFiles.openAllFiles(path + "\\db");
            foreach (string db in dbFiles)
            {
                try
                {
                    string dbJSON = xFerFiles.getFile(db);
                    if (dbJSON.Length > 0)
                    {
                        xFerDb _d = new xFerDb();
                        _d = LitJson.JsonMapper.ToObject<xFerDb>(dbJSON);

                        if (_d != null)
                        {
                            _dbs.Add(_d);
                            Console.WriteLine("Loaded DB: " + _d.DbName);
                        }
                    }
                }
                catch
                {
                }
            }

        }
        #endregion

        #region methods: Commands
        public void main(string call)
        {

            //WHICH COMMAND
            string command = string.Empty;
            int len = call.IndexOf("(", 5) - 5;
            command = call.Substring(5, len);


            //CREATE DB COMMAND
            //get input vars
            Regex x = new Regex("(?<=\\().*(?=\\))", RegexOptions.IgnoreCase);
            MatchCollection matches = x.Matches(call);
            List<xFerParamater> _props = new List<xFerParamater>();
            foreach (Match m in matches)
            {
                string sParam = string.Empty;
                xFerParamater iparam = new xFerParamater();
                //NEED TO CHECK FOR PARAMETERS AND REMOVE FIRST//ASSIGN TO END OF PARAMS
                if (m.Value.IndexOf("[") > -1 && m.Value.IndexOf("]") > -1)
                {
                    //REMOVE 
                    string inputParams = string.Empty;
                    int sip = m.Value.IndexOf("[");
                    int end = m.Value.IndexOf("]") + 1;
                    inputParams = m.Value.Substring(sip, end - sip);
                    sParam = m.Value.Replace(inputParams, "");

                    if (inputParams != string.Empty)
                    {
                        iparam.Value = inputParams;
                    }
                }
                else
                {
                    sParam = m.Value.ToString();
                }

                string[] s = sParam.Split(',');

                foreach (string sme in s)
                {
                    if (sme.Trim().Length > 0)
                    {
                        _props.Add(new xFerParamater("", sme.Trim()));
                    }
                }

                if (iparam.Value != string.Empty)
                {
                    _props.Add(iparam);
                }

            }

            
            
           switch (command)
            {
               case "help":
                    help();
                    break;
               case "reload":
                    initMe(_initPath);
                    break;
               case "testConn":
                    testConn(_props);
                    break;
               case "runQuery":
                    if (_props.Count > 0)
                    {
                        runQuery(_props);
                    }
                    break;
               case "run":
                    if (_props.Count > 0)
                    {
                        run(_props);
                    }
                    break;
               case "xFers":
                    if (_props.Count > 0)
                    {
                        xFers(_props);
                    }
                    break;
               case "createxFer":
                    if (_props.Count > 0)
                    {
                        createxFer(_props);
                    }
                    break;
               case "queries":
                    if (_props.Count > 0)
                    {
                        queries(_props);
                    }
                    break;
                case "createQuery":
                    if (_props.Count > 0)
                    {
                        createQuery(_props);
                    }
                    break;
                case "createPackage":
                    if (_props.Count > 0)
                    {
                        createPackage(_props);
                    }
                    break;
                case "packages":
                    packages();
                    break;
                case "createDb":
                    if (_props.Count > 0)
                    {

                        createDb(_props);
                    }
                    else
                    {
                        Console.WriteLine("missing paramaters: ");
                    }
                    break;
                case "dbs":
                    dbs();
                    break;
               default:
                   Console.WriteLine("unkown command.");
                   break;

            }


        }

        #region testConn
        public void testConn(List<xFerParamater> _vars)
        {
            if (_vars.Count > 0)
            {
                string dbName = _vars[0].Value.ToString().Trim();

                foreach (xFerDb db in _dbs)
                {
                    if (db.DbName == dbName)
                    {
                        xFerCalls call = new xFerCalls();
                        call.testConnection(db.ConnectionString, db.DbType);
                    }
                }

            }
            else
            {
                Console.WriteLine("missing paramaters: dbName");
            }
        }
        #endregion


        #region package
        #region packages
        public void packages()
        {
            foreach (xFerPackage p in _xFerPackages)
            {
                Console.WriteLine(p.PackageName);
            }
        }
        #endregion

        #region createPackage
        public void createPackage(List<xFerParamater> _vars)
        {
            bool packageExists = false;            
            //SEE IF PACKAGE ALREADY EXISTS
            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == _vars[0].Value.ToString().Trim())
                {
                    packageExists = true;
                }
            }

            if (!packageExists)
            {
                xFerPackage xPack = new xFerPackage();
                xPack.PackageName = _vars[0].Value.ToString().Trim();
                _xFerPackages.Add(xPack);
                savePackage(xPack.PackageName);
                //xFerFiles.createFile(LitJson.JsonMapper.ToJson(xPack), _initPath + "\\package_" + xPack.PackageName + ".txt");
            }
            else
            {
                if (_vars.Count > 1)
                {
                    foreach (xFerPackage p in _xFerPackages)
                    {
                        if (p.PackageName == _vars[0].Value.ToString().Trim())
                        {
                            //ADD STUFF 
                        }
                    }
                }
            }


        }
        #endregion

        private void savePackage(string packagename)
        {
            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == packagename)
                {
                    string val = LitJson.JsonMapper.ToJson(p);
                    xFerFiles.createFile(val, _initPath + "\\package_" + p.PackageName + ".txt");
                }
            }
        }

        #endregion

        #region method: dbs
        #region method: dbs
        public void dbs()
        {
            //LOAD FILES
            foreach (xFerDb i in _dbs)
            {
                Console.WriteLine("DB:" + i.DbName + " CONN: " + i.ConnectionString + " Type: " + i.DbType);
            }
        }
        #endregion

        #region createDb
        public void createDb(List<xFerParamater> _vars)
        {
            //JSON or COMMA SEPERATED?
            //CHECK LENGTH TO MAKE SURE ENOUGH VARIABLES WERE PASSED
            if (_vars.Count == 4)
            {
                string dbname = _vars[0].Value.ToString();
                string connString = _vars[1].Value.ToString();
                int timeout = Convert.ToInt32(_vars[2].Value.ToString());
                string type = _vars[3].Value.ToString();
                List<xFerParamater> _params = new List<xFerParamater>();

                if (_vars.Count > 4)
                {
                    try
                    {
                        _params = getParametersPassed(_vars[4].Value.ToString());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }
                }

                xFerDb db = new xFerDb();
                db.ConnectionString = connString;
                db.CommandTimeout = timeout;
                switch (type)
                {
                    case "MSSQL":
                        db.DbType = xFerDbType.MSSQL;
                        break;
                    case "MONGO":
                        db.DbType = xFerDbType.mongoDb;
                        break;
                    case "MYSQL":
                        db.DbType = xFerDbType.MySql;
                        break;

                }
                db.DbName = dbname;

                if (_params.Count > 0)
                {
                    //username : value, password : value, isadmin
                    foreach (xFerParamater p in _params)
                    {
                        if (p.Name.Trim().ToLower() == "username")
                        {
                            db.UserName = p.Value.Trim();
                        }
                        else if (p.Name.Trim().ToLower() == "password")
                        {
                            db.Password = p.Value.Trim();
                        }
                        else if (p.Name.ToLower().Trim() == "isadmin")
                        {
                            db.IsAdmin = Convert.ToBoolean(p.Value.Trim().ToLower());
                        }
                    }
                }


                _dbs.Add(db);
                xFerFiles.createFile(LitJson.JsonMapper.ToJson(db), _initPath + "\\db\\db_" + db.DbName + ".txt");
            }
            else
            {
                Console.WriteLine("missing variables");
            }

        }
        #endregion
        #endregion

        #region methods: createxFer
        public void createxFer(List<xFerParamater> _vars)
        {

            string packageName = _vars[0].Value.ToString();
            string xFerName = _vars[1].Value.ToString();
            string sourcedb = _vars[2].Value.ToString();
            string destinationdb = _vars[3].Value.ToString();
            string xFerType = _vars[4].Value.ToString();
            string sourceQuery = _vars[5].Value.ToString();
            string destinationQuery = string.Empty;
            string mongoCollection = _vars[6].Value.ToString();

            if (_vars.Count > 7)
            {
                destinationQuery = _vars[7].Value.ToString();
            }


            xFer _x = new xFer();
            //SEE IF PACKAGE EXISTS
            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == packageName)
                {
                    bool isUpdate = false;
                    foreach (xFer xin in p.XFers)
                    {
                        if (xin.XFerName == xFerName)
                        {
                            isUpdate = true;
                            foreach (xFerDb db in _dbs)
                            {
                                if (db.DbName == sourcedb)
                                {
                                    xin.SourceDb = db;
                                }
                                else if (db.DbName == destinationdb)
                                {
                                    xin.DestinationDb = db;
                                }
                            }

                            xin.xFerType = getTransferType(xFerType);
                            //_x.SourceQuery

                            foreach (xFerQuery q in p.Queries)
                            {
                                if (q.QueryName == sourceQuery)
                                {
                                    xin.SourceQuery = q;
                                }
                                else if (q.QueryName == destinationQuery)
                                {
                                    xin.DestinationQuery = q;
                                }
                            }

                            xin.MongoCollection = mongoCollection;

                            //save pacakge
                            xFerFiles.createFile(LitJson.JsonMapper.ToJson(p), _initPath + "\\package_" + p.PackageName + ".txt");
                        }
                    }

                    if (!isUpdate)
                    {
                        _x.XFerName = xFerName;

                        foreach (xFerDb db in _dbs)
                        {
                            if (db.DbName == sourcedb)
                            {
                                _x.SourceDb = db;
                            }
                            else if (db.DbName == destinationdb)
                            {
                                _x.DestinationDb = db;
                            }
                        }

                        _x.xFerType = getTransferType(xFerType);
                        //_x.SourceQuery

                        foreach (xFerQuery q in p.Queries)
                        {
                            if (q.QueryName == sourceQuery)
                            {
                                _x.SourceQuery = q;
                            }
                            else if (q.QueryName == destinationQuery)
                            {
                                _x.DestinationQuery = q;
                            }
                        }
                        _x.MongoCollection = mongoCollection;
                        p.XFers.Add(_x);
                        //save pacakge
                        xFerFiles.createFile(LitJson.JsonMapper.ToJson(p), _initPath + "\\package_" + p.PackageName + ".txt");
                    }
                }
            }



        }
        #endregion

        #region method: xFers
        public void xFers(List<xFerParamater> _vars)
        {
            string packageName = _vars[0].Value.ToString();

            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == packageName)
                {
                    foreach (xFer x in p.XFers)
                    {
                        Console.WriteLine(x.XFerName);
                        Console.WriteLine("SOURCE: " + x.SourceDb.DbName + " " + x.SourceDb.DbType.ToString());
                        Console.WriteLine("DEST: " + x.DestinationDb.DbName + " " + x.DestinationDb.DbType.ToString());
                        Console.WriteLine("TYPE: " + x.xFerType.ToString());
                        Console.WriteLine("MONGOCOLLECTION: " + x.MongoCollection);
                        Console.WriteLine("SOURCE QUERY: " + x.SourceQuery.QueryName + " TYPE: " + x.SourceQuery.QueryType.ToString() + " TEXT:" + x.SourceQuery.QueryText);
                        if (x.DestinationQuery.QueryName != string.Empty)
                        {
                            Console.WriteLine("DESTINATION QUERY: " + x.DestinationQuery.QueryName + " TYPE: " + x.DestinationQuery.QueryType.ToString() + " TEXT:" + x.DestinationQuery.QueryText);
                        }
                    }
                }
            }
        }
        #endregion


        #region method: createQuery
        public void createQuery(List<xFerParamater> _vars)
        {
            string packageName = _vars[0].Value.ToString().Trim();
            string queryName = _vars[1].Value.ToString().Trim();
            string queryType = _vars[2].Value.ToString().Trim();
            string queryText = _vars[3].Value.ToString().Trim();
            List<xFerParamater> _params = new List<xFerParamater>();
            if (_vars.Count > 4)
            {
                string paramtext = _vars[4].Value.ToString().Trim();
                try
                {
                    _params = getParametersPassed(paramtext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("query parameters failed.");
                }
            }
            
            //SEARCH QUERY FOR EXISTING QUERIES

            bool packageExists = false;
            foreach (xFerPackage _x in _xFerPackages)
            {
                if (_x.PackageName == packageName)
                {
                    packageExists = true;
                    bool exists = false;
                    foreach (xFerQuery _q in _x.Queries)
                    {
                        if (_q.QueryName == queryName)
                        {
                            exists = true;
                            if (queryType == "SP")
                            {
                                _q.QueryType = xFerQueryType.StoredProcedure;
                            }
                            else if (queryType == "T")
                            {
                                _q.QueryType = xFerQueryType.Text;
                            }
                            _q.QueryText = queryText;
                            // newQuery.MongoQuery = "";
                            //newQuery.Paramaters
                            if (_params.Count > 0)
                            {
                                _q.Paramaters = _params;
                            }
                            savePackage(_x.PackageName);
                        }
                    }

                    if (!exists)
                    {
                        xFerQuery newQuery = new xFerQuery();
                        newQuery.QueryName = queryName;
                        if (queryType == "SP")
                        {
                            newQuery.QueryType = xFerQueryType.StoredProcedure;
                        }
                        else if (queryType == "T")
                        {
                            newQuery.QueryType = xFerQueryType.Text;
                        }
                        newQuery.QueryText = queryText;
                        // newQuery.MongoQuery = "";
                        //newQuery.Paramaters
                        if (_params.Count > 0)
                        {
                            newQuery.Paramaters = _params;
                        }

                        _x.Queries.Add(newQuery);

                        

                        savePackage(_x.PackageName);
                    }
                }
            }

            if (!packageExists)
            {
                //CREATE PACKAGE
                xFerPackage xPack = new xFerPackage();
                xPack.PackageName = packageName;
                xFerQuery newQuery = new xFerQuery();
                
                newQuery.QueryName = queryName;
                if (queryType == "SP")
                {
                    newQuery.QueryType = xFerQueryType.StoredProcedure;
                }
                else if (queryType == "T")
                {
                    newQuery.QueryType = xFerQueryType.Text;
                }
                newQuery.QueryText = queryText;
                //newQuery.Paramaters
                if (_params.Count > 0)
                {
                    newQuery.Paramaters = _params;
                }

                xPack.Queries.Add(newQuery);


                _xFerPackages.Add(xPack);
                savePackage(xPack.PackageName);
            }

        }

        #endregion

        #region method: queries
        public void queries(List<xFerParamater> _vars)
        {
            string packageName = _vars[0].Value.ToString().Trim();

            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == packageName)
                {
                    foreach (xFerQuery q in p.Queries)
                    {
                        Console.WriteLine(q.QueryName);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region methods: run/simulate
        public void runQuery(List<xFerParamater> _vars)
        {
            string packageName = _vars[0].Value.ToString();
            string dbName = _vars[1].Value.ToString();
            string xQuery = _vars[2].Value.ToString();
            string collection = string.Empty;
            List<xFerParamater> _params = new List<xFerParamater>();
            if (_vars.Count > 3)
            {
                string paramtext = string.Empty;
                try
                {
                    collection = _vars[3].Value.ToString().Trim();

                    if (collection.IndexOf("[") > -1)
                    {
                        collection = string.Empty;
                        paramtext = _vars[3].Value.ToString().Trim();
                    }


                }
                catch
                {
                }

                if(_vars.Count > 4)
                try
                {
                    paramtext = _vars[4].Value.ToString().Trim();
                }
                catch
                {
                }

                if (paramtext != string.Empty)
                {
                    _params = getParametersPassed(paramtext);
                }
            }


             xFerCalls call = new xFerCalls();
             xFer xMe = new xFer();
             xMe.MongoCollection = collection;
             foreach (xFerPackage p in _xFerPackages)
             {
                 if (p.PackageName == packageName)
                 {
                     foreach (xFerQuery q in p.Queries)
                     {
                         if (q.QueryName == xQuery)
                         {

                             xMe.SourceQuery = q;
                             if (_params.Count > 0)
                             {
                                 xMe.SourceQuery.Paramaters = _params;
                             }
                         }
                     }
                 }

                 foreach (xFerDb db in _dbs)
                 {
                     if (db.DbName == dbName)
                     {
                         xMe.SourceDb = db;
                     }
                 }

             }

             if (xMe.SourceDb.ConnectionString != string.Empty && xMe.SourceQuery.QueryText.ToString() != string.Empty)
             {
                 DataTable dt = call.transfer(xMe);

                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         StringBuilder sbHeader = new StringBuilder();
                         StringBuilder sbSubHeader = new StringBuilder();
                         foreach (DataColumn dc in dt.Columns)
                         {
                             sbHeader.Append(dc.ColumnName.ToString() + "\t");
                             sbSubHeader.Append(returnDots(dc.ColumnName.ToString()) + "\t");
                         }

                         Console.WriteLine(sbHeader.ToString());
                         Console.WriteLine(sbSubHeader.ToString());

                         foreach (DataRow dr in dt.Rows)
                         {
                             StringBuilder sbRow = new StringBuilder();

                             for (int i = 0; i < dt.Columns.Count; i++)
                             {
                                 sbRow.Append(dr[i].ToString() + "\t");
                             }

                             Console.WriteLine(sbRow.ToString());
                         }
                     }
                 }

             }


        }



        public void run(List<xFerParamater> _vars)
        {
            string packageName = _vars[0].Value.ToString();
            string xFerName = string.Empty;

            if (_vars.Count > 1)
            {
                Console.WriteLine(_vars[1].ToString());
                xFerName = _vars[1].Value.ToString();
            }

            List<xFerParamater> _params = new List<xFerParamater>();
            if (_vars.Count > 2)
            {
                try
                {
                    string paramtext = _vars[2].Value.ToString().Trim();
                    _params = getParametersPassed(paramtext);
                    foreach (xFerParamater p in _params)
                    {
                        Console.WriteLine(p.Name + ": " + p.Value);
                    }

                }
                catch
                {
                }
            }

            xFerCalls call = new xFerCalls();
            foreach (xFerPackage p in _xFerPackages)
            {
                if (p.PackageName == packageName)
                {
                    if (xFerName != string.Empty)
                    {
                        foreach (xFer x in p.XFers)
                        {
                            if (x.XFerName == xFerName)
                            {
                                if (_params.Count > 0)
                                {
                                    x.SourceQuery.Paramaters = _params;
                                }
                                Console.WriteLine("Run: " + x.XFerName);
                                call.transfer(x);
                            }
                        }

                    }
                    else
                    {
                        foreach (xFer x in p.XFers)
                        {
                            Console.WriteLine("Run: " + x.XFerName);
                            call.transfer(x);
                        }
                    }
                }
            }

        }
        #endregion



        #region get command vars
        public void help(string name)
        {
            
        }

        #endregion

        private xFerTransferType getTransferType(string type)
        {
            xFerTransferType t = xFerTransferType.ADD;
            switch (type)
            {
                case "ADD":
                    t = xFerTransferType.ADD;
                    break;
                case "COMPARE":
                    t = xFerTransferType.COMPARE;
                    break;
                case "MAPREDUCE":
                    t = xFerTransferType.MAPREDUCE;
                    break;
                case "REPLACE":
                    t = xFerTransferType.REPLACE;
                    break;
            }

            return t;
        }


        private List<xFerParamater> getParametersPassed(string inputVal)
        {
            List<xFerParamater> _params = new List<xFerParamater>();
            if (inputVal.Length > 0)
            {
                string paramtext = inputVal;
                try
                {
                    LitJson.JsonReader reader = new LitJson.JsonReader(paramtext);
                    string name = string.Empty;
                    object val = new object();

                    while (reader.Read())
                    {
                        if (name != string.Empty)
                        {
                            val = reader.Value;
                            _params.Add(new xFerParamater(name, val.ToString()));
                            name = string.Empty;

                        }
                        else
                        {
                            if (reader.Token.ToString() == "PropertyName")
                            {
                                name = reader.Value.ToString();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("query parameters failed.");
                }
            }

            return _params;

        }


        private string returnDots(string val)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < val.Length; i++)
            {
                sb.Append(".");
            }

            return sb.ToString();
        }

        private void help()
        {
            Console.WriteLine("xFer.createPackage(PACKAGENAME)");

            Console.WriteLine("xFer.packages()");

            Console.WriteLine("xFer.createQuery(packagename, queryName, queryType, queryText, [paramaters optional])");
            
            Console.WriteLine("xFer.queries(packagename)");
            

            Console.WriteLine("xFer.CreatexFer(pacakagename, xFerName, sourceDbName, destinationDbName, xFerType, sourceQueryName, mongoCollection [OPTIONAL], desitnationQueryName [OPTIONAL])");

            Console.WriteLine("xFer.xFer(packagename)");

            Console.WriteLine("xFer.run(packagename, xferName)"); 

            Console.WriteLine("xFer.run(packagename)");

            Console.WriteLine("xFer.runQuery(pacakgename, DbName, queryName, Collection, paramaters)");

            Console.WriteLine("xFer.reload()");
        }



    }
}

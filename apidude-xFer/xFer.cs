
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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MySql.Data.MySqlClient;

namespace apidude_xFer
{
    #region xFerPackage
    public class xFerPackage
    {
        public xFerPackage()
        {
        }

        #region props
        private string _packageName = string.Empty;

        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        private List<xFer> _xFers = new List<xFer>();

        public List<xFer> XFers
        {
            get { return _xFers; }
            set { _xFers = value; }
        }
        private List<xFerJob> _jobs = new List<xFerJob>();

        public List<xFerJob> Jobs
        {
            get { return _jobs; }
            set { _jobs = value; }
        }
        private List<xFerQuery> _Queries = new List<xFerQuery>();

        public List<xFerQuery> Queries
        {
            get { return _Queries; }
            set { _Queries = value; }
        }

        #endregion

    }

    #endregion

    #region enums
    public enum xFerDbType
    {
        mongoDb,
        MSSQL,
        MySql
    }

    public enum xFerQueryType
    {
        Text, StoredProcedure
    }

    public enum xFerTransferType
    {
        ADD, REPLACE, COMPARE, MAPREDUCE
    }
    #endregion

    #region xFerJob
    public class xFerJob
    {
        public xFerJob()
        {
        }

        #region props
        private List<xFer> _xFers = new List<xFer>();

        public List<xFer> XFers
        {
            get { return _xFers; }
            set { _xFers = value; }
        }


        private string _jobName = string.Empty;

        public string JobName
        {
            get { return _jobName; }
            set { _jobName = value; }
        }
        
        //SCHEDULE
        //EMAIL NOTIFICATIONS
        //FAIL, SUCCESS
        //
        #endregion


        #region method: save
        public bool save()
        {
            bool _didSave = false;



            return _didSave;
        }
        #endregion


    }
    #endregion

    #region xFer
    public class xFer
    {
        public xFer()
        {
        }

        #region props
        /// <summary>
        /// Source DataBase xFerDb object.
        /// </summary>
        private xFerDb _sourceDb = new xFerDb();

        public xFerDb SourceDb
        {
            get { return _sourceDb; }
            set { _sourceDb = value; }
        }

        /// <summary>
        /// Destination DataBase  xFerDb object.
        /// </summary>
        private xFerDb _destinationDb = new xFerDb();

        public xFerDb DestinationDb
        {
            get { return _destinationDb; }
            set { _destinationDb = value; }
        }

        /// <summary>
        /// Type of transfer to commit. Defaults to Replace
        /// </summary>
        private xFerTransferType _xFerType = xFerTransferType.REPLACE;

        public xFerTransferType xFerType
        {
            get { return _xFerType; }
            set { _xFerType = value; }
        }

        private xFerQuery _sourceQuery = new xFerQuery();

        public xFerQuery SourceQuery
        {
            get { return _sourceQuery; }
            set { _sourceQuery = value; }
        }
        private xFerQuery _DestinationQuery = new xFerQuery();

        public xFerQuery DestinationQuery
        {
            get { return _DestinationQuery; }
            set { _DestinationQuery = value; }
        }


        private string _xFerName = string.Empty;

        public string XFerName
        {
            get { return _xFerName; }
            set { _xFerName = value; }
        }

        private string _mongoCollection = string.Empty;

        public string MongoCollection
        {
            get { return _mongoCollection; }
            set { _mongoCollection = value; }
        }

        #endregion
    }
    #endregion

    #region xFerDb

    public class xFerDb
    {
        public xFerDb()
        {
        }


        #region props
        private string _dbName = string.Empty;

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        //private string _dbCollectionName = string.Empty;

        //public string DbCollectionName
        //{
        //    get { return _dbCollectionName; }
        //    set { _dbCollectionName = value; }
        //}

        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
        private int _ConnectionTimeout = 300;

        public int ConnectionTimeout
        {
            get { return _ConnectionTimeout; }
            set { _ConnectionTimeout = value; }
        }
        private int _CommandTimeout = 300;

        public int CommandTimeout
        {
            get { return _CommandTimeout; }
            set { _CommandTimeout = value; }
        }
        private xFerDbType _dbType = xFerDbType.MySql;

        public xFerDbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        private string _UserName = string.Empty;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Password = string.Empty;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private bool _isAdmin = false;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        #endregion

    }
    #endregion

    #region xFerMapReduce
    public class xFerMapReduce
    {
        public xFerMapReduce()
        {
        }

        #region props
        private string _MapReduceName = string.Empty;

        private string _map = string.Empty;
        private string _reduce = string.Empty;


        #endregion

    }
    #endregion

    #region xFerQuery
    public class xFerQuery
    {
        public xFerQuery()
        {
        }

        #region props
        private string _queryText = string.Empty;

        public string QueryText
        {
            get { return _queryText; }
            set { _queryText = value; }
        }
        private xFerQueryType _queryType = xFerQueryType.StoredProcedure;

        public xFerQueryType QueryType
        {
            get { return _queryType; }
            set { _queryType = value; }
        }
        private List<xFerParamater> _paramaters = new List<xFerParamater>();

        public List<xFerParamater> Paramaters
        {
            get { return _paramaters; }
            set { _paramaters = value; }
        }

        //private string _mongoCollection = string.Empty;

        //public string MongoCollection
        //{
        //    get { return _mongoCollection; }
        //    set { _mongoCollection = value; }
        //}

        private string _queryName = string.Empty;

        public string QueryName
        {
            get { return _queryName; }
            set { _queryName = value; }
        }

        #endregion

    }
    #endregion

    #region xFerCompare
    public class xFerCompare
    {
        public xFerCompare()
        {
        }

        #region props
        //FIELDS TO COMPARE //OR ALL
        //WHAT TO DO //UPDATE //INSERT
        //NEED QUERY FOR BOTH DATA PULLS

        //ALLOW FOR COMPLEXE QUERY BASED MATCHING?

        //COMPARE SOURCE TO DESTINATION
        //A INSERT MISSING ROWS IN DESTINATION
        //B INSERT MISSING ROWS IN SOURCE
        //C UPDATE DIFFERENT ROWS IN DESTINATION
        //D UPDATE DIFFERENT ROWS IN SOURCE
        //DELETE?
        #endregion


    }
    #endregion

    #region xFerParamater
    public class xFerParamater{

        public xFerParamater()
        {
        }

        public xFerParamater(string Name, string Value)
        {
            _Name = Name;
            _Value = Value;
        }



        #region props
        private string _Name = string.Empty;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Value = string.Empty;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        #endregion
    }
    #endregion

    #region dbCalls
    public class xFerCalls
    {
        public xFerCalls()
        {
        }

        #region props
        private List<string> _errors = new List<string>();

        public List<string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        private string _Query = "{}";

        private int _Limit = 0;
        private string _Sort = "{}";

        private string _Field = "{}";

        #endregion

        #region method: transfer
        public DataTable transfer(xFer x)
        {
            //TEST CONNECTIONS
            bool sourceConn = testConnection(x.SourceDb.ConnectionString, x.SourceDb.DbType);
            bool destConn = false;
            if (x.DestinationDb.ConnectionString.Trim().Length > 0)
            {
                destConn = testConnection(x.DestinationDb.ConnectionString, x.DestinationDb.DbType);
            }
            DataTable dt = new DataTable();

            if (!sourceConn)
            {
                _errors.Add("Source Connection Failed.");
                Console.WriteLine("Source Connection Failed: " + x.SourceDb.DbName);
            }
            if (!destConn && x.DestinationDb.ConnectionString.Trim().Length > 0)
            {
                _errors.Add("Destination Connection Failed.");
                Console.WriteLine("Destination Connection Failed: " + x.SourceDb.DbName);
            }

            if(sourceConn)
            {

                dt = getData(x.SourceDb, x.SourceQuery, x.MongoCollection);


                if (destConn)
                {
                    if (dt.Rows.Count > 0)
                    {
                        putData(x.DestinationDb, dt, x.xFerType, x.DestinationQuery, x.MongoCollection);
                    }
                }
                else
                {
                    
                }

            }
            return dt;
        }

        #endregion

        #region method: testConnection
        public bool testConnection(string ConnString, xFerDbType type)
        {
            bool _conn = false;
            if (ConnString != null)
            {
                if (ConnString.Length > 0)
                {
                    switch (type)
                    {
                        case xFerDbType.mongoDb:
                            _conn = mongoConnTest(ConnString);
                            break;
                        case xFerDbType.MSSQL:
                            _conn = sqlConnTest(ConnString);
                            break;
                        case xFerDbType.MySql:
                            _conn = MySqlConnTest(ConnString);
                            break;
                    }
                }
            }

            return _conn;
        }
        #endregion

        #region method: connectionTests
        public bool mongoConnTest(string ConnString)
        {
            bool _conn = false;
            try
            {
                MongoServer server = MongoServer.Create(ConnString);
                _conn = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message.ToString());
            }

            return _conn;
        }

        private bool sqlConnTest(string ConnString)
        {
            bool _conn = false;
            SqlConnection oSqlConn = new SqlConnection(ConnString);
            SqlCommand com = new SqlCommand();

            try
            {
                com.Connection = oSqlConn;
                com.Connection.Open();
                _conn = true;
                com.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message.ToString());
            }
            finally
            {
                if (com.Connection.State == ConnectionState.Open)
                {
                    com.Connection.Close();
                }
            }

            return _conn;
        }

        private bool MySqlConnTest(string ConnString)
        {
            bool _conn = false;
            //SqlConnection oSqlConn = new SqlConnection(ConnString);
            //SqlCommand com = new SqlCommand();
            MySqlConnection oSqlConn = new MySqlConnection(ConnString);
            MySqlCommand com = new MySqlCommand();
            try
            {
                com.Connection = oSqlConn;
                com.Connection.Open();
                _conn = true;
                com.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message.ToString());
            }
            finally
            {
                if (com.Connection.State == ConnectionState.Open)
                {
                    com.Connection.Close();
                }
            }

            return _conn;
        }
        #endregion

        #region method: getData
        private DataTable getData(xFerDb x, xFerQuery q, string mongoCollection)
        {
            DataTable dt = new DataTable();

            switch (x.DbType)
            {
                case xFerDbType.MSSQL:
                    dt = sqlGetData(q.QueryText.ToString(), x.ConnectionString, q.Paramaters, q.QueryType, x.CommandTimeout);
                    break;
                case xFerDbType.mongoDb:
                    dt = getMongoData(x, q, mongoCollection);
                    break;
                case xFerDbType.MySql:
                    dt = sqlMyGetData(q.QueryText.ToString(), x.ConnectionString, q.Paramaters, q.QueryType, x.CommandTimeout);
                    break;
            }
            

            return dt;
        }
        #endregion

        #region method: getMongoData
        public DataTable getMongoData(xFerDb db, xFerQuery q, string mongoCollection)
        {
            DataTable dt = new DataTable();

            if (q.QueryText != null)
            {
                try
                {
                    MongoServer server = MongoServer.Create(db.ConnectionString);
                    MongoDatabase database = server.GetDatabase(db.DbName);

                    MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(mongoCollection);
                    setQuery(q.QueryText.ToString());
                    QueryDocument _q = new QueryDocument();
                    if (q.QueryText.ToString() == string.Empty)
                    {
                        q.QueryText = "{}";
                    }

                    BsonDocument query = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(_Query);

                    _q.Add(query);

                    FieldsDocument _fields = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<FieldsDocument>(_Field);

                    MongoCursor<BsonDocument> _ret = collection.Find(_q).SetFields(_fields).SetSortOrder(_Sort).SetLimit(_Limit);

                    bool isSet = false;
                    foreach (BsonDocument i in _ret)
                    {

                        if (!isSet)
                        {
                            foreach (BsonElement mei in i.Elements)
                            {
                                dt.Columns.Add(mei.Name);
                                isSet = true;
                            }
                        }


                        DataRow _dr = dt.NewRow();

                        foreach (BsonElement mei in i.Elements)
                        {
                            _dr[mei.Name] = mei.Value;
                        }

                        dt.Rows.Add(_dr);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                }
            }

            return dt;
        }
        #endregion

        #region method: getSqlData
        public DataTable sqlGetData(string sp, string ConnString, List<xFerParamater> oPrams,  xFerQueryType qt, int CommandTimeout)
        {
            DataTable dt = new DataTable();
            CommandType ct = CommandType.StoredProcedure;

            if (qt == xFerQueryType.Text)
            {
                ct = CommandType.Text;
            }

            if (sp != string.Empty && ConnString != string.Empty)
            {
                SqlConnection oSqlConn = new SqlConnection(ConnString);
                SqlCommand com = new SqlCommand();

                try
                {
                    com.Connection = oSqlConn;
                    com.CommandTimeout = CommandTimeout;
                    com.Connection = oSqlConn;
                    com.CommandType = ct;
                    com.CommandText = sp;
                   
                    if (oPrams != null)
                    {
                        foreach (xFerParamater p in oPrams)
                        {
                            com.Parameters.AddWithValue(p.Name, p.Value);
                        }
                    }

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(dt);
                    com.Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                }
                finally
                {
                    if (com.Connection.State == ConnectionState.Open)
                    {
                        com.Connection.Close();
                    }
                }
            }

            Console.WriteLine("returned from sql: " + dt.Rows.Count);

            return dt;
        }
        #endregion

        #region method: getMySqlData
        public DataTable sqlMyGetData(string sp, string ConnString, List<xFerParamater> oPrams, xFerQueryType qt, int CommandTimeout)
        {
            DataTable dt = new DataTable();
            CommandType ct = CommandType.StoredProcedure;
            
            if (qt == xFerQueryType.Text)
            {
                ct = CommandType.Text;
            }

            if (sp != string.Empty && ConnString != string.Empty)
            {
                MySqlConnection oSqlConn = new MySqlConnection(ConnString);
                MySqlCommand com = new MySqlCommand();
                try
                {
                    com.Connection = oSqlConn;
                    com.CommandTimeout = CommandTimeout;
                    com.Connection = oSqlConn;
                    com.CommandType = ct;
                    com.CommandText = sp;

                    if (oPrams != null)
                    {
                        foreach (xFerParamater p in oPrams)
                        {
                            com.Parameters.AddWithValue(p.Name, p.Value);
                        }
                    }

                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(dt);
                    com.Connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                }
                finally
                {
                    if (com.Connection.State == ConnectionState.Open)
                    {
                        com.Connection.Close();
                    }
                }
            }

            Console.WriteLine("returned from sql: " + dt.Rows.Count);

            return dt;
        }
        #endregion


        #region method: putData
        private void putData(xFerDb db, DataTable dt, xFerTransferType t, xFerQuery xOut, string mongoCollection)
        {
            switch (db.DbType)
            {
                case xFerDbType.mongoDb:
                    putMongoDBData(db, dt, t, mongoCollection);
                    break;
                case xFerDbType.MSSQL:
                    putSqlDbData(db, dt, t, xOut);
                    break;
                case xFerDbType.MySql:
                    putMySqlDbData(db, dt, t, xOut);
                    break;
            }
        }
        #endregion

        #region method: putMongoDBData
        private void putMongoDBData(xFerDb db, DataTable dt, xFerTransferType t, string mongoCollection)
        {

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        Console.WriteLine("Insert mongodb begin");
                        MongoServer server = MongoServer.Create(db.ConnectionString);

                        MongoDatabase database = server.GetDatabase(db.DbName); // "test" is the name of the database


                        MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(mongoCollection);

                        if (t == xFerTransferType.REPLACE)
                        {
                            collection.RemoveAll();
                        }


                        List<BsonDocument> docs = new List<BsonDocument>();


                        foreach (DataRow dr in dt.Rows)
                        {
                            BsonDocument d = new BsonDocument();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                Type colType = dr[dc.ColumnName].GetType();
                                string val = string.Empty;
           
                                if (colType.FullName != "System.String")
                                {
                                    if (colType.FullName == "System.DateTime")
                                    {
                                        d.Add(dc.ColumnName.ToString(), Convert.ToDateTime(dr[dc.ColumnName].ToString()));
                                    }
                                    else if (colType.FullName == "System.Int32")
                                    {
                                        d.Add(dc.ColumnName.ToString(), Convert.ToInt32(dr[dc.ColumnName].ToString()));
                                    }
                                    else if (colType.FullName == "System.Int64")
                                    {
                                        d.Add(dc.ColumnName.ToString(), Convert.ToInt64(dr[dc.ColumnName].ToString()));
                                    }
                                    else if (colType.FullName == "System.Boolean")
                                    {
                                        d.Add(dc.ColumnName.ToString(), Convert.ToBoolean(dr[dc.ColumnName].ToString()));
                                    }
                                    else if (colType.FullName == "System.Decimal")
                                    {
                                        d.Add(dc.ColumnName.ToString(), Convert.ToDouble(dr[dc.ColumnName].ToString()));
                                    }
                                }
                                else
                                {
                                    d.Add(dc.ColumnName.ToString(), dr[dc.ColumnName].ToString());
                                }
                            }

                            docs.Add(d);
                        }


                        collection.InsertBatch(docs);
                        Console.WriteLine("Insert mongodb finish: rows added to " + db.DbName + "." + mongoCollection + "(" + dt.Rows.Count + ")");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message.ToString());
                    }

                }
                else
                {
                    Console.WriteLine("Source failed to return data.");
                }
            }
            else
            {
                Console.WriteLine("Source failed to return data.");
            }
        }
        #endregion

        #region method: putSqlDbData
        public void putSqlDbData(xFerDb db, DataTable dt, xFerTransferType t, xFerQuery q)
        {
            CommandType ct = CommandType.StoredProcedure;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (q.QueryType == xFerQueryType.Text)
                    {
                        ct = CommandType.Text;
                    }

                    if (q.QueryText.ToString() != string.Empty && db.ConnectionString != string.Empty)
                    {
                        SqlConnection oSqlConn = new SqlConnection(db.ConnectionString);
                        SqlCommand com = new SqlCommand();

                        try
                        {
                            com.Connection = oSqlConn;
                            com.CommandTimeout = db.CommandTimeout;
                            com.Connection = oSqlConn;
                            com.CommandType = ct;
                            com.CommandText = q.QueryText.ToString();

                            com.Connection.Open();


                            foreach (DataRow dr in dt.Rows)
                            {
                                if (com.Parameters.Count > 0)
                                {
                                    com.Parameters.Clear();
                                }
                                if (q.Paramaters != null)
                                {
                                    if (q.Paramaters.Count == 0)
                                    {
                                        foreach (DataColumn c in dt.Columns)
                                        {
                                            q.Paramaters.Add(new xFerParamater(c.ColumnName.Trim(), ""));
                                        }
                                    }


                                    foreach (xFerParamater p in q.Paramaters)
                                    {
                                        try
                                        {
                                            com.Parameters.AddWithValue("@" + p.Name, dr[p.Name].ToString());
                                        }
                                        catch
                                        {
                                            _errors.Add("Insert missing column " + p.Name);
                                        }
                                    }
                                }

                                com.ExecuteNonQuery();
                            }


                            com.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message.ToString());
                        }
                        finally
                        {
                            if (com.Connection.State == ConnectionState.Open)
                            {
                                com.Connection.Close();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Source failed to return data.");
                }
            }
            else
            {
                Console.WriteLine("Source failed to return data.");
            }
        }
        #endregion


        #region method: putMySqlDbData
        public void putMySqlDbData(xFerDb db, DataTable dt, xFerTransferType t, xFerQuery q)
        {
            CommandType ct = CommandType.StoredProcedure;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (q.QueryType == xFerQueryType.Text)
                    {
                        ct = CommandType.Text;
                    }

                    if (q.QueryText.ToString() != string.Empty && db.ConnectionString != string.Empty)
                    {
                        MySqlConnection oSqlConn = new MySqlConnection(db.ConnectionString);
                         MySqlCommand com = new MySqlCommand();
                        try
                        {
                            com.Connection = oSqlConn;
                            com.CommandTimeout = db.CommandTimeout;
                            com.Connection = oSqlConn;
                            com.CommandType = ct;
                            com.CommandText = q.QueryText.ToString();

                            com.Connection.Open();


                            foreach (DataRow dr in dt.Rows)
                            {
                                if (com.Parameters.Count > 0)
                                {
                                    com.Parameters.Clear();
                                }

                                if (q.Paramaters != null)
                                {
                                    if (q.Paramaters.Count == 0)
                                    {
                                        foreach (DataColumn c in dt.Columns)
                                        {
                                            q.Paramaters.Add(new xFerParamater(c.ColumnName.Trim(), ""));
                                        }
                                    }


                                    if (q.Paramaters.Count > 0)
                                    {
                                        foreach (xFerParamater p in q.Paramaters)
                                        {
                                            try
                                            {
                                                com.Parameters.AddWithValue("@" + p.Name, dr[p.Name].ToString());
                                            }
                                            catch
                                            {
                                                _errors.Add("Insert missing column " + p.Name);
                                            }
                                        }
                                    }
                                }

                                com.ExecuteNonQuery();
                            }


                            com.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message.ToString());
                        }
                        finally
                        {
                            if (com.Connection.State == ConnectionState.Open)
                            {
                                com.Connection.Close();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Source failed to return data.");
                }
            }
            else
            {
                Console.WriteLine("Source failed to return data.");
            }
        }
        #endregion


        #region setquery
        public void setQuery(string query)
        {
            string limit = "{}";
            int[] ip = new int[4] { query.IndexOf("$Query"), query.IndexOf("$Field"), query.IndexOf("$Limit"), query.IndexOf("$Sort") };
            int[] ipsort = new int[4] { query.IndexOf("$Query"), query.IndexOf("$Field"), query.IndexOf("$Limit"), query.IndexOf("$Sort") };
            Array.Sort(ipsort);

            _Query = getQueryParams("$Query", 0, query, ip, ipsort);
            _Field = getQueryParams("$Field", 1, query, ip, ipsort);
            limit = getQueryParams("$Limit", 2, query, ip, ipsort).Replace("{", "").Replace("}", "").Trim();
            try
            {
                _Limit = Convert.ToInt32(limit);
            }
            catch
            {

            }
            _Sort = getQueryParams("$Sort", 3, query, ip, ipsort);
        }


        private string getQueryParams(string inputquery, int index, string val, int[] ip, int[] ipsort)
        {
            string ret = "{}";
            if (ip[index] > -1)
            {
                //HAS QUERY FIND NEXT HIGHEST NUMBER
                for (int i = 0; i < ipsort.Length; i++)
                {
                    if (ip[index] == ipsort[i])
                    {
                        if (i == ipsort.Length - 1)
                        {
                            ret = val.Substring(ip[index], val.Length - ip[index]).Replace(inputquery + " : ", "").Replace(inputquery + ":", "").Trim();
                        }
                        else
                        {
                            //GET NEXT NUM
                            ret = val.Substring(ip[index], ipsort[i + 1] - ip[index]).Replace(inputquery + " : ", "").Replace(inputquery + ":", "").Trim();
                        }
                    }
                }
            }
            return ret;
        }
        #endregion


    }




    #endregion
}

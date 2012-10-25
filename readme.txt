
    apiDude-xFer is a shell program for managing data between mongodb and sql. 
    Copyright (C) 2012  apidude - Kern patton

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.


apiDude-xFer
This is a command shell application for transfering data between mS SQL server and MongoDB. 

Conventions:
DB - you create DBs to connect to and move data to and from.
xFer - this is the transfer setup. It has a source db, destination DB and queries for managing both. 

Package - a package is a collection of xFers and assocated queries. Which can be automated on the run command below. 

Queries - a query is a set of commands and properties to manage data from or going into a data source. 


xFer.createDb(DbName, ConnString, Timeout, TYPE, parameters)
--Name - For MSSQL This is for reference only and is used to assign to an xFer. For MONGODB this is your DB name to use in your query.
--ConnString - this is the connection string for connecting to your data source Example: mongodb://localhost/?safe=true
--TimeOut - This is only for SQL server and controls the command timeout. 
--Type - Two Values MSSQL or MONGO

EXAMPLE: xFer.createDb(test, mongodb://localhost/?safe=true, 300, MONGO)
I have a local copy of mongodb that has a database of named test. 

xFer.dbs()
--Returns all saved dbs


xFer.createPackage(PACKAGENAME)

xFer.packages()
--returns all created pacakges

xFer.createQuery(packagename, queryName, queryType, queryText, paramaters)
-- Name of previously created package.
-- Name of Query - for reference
-- QUeryType - SP for Stored Procedure, T - For Text.
-- QueryText - Name of Stored Procedure or SQL query. For mongo - if this is being used as a select statement put your query in the form of a find command. 
-- Paramaters - For source queries (MySql, MSSQL only), this is for passing set input paramaters in the value of a JSON array of string value pairs. [ { "Name" : "NameOfParm", "Value" : "ValueOfParam" }] , for destination queries - this is used for limiting the input fields to use returned by your source. [ { "Name" : "NameOfColumnToMatch", "Value" : 1 }]


MONGODB select EXAMPLE:
{ $Query : { bookmarkname: \"google\" , bookmarkid : { $gt : 20}}, $Field : { bookmarkUrl : true }, $Sort : { bookmarkname : 0 }, $Limit : { 10 }}


xFer.queries(packagename)
--returns all queries for a given package


xFer.CreatexFer(pacakagename, xFerName, sourceDbName, destinationDbName, xFerType, sourceQueryName, mongoCollection, desitnationQueryName)
--name of created package - if doesn't exist it will get created.
--xFerName - name of the xFer.
--SourceDBName - name of saved DB.
--DestinationDbName - name of saved DB.
--xFerType - values - ADD, REPLACE (more to come)
--sourceQueryName - name of query to use against your sourceDB.
--mongoCollection - the name of your mongo collection to use for either source or destination query. if the collection doesn't exist (on insert) it will be created. 
-- destinationQueryName - (OPTIONAL) for managing data into your destination DB. if not specified for mongodb, what is returned from the source query/source db is inserted into the destination db/collection. 


xFer.xFer(packagename)
--returns all xFer Jobs created by package name


xFer.run(packagename, xferName) 
- executes the named package xfer. 

xFer.run(packagename)
--executes all xFers for the given package. 

xFer.runQuery(pacakgename, DbName, queryName, Collection, paramaters)
-- Package Name
-- DBName
-- QueryName
-- Collection (OPTIONAL) - if this query is running against mongodb. 
-- Paramaters (OPTIONAL) - if your query was created with default paramaters - you can override the default paramater value. [ { "inputParam", "inputValue" }]

xFer.reload() 
--reloads the xFer source files. Usefull if you make manual changes. 

xFer.help()

xFer.exit


From the CMD tool you can execute pacakages. 
C:\xFer\apidude-xfer -p [PackageName]

or as specific xFer 
C:\xFer\apidude-xfer -p [PackageName] -xfer [xFerName]




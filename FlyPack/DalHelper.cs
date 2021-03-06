﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace FlyPack
{
    public class DalHelper
    {

        /// <summary>
        /// runs an insert SQL statement
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <returns>id of newly inserted row, -1 if it didn't work</returns>
        public static int Insert(string sql)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);


            if (!helper.OpenConnection()) throw new ConnectionException();

            int id = helper.InsertWithAutoNumKey(sql);
            helper.CloseConnection();

            return id;
        }

        /// <summary>
        /// queries a select statement on the SQL access database
        /// </summary>
        /// <param name="sql">sql query to run</param>
        /// <returns>DataTable containing the results</returns>
        public static DataTable Select(string sql)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);


            if (!helper.OpenConnection()) throw new ConnectionException();

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();

            return tb;
        }

        /// <summary>
        /// a method to check if a sql select statement has an existing row
        /// </summary>
        /// <param name="sql">select sql query to run</param>
        /// <returns>true if row exists</returns>
        public static bool RowExists(string sql)
        {
            return Select(sql).Rows.Count > 0;
        }

        /// <summary>
        /// returns ta single row from a table by id
        /// </summary>
        /// <param name="id">id of the row requested</param>
        /// <param name="table">name of SQL table</param>
        /// <returns>DataRow containing the row</returns>
        public static DataRow GetRowById(string id, string table)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table} WHERE ID ='{id}'";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();

            if (tb.Rows.Count < 1) return null;

            return tb.Rows[0];
        }

        /// <summary>
        /// Returns a random row from a table
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">name of a column in the table, will only look at rows that have this</param>
        /// <returns>DataRow containing the results</returns>
        public static DataRow Random(string table, string column)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT top 1 * from {table} ORDER BY rnd({column})";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            if (tb.Rows.Count == 0) return null;
            return tb.Rows[0];
        }

        /// <summary>
        /// Returns a random row from a table with a where clause
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">column to compare</param>
        /// <param name="value">value that is required for the row</param>
        /// <returns>DataRow containing the results</returns>
        public static DataRow RandomWhere(string table, string column, int value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT top 1 * from {table} WHERE {column} = {value} ORDER BY rnd({column})";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            if (tb.Rows.Count == 0) return null;
            return tb.Rows[0];
        }

        /// <summary>
        /// Returns a random row from a table with a where clause
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">column to compare</param>
        /// <param name="value">value that is required for the row</param>
        /// <returns>DataRow containing the results</returns>
        public static DataRow RandomWhere(string table, string column, string value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT top 1 * from {table} WHERE {column} = '{value}' ORDER BY rnd({column})";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            if (tb.Rows.Count == 0) return null;
            return tb.Rows[0];
        }



        /// <summary>
        /// Returns row from a table with a where clause
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">column to compare</param>
        /// <param name="value">value that is required for the rows</param>
        /// <returns>DataRow containing the matching row</returns>
        public static DataRow RowWhere(string table, string column, string value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table} WHERE `{column}` = '{value}'";

            DataTable tb = helper.GetDataTable(sql);
            if (tb.Rows.Count == 0) return null;
            helper.CloseConnection();
            return tb.Rows[0];
        }        /// <summary>
                 /// Returns row from a table with a where clause
                 /// </summary>
                 /// <param name="table">name of SQL table</param>
                 /// <param name="column">column to compare</param>
                 /// <param name="value">value that is required for the rows</param>
                 /// <returns>DataRow containing the matching row</returns>
        public static DataRow RowWhere(string table, string column, int value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table} WHERE `{column}` = {value}";

            DataTable tb = helper.GetDataTable(sql);
            if (tb.Rows.Count == 0) return null;
            helper.CloseConnection();
            return tb.Rows[0];
        }

        /// <summary>
        /// Returns a table of al rows from a table with a where clause
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">column to compare</param>
        /// <param name="value">value that is required for the rows</param>
        /// <returns>DataTable containing the matching rows</returns>
        public static DataTable AllWhere(string table, string column, int value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table} WHERE `{column}` = {value}";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            return tb;
        }

        /// <summary>
        /// Returns a table of al rows from a table with a where clause
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <param name="column">column to compare</param>
        /// <param name="value">value that is required for the rows</param>
        /// <returns>DataTable containing the matching rows</returns>
        public static DataTable AllWhere(string table, string column, string value)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table} WHERE `{column}` = '{value}'";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            return tb;
        }

        /// <summary>
        /// returns all rows from a table
        /// </summary>
        /// <param name="table">name of SQL table</param>
        /// <returns>DataTable containing all rows of a table</returns>
        public static DataTable AllFromTable(string table)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"SELECT * FROM {table}";

            DataTable tb = helper.GetDataTable(sql);
            helper.CloseConnection();
            return tb;
        }
        /// <summary>
        /// delete row by id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <param name="idColumnName"></param>
        /// <returns>true if delete success</returns>
        public static bool DeleteRowById(int id,string table,string idColumnName)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);

            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"DELETE * FROM {table} WHERE {idColumnName}={id}";
           return  helper.WriteData(sql)==1;
        }
        /// <summary>
        /// update one files by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="table"></param>
        /// <param name="idColumnName"></param>
        /// <param name="replaceValue"></param>
        /// <param name="replacedColumName"></param>
        /// <returns>true if update success</returns>
        public static bool UpdateColumnById(int id, string table, string idColumnName,string replaceValue, string replacedColumName)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);
            helper.CloseConnection();
            if (!helper.OpenConnection()) throw new ConnectionException();
            string sql = $"UPDATE {table} SET {replacedColumName}={replaceValue} WHERE {idColumnName}={id}";
            return helper.WriteData(sql) == 1;
        }
    }
}

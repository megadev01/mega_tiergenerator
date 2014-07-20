using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TierGenerator.Common;

namespace TierGenerator.DatabaseSchema
{
    public class SqlDatabaseSchema : IDatabaseSchema
    {

        private string _connectionString = string.Empty;

        /// <summary>
        /// Method to Test Connection
        /// </summary>
        /// <param name="connectionString">Connection string </param>
        /// <returns></returns>
        public static bool TestConnection(string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return false;
        }

        #region  Implement IDatabaseSchema

        /// <summary>
        /// load the database schema
        /// </summary>
        /// <param name="databaseServer">name of the database server</param>
        /// /// <param name="catalog">name of the catalog</param>
        /// <param name="connectionString">connection string</param>
        /// <returns>database schema</returns>
        public Database GetDataBaseSchema(string databaseServer, string catalog, string connectionString)
        {
            _connectionString = connectionString;


            Database database = new Database();
            database.ConnectionString = connectionString;
            database.DatabaseServer = databaseServer;
            database.Catalog = catalog;

            // Populate tables in it.
            PopulateTables(database);

            return database;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Populate the Tables
        /// </summary>
        /// <param name="database"></param>
        private void PopulateTables(Database database)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE " +
                                     "FROM INFORMATION_SCHEMA.TABLES";

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseTable databaseTable = new DatabaseTable();
                    databaseTable.TableSchema = reader.GetString(1);
                    databaseTable.TableName = reader.GetString(2);
                    databaseTable.TableType = reader.GetString(3);

                    // Populate Column
                    PopulateTableColumns(databaseTable);


                    // Add into the list
                    database.Tables.Add(databaseTable);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

        }

        /// <summary>
        /// Populate the Table column
        /// </summary>
        /// <param name="databaseTable">Table</param>
        private void PopulateTableColumns2(DatabaseTable databaseTable)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * " +
                                     "FROM INFORMATION_SCHEMA.COLUMNS " +
                                     "WHERE TABLE_NAME = '" + databaseTable.TableName + "'";

            try
            {
                DataTable dtKeys = GetPrimaryKeyAndForeignKey(databaseTable.TableName);

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    DatabaseColumn databaseColumn = new DatabaseColumn();
                    databaseColumn.Name = reader["COLUMN_NAME"].ToString();
                    databaseColumn.OrdinalPosition = Convert.ToInt32(reader["ORDINAL_POSITION"]);
                    databaseColumn.DataType = reader["DATA_TYPE"].ToString();
                    databaseColumn.ColumnSize = (reader["CHARACTER_MAXIMUM_LENGTH"] == DBNull.Value) ? null : (long?)(Convert.ToInt64(reader["CHARACTER_MAXIMUM_LENGTH"]));
                    databaseColumn.IsNull = (reader["IS_NULLABLE"].ToString().ToUpper() == "YES") ? true : false;
                    bool isPK = false;
                    bool isFK = false;
                    SetColumnKeys(dtKeys, databaseColumn.Name, out isPK, out isFK);
                    databaseColumn.IsPK = isPK;
                    databaseColumn.IsFK = isFK;
                    //databaseColumn.IsAutoNumber                      
                    //databaseColumn.VsDataType  



                    // Add into the list
                    databaseTable.Columns.Add(databaseColumn);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }


        }

        /// <summary>
        /// Populate the Table column
        /// </summary>
        /// <param name="databaseTable">Table</param>
        private void PopulateTableColumns(DatabaseTable databaseTable)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT TOP 0 * FROM " + databaseTable.TableName;

            try
            {
                DataTable dtKeys = GetPrimaryKeyAndForeignKey(databaseTable.TableName);

                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                DataTable dtSchema = sqlReader.GetSchemaTable();

                foreach (DataRow row in dtSchema.Rows)
                {
                    DatabaseColumn databaseColumn = new DatabaseColumn();
                    databaseColumn.Name = row["ColumnName"].ToString();
                    databaseColumn.OrdinalPosition = Convert.ToInt32(row["ColumnOrdinal"]);
                    databaseColumn.DataType = row["DataTypeName"].ToString();
                    databaseColumn.ColumnSize = (row["ColumnSize"] == DBNull.Value) ? null : (long?)(Convert.ToInt64(row["ColumnSize"]));
                    databaseColumn.IsNull = (bool)row["AllowDBNull"];
                    bool isPK = false;
                    bool isFK = false;
                    SetColumnKeys(dtKeys, databaseColumn.Name, out isPK, out isFK);
                    databaseColumn.IsPK = isPK;
                    databaseColumn.IsFK = isFK;
                    databaseColumn.IsAutoNumber = (bool)row["Isidentity"];
                    databaseColumn.IsReadonly = (bool)row["IsReadOnly"];

                    databaseColumn.CSharpDataTypeName = GetCSharpTypeName(row["DataType"].ToString(), databaseColumn.IsNull);
                    databaseColumn.DotNetDataTypeName = GetDotNetTypeName(row["DataType"].ToString()); //row["ProviderSpecificDataType"].ToString();


                    // Add into the list
                    databaseTable.Columns.Add(databaseColumn);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }


        }

        /// <summary>
        /// get the primary and forigen keys of the table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable GetPrimaryKeyAndForeignKey(string tableName)
        {
            string sqlQuery =
            "SELECT	  k.table_name, k.column_name 'COLUMN_NAME', c.constraint_type 'CONSTRAINT_TYPE', " +
                     "c.CONSTRAINT_NAME, ccu.table_name 'REFERENCE_TABLE', ccu.column_name 'REFERENCE_COLUMN'  " +
            "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE k " +
            "LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS c " +
                  "ON k.table_name = c.table_name " +
                      "AND k.table_schema = c.table_schema " +
                      "AND k.table_catalog = c.table_catalog " +
                      "AND k.constraint_catalog = c.constraint_catalog " +
                      "AND k.constraint_name = c.constraint_name " +
            "LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc " +
                   "ON rc.constraint_schema = c.constraint_schema " +
                      "AND rc.constraint_catalog = c.constraint_catalog " +
                      "AND rc.constraint_name = c.constraint_name " +
            "LEFT JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu " +
                  "ON rc.unique_constraint_schema = ccu.constraint_schema " +
                      "AND rc.unique_constraint_catalog = ccu.constraint_catalog " +
                      "AND rc.unique_constraint_name = ccu.constraint_name " +
            "WHERE k.constraint_catalog = DB_NAME() " +
                  "AND k.table_name = '" + tableName + "' " +
            "ORDER BY k.constraint_name, k.ordinal_position";


            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            SqlDataAdapter sqlDataAdopter = new SqlDataAdapter(sqlCommand);

            try
            {
                sqlConnection.Open();
                DataTable dtToReturn = new DataTable();

                sqlDataAdopter.Fill(dtToReturn);
                return dtToReturn;



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return null;
        }

        /// <summary>
        /// Method to set the column keys
        /// </summary>
        /// <param name="dtKeys"></param>
        /// <param name="databaseColumn"></param>
        private void SetColumnKeys(DataTable dtKeys, string columnName, out bool isPK, out bool isFK)
        {
            // Primary KEY
            DataRow[] pkRows = dtKeys.Select("COLUMN_NAME ='" + columnName + "' AND CONSTRAINT_TYPE='PRIMARY KEY'");
            isPK = (pkRows != null && pkRows.Length > 0);

            // Foreign KEY
            DataRow[] fkRows = dtKeys.Select("COLUMN_NAME ='" + columnName + "' AND CONSTRAINT_TYPE='FOREIGN KEY'");
            isFK = (fkRows != null && fkRows.Length > 0);

        }

        /// <summary>
        /// Method to get C Sharp Type Name
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public string GetCSharpTypeName(string csTypeName, bool isNull)
        {
            string typeName = csTypeName;

            if (Type.GetType(csTypeName) == typeof(string))
            {
                typeName = "string";
            }
            else if (Type.GetType(csTypeName) == typeof(int))
            {
                typeName = "int";
            }
            else if (Type.GetType(csTypeName) == typeof(bool))
            {
                typeName = "bool";
            }
            else if (Type.GetType(csTypeName) == typeof(long))
            {
                typeName = "long";
            }
            else if (Type.GetType(csTypeName) == typeof(byte[]))
            {
                typeName = "byte[]";
            }
            else if (Type.GetType(csTypeName) == typeof(DateTime))
            {
                typeName = "DateTime";
            }
            else if (Type.GetType(csTypeName) == typeof(decimal))
            {
                typeName = "decimal";
            }
            else if (Type.GetType(csTypeName) == typeof(double))
            {
                typeName = "double";
            }
            else if (Type.GetType(csTypeName) == typeof(Single))
            {
                typeName = "float";
            }
            else if (Type.GetType(csTypeName) == typeof(object))
            {
                typeName = "object";
            }
            else if (Type.GetType(csTypeName) == typeof(byte))
            {
                typeName = "byte";
            }
            else if (Type.GetType(csTypeName) == typeof (Int16))
            {
                typeName = "short";
            }
            else if (Type.GetType(csTypeName) == typeof(Guid))
            {
                typeName = "Guid";
            }
            


            else
            {
                Type type = Type.GetType(csTypeName);
                typeName = type.Name;
            }


            // Check for Null
            if (isNull && typeName != "string" && typeName != "byte[]" && typeName != "object")
            {
                typeName += "?";
            }

            return typeName;
        }

        /// <summary>
        /// Get dot net type name
        /// </summary>
        /// <param name="csTypeName"></param>
        /// <returns></returns>
        public string GetDotNetTypeName(string csTypeName)
        {
            Type t = Type.GetType(csTypeName);
            return t.Name;
        }
        #endregion

    }
}

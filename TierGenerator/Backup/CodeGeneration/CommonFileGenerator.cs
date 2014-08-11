using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using TierGenerator.Properties;
using TierGenerator.Common;

namespace TierGenerator.CodeGeneration
{
    class CommonFileGenerator
    {

        #region Properties

        public string RootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// method to generate app.config
        /// </summary>
        public void GenerateAppDotConfig()
        {

            string fileText = Resources.app;
            fileText = fileText.Replace(ProjectTokens.ConnectionString, TierGeneratorSettings.Instance.ConnectionString);


            FileWriter.WriteFile(RootPath, "app.Config", fileText);
        }

        /// <summary>
        /// method to generate Sql Store procedures
        /// </summary>
        public void GenerateSqlStoreProcedures()
        {

            string filePath = RootPath +
                              System.IO.Path.DirectorySeparatorChar +
                              TierGeneratorSettings.Instance.ProjectNameSpace +
                              "_StoredProcedures.sql";

            using (StreamWriter sw = new StreamWriter(filePath))
            {

                sw.WriteLine("SET NOCOUNT ON");
                sw.WriteLine("GO");
                sw.WriteLine("USE [" + TierGeneratorSettings.Instance.Database.Catalog + "]");
                sw.WriteLine("GO");


                foreach (DatabaseTable table in TierGeneratorSettings.Instance.Database.Tables)
                {
                    if (table.IsSelected)
                    {

                        // Insert
                        InsertSP(sw, table);

                        // UPdate
                        UpdateSP(sw, table);

                        // Select By Primary Key
                        SelectByPrimaryKeySP(sw, table);

                        // Select All
                        SelectAllSP(sw, table);

                        // Select By Field
                        SelectByFieldSP(sw, table);

                        // Delete by Primary key
                        DeleteByPrimaryKeySP(sw, table);

                        // Delete by field
                        DeleteByFieldSP(sw, table);
                    }
                }
            }
        }

        
        #endregion

        #region Private Methods

        #region Store Procedures

        /// <summary>
        /// Get parameters
        /// </summary>
        /// <param name="column">column name</param>
        /// <returns>paramter string</returns>
        private string GetParameter(DatabaseColumn column)
        {
            string parameter = column.SqlParameterName + " " + column.DataType;

            switch (column.DataType)
            {
                case "binary":
                case "char":
                case "nchar":
                case "nvarchar":
                case "varbinary":
                case "varchar":
                    {
                        string size = (column.ColumnSize >= 2147483647) ? "MAX" : column.ColumnSize.Value.ToString();
                        parameter += "(" + size + ")";
                        break;
                    }
            }


            if (column.IsNull)
                parameter += " = null";

            return parameter;

        }

        /// <summary>
        /// Method to get delete store procedure statement
        /// </summary>
        /// <param name="storeProcedures">name of store procedure</param>
        /// <returns>statement</returns>
        private string GetDeleteExistProcedureSQL(string storeProcedures)
        {
            return "if exists (select * from dbo.sysobjects where id = object_id(N'" + storeProcedures + "') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure " + storeProcedures + "\r\nGO";
        }

        /// <summary>
        /// Method to generate insert store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void InsertSP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.Insert);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            StringBuilder strInputParams = new StringBuilder();
            StringBuilder strOutputParams = new StringBuilder();
            StringBuilder strFields = new StringBuilder();
            StringBuilder strValues = new StringBuilder();
            StringBuilder strOutSelect = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                string pType = column.IsAutoNumber ? "output" : "";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameter(column) + " " + pType + end);

                if (!column.IsAutoNumber)
                {
                    // Fileds
                    strFields.AppendLine("\t[" + column.Name + "]" + end);

                    // Values
                    strValues.AppendLine("\t" + column.SqlParameterName + end);
                }

                // Output select
                if (column.IsAutoNumber)
                {
                    strOutSelect.AppendLine("\tSELECT " + column.SqlParameterName + "=SCOPE_IDENTITY();");
                }

            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("INSERT [" + table.TableSchema + "].[" + table.TableName + "]");
            sw.WriteLine("(");

            // Fileds
            sw.WriteLine(strFields.ToString());

            sw.WriteLine(")");
            sw.WriteLine("VALUES");
            sw.WriteLine("(");

            // Values
            sw.WriteLine(strValues.ToString());

            sw.WriteLine(")");

            sw.WriteLine(strOutSelect.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate Update store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void UpdateSP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.Update);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            StringBuilder strInputParams = new StringBuilder();
            StringBuilder strSet = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                if (i == count - 1) end = "";

                // Parameters
                strInputParams.AppendLine("\t" + GetParameter(column) + end);

                // Set Value
                if (!column.IsAutoNumber)
                {
                    if (strSet.Length > 0) strSet.Append(",\r\n");
                    strSet.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }



            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("UPDATE [" + table.TableSchema + "].[" + table.TableName + "]");
            sw.WriteLine("SET");

            // Set Values
            sw.WriteLine(strSet.ToString());

            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate select by primary key store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void SelectByPrimaryKeySP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.SelectByPrimaryKey);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            StringBuilder strInputParams = new StringBuilder();
            StringBuilder strParams = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];

                // Parameters
                if (column.IsPK)
                {
                    if (strInputParams.Length > 0) strInputParams.Append(",\r\n");
                    strInputParams.Append("\t" + GetParameter(column));
                }

                // Select Parameters
                if (strParams.Length > 0) strParams.Append(", ");
                strParams.Append("[" + column.Name + "]");


                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }



            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("\tSELECT ");
            sw.WriteLine("\t\t" + strParams.ToString());
            sw.WriteLine("\tFROM [" + table.TableSchema + "].[" + table.TableName + "]");

            // where clause
            sw.WriteLine("\tWHERE ");

            // Where
            sw.WriteLine("\t\t" + strWhere.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate select All store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void SelectAllSP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.SelectAll);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));


            StringBuilder strParams = new StringBuilder();


            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];


                // Select Parameters
                if (strParams.Length > 0) strParams.Append(", ");
                strParams.Append("[" + column.Name + "]");

            }

            sw.WriteLine("CREATE PROCEDURE " + spName);
            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("\tSELECT ");
            sw.WriteLine("\t\t" + strParams.ToString());
            sw.WriteLine("\tFROM [" + table.TableSchema + "].[" + table.TableName + "]");

            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate select by field store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void SelectByFieldSP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.SelectByField);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            StringBuilder strParams = new StringBuilder();



            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];

                // Select Parameters
                if (strParams.Length > 0) strParams.Append(", ");
                strParams.Append("[" + column.Name + "]");

            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine("\t@FieldName varchar(100),");
            sw.WriteLine("\t@Value varchar(1000)");

            sw.WriteLine("AS");
            sw.WriteLine("");
            sw.WriteLine("\tDECLARE @query varchar(2000);");
            sw.WriteLine("");
            sw.WriteLine("\tSET @query = 'SELECT " + strParams.ToString() + " FROM [" + table.TableSchema + "].[" + table.TableName + "] WHERE [' + @FieldName  + '] = ''' + @Value + ''''");
            sw.WriteLine("\tEXEC(@query)");
            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate delete store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void DeleteByPrimaryKeySP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.DeleteByPrimaryKey);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));

            StringBuilder strInputParams = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();

            int count = table.Columns.Count;

            for (int i = 0; i < count; i++)
            {
                DatabaseColumn column = table.Columns[i];

                // Parameters
                if (column.IsPK)
                {
                    if (strInputParams.Length > 0) strInputParams.Append(",\r\n");
                    strInputParams.Append("\t" + GetParameter(column));
                }

                // Where
                if (column.IsPK)
                {
                    if (strWhere.Length > 0) strWhere.Append(" AND \r\n");
                    strWhere.Append("\t[" + column.Name + "] = " + column.SqlParameterName);
                }



            }

            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine(strInputParams.ToString());

            sw.WriteLine("AS");
            sw.WriteLine("");

            sw.WriteLine("DELETE FROM [" + table.TableSchema + "].[" + table.TableName + "]");
            // where clause
            sw.WriteLine(" WHERE ");

            // Where
            sw.WriteLine(strWhere.ToString());

            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        /// <summary>
        /// Method to generate delete by field store procedure
        /// </summary>
        /// <param name="sw">stream writer</param>
        /// <param name="table">Database table</param>
        private void DeleteByFieldSP(StreamWriter sw, DatabaseTable table)
        {
            string spName = TierGeneratorSettings.Instance.GetStoreProcedureName(table.TableSchema, table.TableName, StoreProcedureType.DeleteByField);
            sw.WriteLine();
            sw.WriteLine(GetDeleteExistProcedureSQL(spName));


            sw.WriteLine("CREATE PROCEDURE " + spName);

            // Parameters
            sw.WriteLine("\t@FieldName varchar(100),");
            sw.WriteLine("\t@Value varchar(1000)");

            sw.WriteLine("AS");
            sw.WriteLine("");
            sw.WriteLine("\tDECLARE @query varchar(2000);");
            sw.WriteLine("");
            sw.WriteLine("\tSET @query = 'DELETE FROM [" + table.TableSchema + "].[" + table.TableName + "] WHERE [' + @FieldName  + '] = ''' + @Value + ''''");
            sw.WriteLine("\tEXEC(@query)");
            sw.WriteLine("");
            sw.WriteLine("GO");

        }

        #endregion

        #endregion

    }
}

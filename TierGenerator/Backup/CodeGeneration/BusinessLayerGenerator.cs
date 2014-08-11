using System;
using System.Collections.Generic;
using System.Text;
using TierGenerator.Properties;
using TierGenerator.Common;
using System.IO;
using System.Data;

namespace TierGenerator.CodeGeneration
{
    /// <summary>
    /// generate the Data layer
    /// </summary>
    public class BusinessLayerGenerator
    {

        #region Data Members



        #endregion

        #region Properties

        public string BusinessLayerRootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace + ".Business";

            }
        }

        public string PropertiesPath
        {
            get
            {
                return BusinessLayerRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "Properties";

            }
        }

        public string DataLayerPath
        {
            get
            {
                return BusinessLayerRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "DataLayer";

            }
        }

        public string ValidationPath
        {
            get
            {
                return BusinessLayerRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "Validation";

            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessLayerGenerator()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// generate the code for Business layer and data layer.
        /// </summary>
        public void Generate()
        {
            // generate assembly file
            GenerateAssemblyFile();

            // generate Data Layer
            GenerateDataLayer();

            // Generate validation
            GenerateValidation();

            // Method to generate business layer
            GenerateBusinessLayer();

            // Generate project File
            GenerateBusinessLayerProjectFile();
        }

        #endregion

        #region Private Methods

        #region Assembly File

        /// <summary>
        /// method to generate assembly file
        /// </summary>
        private void GenerateAssemblyFile()
        {

            string fileText = Resources.AssemblyInfo;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            FileWriter.WriteFile(PropertiesPath, "AssemblyInfo.cs", fileText);

        }


        #endregion

        #region Data Layer

        /// <summary>
        /// generate the files for the data layer
        /// </summary>
        private void GenerateDataLayer()
        {
            Database database = TierGeneratorSettings.Instance.Database;
            string fileText = string.Empty;

            #region generate DataLayer Base

            fileText = Resources.DataLayer_DataLayerBase;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            FileWriter.WriteFile(DataLayerPath, "DataLayerBase.cs", fileText);

            #endregion

            #region Generate DataLayer

            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                    GenerateDataLayerClass(table);
            }


            #endregion

        }

        /// <summary>
        /// generate Data layer class
        /// </summary>
        /// <param name="table">database table</param>
        private void GenerateDataLayerClass(DatabaseTable table)
        {
            string fileText = Resources.DataLayer_EntitySql;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            fileText = fileText.Replace(ProjectTokens.ClassName, table.ClassName);

            fileText = fileText.Replace(ProjectTokens.TableName, table.TableName);
            fileText = fileText.Replace(ProjectTokens.TableSchema, table.TableSchema);
            fileText = fileText.Replace(ProjectTokens.SpPrefix, TierGeneratorSettings.Instance.StoreProcedurePrefix);


            #region Insert Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlInsertParameter, GetParameterForInsertOrUpdate(table, true));
            fileText = fileText.Replace(ProjectTokens.EntitySqlGetReturnedValue, GetOutputParameterForInsert(table));

            #endregion

            #region Update Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlUpdateParameter, GetParameterForInsertOrUpdate(table, false));

            #endregion

            #region Select By Primary Key

            fileText = fileText.Replace(ProjectTokens.EntitySqlSelectByPkParameter, GetParameterForPrimaryKey(table));

            #endregion

            #region Populate Object From Reader

            fileText = fileText.Replace(ProjectTokens.EntitySqlPopulateBusinessObjectParameter, GetPopulateObjectFromReader(table));

            #endregion

            FileWriter.WriteFile(DataLayerPath, table.ClassName + "Sql.cs", fileText);

        }

        /// <summary>
        /// Method to get parameters
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetParameterForInsertOrUpdate(DatabaseTable table, bool isInsert)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "\t\t\t\tsqlCommand.Parameters.Add(new SqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            foreach (DatabaseColumn column in table.Columns)
            {

                string dir = "Input";
                if (isInsert)
                {
                    dir = column.IsAutoNumber ? "Output" : "Input";
                }
                string dbType = GetSqlDbType(column.DataType);
                strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "businessObject." + column.PropertyName));

            }

            return strToReturn.ToString();

        }

        /// <summary>
        /// Method to get parameters
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetParameterForPrimaryKey(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "\t\t\t\tsqlCommand.Parameters.Add(new SqlParameter(\"{0}\", {1}, {2}, ParameterDirection.{3}, false, 0, 0, \"\", DataRowVersion.Proposed, {4}));";
            foreach (DatabaseColumn column in table.Columns)
            {
                if (column.IsPK)
                {
                    string dir = "Input";

                    string dbType = GetSqlDbType(column.DataType);
                    strToReturn.AppendLine(string.Format(format, column.SqlParameterName, dbType, column.ColumnSize, dir, "keys." + column.PropertyName));
                }
            }

            return strToReturn.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetOutputParameterForInsert(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();

            string format = "{0}.{1} = ({2})sqlCommand.Parameters[\"{3}\"].Value;";
            foreach (DatabaseColumn column in table.Columns)
            {
                if (!column.IsAutoNumber) continue;

                strToReturn.AppendLine(string.Format(format, "businessObject", column.PropertyName, column.CSharpDataTypeName, column.SqlParameterName));

            }

            return strToReturn.ToString();

        }

        /// <summary>
        /// populate the object from reader
        /// </summary>
        /// <param name="table">Data base table</param>
        /// <returns></returns>
        private string GetPopulateObjectFromReader(DatabaseTable table)
        {
            StringBuilder strToReturn = new StringBuilder();
            string className = table.ClassName;
            string formatForNUll = "\t\t\t\tif (!dataReader.IsDBNull(dataReader.GetOrdinal({0}.{0}Fields.{1}.ToString())))";
            string assignFormat = "\t\t\t\tbusinessObject.{0} = dataReader.Get{1}(dataReader.GetOrdinal({2}.{2}Fields.{0}.ToString()));";
            string assignSpecialFormat = "\t\t\t\tbusinessObject.{0} = ({1})dataReader.Get{2}(dataReader.GetOrdinal({3}.{3}Fields.{0}.ToString()));";

            foreach (DatabaseColumn column in table.Columns)
            {
                strToReturn.AppendLine("");

                if (column.IsNull)
                {
                    strToReturn.AppendLine(string.Format(formatForNUll, className, column.PropertyName));
                    strToReturn.AppendLine("\t\t\t\t{");
                    strToReturn.Append("\t");
                }

                string srlType = column.DotNetDataTypeName;
                if (srlType == "Byte[]" || srlType == "Object" || srlType == "Int16")
                {                    
                    string convertType = (srlType == "Int16") ? column.CSharpDataTypeName : "System." + srlType;
                    string sType = (srlType == "Int16") ? "Int16" : "Value";
                    strToReturn.AppendLine(string.Format(assignSpecialFormat, column.PropertyName, convertType, sType, className));
                }
                else
                {
                    if (srlType == "Single")
                        srlType = "Float";

                    strToReturn.AppendLine(string.Format(assignFormat, column.PropertyName, srlType, className));
                }

                if (column.IsNull)
                {
                    strToReturn.AppendLine("\t\t\t\t}");
                }

            }

            return strToReturn.ToString();
        }

        /// <summary>
        /// Method to get the sql DB type enumeration
        /// </summary>
        /// <param name="sqlType">sql type name</param>
        /// <returns>SqlDbType.type</returns>
        private string GetSqlDbType(string sqlType)
        {
            SqlDbType dbType = SqlDbType.Text;

            #region SWTICH CASE

            switch (sqlType.ToLower())
            {
                case "bigint":
                    {
                        dbType = SqlDbType.BigInt;
                        break;
                    }
                case "binary":
                    {
                        dbType = SqlDbType.Binary;
                        break;
                    }
                case "bit":
                    {
                        dbType = SqlDbType.Bit;
                        break;
                    }
                case "char":
                    {
                        dbType = SqlDbType.Char;
                        break;
                    }
                case "datetime":
                    {
                        dbType = SqlDbType.DateTime;
                        break;
                    }
                case "decimal":
                    {
                        dbType = SqlDbType.Decimal;
                        break;
                    }
                case "float":
                    {
                        dbType = SqlDbType.Float;
                        break;
                    }
                case "image":
                    {
                        dbType = SqlDbType.Image;
                        break;
                    }
                case "int":
                    {
                        dbType = SqlDbType.Int;
                        break;
                    }
                case "money":
                    {
                        dbType = SqlDbType.Money;
                        break;
                    }
                case "nchar":
                    {
                        dbType = SqlDbType.NChar;
                        break;
                    }
                case "ntext":
                    {
                        dbType = SqlDbType.NText;
                        break;
                    }
                case "numeric":
                    {
                        dbType = SqlDbType.Decimal;
                        break;
                    }
                case "nvarchar":
                    {
                        dbType = SqlDbType.NVarChar;
                        break;
                    }
                case "real":
                    {
                        dbType = SqlDbType.Real;
                        break;
                    }
                case "smalldatetime":
                    {
                        dbType = SqlDbType.SmallDateTime;
                        break;
                    }
                case "smallint":
                    {
                        dbType = SqlDbType.SmallInt;
                        break;
                    }
                case "smallmoney":
                    {
                        dbType = SqlDbType.SmallMoney;
                        break;
                    }
                case "sql_variant":
                    {
                        dbType = SqlDbType.Variant;
                        break;
                    }
                case "text":
                    {
                        dbType = SqlDbType.Text;
                        break;
                    }
                case "timestamp":
                    {
                        dbType = SqlDbType.Timestamp;
                        break;
                    }
                case "tinyint":
                    {
                        dbType = SqlDbType.TinyInt;
                        break;
                    }
                case "uniqueidentifier":
                    {
                        dbType = SqlDbType.UniqueIdentifier;
                        break;
                    }
                case "varbinary":
                    {
                        dbType = SqlDbType.VarBinary;
                        break;
                    }
                case "varchar":
                    {
                        dbType = SqlDbType.VarChar;
                        break;
                    }
                case "xml":
                    {
                        dbType = SqlDbType.Xml;
                        break;
                    }
            }

            #endregion

            return "SqlDbType." + dbType.ToString();

        }

        #endregion

        #region generate Validation

        /// <summary>
        /// Method to generate Validation
        /// </summary>
        private void GenerateValidation()
        {
            #region Validation

            string[] validationFiles = { "Validation_BrokenRule", 
                                         "Validation_BrokenRulesList",
                                         "Validation_ValidateRuleBase",
                                         "Validation_ValidateRuleNotNull",
                                         "Validation_ValidateRuleRegexMatching",
                                         "Validation_ValidateRuleStringMaxLength",
                                         "Validation_ValidateRuleStringRequired",
                                         "Validation_ValidationRules"
                                       };

            foreach (string fileKey in validationFiles)
            {
                string fileText = Resources.ResourceManager.GetString(fileKey);

                fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
                string fileName = fileKey.Substring("Validation_".Length) + ".cs";
                FileWriter.WriteFile(ValidationPath, fileName, fileText);

            }

            #endregion
        }

        #endregion

        #region BusinessLayer

        /// <summary>
        /// method to generate business layer
        /// </summary>
        private void GenerateBusinessLayer()
        {
            Database database = TierGeneratorSettings.Instance.Database;

            #region Base Classes

            string[] validationFiles = { "Business_BusinessObjectBase", 
                                         "Business_InvalidBusinessObjectException"
                                       };

            foreach (string fileKey in validationFiles)
            {
                string fileText = Resources.ResourceManager.GetString(fileKey);

                fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
                string fileName = fileKey.Substring("Business_".Length) + ".cs";
                FileWriter.WriteFile(BusinessLayerRootPath, fileName, fileText);

            }


            #endregion

            #region generated Business Object classes

            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {
                    // Method to generate business object
                    GenerateBusinessObject(table);

                    // Method to generate primary key
                    GenerateBusinessObjectPrimaryKey(table);

                    // Method to generate factory
                    GenerateBusinessObjectFactory(table);
                }

            }

            #endregion
        }

        /// <summary>
        /// Method to generate Business Object
        /// </summary>
        /// <param name="table"></param>
        private void GenerateBusinessObject(DatabaseTable table)
        {
            string file = BusinessLayerRootPath + Path.DirectorySeparatorChar + table.ClassName + ".cs";
            string className = table.ClassName;

            using (StreamWriter sw = new StreamWriter(file))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using System.Text;");

                sw.WriteLine("namespace " + TierGeneratorSettings.Instance.ProjectNameSpace + ".BusinessLayer");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic class " + className + ": BusinessObjectBase");
                sw.WriteLine("\t{");


                sw.WriteLine("");
                #region Enumeration For Column Name

                sw.WriteLine("\t\t#region InnerClass");
                sw.WriteLine("\t\tpublic enum " + className + "Fields");
                sw.WriteLine("\t\t{");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DatabaseColumn column = table.Columns[i];
                    string end = ",";
                    if (i == table.Columns.Count - 1) end = "";
                    sw.WriteLine("\t\t\t" + column.PropertyName + end);

                }

                sw.WriteLine("\t\t}");

                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                #region Data Members

                sw.WriteLine("\t\t#region Data Members");
                sw.WriteLine("");

                foreach (DatabaseColumn column in table.Columns)
                {
                    sw.WriteLine("\t\t\t" + column.CSharpDataTypeName + " " + column.PrivateVariableName + ";");
                }

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                #region Properties

                sw.WriteLine("\t\t#region Properties");
                sw.WriteLine("");

                foreach (DatabaseColumn column in table.Columns)
                {
                    sw.WriteLine("\t\tpublic " + column.CSharpDataTypeName + "  " + column.PropertyName);
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t\t get { return " + column.PrivateVariableName + "; }");
                    sw.WriteLine("\t\t\t set");
                    sw.WriteLine("\t\t\t {");
                    sw.WriteLine("\t\t\t\t if (" + column.PrivateVariableName + " != value)");
                    sw.WriteLine("\t\t\t\t {");
                    sw.WriteLine("\t\t\t\t\t" + column.PrivateVariableName + " = value;");
                    sw.WriteLine("\t\t\t\t\t PropertyHasChanged(\"" + column.PropertyName + "\");");
                    sw.WriteLine("\t\t\t\t }");
                    sw.WriteLine("\t\t\t }");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("");
                }

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                #region Validation

                sw.WriteLine("\t\t#region Validation");
                sw.WriteLine("");

                sw.WriteLine("\t\tinternal override void AddValidationRules()");
                sw.WriteLine("\t\t{");

                foreach (DatabaseColumn column in table.Columns)
                {
                    if (!column.IsNull)
                    {
                        sw.WriteLine("\t\t\tValidationRules.AddRules(new Validation.ValidateRuleNotNull(\"" + column.PropertyName + "\", \"" + column.PropertyName + "\"));");
                    }

                    if ((column.CSharpDataTypeName.ToLower() == "string") && column.ColumnSize.HasValue)
                    {
                        sw.WriteLine("\t\t\tValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength(\"" + column.PropertyName + "\", \"" + column.PropertyName + "\"," + column.ColumnSize.Value + "));");
                    }
                }
                sw.WriteLine("\t\t}");

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");

                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }


        }

        /// <summary>
        /// generate the primary key of the database table
        /// </summary>
        /// <param name="table">table</param>
        private void GenerateBusinessObjectPrimaryKey(DatabaseTable table)
        {

            string file = BusinessLayerRootPath + Path.DirectorySeparatorChar + table.ClassName + "Keys.cs";
            string className = table.ClassName + "Keys";

            using (StreamWriter sw = new StreamWriter(file))
            {

                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using System.Text;");

                sw.WriteLine("namespace " + TierGeneratorSettings.Instance.ProjectNameSpace + ".BusinessLayer");
                sw.WriteLine("{");
                sw.WriteLine("\tpublic class " + className );
                sw.WriteLine("\t{");

                sw.WriteLine("");
                #region Data Members

                sw.WriteLine("\t\t#region Data Members");
                sw.WriteLine("");

                foreach (DatabaseColumn column in table.Columns)
                {
                    if (column.IsPK)
                    {
                        sw.WriteLine("\t\t" + column.CSharpDataTypeName + " " + column.PrivateVariableName + ";");
                    }
                }

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                #region Constructor

                string parameters = string.Empty;

                foreach (DatabaseColumn column in table.Columns)
                {
                    if (column.IsPK)
                    {
                        if (parameters.Length > 0)
                            parameters += ", ";

                        parameters += column.CSharpDataTypeName + " " + column.publicVariableName;
                    }
                }

                sw.WriteLine("\t\t#region Constructor");
                sw.WriteLine("");

                sw.WriteLine("\t\tpublic " + className + "(" + parameters + ")");
                sw.WriteLine("\t\t{");
                foreach (DatabaseColumn column in table.Columns)
                {
                    if (column.IsPK)
                    {
                        sw.WriteLine("\t\t\t " + column.PrivateVariableName + " = " + column.publicVariableName + "; ");
                    }
                }
                sw.WriteLine("\t\t}");

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                #region Properties

                sw.WriteLine("\t\t#region Properties");
                sw.WriteLine("");

                foreach (DatabaseColumn column in table.Columns)
                {
                    if (column.IsPK)
                    {
                        sw.WriteLine("\t\tpublic " + column.CSharpDataTypeName + "  " + column.PropertyName);
                        sw.WriteLine("\t\t{");
                        sw.WriteLine("\t\t\t get { return " + column.PrivateVariableName + "; }");
                        sw.WriteLine("\t\t}");
                    }
                }

                sw.WriteLine("");
                sw.WriteLine("\t\t#endregion");

                #endregion

                sw.WriteLine("");
                sw.WriteLine("\t}"); // END OF CLASS
                sw.WriteLine("}"); // END OF NAME SPACE

            }

        }

        /// <summary>
        /// Class to generate business object factory class
        /// </summary>
        /// <param name="table">table</param>
        private void GenerateBusinessObjectFactory(DatabaseTable table)
        {
            string fileText = Resources.Business_BusinessObjectFactory;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            fileText = fileText.Replace(ProjectTokens.ClassName, table.ClassName);

            FileWriter.WriteFile(BusinessLayerRootPath, table.ClassName + "Factory.cs", fileText);
        }

        #endregion

        #region ProjectFile


        /// <summary>
        /// Method to generate business layer project file.
        /// </summary>
        private void GenerateBusinessLayerProjectFile()
        {
            Database database = TierGeneratorSettings.Instance.Database;
            string fileText = Resources.Business_ProdjectFile;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            StringBuilder strFiles = new StringBuilder();
            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {
                    strFiles.AppendLine("\t<Compile Include=\"DataLayer\\" + table.ClassName + "Sql.cs\" />");
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + ".cs\" />");
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + "Factory.cs\" />");
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + "Keys.cs\" />");
                }
            }


            fileText = fileText.Replace(ProjectTokens.IncludeFilesInBusinessProjectFile, strFiles.ToString());

            FileWriter.WriteFile(BusinessLayerRootPath, TierGeneratorSettings.Instance.ProjectNameSpace + ".BusinessLayer.csproj", fileText);


        }

        #endregion

        #endregion

    }
}

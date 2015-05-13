using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
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

        public string BdoRootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace + ".Bdo";

            }
        }

        public string DaoRootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace + ".Dao";

            }
        }

        public string LogicRootPath
        {
            get
            {
                return TierGeneratorSettings.Instance.CodeGenerationPath +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace +
                       System.IO.Path.DirectorySeparatorChar +
                       TierGeneratorSettings.Instance.ProjectNameSpace + ".Logic";

            }
        }

        public string LogicPropertiesPath
        {
            get
            {
                return LogicRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "Properties";

            }
        }

        public string DaoPropertiesPath
        {
            get
            {
                return DaoRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "Properties";

            }
        }

        public string BdoPropertiesPath
        {
            get
            {
                return BdoRootPath +
                       System.IO.Path.DirectorySeparatorChar +
                       "Properties";

            }
        }

        public string ValidationPath
        {
            get
            {
                return BdoRootPath +
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
            // generate logic assembly file
            GenerateLogicAssemblyFile();

            // generate bdo assembly file
            GenerateBdoAssemblyFile();

            // generate dao assembly file
            GenerateDaoAssemblyFile();


            // generate Data Layer
            GenerateDataLayer();

            // Generate validation
            GenerateValidation();

            // Method to generate business layer
            GenerateBusinessLayer();

            // Generate project File Dao
            GenerateDaoProjectFile();

            // Generate project File Bdo
            GenerateBdoProjectFile();

            // Generate project File Logic
            GenerateLogicProjectFile();
        }

        #endregion        

        #region Assembly File

        /// <summary>
        /// method to generate assembly file
        /// </summary>
        private void GenerateLogicAssemblyFile()
        {

            string fileText = Resources.Logic_AssemblyInfo;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            FileWriter.WriteFile(LogicPropertiesPath, "AssemblyInfo.cs", fileText);
        }

        private void GenerateDaoAssemblyFile()
        {

            string fileText = Resources.Dao_AssemblyInfo;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            FileWriter.WriteFile(DaoPropertiesPath, "AssemblyInfo.cs", fileText);
        }

        private void GenerateBdoAssemblyFile()
        {

            string fileText = Resources.Bdo_AssemblyInfo;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            FileWriter.WriteFile(BdoPropertiesPath, "AssemblyInfo.cs", fileText);
        }


        #endregion

        #region Data Layer

        /// <summary>
        /// generate the files for the data layer
        /// </summary>
        private void GenerateDataLayer()
        {
            Database database = TierGeneratorSettings.Instance.Database;  

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
            string fileText = Resources.Dao;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            fileText = fileText.Replace(ProjectTokens.ClassName, table.ClassName);

            fileText = fileText.Replace(ProjectTokens.TableName, table.TableName);
            fileText = fileText.Replace(ProjectTokens.TableSchema, table.TableSchema);
            fileText = fileText.Replace(ProjectTokens.SpPrefix, TierGeneratorSettings.Instance.StoreProcedurePrefix);
            fileText = fileText.Replace(ProjectTokens.Context, TierGeneratorSettings.Instance.EFContext);


            #region Insert Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlInsertParameter, GetParameterForInsertOrUpdate(table, true));            

            #endregion

            #region Update Method

            fileText = fileText.Replace(ProjectTokens.EntitySqlUpdateParameter, GetParameterForInsertOrUpdate(table, false));

            #endregion

            #region Select By Primary Key

            fileText = fileText.Replace(ProjectTokens.EntitySqlSelectByPkParameter, GetParameterForPrimaryKey(table));

            #endregion

            #region Type field Pk

            fileText = fileText.Replace(ProjectTokens.EntitySqlTypePk, GetTypePk(table));

            #endregion

            #region Field Pk

            fileText = fileText.Replace(ProjectTokens.EntitySqlFieldPk, GetEqualFieldPk(table));

            #endregion

            #region 

            #region Populate Object From Reader

            fileText = fileText.Replace(ProjectTokens.EntitySqlPopulateColumns, GetPopulateObjectFromReader(table));

            #endregion

            FileWriter.WriteFile(DaoRootPath, table.ClassName + "Dao.cs", fileText);

        }

        /// <summary>
        /// Method to get parameters
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetParameterForInsertOrUpdate(DatabaseTable table, bool isInsert)
        {
            var strToReturn = new StringBuilder();

            const string assignInsertFormat = "\t\t\t\t\t\t\t{0} = objectBdo.{1}";
            const string assignUpdateFormat = "\t\t\t\t\t\tobjectInDb.{0} = objectBdo.{1};";
            int count = 1;            
            foreach (DatabaseColumn column in table.Columns)
            {
                if (isInsert)
                {
                    if (count < table.Columns.Count)
                        strToReturn.AppendLine(String.Format(assignInsertFormat, column.PropertyName, column.PropertyName)+ ",");
                    else                    
                        strToReturn.AppendLine(String.Format(assignInsertFormat, column.PropertyName, column.PropertyName));                    
                }
                else
                {
                    strToReturn.AppendLine(String.Format(assignUpdateFormat, column.PropertyName, column.PropertyName));
                }
                count++;
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
            var strToReturn = new StringBuilder();

            const string assignFormat = "\t\t\t\t\t\t\t{0} = objectInDb.{1}";
            int count = 1;
            foreach (DatabaseColumn column in table.Columns)
            {
                if (count < table.Columns.Count)
                    strToReturn.AppendLine(String.Format(assignFormat, column.PropertyName, column.PropertyName)+",");
                else                
                    strToReturn.AppendLine(String.Format(assignFormat, column.PropertyName, column.PropertyName));                
                count++;
            }

            return strToReturn.ToString();
        }

        private string GetTypePk(DatabaseTable table)
        {
            var typePk = "int";

            foreach (DatabaseColumn column in table.Columns)
            {
                if (column.IsPK)
                {
                    typePk = column.CSharpDataTypeName;
                }
            }
            return typePk;
        }

        private string GetEqualFieldPk(DatabaseTable table)
        {
            var fieldPk = "ID";

            foreach (DatabaseColumn column in table.Columns)
            {
                if (column.IsPK)
                {
                    fieldPk = column.PropertyName;
                }                
            }
            return fieldPk;
        }

        /// <summary>
        /// populate the object from reader
        /// </summary>
        /// <param name="table">Data base table</param>
        /// <returns></returns>
        private string GetPopulateObjectFromReader(DatabaseTable table)
        {
            var strToReturn = new StringBuilder();
            const string assignFormat = "\t\t\t\t\t\t{0} = item.{1}";
            int count = 1;
            foreach (DatabaseColumn column in table.Columns)
            {
                if (count < table.Columns.Count)
                    strToReturn.AppendLine(string.Format(assignFormat, column.PropertyName, column.PropertyName)+",");
                else                
                    strToReturn.AppendLine(string.Format(assignFormat, column.PropertyName, column.PropertyName));                
                count++;
            }

            return strToReturn.ToString();
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

            //string[] validationFiles = { "Bdo_BdoObjectBase", 
            //                             "Logic_InvalidLogicObjectException"
            //                           };

            //foreach (string fileKey in validationFiles)
            //{
            //    string fileText = Resources.ResourceManager.GetString(fileKey);

            //    fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            //    string fileName = fileKey.Substring("Bdo_".Length) + ".cs";
            //    FileWriter.WriteFile(BdoRootPath, fileName, fileText);

            //}

            var fileKey1 = "Bdo_BdoObjectBase";
            string fileText1 = Resources.ResourceManager.GetString(fileKey1);

            fileText1 = fileText1.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            string fileName1 = fileKey1.Substring("Bdo_".Length) + ".cs";
            FileWriter.WriteFile(BdoRootPath, fileName1, fileText1);

            var fileKey2 = "Logic_InvalidLogicObjectException";
            string fileText2 = Resources.ResourceManager.GetString(fileKey2);

            fileText2 = fileText2.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            
            

            string fileName2 = fileKey2.Substring("Logic_".Length) + ".cs";
            FileWriter.WriteFile(LogicRootPath, fileName2, fileText2);

            #endregion

            #region generated Business Object classes

            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {
                    // Method to generate business object
                    GenerateBusinessObject(table);

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
            var fileText = new StringBuilder();
            fileText.AppendLine("using System;");
            fileText.AppendLine("using System.Collections.Generic;");
            fileText.AppendLine("using System.Text;");

            fileText.AppendLine("namespace " + TierGeneratorSettings.Instance.ProjectNameSpace + ".Bdo");
            fileText.AppendLine("{");
            fileText.AppendLine("\tpublic partial class " + table.ClassName + "Bdo : BdoObjectBase");
            fileText.AppendLine("\t{");


            fileText.AppendLine("");
            #region Enumeration For Column Name

            fileText.AppendLine("\t\t#region InnerClass");
            fileText.AppendLine("\t\tpublic enum " + table.ClassName + "Fields");
            fileText.AppendLine("\t\t{");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                DatabaseColumn column = table.Columns[i];
                string end = ",";
                if (i == table.Columns.Count - 1) end = "";
                fileText.AppendLine("\t\t\t" + column.PropertyName + end);

            }

            fileText.AppendLine("\t\t}");

            fileText.AppendLine("\t\t#endregion");

            #endregion

            fileText.AppendLine("");
            #region Data Members

            fileText.AppendLine("\t\t#region Data Members");
            fileText.AppendLine("");

            foreach (DatabaseColumn column in table.Columns)
            {
                fileText.AppendLine("\t\t\t" + column.CSharpDataTypeName + " " + column.PrivateVariableName + ";");
            }

            fileText.AppendLine("");
            fileText.AppendLine("\t\t#endregion");

            #endregion

            fileText.AppendLine("");
            #region Properties

            fileText.AppendLine("\t\t#region Properties");
            fileText.AppendLine("");

            foreach (DatabaseColumn column in table.Columns)
            {
                fileText.AppendLine("\t\tpublic " + column.CSharpDataTypeName + "  " + column.PropertyName);
                fileText.AppendLine("\t\t{");
                fileText.AppendLine("\t\t\t get { return " + column.PrivateVariableName + "; }");
                fileText.AppendLine("\t\t\t set");
                fileText.AppendLine("\t\t\t {");
                fileText.AppendLine("\t\t\t\t if (" + column.PrivateVariableName + " != value)");
                fileText.AppendLine("\t\t\t\t {");
                fileText.AppendLine("\t\t\t\t\t" + column.PrivateVariableName + " = value;");
                fileText.AppendLine("\t\t\t\t\t PropertyHasChanged(\"" + column.PropertyName + "\");");
                fileText.AppendLine("\t\t\t\t }");
                fileText.AppendLine("\t\t\t }");
                fileText.AppendLine("\t\t}");
                fileText.AppendLine("");
            }

            fileText.AppendLine("");
            fileText.AppendLine("\t\t#endregion");

            #endregion

            fileText.AppendLine("");
            #region Validation

            fileText.AppendLine("\t\t#region Validation");
            fileText.AppendLine("");

            fileText.AppendLine("\t\tinternal override void AddValidationRules()");
            fileText.AppendLine("\t\t{");

            foreach (DatabaseColumn column in table.Columns)
            {
                if (!column.IsNull)
                {
                    fileText.AppendLine("\t\t\tValidationRules.AddRules(new Validation.ValidateRuleNotNull(\"" + column.PropertyName + "\", \"" + column.PropertyName + "\"));");
                }

                if ((column.CSharpDataTypeName.ToLower() == "string") && column.ColumnSize.HasValue)
                {
                    fileText.AppendLine("\t\t\tValidationRules.AddRules(new Validation.ValidateRuleStringMaxLength(\"" + column.PropertyName + "\", \"" + column.PropertyName + "\"," + column.ColumnSize.Value + "));");
                }
            }
            fileText.AppendLine("\t\t}");

            fileText.AppendLine("");
            fileText.AppendLine("\t\t#endregion");

            #endregion

            fileText.AppendLine("");

            fileText.AppendLine("\t}"); // END OF CLASS
            fileText.AppendLine("}"); // END OF NAME SPACE

            FileWriter.WriteFile(BdoRootPath, table.ClassName + "Bdo.cs", fileText.ToString());
        }        

        /// <summary>
        /// Class to generate business object factory class
        /// </summary>
        /// <param name="table">table</param>
        private void GenerateBusinessObjectFactory(DatabaseTable table)
        {
            string fileText = Resources.Logic;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);
            fileText = fileText.Replace(ProjectTokens.ClassName, table.ClassName);
            #region Type field Pk

            fileText = fileText.Replace(ProjectTokens.EntitySqlTypePk, GetTypePk(table));

            #endregion

            FileWriter.WriteFile(LogicRootPath, table.ClassName + "Logic.cs", fileText);
        }

        #endregion

        #region ProjectFile


        private void GenerateDaoProjectFile()
        {
            Database database = TierGeneratorSettings.Instance.Database;
            string fileText = Resources.Dao_ProdjectFile;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            StringBuilder strFiles = new StringBuilder();
            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + "Dao.cs\" />");
                }
            }

            fileText = fileText.Replace(ProjectTokens.IncludeFilesInBusinessProjectFile, strFiles.ToString());

            FileWriter.WriteFile(DaoRootPath, TierGeneratorSettings.Instance.ProjectNameSpace + ".Dao.csproj", fileText);
        }

        /// <summary>
        /// Method to generate business layer project file.
        /// </summary>
        private void GenerateBdoProjectFile()
        {
            Database database = TierGeneratorSettings.Instance.Database;
            string fileText = Resources.Bdo_ProdjectFile;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            var strFiles = new StringBuilder();
            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + "Bdo.cs\" />");                    
                }
            }


            fileText = fileText.Replace(ProjectTokens.IncludeFilesInBusinessProjectFile, strFiles.ToString());

            FileWriter.WriteFile(BdoRootPath, TierGeneratorSettings.Instance.ProjectNameSpace + ".Bdo.csproj", fileText);
        }

        /// <summary>
        /// Method to generate business layer project file.
        /// </summary>
        private void GenerateLogicProjectFile()
        {
            Database database = TierGeneratorSettings.Instance.Database;
            string fileText = Resources.Logic_ProdjectFile;
            fileText = fileText.Replace(ProjectTokens.NameSpace, TierGeneratorSettings.Instance.ProjectNameSpace);

            StringBuilder strFiles = new StringBuilder();
            foreach (DatabaseTable table in database.Tables)
            {
                if (table.IsSelected)
                {                                       
                    strFiles.AppendLine("\t<Compile Include=\"" + table.ClassName + "Logic.cs\" />");                    
                }
            }


            fileText = fileText.Replace(ProjectTokens.IncludeFilesInBusinessProjectFile, strFiles.ToString());

            FileWriter.WriteFile(LogicRootPath, TierGeneratorSettings.Instance.ProjectNameSpace + ".Logic.csproj", fileText);
        }

        #endregion

        #endregion

    }
}

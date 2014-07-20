using System;
using System.Collections.Generic;
using System.Text;
using TierGenerator.Common;
namespace TierGenerator
{
    /// <summary>
    /// Singleton class for the tier generator setting
    /// </summary>
    class TierGeneratorSettings
    {

        #region Data Members

        private static TierGeneratorSettings _setting = null;

        private string _projectNameSpace = string.Empty;
        private string _efContext = string.Empty;
        private string _connectionString = string.Empty;
        private string _codeGenerationPath = string.Empty;
        private string _classPrefix = string.Empty;
        private string _storeProcedurePrefix = string.Empty;
        private bool _generateBusinessLayer = false;
        private bool _generateWindowProject = false;
        private bool _generateWebProject = false;
        private bool _generateHelp = false;


        private Database _database = null;

        #endregion

        #region Private Constructor

        /// <summary>
        /// private constructor
        /// </summary>
        private TierGeneratorSettings() { }

        #endregion

        #region Instance Property

        public static TierGeneratorSettings Instance
        {
            get
            {
                if (_setting == null)
                    _setting = new TierGeneratorSettings();

                return _setting;
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// get/set the namespace of the project
        /// </summary>
        public string ProjectNameSpace
        {
            get { return _projectNameSpace; }
            set { _projectNameSpace = value; }
        }

        public string EFContext
        {
            get { return _efContext; }
            set { _efContext = value; }
        }

        /// <summary>
        /// get/set the connection string for Data base
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// get/set the path for code generation.
        /// </summary>
        public string CodeGenerationPath
        {
            get { return _codeGenerationPath; }
            set { _codeGenerationPath = value; }
        }

        /// <summary>
        /// get/set the Database schema
        /// </summary>
        public Database Database
        {
            get { return _database; }
            set { _database = value; }
        }

        /// <summary>
        /// get/set the class Prefix
        /// </summary>
        public string ClassPrefix
        {
            get { return _classPrefix; }
            set { _classPrefix = value; }
        }

        /// <summary>
        /// get/set the store procedure prefix
        /// </summary>
        public string StoreProcedurePrefix
        {
            get { return _storeProcedurePrefix; }
            set { _storeProcedurePrefix = value; }
        }

        /// <summary>
        /// get/set the either to generate business layer or not
        /// </summary>
        public bool GenerateBusinessLayer
        {
            get { return _generateBusinessLayer; }
            set { _generateBusinessLayer = value; }
        }

        ///// <summary>
        ///// get/set the either to generate win project or not
        ///// </summary>
        //public bool GenerateWindowProject
        //{
        //    get { return _generateWindowProject; }
        //    set { _generateWindowProject = value; }
        //}

        ///// <summary>
        ///// get/set the either to generate web project or not
        ///// </summary>
        //public bool GenerateWebProject
        //{
        //    get { return _generateWebProject; }
        //    set { _generateWebProject = value; }
        //}

        ///// <summary>
        ///// get/set either to generate help or not
        ///// </summary>
        //public bool GenerateHelp
        //{
        //    get { return _generateHelp; }
        //    set { _generateHelp = value; }
        //}

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to ge the name of the store procedure
        /// </summary>
        /// <param name="tableSchema">table schema</param>
        /// <param name="tableName">table name</param>
        /// <param name="spType">store procedure name</param>
        /// <returns>name of the store procedure</returns>
        public string GetStoreProcedureName(string tableSchema, string tableName, StoreProcedureType spType)
        {
            string format = "[{0}].[{1}{2}_{3}]";

            return string.Format(format, tableSchema, StoreProcedurePrefix, tableName, spType.ToString());
        }

        #endregion
    }
}

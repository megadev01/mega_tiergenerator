using System;
using System.Collections.Generic;
using System.Text;

namespace TierGenerator.Common
{
    /// <summary>
    /// Class to contain the information of the database table
    /// </summary>
    public class DatabaseTable
    {
        #region Data Members

        private string _tableName = string.Empty;
        private string _tableType = string.Empty;
        private string _tableSchema = string.Empty;
        private bool _isSelected = false;

        private List<DatabaseColumn> _columns = null;

        #endregion

        #region Properties


        /// <summary>
        /// get/set the table name
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// get the class name
        /// </summary>
        public string ClassName
        {
            get
            {
                string cName = string.Empty;
                if (TableName.Length == 1)
                {
                    cName = TableName[0].ToString().ToUpper();
                }
                else if (TableName.Length > 1)
                {
                    cName = TableName[0].ToString().ToUpper() + TableName.Substring(1);
                }

                return TierGeneratorSettings.Instance.ClassPrefix + cName;
            }

        }

        /// <summary>
        /// get/set the tableType
        /// </summary>
        public string TableType
        {
            get { return _tableType; }
            set { _tableType = value; }
        }

        /// <summary>
        /// get/set the table schema
        /// </summary>
        public string TableSchema
        {
            get { return _tableSchema; }
            set { _tableSchema = value; }
        }


        /// <summary>
        /// get/set either table is selected or not
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        /// <summary>
        /// get the collection of the columns which belongs to the table
        /// </summary>
        public List<DatabaseColumn> Columns
        {
            get
            {

                if (_columns == null)
                    _columns = new List<DatabaseColumn>();
                return _columns;
            }
        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TierGenerator.Common
{
    /// <summary>
    /// represent the column of the database table
    /// </summary>
    public class DatabaseColumn
    {
        #region Data Members

        string _name = string.Empty;
        Type _cSharpDataType = typeof(object);
        Type _DotNetDataType = typeof(object);

        string _cSharpDataTypeName = string.Empty;
        string _DotNetDataTypeName = string.Empty;




        string _dataType = string.Empty;
        bool _isNull = false;
        bool _isPK = false;
        bool _isFK = false;
        bool _isIdentity = false;
        bool _isAutoNumber = false;
        bool _isReadonly = false;

        int _ordinalPosition;
        long? _ColumnSize = 0;


        #endregion

        #region Properties

        /// <summary>
        /// get/set the Name of column
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string PropertyName
        {
            get
            {
                if (Name.Length > 1)
                {
                    return Name[0].ToString().ToUpper() + Name.Substring(1);
                }
                else
                {
                    return Name.ToUpper();
                }
            }
        }

        public string PrivateVariableName
        {
            get
            {
                if (Name.Length > 1)
                {
                    return "_" + Name[0].ToString().ToLower() + Name.Substring(1);
                }
                else
                {
                    return "_" + Name.ToLower();
                }
            }
        }

        public string publicVariableName
        {
            get
            {
                if (Name.Length > 1)
                {
                    return Name[0].ToString().ToLower() + Name.Substring(1);
                }
                else
                {
                    return Name.ToLower();
                }
            }
        }

        /// <summary>
        /// get the name of prameter
        /// </summary>
        public string SqlParameterName
        {
            get
            {
                return "@" + Name;
            }
        }

        /// <summary>
        /// get/set Visual Studio Type of the Column
        /// </summary>
        public Type CSharpDataType
        {
            get
            {
                if (Type.GetType(CSharpDataTypeName) is string)
                {
                    return Type.GetType(CSharpDataTypeName);
                }
                if (IsNull)
                {
                    return Type.GetType(CSharpDataTypeName + "?");
                }
                else
                {
                    return Type.GetType(CSharpDataTypeName);
                }
                //return _cSharpDataType; 
            }
            //set { _cSharpDataType = value; }
        }

        /// <summary>
        /// get/set C Sharp data type name
        /// </summary>
        public string CSharpDataTypeName
        {
            get { return _cSharpDataTypeName; }
            set { _cSharpDataTypeName = value; }
        }

        /// <summary>
        /// get/set Dot net Type of the Column
        /// </summary>
        public Type DotNetDataType
        {
            get { return _DotNetDataType; }
            set { _DotNetDataType = value; }
        }

        /// <summary>
        /// get/set dot net type name
        /// </summary>
        public string DotNetDataTypeName
        {
            get { return _DotNetDataTypeName; }
            set { _DotNetDataTypeName = value; }
        }

        /// <summary>
        /// Sql Server Data Tye of the column
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// get/set is column allow null value
        /// </summary>
        public bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }

        /// <summary>
        /// get/set is this column Primary key
        /// </summary>
        public bool IsPK
        {
            get { return _isPK; }
            set { _isPK = value; }
        }

        /// <summary>
        /// get/set is this column forign key
        /// </summary>
        public bool IsFK
        {
            get { return _isFK; }
            set { _isFK = value; }
        }

        /// <summary>
        /// get/set is this column auto number
        /// </summary>
        public bool IsAutoNumber
        {
            get { return _isAutoNumber; }
            set { _isAutoNumber = value; }
        }

        /// <summary>
        /// get/set the ordinal position of the column
        /// </summary>
        public int OrdinalPosition
        {
            get { return _ordinalPosition; }
            set { _ordinalPosition = value; }
        }

        /// <summary>
        /// get/set the length of the column
        /// </summary>
        public long? ColumnSize
        {
            get { return _ColumnSize; }
            set { _ColumnSize = value; }
        }

        /// <summary>
        /// get/set column is identy or not
        /// </summary>
        public bool IsIdentity
        {
            get { return _isIdentity; }
            set { _isIdentity = value; }
        }

        /// <summary>
        /// get/set is readonly 
        /// </summary>
        public bool IsReadonly
        {
            get { return _isReadonly; }
            set { _isReadonly = value; }
        }

        #endregion

        #region Private Data Type

       
        #endregion

    }
}

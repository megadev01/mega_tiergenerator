using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using $PROJECT_NAMESPACE$.Dao;
using $PROJECT_NAMESPACE$.Bdo;

namespace $PROJECT_NAMESPACE$.Logic
{
    public partial class $CLASS_NAME$Logic
    {

        #region data Members

        $CLASS_NAME$Dao _dataObject = null;

        #endregion

        #region Constructor

        public $CLASS_NAME$Logic()
        {
            _dataObject = new $CLASS_NAME$Dao();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Insert new $CLASS_NAME$
        /// </summary>
        /// <param name="businessObject">$CLASS_NAME$ object</param>
        /// <returns>true for successfully saved</returns>
        public bool Insert(ref $CLASS_NAME$Bdo objecBdo, ref string message)
        {
            if (!objecBdo.IsValid)
            {
                throw new InvalidBusinessObjectException(objecBdo.BrokenRulesList.ToString());
            }


            return _dataObject.Insert(ref objecBdo, ref message);

        }

        /// <summary>
        /// Update existing $CLASS_NAME$
        /// </summary>
        /// <param name="businessObject">$CLASS_NAME$ object</param>
        /// <returns>true for successfully saved</returns>
        public bool Update(ref $CLASS_NAME$Bdo objecBdo, ref string message)
        {
            if (!objecBdo.IsValid)
            {
                throw new InvalidBusinessObjectException(objecBdo.BrokenRulesList.ToString());
            }


            return _dataObject.Update(ref objecBdo, ref message);
        }

        /// <summary>
        /// get $CLASS_NAME$ by primary key.
        /// </summary>
        /// <param name="keys">primary key</param>
        /// <returns>Student</returns>
        public $CLASS_NAME$Bdo GetById($TYPE_PK$ id)
        {
            return _dataObject.GetById(id); 
        }

        /// <summary>
        /// get list of all $CLASS_NAME$s
        /// </summary>
        /// <returns>list</returns>
        public List<$CLASS_NAME$Bdo> GetAll()
        {
            return _dataObject.GetAll(); 
        }

        /// <summary>
        /// get list of $CLASS_NAME$ by field
        /// </summary>
        /// <param name="fieldName">field name</param>
        /// <param name="value">value</param>
        /// <returns>list</returns>
        public List<$CLASS_NAME$Bdo> GetAllBy($CLASS_NAME$Bdo.$CLASS_NAME$Fields fieldName, object value)
        {
            return _dataObject.GetAllBy(fieldName.ToString(), value);  
        }        

        #endregion

    }
}

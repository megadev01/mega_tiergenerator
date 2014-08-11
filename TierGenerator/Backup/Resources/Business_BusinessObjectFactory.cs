using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using $PROJECT_NAMESPACE$.BusinessLayer.DataLayer;

namespace $PROJECT_NAMESPACE$.BusinessLayer
{
    public class $CLASS_NAME$Factory
    {

        #region data Members

        $CLASS_NAME$Sql _dataObject = null;

        #endregion

        #region Constructor

        public $CLASS_NAME$Factory()
        {
            _dataObject = new $CLASS_NAME$Sql();
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Insert new $CLASS_NAME$
        /// </summary>
        /// <param name="businessObject">$CLASS_NAME$ object</param>
        /// <returns>true for successfully saved</returns>
        public bool Insert($CLASS_NAME$ businessObject)
        {
            if (!businessObject.IsValid)
            {
                throw new InvalidBusinessObjectException(businessObject.BrokenRulesList.ToString());
            }


            return _dataObject.Insert(businessObject);

        }

        /// <summary>
        /// Update existing $CLASS_NAME$
        /// </summary>
        /// <param name="businessObject">$CLASS_NAME$ object</param>
        /// <returns>true for successfully saved</returns>
        public bool Update($CLASS_NAME$ businessObject)
        {
            if (!businessObject.IsValid)
            {
                throw new InvalidBusinessObjectException(businessObject.BrokenRulesList.ToString());
            }


            return _dataObject.Update(businessObject);
        }

        /// <summary>
        /// get $CLASS_NAME$ by primary key.
        /// </summary>
        /// <param name="keys">primary key</param>
        /// <returns>Student</returns>
        public $CLASS_NAME$ GetByPrimaryKey($CLASS_NAME$Keys keys)
        {
            return _dataObject.SelectByPrimaryKey(keys); 
        }

        /// <summary>
        /// get list of all $CLASS_NAME$s
        /// </summary>
        /// <returns>list</returns>
        public List<$CLASS_NAME$> GetAll()
        {
            return _dataObject.SelectAll(); 
        }

        /// <summary>
        /// get list of $CLASS_NAME$ by field
        /// </summary>
        /// <param name="fieldName">field name</param>
        /// <param name="value">value</param>
        /// <returns>list</returns>
        public List<$CLASS_NAME$> GetAllBy($CLASS_NAME$.$CLASS_NAME$Fields fieldName, object value)
        {
            return _dataObject.SelectByField(fieldName.ToString(), value);  
        }

        /// <summary>
        /// delete by primary key
        /// </summary>
        /// <param name="keys">primary key</param>
        /// <returns>true for succesfully deleted</returns>
        public bool Delete($CLASS_NAME$Keys keys)
        {
            return _dataObject.Delete(keys); 
        }

        /// <summary>
        /// delete $CLASS_NAME$ by field.
        /// </summary>
        /// <param name="fieldName">field name</param>
        /// <param name="value">value</param>
        /// <returns>true for successfully deleted</returns>
        public bool Delete($CLASS_NAME$.$CLASS_NAME$Fields fieldName, object value)
        {
            return _dataObject.DeleteByField(fieldName.ToString(), value); 
        }

        #endregion

    }
}

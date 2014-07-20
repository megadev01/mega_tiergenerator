using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using $PROJECT_NAMESPACE$.Bdo;
using $PROJECT_NAMESPACE$.Data;

namespace $PROJECT_NAMESPACE$.Dao
{
	/// <summary>
	/// Data access layer class for $CLASS_NAME$
	/// </summary>
	public class $CLASS_NAME$Dao
	{

        #region Constructor

		/// <summary>
		/// Class constructor
		/// </summary>
		public $CLASS_NAME$Dao()
		{
			// Nothing for now.
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// insert new row in the table
        /// </summary>
		/// <param name="businessObject">business object</param>
		/// <returns>true of successfully insert</returns>
		public bool Insert(ref $CLASS_NAME$Bdo objectBdo, ref string message)
		{
            message = "Agregado correctamente";
            bool ret = true;

			try
			{                
                using (var context = new $CONTEXT$())
                {
                    var objectId = objectBdo.$FIELD_PK$;
                    $CLASS_NAME$ objectInDb = (from p in context.$CLASS_NAME$
                                           where p.$FIELD_PK$ == objectId
                                           select p).FirstOrDefault();

                    //check product
                    if (objectInDb == null)
                    {
                        context.$CLASS_NAME$.Add(new $CLASS_NAME$()
                        {                        
$INSERT_PARAMETER$
                        });
                    
                        //elementInDb.RowVersion = elementBdo.RowVersion;                    
                    }
                
                    int num = context.SaveChanges();

                    //elementBdo.RowVersion = elementInDb.RowVersion;

                    if (num == 1) return ret;
                    ret = false;
                    message = "Error al agregar";
                }
                return ret;
			}
			catch(Exception ex)
			{				
				throw new Exception("$CLASS_NAME$::Insert::Error occured.", ex);
			}			
		}

         /// <summary>
        /// update row in the table
        /// </summary>
        /// <param name="businessObject">business object</param>
        /// <returns>true for successfully updated</returns>
        public bool Update(ref $CLASS_NAME$Bdo objectBdo, ref string message)
        {
            message = "El elemento fue actualizado";
            bool ret = true;

            try
            {                
                using (var context = new $CONTEXT$())
                {
                    var objectId = objectBdo.$FIELD_PK$;
                    $CLASS_NAME$ objectInDb = (from p in context.$CLASS_NAME$
                                           where p.$FIELD_PK$ == objectId
                                           select p).FirstOrDefault();

                    //check product
                    if (objectInDb != null)
                    {
$UPDATE_PARAMETER$
                        //elementInDb.RowVersion = elementBdo.RowVersion;                    
                    }

                    int num = context.SaveChanges();

                    //elementBdo.RowVersion = elementInDb.RowVersion;

                    if (num == 1) return ret;
                    ret = false;
                    message = "El elemento no fue actualizado";
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("$CLASS_NAME$::Update::Error occured.", ex);
            }            
        }

        /// <summary>
        /// Select by primary key
        /// </summary>
        /// <param name="keys">primary keys</param>
        /// <returns>$CLASS_NAME$ business object</returns>
        public $CLASS_NAME$Bdo GetById($TYPE_PK$ id)
        {
            try
            {
                $CLASS_NAME$Bdo objectBdo = null;
                using (var context = new $CONTEXT$())
                {
                    $CLASS_NAME$ objectInDb = (from p in context.$CLASS_NAME$
                                       where p.$FIELD_PK$ == id
                                       select p).FirstOrDefault();

                    if (objectInDb != null)
                        objectBdo = new $CLASS_NAME$Bdo()
                        {
$SELECT_BY_PK_PARAMETERS$
                            
                            //RowVersion = element.RowVersion
                        };
                }
                return objectBdo;
            }
            catch (Exception ex)
            {
                throw new Exception("$CLASS_NAME$::GetById::Error occured.", ex);
            }            
        }

        /// <summary>
        /// Select all rescords
        /// </summary>
        /// <returns>list of $CLASS_NAME$</returns>
        public List<$CLASS_NAME$Bdo> GetAll()
        {            
            try
            {
                
                var $CLASS_NAME$Bdos = new List<$CLASS_NAME$Bdo>();
                using (var context = new $CONTEXT$())
                {
                    var items = context.$CLASS_NAME$.ToList();
                
                    $CLASS_NAME$Bdos.AddRange(items.Select(item => new $CLASS_NAME$Bdo()
                    {
$POPULATE_COLUMNS$
                    }));
                }
                return $CLASS_NAME$Bdos;

            }
            catch (Exception ex)
            {                
                throw new Exception("$CLASS_NAME$::GetAll::Error occured.", ex);
            }            
        }

        public List<$CLASS_NAME$Bdo> GetAllBy(string fieldName, object value)
        {
            var $CLASS_NAME$Bdos = new List<$CLASS_NAME$Bdo>();

            try
            {
                using (var context = new $CONTEXT$())
                {
                    string sql = "SELECT * FROM $CLASS_NAME$ WHERE {0} = {1} ";

                    var items = context.$CLASS_NAME$.SqlQuery(String.Format(sql, fieldName, value)).ToList();

                    $CLASS_NAME$Bdos.AddRange(items.Select(item => new $CLASS_NAME$Bdo()
                    {
$POPULATE_COLUMNS$
                    }));
                }

                return $CLASS_NAME$Bdos;
            }
            catch (Exception ex)
            {                
                throw new Exception("$CLASS_NAME$::GetAllBy::Error occured.", ex);
            }
            
        }
        #endregion
	}
}

using System;
using System.Collections.Generic;
using System.Text;

using TierGenerator.Common;

namespace TierGenerator.DatabaseSchema
{
    /// <summary>
    /// Provides the functionality to get the database schema
    /// </summary>
    interface IDatabaseSchema 
    {
        
        /// <summary>
        /// load the database schema
        /// </summary>
        /// <param name="databaseServer">name of the database server</param>
        /// /// <param name="catalog">name of the catalog</param>
        /// <param name="connectionString">connection string</param>
        /// <returns>database schema</returns>
        Database GetDataBaseSchema(string databaseServer,string catalog, string connectionString);
    }
}

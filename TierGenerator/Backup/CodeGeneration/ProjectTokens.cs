using System;
using System.Collections.Generic;
using System.Text;

namespace TierGenerator.CodeGeneration
{
    class ProjectTokens
    {
        public const string NameSpace = "$PROJECT_NAMESPACE$";
        public const string ClassName = "$CLASS_NAME$";

        public const string TableName = "$TABLE_NAME$";
        public const string TableSchema = "$TABLE_SCHEMA$";
        public const string SpPrefix = "$SP_PREFIX$";


        public const string EntitySqlInsertParameter = "$INSERT_PARAMETER$";
        public const string EntitySqlGetReturnedValue = "$GET_RETURNED_VALUE$";

        public const string EntitySqlUpdateParameter = "$UPDATE_PARAMETER$";

        public const string EntitySqlSelectByPkParameter = "$SELECT_BY_PK_PARAMETERS$";

        public const string EntitySqlPopulateBusinessObjectParameter = "$POPULATE_BUSINESS_OBJECT_PARAMERTERS$";

        public const string IncludeFilesInBusinessProjectFile = "$INCLUDE_FILES$";

        public const string ConnectionString = "$CONNECTION_STRING$";
    }
}

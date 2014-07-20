using System;
using System.Collections.Generic;
using System.Text;

namespace TierGenerator.Common
{
    enum  StoreProcedureType
    {
        Insert,
        Update,
        SelectByPrimaryKey,
        SelectAll,
        SelectByField,
        DeleteByPrimaryKey,
        DeleteByField
    }
}

using System;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.Search
{
    public enum SearchConditionType
    {
        And = CONDITION_TYPE.CT_AND_CONDITION,

        Or = CONDITION_TYPE.CT_OR_CONDITION,

        Not = CONDITION_TYPE.CT_NOT_CONDITION,

        Leaf = CONDITION_TYPE.CT_LEAF_CONDITION
    }
}
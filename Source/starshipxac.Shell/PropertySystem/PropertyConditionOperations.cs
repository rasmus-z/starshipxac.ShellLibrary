﻿using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property condition operations.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PropertyConditionOperations
    {
        Implicit = CONDITION_OPERATION.COP_IMPLICIT,

        Equal = CONDITION_OPERATION.COP_EQUAL,

        NotEqual = CONDITION_OPERATION.COP_NOTEQUAL,

        LessThan = CONDITION_OPERATION.COP_LESSTHAN,

        GreaterThan = CONDITION_OPERATION.COP_GREATERTHAN,

        LessThanOrEqual = CONDITION_OPERATION.COP_LESSTHANOREQUAL,

        GreaterThanOrEqual = CONDITION_OPERATION.COP_GREATERTHANOREQUAL,

        ValueStartsWith = CONDITION_OPERATION.COP_VALUE_STARTSWITH,

        ValueEndsWith = CONDITION_OPERATION.COP_VALUE_ENDSWITH,

        ValueContains = CONDITION_OPERATION.COP_VALUE_CONTAINS,

        ValueNotContains = CONDITION_OPERATION.COP_VALUE_NOTCONTAINS,

        DOSWildCards = CONDITION_OPERATION.COP_DOSWILDCARDS,

        WordEqual = CONDITION_OPERATION.COP_WORD_EQUAL,

        WordStartsWith = CONDITION_OPERATION.COP_WORD_STARTSWITH,

        ApplicationSpecific = CONDITION_OPERATION.COP_APPLICATION_SPECIFIC,
    }
}
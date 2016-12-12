using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761487(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPENUMTYPE
    {
        /// <summary>
        ///     Use <c>GetDisplayText</c> and either <c>GetRangeMinValue</c> or <c>GetRangeSetValue</c>.
        /// </summary>
        PET_DISCRETEVALUE = 0,

        /// <summary>
        ///     Use <c>GetDisplayText</c> and either <c>GetRangeMinValue</c> or <c>GetRangeSetValue</c>.
        /// </summary>
        PET_RANGEDVALUE = 1,

        /// <summary>
        ///     Use <c>GetDisplayText</c>.
        /// </summary>
        PET_DEFAULTVALUE = 2,

        /// <summary>
        ///     Use <c>GetValue</c> or <c>GetRangeMinValue</c>.
        /// </summary>
        PET_ENDRANGE = 3
    };
}
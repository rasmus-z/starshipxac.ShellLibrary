using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     プロパティ列挙種別を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761487(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPENUMTYPE
    {
        PET_DISCRETEVALUE = 0,
        PET_RANGEDVALUE = 1,
        PET_DEFAULTVALUE = 2,
        PET_ENDRANGE = 3
    };
}
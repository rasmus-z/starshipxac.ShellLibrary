using System;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// シェルアイテム比較フラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762543(v=vs.85).aspx
    /// </remarks>
    internal enum SICHINTF
    {
        SICHINT_DISPLAY = 0x00000000,
        SICHINT_CANONICAL = 0x10000000,
        SICHINT_TEST_FILESYSPATH_IF_NOT_EQUAL = 0x20000000,
        SICHINT_ALLFIELDS = unchecked((int)0x80000000)
    }
}
using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// シェルアイテムIDを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb759800(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SHITEMID
    {
        /// <summary>
        /// シェルアイテムIDのサイズ。
        /// </summary>
        public ushort cb;

        /// <summary>
        /// 可変長のシェルアイテムID。
        /// </summary>
        public byte[] abID;
    }
}
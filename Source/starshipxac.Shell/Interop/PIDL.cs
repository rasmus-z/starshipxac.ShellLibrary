using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// <c>PIDL</c>を定義します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct PIDL
    {
        public PIDL(IntPtr pidl)
            : this()
        {
            this.Value = pidl;
        }

        public static PIDL FromParsingName(string parsingName)
        {
            IntPtr pidl;
            UInt32 sfgao;
            var code = ShellNativeMethods.SHParseDisplayName(parsingName, IntPtr.Zero, out pidl, 0, out sfgao);
            return HRESULT.Succeeded(code) ? new PIDL(pidl) : Null;
        }

        internal static PIDL FromShellItem(IShellItem2 shellItem)
        {
            var unknown = Marshal.GetIUnknownForObject(shellItem);
            return FromUnknown(unknown);
        }

        internal static PIDL FromUnknown(IntPtr unknown)
        {
            IntPtr pidl;
            var code = ShellNativeMethods.SHGetIDListFromObject(unknown, out pidl);
            return HRESULT.Succeeded(code) ? new PIDL(pidl) : Null;
        }

        public static readonly PIDL Null = (PIDL)IntPtr.Zero;

        public IntPtr Value { get; private set; }

        public bool IsNull
        {
            get
            {
                return this.Value == IntPtr.Zero;
            }
        }

        public static explicit operator PIDL(IntPtr pidl)
        {
            return new PIDL(pidl);
        }

        public static implicit operator IntPtr(PIDL pidl)
        {
            return pidl.Value;
        }

        public static bool operator ==(PIDL x, PIDL y)
        {
            return x.Value == y.Value;
        }

        public static bool operator !=(PIDL x, PIDL y)
        {
            return !(x == y);
        }

        public static bool operator ==(PIDL x, IntPtr y)
        {
            return x.Value == y;
        }

        public static bool operator !=(PIDL x, IntPtr y)
        {
            return !(x == y);
        }

        public static bool operator ==(IntPtr x, PIDL y)
        {
            return x == y.Value;
        }

        public static bool operator !=(IntPtr x, PIDL y)
        {
            return !(x == y);
        }

        public T MarshalAs<T>()
        {
            return (T)Marshal.PtrToStructure(this.Value, typeof(T));
        }

        public void Free()
        {
            ShellNativeMethods.ILFree(this.Value);
            this.Value = IntPtr.Zero;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is PIDL)
                {
                    return this.Value == ((PIDL)obj).Value;
                }
                if (obj is IntPtr)
                {
                    return this.Value == (IntPtr)obj;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
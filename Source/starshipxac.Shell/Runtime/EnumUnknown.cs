using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Runtime.Interop;

namespace starshipxac.Shell.Runtime
{
    internal class EnumUnknown<T> : IEnumUnknown
    {
        private readonly List<T> conditions = new List<T>();
        private int current = -1;

        internal EnumUnknown(IEnumerable<T> conditions)
        {
            Contract.Requires<ArgumentNullException>(conditions != null);

            this.conditions.AddRange(conditions);
        }

        #region IEnumUnknown Member

        public HRESULT Next(uint requestedNumber, ref IntPtr buffer, ref uint fetchedNumber)
        {
            this.current += 1;

            if (this.current < this.conditions.Count)
            {
                buffer = Marshal.GetIUnknownForObject(this.conditions[this.current]);
                fetchedNumber = 1;
                return COMErrorCodes.S_OK;
            }

            return COMErrorCodes.S_FALSE;
        }

        public HRESULT Skip(uint number)
        {
            var temp = this.current + (int)number;
            if (temp > this.conditions.Count - 1)
            {
                return COMErrorCodes.S_FALSE;
            }

            this.current = temp;
            return COMErrorCodes.S_OK;
        }

        public HRESULT Reset()
        {
            this.current = -1;
            return COMErrorCodes.S_OK;
        }

        public HRESULT Clone(out IEnumUnknown result)
        {
            result = new EnumUnknown<T>(this.conditions.ToArray());
            return COMErrorCodes.S_OK;
        }

        #endregion
    }
}
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class ExplorerControlException : COMException
    {
        public ExplorerControlException()
        {
        }

        public ExplorerControlException(string message)
            : base(message)
        {
        }

        public ExplorerControlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExplorerControlException(string message, int errorCode)
            : base(message, errorCode)
        {
        }

        public ExplorerControlException(string message, HRESULT hResult)
            : base(message, hResult)
        {
        }

        protected ExplorerControlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
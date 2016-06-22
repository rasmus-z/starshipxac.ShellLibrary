using System;
using System.Collections;
using starshipxac.Shell.PropertySystem;

namespace ShellFileDialogSample.ViewModels
{
    public class ShellPropertyComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return String.CompareOrdinal(((IShellProperty)x).CanonicalName, ((IShellProperty)y).CanonicalName);
        }
    }
}

using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    public abstract class TaskDialogProgressBarBase : TaskDialogControl
    {
        protected TaskDialogProgressBarBase(string name)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        protected internal abstract void Reset();
    }
}
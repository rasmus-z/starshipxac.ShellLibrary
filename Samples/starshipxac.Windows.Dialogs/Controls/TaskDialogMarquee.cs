using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    public class TaskDialogMarquee : TaskDialogProgressBarBase
    {
        public TaskDialogMarquee(string name)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        protected internal override void Reset()
        {
        }
    }
}
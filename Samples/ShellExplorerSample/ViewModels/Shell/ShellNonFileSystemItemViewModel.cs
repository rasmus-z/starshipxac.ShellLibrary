using System;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.PropertySystem;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellNonFileSystemItemViewModel : ShellObjectViewModel
    {
        public ShellNonFileSystemItemViewModel(ShellObject shellObject)
            : base(shellObject)
        {
            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellObject.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                new ShellProperty<string>(this.ShellObject, "System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellObject.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellObject.DateModified);

            #endregion
        }

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }
    }
}
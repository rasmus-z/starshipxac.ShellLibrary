using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Reactive.Bindings;
using ShellExplorerSample.ViewModels.Shell;

namespace ShellExplorerSample.Views
{
    public class TreeViewSelectedItemChangedAction : TriggerAction<TreeView>
    {
        #region ReactiveProperty Property

        public ReactiveProperty<ShellFolderViewModel> ReactiveProperty
        {
            get
            {
                return (ReactiveProperty<ShellFolderViewModel>)GetValue(ReactivePropertyProperty);
            }
            set
            {
                SetValue(ReactivePropertyProperty, value);
            }
        }

        public static readonly DependencyProperty ReactivePropertyProperty = DependencyProperty.Register(
            nameof(ReactiveProperty), typeof(ReactiveProperty<ShellFolderViewModel>), typeof(TreeViewSelectedItemChangedAction),
            new PropertyMetadata(null));

        #endregion

        protected override void Invoke(object parameter)
        {
            if (this.ReactiveProperty == null)
            {
                return;
            }

            this.ReactiveProperty.Value = this.AssociatedObject.SelectedItem as ShellFolderViewModel;
        }
    }
}

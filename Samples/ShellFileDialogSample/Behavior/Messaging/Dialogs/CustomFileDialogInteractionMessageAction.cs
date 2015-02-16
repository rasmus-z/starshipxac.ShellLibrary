using System;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using ShellFileDialogSample.Messaging.Dialogs;
using starshipxac.Windows.Shell.Dialogs;
using starshipxac.Windows.Shell.Dialogs.Controls;

namespace ShellFileDialogSample.Behavior.Messaging.Dialogs
{
    public class CustomFileDialogInteractionMessageAction : InteractionMessageAction<FrameworkElement>
    {
        protected override void InvokeAction(InteractionMessage m)
        {
            var message = m as CustomFileOpenDialogMessage;
            if (message != null)
            {
                using (var dialog = new FileOpenDialog(message.Title))
                {
                    if (message.InitialFolder != null)
                    {
                        dialog.InitialFolder = message.InitialFolder;
                    }
                    if (message.DefaultFolder != null)
                    {
                        dialog.DefaultFolder = message.DefaultFolder;
                    }

                    // Custom Controls
                    var button1 = new FileDialogButton("button", "Button1");
                    button1.Click += (_, args) => MessageBox.Show("Button1", "Message");
                    dialog.Controls.Add(button1);

                    var combo1 = new FileDialogComboBox("combo1",
                        new FileDialogComboBoxItem("Item1"),
                        new FileDialogComboBoxItem("Item2"),
                        new FileDialogComboBoxItem("Item3"));
                    dialog.Controls.Add(combo1);

                    if (dialog.Show() == FileDialogResult.Ok)
                    {
                        message.Response = new CustomFileOpenDialogResponse(dialog.GetShellFiles());
                        message.Response.SelectedItemIndex = combo1.SelectedIndex;
                    }
                }
            }
        }
    }
}
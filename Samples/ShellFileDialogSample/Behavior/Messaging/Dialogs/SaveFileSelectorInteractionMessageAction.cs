using System;
using System.Collections.Generic;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using Livet.Messaging.IO;
using ShellFileDialogSample.Messaging.Dialogs;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Dialogs;

namespace ShellFileDialogSample.Behavior.Messaging.Dialogs
{
    public class SaveFileSelectorInteractionMessageAction : InteractionMessageAction<FrameworkElement>
    {
        protected override void InvokeAction(InteractionMessage m)
        {
            if (m is SelectSaveFileMessage)
            {
                var message = (SelectSaveFileMessage)m;

                var selector = new SaveFileSelector(message.Title);

                selector.OverwritePrompt = message.OverwritePrompt;
                selector.CreatePrompt = message.CreatePrompt;
                selector.IsExpandedMode = message.IsExpandedMode;
                selector.ValidateNames = message.ValidateNames;
                selector.AppendExtension = message.AppendExtension;
                selector.RestoreDirectory = message.RestoreDirectory;
                selector.AddToMostRecentlyUsedList = message.AddToMostRecentlyUsedList;

                selector.DefaultFileName = message.DefaultFileName;
                selector.DefaultFileExtension = message.DefaultFileExtension;
                selector.FileTypeFilters.AddRange(message.FileTypeFilters);

                if (message.InitialFolder != null)
                {
                    selector.InitialFolder = message.InitialFolder;
                }
                if (message.DefaultFolder != null)
                {
                    selector.DefaultFolder = message.DefaultFolder;
                }

                var selectedFile = selector.SelectSaveFileAsync().Result;
                if (selectedFile == null)
                {
                    message.Response = new List<ShellFile>();
                }
                else
                {
                    message.Response = new List<ShellFile>() { selectedFile };
                }
            }
            else if (m is SavingFileSelectionMessage)
            {
                var message = (SavingFileSelectionMessage)m;

                var selector = new SaveFileSelector(message.Title);
                selector.AppendExtension = message.AddExtension;

                selector.OverwritePrompt = message.OverwritePrompt;
                selector.CreatePrompt = message.CreatePrompt;

                if (!String.IsNullOrWhiteSpace(message.InitialDirectory))
                {
                    selector.InitialFolder = ShellFactory.FromFolderPath(message.InitialDirectory);
                }

                var selectedFile = selector.SelectSaveFileAsync().Result;
                if (selectedFile == null)
                {
                    message.Response = new string[0];
                }
                else
                {
                    message.Response = new[] { selectedFile.Path };
                }
            }
        }
    }
}
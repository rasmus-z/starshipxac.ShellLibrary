using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using ShellFileDialogSample.Messaging.Dialogs;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Dialogs;

namespace ShellFileDialogSample.Behavior.Messaging.Dialogs
{
	public class FolderSelectorInteractionMessageAction : InteractionMessageAction<FrameworkElement>
	{
		protected override void InvokeAction(InteractionMessage m)
		{
			var message = m as SelectFolderMessage;
			if (message != null)
			{
				var folderSelector = new FolderSelector(message.Title);
				folderSelector.ForceFileSystem = message.ForceFileSystem;
				if (message.InitialFolder != null)
				{
					folderSelector.InitialFolder = message.InitialFolder;
				}
				if (message.DefaultFolder != null)
				{
					folderSelector.DefaultFolder = message.DefaultFolder;
				}

				if (message.MultiSelect)
				{
					var selectedFolders = folderSelector.SelectMultipleFoldersAsync().Result.ToList();
					message.Response = selectedFolders;
				}
				else
				{
					var selectedFolder = folderSelector.SelectSingleFolderAsync().Result;
					message.Response = new List<ShellFolder>()
					{
						selectedFolder
					};
				}
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Livet.Behaviors.Messaging;
using Livet.Messaging;
using Livet.Messaging.IO;
using ShellFileDialogSample.Messaging.Dialogs;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Dialogs;

namespace ShellFileDialogSample.Behavior.Messaging.Dialogs
{
	public class OpenFileSelectorInteractionMessageAction : InteractionMessageAction<FrameworkElement>
	{
		protected override void InvokeAction(InteractionMessage m)
		{
			if (m is SelectOpenFileMessage)
			{
				var message = (SelectOpenFileMessage)m;

				var selector = new OpenFileSelector(message.Title);
				if (message.InitialFolder != null)
				{
					selector.InitialFolder = message.InitialFolder;
				}
				if (message.DefaultFolder != null)
				{
					selector.DefaultFolder = message.DefaultFolder;
				}

				if (message.MultiSelect)
				{
					var selectedFiles = selector.SelectMultipleFilesAsync().Result.ToList();
					message.Response = selectedFiles;
				}
				else
				{
					var selectedFile = selector.SelectSingleFileAsync().Result;
					if (selectedFile == null)
					{
						message.Response = new List<ShellFile>();
					}
					else
					{
						message.Response = new List<ShellFile>() { selectedFile };
					}
				}
			}
			else if (m is OpeningFileSelectionMessage)
			{
				var message = (OpeningFileSelectionMessage)m;

				var selector = new OpenFileSelector(message.Title);
				if (!String.IsNullOrWhiteSpace(message.InitialDirectory))
				{
					selector.InitialFolder = ShellFactory.FromFolderPath(message.InitialDirectory);
				}

				if (message.MultiSelect)
				{
					var selectedFiles = selector.SelectMultipleFilesAsync().Result;
					message.Response = selectedFiles.Select(x => x.Path).ToArray();
				}
				else
				{
					var selectedFile = selector.SelectSingleFileAsync().Result;
					if (selectedFile != null)
					{
						message.Response = new[] { selectedFile.Path };
					}
				}
			}
		}
	}
}
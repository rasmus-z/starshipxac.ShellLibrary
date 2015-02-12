using System;
using System.Collections.Generic;
using System.Windows;
using Livet.Messaging;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Dialogs;

namespace ShellFileDialogSample.Messaging.Dialogs
{
	public class SelectSaveFileMessage : ResponsiveInteractionMessage<IReadOnlyList<ShellFile>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// Viewでメッセージインスタンスを生成する時のためのコンストラクタ
		/// </remarks>
		public SelectSaveFileMessage()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageKey"></param>
		public SelectSaveFileMessage(string messageKey)
			: base(messageKey)
		{
		}

		#region Title Property

		public string Title
		{
			get
			{
				return (string)GetValue(TitleProperty);
			}
			set
			{
				SetValue(TitleProperty, value);
			}
		}

		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(SelectSaveFileMessage),
				new PropertyMetadata(String.Empty));

		#endregion

		#region OverwritePrompt Property

		public bool OverwritePrompt
		{
			get
			{
				return (bool)GetValue(OverwritePromptProperty);
			}
			set
			{
				SetValue(OverwritePromptProperty, value);
			}
		}

		public static readonly DependencyProperty OverwritePromptProperty =
			DependencyProperty.Register("OverwritePrompt", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(true));

		#endregion

		#region CreatePrompt Property

		public bool CreatePrompt
		{
			get
			{
				return (bool)GetValue(CreatePromptProperty);
			}
			set
			{
				SetValue(CreatePromptProperty, value);
			}
		}

		public static readonly DependencyProperty CreatePromptProperty =
			DependencyProperty.Register("CreatePrompt", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(true));

		#endregion

		#region IsExpandedMode Property

		public bool IsExpandedMode
		{
			get
			{
				return (bool)GetValue(IsExpandedModeProperty);
			}
			set
			{
				SetValue(IsExpandedModeProperty, value);
			}
		}

		public static readonly DependencyProperty IsExpandedModeProperty =
			DependencyProperty.Register("IsExpandedMode", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(false));

		#endregion

		#region ValidateNames Property

		public bool ValidateNames
		{
			get
			{
				return (bool)GetValue(ValidateNamesProperty);
			}
			set
			{
				SetValue(ValidateNamesProperty, value);
			}
		}

		public static readonly DependencyProperty ValidateNamesProperty =
			DependencyProperty.Register("ValidateNames", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(true));

		#endregion

		#region AppendExtension Property

		public bool AppendExtension
		{
			get
			{
				return (bool)GetValue(AppendExtensionProperty);
			}
			set
			{
				SetValue(AppendExtensionProperty, value);
			}
		}

		public static readonly DependencyProperty AppendExtensionProperty =
			DependencyProperty.Register("AppendExtension", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(true));

		#endregion

		#region RestoreDirectory Property

		public bool RestoreDirectory
		{
			get
			{
				return (bool)GetValue(RestoreDirectoryProperty);
			}
			set
			{
				SetValue(RestoreDirectoryProperty, value);
			}
		}

		public static readonly DependencyProperty RestoreDirectoryProperty =
			DependencyProperty.Register("RestoreDirectory", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(false));

		#endregion

		#region AddToMostRecentlyUsedList Property

		public bool AddToMostRecentlyUsedList
		{
			get
			{
				return (bool)GetValue(AddToMostRecentlyUsedListProperty);
			}
			set
			{
				SetValue(AddToMostRecentlyUsedListProperty, value);
			}
		}

		public static readonly DependencyProperty AddToMostRecentlyUsedListProperty =
			DependencyProperty.Register("AddToMostRecentlyUsedList", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(true));

		#endregion

		#region AllowPropertyEditing property

		public bool AllowPropertyEditing
		{
			get
			{
				return (bool)GetValue(AllowPropertyEditingProperty);
			}
			set
			{
				SetValue(AllowPropertyEditingProperty, value);
			}
		}

		public static readonly DependencyProperty AllowPropertyEditingProperty =
			DependencyProperty.Register("AllowPropertyEditing", typeof(bool), typeof(SelectSaveFileMessage),
				new PropertyMetadata(false));

		#endregion

		#region DefaultFileName Property

		public string DefaultFileName
		{
			get
			{
				return (string)GetValue(DefaultFileNameProperty);
			}
			set
			{
				SetValue(DefaultFileNameProperty, value);
			}
		}

		public static readonly DependencyProperty DefaultFileNameProperty =
			DependencyProperty.Register("DefaultFileName", typeof(string), typeof(SelectSaveFileMessage), new PropertyMetadata(String.Empty));

		#endregion

		#region DefaultFileExtension Property

		public string DefaultFileExtension
		{
			get
			{
				return (string)GetValue(DefaultProperty);
			}
			set
			{
				SetValue(DefaultProperty, value);
			}
		}

		public static readonly DependencyProperty DefaultProperty =
			DependencyProperty.Register("DefaultFileExtension", typeof(string), typeof(SelectSaveFileMessage),
				new PropertyMetadata(String.Empty));

		#endregion

		#region FileTypeFilters Property

		public FileTypeFilterCollection FileTypeFilters
		{
			get
			{
				return (FileTypeFilterCollection)GetValue(MyPropertyProperty);
			}
			set
			{
				SetValue(MyPropertyProperty, value);
			}
		}

		public static readonly DependencyProperty MyPropertyProperty =
			DependencyProperty.Register("MyProperty", typeof(FileTypeFilterCollection), typeof(SelectSaveFileMessage),
				new PropertyMetadata(new FileTypeFilterCollection()));

		#endregion

		#region InitialFolder Property

		public ShellFolder InitialFolder
		{
			get
			{
				return (ShellFolder)GetValue(InitialFolderProperty);
			}
			set
			{
				SetValue(InitialFolderProperty, value);
			}
		}

		public static readonly DependencyProperty InitialFolderProperty =
			DependencyProperty.Register("InitialFolder", typeof(ShellFolder), typeof(SelectSaveFileMessage),
				new PropertyMetadata(null));

		#endregion

		#region DefaultFolder Property

		public ShellFolder DefaultFolder
		{
			get
			{
				return (ShellFolder)GetValue(DefaultFolderProperty);
			}
			set
			{
				SetValue(DefaultFolderProperty, value);
			}
		}

		public static readonly DependencyProperty DefaultFolderProperty =
			DependencyProperty.Register("DefaultFolder", typeof(ShellFolder), typeof(SelectSaveFileMessage),
				new PropertyMetadata(null));

		#endregion

		/// <summary>
		/// 派生クラスでは必ずオーバーライドしてください。Freezableオブジェクトとして必要な実装です。<br/>
		/// 通常このメソッドは、自身の新しいインスタンスを返すように実装します。
		/// </summary>
		/// <returns>自身の新しいインスタンス</returns>
		protected override Freezable CreateInstanceCore()
		{
			return new SelectSaveFileMessage();
		}
	}
}
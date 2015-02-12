using System;
using System.Collections.Generic;
using System.Windows;
using Livet.Messaging;
using starshipxac.Shell;

namespace ShellFileDialogSample.Messaging.Dialogs
{
	public sealed class SelectFolderMessage : ResponsiveInteractionMessage<IReadOnlyList<ShellFolder>>
	{
		/// <summary>
		/// <see cref="SelectFolderMessage"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <remarks>
		/// Viewでメッセージインスタンスを生成する時のためのコンストラクタ。
		/// </remarks>
		public SelectFolderMessage()
		{
		}

		/// <summary>
		/// メッセージキーを指定して、<see cref="SelectFolderMessage"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <remarks>
		/// ViewModelからMessenger経由での発信目的でメッセージインスタンスを生成するためのコンストラクタ。
		/// </remarks>
		/// <param name="messageKey"></param>
		public SelectFolderMessage(string messageKey)
			: base(messageKey)
		{
		}

		#region MultiSelect Property

		public static readonly DependencyProperty MultiSelectProperty =
			DependencyProperty.Register("MultiSelect", typeof(bool), typeof(SelectFolderMessage),
				new PropertyMetadata(false));

		public bool MultiSelect
		{
			get
			{
				return (bool)GetValue(MultiSelectProperty);
			}
			set
			{
				SetValue(MultiSelectProperty, value);
			}
		}

		#endregion

		#region Title Property

		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(SelectFolderMessage),
				new PropertyMetadata(String.Empty));

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

		#endregion

		#region ForceFileSystem Property

		public static readonly DependencyProperty ForceFileSystemProperty =
			DependencyProperty.Register("ForceFileSystem", typeof(bool), typeof(SelectFolderMessage),
				new PropertyMetadata(false));

		public bool ForceFileSystem
		{
			get
			{
				return (bool)GetValue(ForceFileSystemProperty);
			}
			set
			{
				SetValue(ForceFileSystemProperty, value);
			}
		}

		#endregion

		#region InitialFolder Property

		public static readonly DependencyProperty InitialFolderProperty =
			DependencyProperty.Register("InitialFolder", typeof(ShellFolder), typeof(SelectFolderMessage),
				new PropertyMetadata(null));

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

		#endregion

		#region DefaultFolder Property

		public static readonly DependencyProperty DefaultFolderProperty =
			DependencyProperty.Register("DefaultFolder", typeof(ShellFolder), typeof(SelectFolderMessage),
				new PropertyMetadata(null));

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

		#endregion

		/// <summary>
		/// 派生クラスでは必ずオーバーライドしてください。Freezableオブジェクトとして必要な実装です。<br/>
		/// 通常このメソッドは、自身の新しいインスタンスを返すように実装します。
		/// </summary>
		/// <returns>自身の新しいインスタンス</returns>
		protected override Freezable CreateInstanceCore()
		{
			return new SelectFolderMessage(this.MessageKey);
		}
	}
}
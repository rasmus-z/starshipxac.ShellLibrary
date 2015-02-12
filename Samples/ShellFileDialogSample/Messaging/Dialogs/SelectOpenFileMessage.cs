using System;
using System.Collections.Generic;
using System.Windows;
using Livet.Messaging;
using starshipxac.Shell;

namespace ShellFileDialogSample.Messaging.Dialogs
{
	public class SelectOpenFileMessage : ResponsiveInteractionMessage<IReadOnlyList<ShellFile>>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// Viewでメッセージインスタンスを生成する時のためのコンストラクタ
		/// </remarks>
		public SelectOpenFileMessage()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageKey"></param>
		public SelectOpenFileMessage(string messageKey)
			: base(messageKey)
		{
		}

		#region MultiSelect Property

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

		public static readonly DependencyProperty MultiSelectProperty =
			DependencyProperty.Register("MultiSelect", typeof(bool), typeof(SelectOpenFileMessage),
				new PropertyMetadata(false));

		#endregion

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
			DependencyProperty.Register("Title", typeof(string), typeof(SelectOpenFileMessage),
				new PropertyMetadata(String.Empty));

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
			DependencyProperty.Register("InitialFolder", typeof(ShellFolder), typeof(SelectOpenFileMessage),
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
			DependencyProperty.Register("DefaultFolder", typeof(ShellFolder), typeof(SelectOpenFileMessage),
				new PropertyMetadata(null));

		#endregion

		/// <summary>
		/// 派生クラスでは必ずオーバーライドしてください。Freezableオブジェクトとして必要な実装です。<br/>
		/// 通常このメソッドは、自身の新しいインスタンスを返すように実装します。
		/// </summary>
		/// <returns>自身の新しいインスタンス</returns>
		protected override Freezable CreateInstanceCore()
		{
			return new SelectOpenFileMessage();
		}
	}
}
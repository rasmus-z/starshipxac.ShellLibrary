using System;
using System.Windows;
using Livet.Messaging;
using starshipxac.Shell;

namespace ShellFileDialogSample.Messaging.Dialogs
{
	public sealed class CustomFileOpenDialogMessage : ResponsiveInteractionMessage<CustomFileOpenDialogResponse>
	{
		public CustomFileOpenDialogMessage()
		{
		}

		public CustomFileOpenDialogMessage(string messageKey)
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
			DependencyProperty.Register("Title", typeof(string), typeof(CustomFileOpenDialogMessage),
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
			DependencyProperty.Register("InitialFolder", typeof(ShellFolder), typeof(CustomFileOpenDialogMessage),
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
			DependencyProperty.Register("DefaultFolder", typeof(ShellFolder), typeof(CustomFileOpenDialogMessage),
				new PropertyMetadata(null));

		#endregion

		/// <summary>
		/// 派生クラスでは必ずオーバーライドしてください。Freezableオブジェクトとして必要な実装です。<br/>
		/// 通常このメソッドは、自身の新しいインスタンスを返すように実装します。
		/// </summary>
		/// <returns>自身の新しいインスタンス</returns>
		protected override Freezable CreateInstanceCore()
		{
			return new CustomFileOpenDialogMessage();
		}
	}
}
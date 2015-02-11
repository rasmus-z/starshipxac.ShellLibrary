using System;
using System.IO;
using starshipxac.Shell;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.FileSystem
{
	/// <summary>
	/// <para>
	/// ファイル操作の進捗を通知します。
	/// </para>
	/// </summary>
	public abstract class FileOperationProgress
	{
		/// <summary>
		/// ファイル操作が開始されると呼び出されます。
		/// </summary>
		public virtual void StartOperations()
		{
		}

		/// <summary>
		/// ファイル操作が終了すると呼び出されます。
		/// </summary>
		/// <param name="result"></param>
		public virtual void FinishOperations(HRESULT result)
		{
		}

		/// <summary>
		/// ファイル操作状況が更新されると呼び出されます。
		/// </summary>
		/// <param name="workTotal"></param>
		/// <param name="workSoFar"></param>
		public virtual void UpdateProgress(UInt32 workTotal, UInt32 workSoFar)
		{
		}

		/// <summary>
		/// ファイルがコピーされる前に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		public virtual void PreCopy(TransferSourceFlags flags,
			ShellObject shellObject, ShellObject destinationFolder,
			string newName)
		{
		}

		/// <summary>
		/// ファイルがコピーされた後に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		/// <param name="hrCopy"></param>
		/// <param name="newlyCreated"></param>
		public virtual void PostCopy(TransferSourceFlags flags,
			ShellObject shellObject, ShellObject destinationFolder,
			string newName, HRESULT hrCopy, ShellObject newlyCreated)
		{
		}

		/// <summary>
		/// ファイルが移動される前に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		public virtual void PreMove(TransferSourceFlags flags,
			ShellObject shellObject, ShellObject destinationFolder,
			string newName)
		{
		}

		/// <summary>
		/// ファイルが移動した後に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		/// <param name="hrMove"></param>
		/// <param name="newlyCreated"></param>
		public virtual void PostMove(TransferSourceFlags flags,
			ShellObject shellObject, ShellObject destinationFolder,
			string newName, HRESULT hrMove, ShellObject newlyCreated)
		{
		}

		/// <summary>
		/// ファイルが削除される前に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		public virtual void PreDelete(TransferSourceFlags flags, ShellObject shellObject)
		{
		}

		/// <summary>
		/// ファイルが削除された後に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="hrDelete"></param>
		/// <param name="newlyCreated"></param>
		public virtual void PostDelete(TransferSourceFlags flags, ShellObject shellObject,
			HRESULT hrDelete, ShellObject newlyCreated)
		{
		}

		/// <summary>
		/// ファイル名が変更される前に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="newName"></param>
		public virtual void PreRename(TransferSourceFlags flags, ShellObject shellObject,
			string newName)
		{
		}

		/// <summary>
		/// ファイル名が変更された後に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="shellObject"></param>
		/// <param name="newName"></param>
		/// <param name="hrRename"></param>
		/// <param name="newlyCreated"></param>
		public virtual void PostRename(TransferSourceFlags flags, ShellObject shellObject,
			string newName, HRESULT hrRename, ShellObject newlyCreated)
		{
		}

		/// <summary>
		/// ファイルが新規作成される前に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		public virtual void PreNew(TransferSourceFlags flags, ShellObject destinationFolder,
			string newName)
		{
		}

		/// <summary>
		/// ファイルが新規作成された後に呼び出されます。
		/// </summary>
		/// <param name="flags"></param>
		/// <param name="destinationFolder"></param>
		/// <param name="newName"></param>
		/// <param name="templateName"></param>
		/// <param name="dwFileAttributes"></param>
		/// <param name="hrNew"></param>
		/// <param name="newItem"></param>
		public virtual void PostNew(TransferSourceFlags flags, ShellObject destinationFolder,
			string newName, string templateName,
			FileAttributes dwFileAttributes,
			HRESULT hrNew, ShellObject newItem)
		{
		}
	}
}
using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    /// フォルダーの動作を定義します。
    /// </summary>
    [Flags]
    public enum FolderDefinitionFlags : uint
    {
        /// <summary>
        /// 定義なし。
        /// </summary>
        None = 0,

        LocalRedirectOnly = KF_DEFINITION_FLAGS.KFDF_LOCAL_REDIRECT_ONLY,

        Roamable = KF_DEFINITION_FLAGS.KFDF_ROAMABLE,

        Precreate = KF_DEFINITION_FLAGS.KFDF_PRECREATE,

        Stream = KF_DEFINITION_FLAGS.KFDF_STREAM,

        PublishExpandedPath = KF_DEFINITION_FLAGS.KFDF_PUBLISHEXPANDEDPATH,
    }
}
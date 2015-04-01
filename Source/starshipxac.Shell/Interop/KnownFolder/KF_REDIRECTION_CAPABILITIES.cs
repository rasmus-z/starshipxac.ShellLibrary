using System;

namespace starshipxac.Shell.Interop.KnownFolder
{
    /// <summary>
    /// 標準フォルダーリダイレクトフラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762514(v=vs.85).aspx
    /// </remarks>
    internal enum KF_REDIRECTION_CAPABILITIES : uint
    {
        KF_REDIRECTION_CAPABILITIES_ALLOW_ALL = 0xff,
        KF_REDIRECTION_CAPABILITIES_REDIRECTABLE = 0x1,
        KF_REDIRECTION_CAPABILITIES_DENY_ALL = 0xfff00,
        KF_REDIRECTION_CAPABILITIES_DENY_POLICY_REDIRECTED = 0x100,
        KF_REDIRECTION_CAPABILITIES_DENY_POLICY = 0x200,
        KF_REDIRECTION_CAPABILITIES_DENY_PERMISSIONS = 0x400
    }
}
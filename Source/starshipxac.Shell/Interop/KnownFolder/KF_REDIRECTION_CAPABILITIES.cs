using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.KnownFolder
{
    /// <summary>
    ///     Flags that specify the current redirection capabilities of a known folder.
    ///     Used by <see cref="IKnownFolder.GetRedirectionCapabilities" />.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762514(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum KF_REDIRECTION_CAPABILITIES : uint
    {
        /// <summary>
        ///     The folder can be redirected if any of the bits in the lower byte of the value are set but no DENY flag is set.
        ///     DENY flags are found in the upper byte of the value.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_ALLOW_ALL = 0xff,

        /// <summary>
        ///     The folder can be redirected. Currently, redirection exists for only common and user folders;
        ///     fixed and virtual folders cannot be redirected.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_REDIRECTABLE = 0x1,

        /// <summary>
        ///     Redirection is not allowed.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_DENY_ALL = 0xfff00,

        /// <summary>
        ///     The folder cannot be redirected because it is already redirected by group policy.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_DENY_POLICY_REDIRECTED = 0x100,

        /// <summary>
        ///     The folder cannot be redirected because the policy prohibits redirecting this folder.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_DENY_POLICY = 0x200,

        /// <summary>
        ///     The folder cannot be redirected because the calling application does not have sufficient permissions.
        /// </summary>
        KF_REDIRECTION_CAPABILITIES_DENY_PERMISSIONS = 0x400
    }
}
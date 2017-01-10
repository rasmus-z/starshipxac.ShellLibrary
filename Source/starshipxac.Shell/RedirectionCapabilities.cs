using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Flags that specify the current redirection capabilities of a known folder.
    /// </summary>
    public enum RedirectionCapability : uint
    {
        None = 0,

        /// <summary>
        ///     The folder can be redirected if any of the bits in the lower byte of the value are set but no DENY flag is set.
        ///     DENY flags are found in the upper byte of the value.
        /// </summary>
        AllowAll = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_ALLOW_ALL,

        /// <summary>
        ///     The folder can be redirected. Currently, redirection exists for only common and user folders;
        ///     fixed and virtual folders cannot be redirected.
        /// </summary>
        Redirectable = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_REDIRECTABLE,

        /// <summary>
        ///     Redirection is not allowed.
        /// </summary>
        DenyAll = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_ALL,

        /// <summary>
        ///     The folder cannot be redirected because it is already redirected by group policy.
        /// </summary>
        DenyPolicyRedirected = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_POLICY_REDIRECTED,

        /// <summary>
        ///     The folder cannot be redirected because the policy prohibits redirecting this folder.
        /// </summary>
        DenyPolicy = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_POLICY,

        /// <summary>
        ///     The folder cannot be redirected because the calling application does not have sufficient permissions.
        /// </summary>
        DenyPermissions = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_PERMISSIONS
    }
}
using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
	public enum RedirectionCapability : uint
	{
		None = 0,

		AllowAll = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_ALLOW_ALL,

		Redirectable = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_REDIRECTABLE,

		DenyAll = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_ALL,

		DenyPolicyRedirected = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_POLICY_REDIRECTED,

		DenyPolicy = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_POLICY,

		DenyPermissions = KF_REDIRECTION_CAPABILITIES.KF_REDIRECTION_CAPABILITIES_DENY_PERMISSIONS
	}
}
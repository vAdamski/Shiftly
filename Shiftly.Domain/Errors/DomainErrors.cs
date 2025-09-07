using Shiftly.Domain.Common;

namespace Shiftly.Domain.Errors;

public static class DomainErrors
{
	public static class User
	{
		public static Error EmailAlreadyExists(string email) =>
			new Error("EmailAlreadyExists", $"User with email {email} already exists.");
		
		public static Error UserNotFound =>
			new Error("UserNotFound", $"User with this email not found.");
		
		public static Error InvalidPassword =>
			new Error("InvalidPassword", $"Invalid password.");
		
		public static Error InvalidActivationToken =>
			new Error("InvalidActivationCode", $"Invalid activation code.");
		
		public static Error UserAlreadyActivated =>
			new Error("UserAlreadyActivated", $"User is already activated.");
		
		public static Error UserNotActivated =>
			new Error("UserNotActivated", $"User is not activated.");
	}
	
	public static class RefreshToken
	{
		public static Error InvalidRefreshToken =>
			new Error("InvalidRefreshToken", $"Invalid refresh token.");
		
		public static Error ExpiredRefreshToken =>
			new Error("ExpiredRefreshToken", $"Expired refresh token.");
	}

	public static class Organization
	{
		public static Error OrganizationNotFound =>
			new Error("OrganizationNotFound", "Organization not found.");

		public static Error OnlyOwnerCanManageMembers =>
			new Error("OnlyOwnerCanManageMembers", "Only the organization owner can manage members.");
	}
}
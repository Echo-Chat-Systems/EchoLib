namespace Models.Permissions;

/// <summary>
/// Text channel permissions.
/// </summary>
[Flags]
public enum TextPermissions : ulong
{
	ModerateMessages = 0x2000_0000,
	ModerateEmbeds = 0x1000_0000,
	ModerateAttachments = 0x0800_0000,
	ModeratePins = 0x0400_0000,
	ModerateReactions = 0x0200_0000,
	UnpinMessages = 0x0008_0000,
	PinMessages = 0x0004_0000,
	EmbedLinks = 0x0002_0000,
	AttachFiles = 0x0001_0000,
	AddReactions = 0x0000_8000,
	DeletePrivateThreads = 0x0000_4000,
	CreatePrivateThreads = 0x0000_2000,
	SendTtsMessages = 0x0000_1000,
	DeletePublicThreads = 0x0000_0800,
	CreatePublicThreads = 0x0000_0400,
	UseExternalAnimatedStickers = 0x0000_0200,
	UseExternalStickers = 0x0000_0100,
	UseAnimatedExternalEmojis = 0x0000_0080,
	UseExternalEmojis = 0x0000_0040,
	UseAnimatedStickers = 0x0000_0020,
	UseStickers = 0x0000_0010,
	UseAnimatedEmojis = 0x0000_0008,
	UseEmojis = 0x0000_0004,
	DeleteMessages = 0x0000_0002,
	SendMessages = 0x0000_0001
}

/// <summary>
/// Extensions for <see cref="TextPermissions"/> to handle OAuth scopes and permission checks.
/// </summary>
public static class TextChannelPermissionsExtensions
{
	public static Dictionary<string, TextPermissions> OAuthMapping { get; } = new()
	{
		{ "text:messages:moderate", TextPermissions.ModerateMessages },
		{ "text:embeds:moderate", TextPermissions.ModerateEmbeds },
		{ "text:attachments:moderate", TextPermissions.ModerateAttachments },
		{ "text:pins:moderate", TextPermissions.ModeratePins },
		{ "text:reactions:moderate", TextPermissions.ModerateReactions },
		{ "text:pins:manage", TextPermissions.UnpinMessages },
		{ "text:pins:add", TextPermissions.PinMessages },
		{ "text:links:embed", TextPermissions.EmbedLinks },
		{ "text:files:attach", TextPermissions.AttachFiles },
		{ "text:reactions:add", TextPermissions.AddReactions },
		{ "text:private_threads:delete", TextPermissions.DeletePrivateThreads },
		{ "text:private_threads:create", TextPermissions.CreatePrivateThreads },
		{ "text:tts:send", TextPermissions.SendTtsMessages },
		{ "text:public_threads:delete", TextPermissions.DeletePublicThreads },
		{ "text:public_threads:create", TextPermissions.CreatePublicThreads },
		{ "text:stickers:external_animated", TextPermissions.UseExternalAnimatedStickers },
		{ "text:stickers:external", TextPermissions.UseExternalStickers },
		{ "text:emojis:external_animated", TextPermissions.UseAnimatedExternalEmojis },
		{ "text:emojis:external", TextPermissions.UseExternalEmojis },
		{ "text:stickers:animated", TextPermissions.UseAnimatedStickers },
		{ "text:stickers:default", TextPermissions.UseStickers },
		{ "text:emojis:animated", TextPermissions.UseAnimatedEmojis },
		{ "text:emojis:default", TextPermissions.UseEmojis },
		{ "text:messages:delete", TextPermissions.DeleteMessages },
		{ "text:messages:send", TextPermissions.SendMessages }
	};

	/// <summary>
	/// Converts a list of scopes to a set of permissions.
	/// </summary>
	/// <param name="scopes">Source scopes list.</param>
	/// <returns>Permissions set.</returns>
	public static TextPermissions FromOAuth(List<string> scopes)
	{
		TextPermissions permissions = 0;
		foreach (string scope in scopes)
		{
			if (OAuthMapping.TryGetValue(scope, out TextPermissions permission))
			{
				permissions |= permission;
			}
		}
		return permissions;
	}

	/// <summary>
	/// Converts a set of permissions to a list of OAuth scopes.
	/// </summary>
	/// <param name="permissions"></param>
	/// <returns></returns>
	public static List<string> ToOAuth(this TextPermissions permissions)
	{
		List<string> scopes = [];
		scopes.AddRange(from kvp in OAuthMapping where permissions.HasPermission(kvp.Value) select kvp.Key);

		return scopes;
	}

	/// <summary>
	/// Checks if the permission is set.
	/// </summary>
	/// <param name="permissions">The permissions to check.</param>
	/// <param name="permission">The permission to check for.</param>
	/// <returns>True if the permission is set, otherwise false.</returns>
	public static bool HasPermission(this TextPermissions permissions, TextPermissions permission)
	{
		return (permissions & permission) == permission;
	}

	/// <summary>
	/// Sets the specified permission.
	/// </summary>
	/// <param name="permissions">The permission set.</param>
	/// <param name="permission">The permission to add.</param>
	/// <returns>Updated permissions set.</returns>
	public static TextPermissions AddPermission(this TextPermissions permissions, TextPermissions permission)
	{
		return permissions | permission;
	}


	/// <summary>
	/// Removes the specified permission.
	/// </summary>
	/// <param name="permissions">The permission set.</param>
	/// <param name="permission">The permission to remove.</param>
	/// <returns>Updated permissions set.</returns>
	public static TextPermissions RemovePermission(this TextPermissions permissions, TextPermissions permission)
	{
		return permissions & ~permission;
	}
}
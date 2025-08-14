using Models.Crypto.Encryption;
using Models.Crypto.Signing;

namespace Models.User;

public class UserModel : BaseEntityModel
{
	/// <summary>
	/// User's public signing key.
	/// </summary>
	public new required UserId Id { get; init; }

	/// <summary>
	/// User public encryption key.
	/// </summary>
	public required PublicEncryptionKey EncryptionKey { get; init; }

	/// <summary>
	/// User encrypted settings.
	/// </summary>
	public required byte[] EncryptedSettings { get; set; }

	/// <summary>
	/// Last-seen date for user. May not match their status.
	/// </summary>
	public DateTime? LastOnline { get; set; }

	/// <summary>
	/// Is the user actively online at this moment.
	/// </summary>
	public bool IsOnline { get; set; }

	/// <summary>
	/// Is the user currently banned from the server.
	/// </summary>
	public bool IsBanned { get; set; }

	/// <summary>
	/// User profile.
	/// </summary>
	public required ProfileModel Profile { get; set; }
}
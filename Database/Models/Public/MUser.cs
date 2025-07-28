using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Helpers;

namespace Database.Models.Public;

public class MUser : BaseModel
{
	public MUser(IDataRecord record) : base(record)
	{
		Id = record.GetString(record.GetOrdinal("id"));
		EncryptionKey = record.GetString(record.GetOrdinal("encryption_key"));
		Username = record.GetString(record.GetOrdinal("username"));
		Tag = record.GetInt32(record.GetOrdinal("tag"));
		EncryptedSettings = (byte[])record.GetValue(record.GetOrdinal("settings"));
		LastOnline = record.GetDateTime(record.GetOrdinal("last_online"));
		IsBanned = record.GetBoolean(record.GetOrdinal("is_banned"));

		Profile = JsonSerializer.Deserialize<MProfile>(record.GetString(record.GetOrdinal("profile"))) ?? throw new InvalidDataException(nameof(ProfileRaw));
	}

	// Internal fields for models
	private MProfile _profile = null!;

	/// <summary>
	/// User's public signing key.
	/// </summary>
	public new string Id { get; init; }

	/// <summary>
	/// User public encryption key.
	/// </summary>
	public string EncryptionKey { get; init; }

	/// <summary>
	/// Username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// User's tag
	/// </summary>
	public int Tag { get; set; }

	/// <summary>
	/// User encrypted settings.
	/// </summary>
	public byte[] EncryptedSettings { get; set; }

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
	/// Raw serialised <see cref="Profile"/>
	/// </summary>
	public string ProfileRaw { get; private set; } = null!;

	/// <summary>
	/// Channel customisation.
	/// </summary>
	public MProfile Profile
	{
		get => _profile;
		set
		{
			_profile = value;
			ProfileRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
		}
	}
	

	public class MProfile
	{
		[JsonPropertyName("pronouns")] public string? Pronouns { get; set; }
		[JsonPropertyName("bio")] public string? Bio { get; set; }
		[JsonPropertyName("css")] public string? Css { get; set; }
		[JsonPropertyName("pfp")] public Uri? Pfp { get; set; }
		[JsonPropertyName("banner")] public Uri? Banner { get; set; }
		[JsonPropertyName("timezone")] public TimeZoneInfo? TimeZone { get; set; }
	}
}
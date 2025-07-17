using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Database.Models.Public;

public class MUser(IDataRecord record) : BaseModel(record)
{
	/// <summary>
	/// User's public signing key.
	/// </summary>
	public new PublicSigningKey Id { get; } = new((byte[])record.GetValue(record.GetOrdinal("id")));

	/// <summary>
	/// Username.
	/// </summary>
	public string Username { get; set; } = record.GetString(record.GetOrdinal("username"));

	/// <summary>
	/// User's tag
	/// </summary>
	public int Tag { get; set; } = record.GetInt32(record.GetOrdinal("tag"));

	public string ProfileRaw { get; private set; } = record.GetString(record.GetOrdinal("profile"));

	public byte[] EncryptedSettings { get; set; } = (byte[])record.GetValue(record.GetOrdinal("settings"));

	public DateTime? LastOnline { get; set; } = record.GetDateTime(record.GetOrdinal("last_online"));

	public bool IsOnline { get; set; } = record.GetBoolean(record.GetOrdinal("is_online"));

	public bool IsBanned { get; set; } = record.GetBoolean(record.GetOrdinal("is_banned"));

	private MProfile? _profile;

	public MProfile Profile
	{
		// Deserialise on first read attempt
		get => (_profile ??= JsonSerializer.Deserialize<MProfile>(ProfileRaw, StaticOptions.JsonSerialzer))!;
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
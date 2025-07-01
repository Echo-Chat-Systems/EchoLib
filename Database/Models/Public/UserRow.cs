using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Public;

public class UserRow(IDataRecord record) : BaseModel(record)
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

	public string ProfileRaw { get; set; } = record.GetString(record.GetOrdinal("profile"));

	public byte[] EncryptedSettings { get; set; } = (byte[])record.GetValue(record.GetOrdinal("settings"));

	public DateTime? LastOnline { get; set; } = record.GetDateTime(record.GetOrdinal("last_online"));

	public bool IsOnline { get; set; } = record.GetBoolean(record.GetOrdinal("is_online"));

	public bool IsBanned { get; set; } = record.GetBoolean(record.GetOrdinal("is_banned"));
}
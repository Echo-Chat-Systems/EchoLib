using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Public;

public class MInvite(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; set; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public Guid ChannelId { get; set; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public PublicSigningKey CreatedBy { get; } = new((byte[])record.GetValue(record.GetOrdinal("inviter_id")));
	public int Uses { get; set; } = record.GetInt32(record.GetOrdinal("uses"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public DateTime? ExpiresAt { get; set; } = record.GetDateTime(record.GetOrdinal("expires_at"));
	public PublicSigningKey? TargetUserId { get; set; } = record.IsDBNull(record.GetOrdinal("target_user_id"))
		? null
		: new PublicSigningKey((byte[])record.GetValue(record.GetOrdinal("target_user_id")));
}
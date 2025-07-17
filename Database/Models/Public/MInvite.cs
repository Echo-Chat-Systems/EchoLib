using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Public;

public class MInvite(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; set; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public Guid ChannelId { get; set; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public string CreatedBy { get; } = record.GetString(record.GetOrdinal("inviter_id"));
	public int Uses { get; set; } = record.GetInt32(record.GetOrdinal("uses"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public DateTime? ExpiresAt { get; set; } = record.GetDateTime(record.GetOrdinal("expires_at"));
	public UserId? TargetUserId { get; set; } = new(record.GetString(record.GetOrdinal("target_user_id")));
}
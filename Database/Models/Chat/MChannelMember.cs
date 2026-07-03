using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Chat;

public class MChannelMember(IDataRecord record) : BaseModel(record)
{
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("user_id")));
	public Guid ChannelId { get; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public long Permissions { get; set; } = record.GetInt64(record.GetOrdinal("permissions"));
}
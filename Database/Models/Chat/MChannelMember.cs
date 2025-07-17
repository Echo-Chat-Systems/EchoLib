using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Chat;

public class MChannelMember(IDataRecord record) : BaseModel(record)
{
	public PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("user_id")));
	public Guid ChannelId { get; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public long Permissions { get; set; } = record.GetInt64(record.GetOrdinal("permissions"));
}
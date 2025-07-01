using System.Data;
using EchoLib.Auth.Signing;
using EchoLib.Models.Permissions;

namespace EchoLib.Database.Models.Public;

public class ChannelMemberRow(IDataRecord record) : BaseModel(record)
{
	PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("user_id")));
	public Guid ChannelId { get; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public long Permissions { get; set; } = record.GetInt64(record.GetOrdinal("permissions"));
}
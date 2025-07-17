using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Secure;

public class MChannelCommit(IDataRecord record) : BaseModel(record)
{
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("user_id")));
	public Guid ChannelId { get; set; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public ulong Epoch { get; set; } = (ulong)record.GetInt64(record.GetOrdinal("epoch"));
	public byte[] Commit { get; set; } = (byte[])record.GetValue(record.GetOrdinal("encrypted_commit"));
}
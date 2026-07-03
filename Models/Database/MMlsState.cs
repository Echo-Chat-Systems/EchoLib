using System.Data;

namespace Database.Models;

public class MMlsState(IDataRecord record) : BaseModel(record)
{
	public DateTime LastUpdated { get; set; } = record.GetDateTime(record.GetOrdinal("last_updated"));
	public Guid ChannelMemberId { get; set; } = record.GetGuid(record.GetOrdinal("channel_member_id"));
	public byte[] Nonce { get; set; } = (byte[])record.GetValue(record.GetOrdinal("nonce"));
	public byte[] EncryptedState { get; set; } = (byte[])record.GetValue(record.GetOrdinal("encrypted_state"));
}
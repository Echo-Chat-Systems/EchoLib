using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Public;

public class MMessage(IDataRecord record) : BaseModel(record)
{
	public PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("user_id")));
	public Guid ChannelId { get; } = record.GetGuid(record.GetOrdinal("channel_id"));
	public int? Epoch { get; set; } = record.GetInt32(record.GetOrdinal("epoch"));
	public int? SenderIndex { get; set; } = record.GetInt32(record.GetOrdinal("sender_index"));
	public byte[] Body { get; set; } = (byte[])record.GetValue(record.GetOrdinal("body"));
	public string MetadataRaw { get; private set; } = record.GetString(record.GetOrdinal("metadata"));
}
using System.Data;

namespace EchoLib.Database.Models.Public;

public class MessageAttachmentRow(IDataRecord record) : BaseModel(record)
{
	public Guid MessageId { get; } = record.GetGuid(record.GetOrdinal("message_id"));
	public Guid FileId { get; } = record.GetGuid(record.GetOrdinal("file_id"));
}
using System.Data;

namespace Database.Models.Media;

public class MMessageAttachment(IDataRecord record) : BaseModel(record)
{
	public Guid MessageId { get; } = record.GetGuid(record.GetOrdinal("message_id"));
	public Guid FileId { get; } = record.GetGuid(record.GetOrdinal("file_id"));
}
using System.Data;
using Models;

namespace Database.Models.Media;

public class DMessageAttachment(IDataRecord record) : BaseModel(record)
{
	public Snowflake MessageId { get; } = new(record.GetInt64(record.GetOrdinal("message_id")));
	public Snowflake FileId { get; } = new(record.GetInt64(record.GetOrdinal("file_id")));
}
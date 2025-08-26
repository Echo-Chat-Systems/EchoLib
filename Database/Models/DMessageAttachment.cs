using System.Data;
using Models.Generic;

namespace Database.Models;

public class DMessageAttachment(IDataRecord record) : BaseModel(record)
{
	public Snowflake MessageId { get; } = new(record.GetInt64(record.GetOrdinal("message_id")));
	public Snowflake FileId { get; } = new(record.GetInt64(record.GetOrdinal("file_id")));
}
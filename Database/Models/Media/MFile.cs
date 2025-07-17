using System.Data;

namespace Database.Models.Media;

public class MFile(IDataRecord record) : BaseModel(record)
{
	public DateTime LastAccessed { get; } = record.GetDateTime(record.GetOrdinal("last_accessed"));
	public PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("created_by")));
}
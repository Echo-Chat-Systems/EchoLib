using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Media;

public class MFile(IDataRecord record) : BaseModel(record)
{
	public DateTime LastAccessed { get; } = record.GetDateTime(record.GetOrdinal("last_accessed"));
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("created_by")));
}
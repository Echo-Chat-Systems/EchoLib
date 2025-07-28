using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Media;

public class MFile : BaseModel
{

	/// <inheritdoc />
	public MFile(IDataRecord record) : base(record)
	{
		LastAccessed = record.GetDateTime(record.GetOrdinal("last_accessed"));
		UserId = new UserId(record.GetString(record.GetOrdinal("created_by")));
	}

	public DateTime LastAccessed { get; set; }
	public UserId UserId { get; init; }
}
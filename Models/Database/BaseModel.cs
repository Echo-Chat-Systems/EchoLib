using System.Data;

namespace EchoLib.Models.Database;

public class BaseModel (IDataRecord record)
{
	/// <summary>
	/// Item database id.
	/// </summary>
	public Guid Id { get; } = record.GetGuid(record.GetOrdinal("id"));

	/// <summary>
	/// Row created at.
	/// </summary>
	public DateTime CreatedAt { get; } = record.GetDateTime(record.GetOrdinal("created_at"));
}
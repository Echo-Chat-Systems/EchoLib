using System.Data;

namespace Database.Models.Chat;

public class MGuild(IDataRecord record) : BaseModel(record)
{
	public Guid OwnerId { get; } = record.GetGuid(record.GetOrdinal("owner_id"));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
}
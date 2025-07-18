using System.Data;

namespace Database.Models.Public;

public class MRole(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public ulong Permissions { get; set; } = (ulong)record.GetInt64(record.GetOrdinal("permissions"));
}
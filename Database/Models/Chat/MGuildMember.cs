using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Chat;

public class MGuildMember (IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("user_id")));
	public string? Nickname { get; set; } = record.GetString(record.GetOrdinal("nickname"));
	public string CustomisationOverrideRaw { get; set; } = record.GetString(record.GetOrdinal("customisation_override"));
}
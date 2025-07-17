using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Public;

public class MGuildMember (IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("user_id")));
	public string? Nickname { get; set; } = record.GetString(record.GetOrdinal("nickname"));
	public string CustomisationOverrideRaw { get; set; } = record.GetString(record.GetOrdinal("customisation_override"));
}
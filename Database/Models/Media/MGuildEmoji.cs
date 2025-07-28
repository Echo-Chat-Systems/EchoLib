using System.Data;
using Core.Auth.Signing;
using Core.Helpers.Snowflake;

namespace Database.Models.Media;

public class MGuildEmoji(IDataRecord record) : BaseModel(record)
{
	public Snowflake GuildId { get; } = new(record.GetInt64(record.GetOrdinal("guild_id")));
	public UserId CreatedBy { get; } = new(record.GetString(record.GetOrdinal("created_by")));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public MediaType Type { get; set; } = (MediaType)record.GetInt16(record.GetOrdinal("type"));
	public string? CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public Snowflake FileId { get; set; } = new(record.GetInt64(record.GetOrdinal("file_id")));

	[Flags]
	public enum MediaType : ushort
	{
		Emoji = 0x0001,
		Sticker = 0x0002,
		Animated = 0x0004,
		Static = 0x0008
	}
}
using System.Data;

namespace EchoLib.Database.Models.Public;

public class GuildEmojiRow(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public Guid CreatedBy { get; } = record.GetGuid(record.GetOrdinal("created_by"));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public MediaType Type { get; set; } = (MediaType)record.GetInt16(record.GetOrdinal("type"));
	public string? CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public Guid FileId { get; set; } = record.GetGuid(record.GetOrdinal("file_id"));

	[Flags]
	public enum MediaType : short
	{
		Emoji = 0x0001,
		Sticker = 0x0002,
		Animated = 0x0004,
		Static = 0x0008
	}
}
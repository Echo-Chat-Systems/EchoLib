using System.Data;
using EchoLib.Helpers;

namespace EchoLib.Database.Models.Chat;

public class MChannelCategory(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public CategoryType Type { get; set; } = (CategoryType)record.GetInt32(record.GetOrdinal("type"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public string ConfigRaw { get; private set; } = record.GetString(record.GetOrdinal("config"));

	private ChannelCategoryConfig? _config;

	public ChannelCategoryConfig Config
	{
		get
		{
			if (_config != null) return _config;

			// Config is null, deserialize it
			_config = System.Text.Json.JsonSerializer.Deserialize<ChannelCategoryConfig>(ConfigRaw, StaticOptions.JsonSerialzer);
			return _config!;
		}

		set
		{
			// Serialize the config and set it
			ConfigRaw = System.Text.Json.JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
			_config = value;
		}
	}

	public class ChannelCategoryConfig
	{

	}

	public enum CategoryType : int
	{
		Standard = 0,
		Voice = 1,
		Announcement = 2,
		Forum = 3,
		Stage = 4
	}
}
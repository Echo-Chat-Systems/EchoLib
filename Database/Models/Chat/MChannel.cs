using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using EchoLib.Helpers;

namespace EchoLib.Database.Models.Chat;

public class MChannel(IDataRecord record) : BaseModel(record)
{
	public Guid GuildId { get; } = record.GetGuid(record.GetOrdinal("guild_id"));
	public string Name { get; set; } = record.GetString(record.GetOrdinal("name"));
	public ChannelType Type { get; set; } = (ChannelType)record.GetInt16(record.GetOrdinal("type"));
	public string CustomisationRaw { get; set; } = record.GetString(record.GetOrdinal("customisation"));
	public string ConfigRaw { get; private set; } = record.GetString(record.GetOrdinal("config"));

	private ChannelConfig? _config;

	public ChannelConfig Config
	{
		get
		{
			if (_config != null) return _config;

			// Config is null, deserialize it
			_config = JsonSerializer.Deserialize<ChannelConfig>(ConfigRaw, StaticOptions.JsonSerialzer);
			return _config!;
		}

		set
		{
			// Serialize the config and set it
			ConfigRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
			_config = value;
		}
	}

	public class ChannelConfig
	{
		// Encrypted status cannot be changed after creation
		[JsonPropertyName("encrypted")]
		public bool IsEncrypted { get; }

		[JsonPropertyName("parent")]
		public Guid? ParentId { get; set; }
	}

	public enum ChannelType : short
	{
		Text = 0,
		Voice = 1
	}
}
using System.Data;
using System.Text.Json;
using Core.Helpers;
using Core.Helpers.Snowflake;

namespace Database.Models.Chat;

public class MChannelCategory : BaseModel
{
	// Default models
	public static readonly MCustomisation DefaultCustomisation = new() { };
	public static readonly MConfig DefaultConfig = new MConfig() { };

	/// <inheritdoc />
	public MChannelCategory(IDataRecord record) : base(record)
	{
		GuildId = new Snowflake(record.GetInt64(record.GetOrdinal("guild_id")));
		Name = record.GetString(record.GetOrdinal("name"));
		Type = (CategoryType)record.GetInt32(record.GetOrdinal("type"));
		CustomisationRaw = record.GetString(record.GetOrdinal("customisation"));
		ConfigRaw = record.GetString(record.GetOrdinal("config"));

		// Deserialise models
		DeserialiseModels();
	}

	/// <inheritdoc />
	/// <param name="name">Channel category name/title.</param>
	/// <param name="type">Channel category type.</param>
	/// <param name="customisation">Channel category customisation.</param>
	/// <param name="config">Channel category configuration.</param>
	public MChannelCategory(string name, CategoryType type, MCustomisation? customisation, MConfig? config)
	{
		GuildId = StaticOptions.SnowflakeGenerator.New();
		Name = name;
		Type = type;
		Customisation = customisation ?? DefaultCustomisation;
		Config = config ?? DefaultConfig;

		DeserialiseModels();
	}

	private void DeserialiseModels()
	{
		// Deserialise models
		_customisation = JsonSerializer.Deserialize<MCustomisation>(CustomisationRaw, StaticOptions.JsonSerialzer)
		                 ?? throw new InvalidDataException(nameof(CustomisationRaw));
		_config = JsonSerializer.Deserialize<MConfig>(ConfigRaw, StaticOptions.JsonSerialzer)
		          ?? throw new InvalidDataException(nameof(ConfigRaw));
	}

	// Internal fields for models
	private MCustomisation _customisation = null!;
	private MConfig _config = null!;

	/// <summary>
	/// Parent guild.
	/// </summary>
	public Snowflake GuildId { get; init; }

	/// <summary>
	/// Channel category name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Channel category type.
	/// </summary>
	public CategoryType Type { get; set; }

	/// <summary>
	/// Raw serialised <see cref="Customisation"/>
	/// </summary>
	public string CustomisationRaw { get; private set; } = null!;

	/// <summary>
	/// Channel category customisation.
	/// </summary>
	public MCustomisation Customisation
	{
		get => _customisation;
		set
		{
			_customisation = value;
			CustomisationRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
		}
	}

	/// <summary>
	/// Raw serialised <see cref="Config"/>
	/// </summary>
	public string ConfigRaw { get; private set; } = null!;

	/// <summary>
	/// Channel category configuration.
	/// </summary>
	public MConfig Config
	{
		get => _config;
		set
		{
			_config = value;
			ConfigRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
		}
	}

	/// <summary>
	/// Channel category customisation model.
	/// </summary>
	public class MCustomisation
	{
	}

	/// <summary>
	/// Channel category configuration model.
	/// </summary>
	public class MConfig
	{
	}


	/// <summary>
	/// Channel category type.
	/// </summary>
	public enum CategoryType
	{
		Standard = 0,
		Voice = 1,
		Announcement = 2,
		Forum = 3,
		Stage = 4
	}
}
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Helpers;
using Core.Helpers.Snowflake;
using Core.Models;

namespace Database.Models;

public class ChannelModel : BaseEntityModel
{
	// Default models
	public static readonly MCustomisation DefaultCustomisation = new() { };

	public static readonly MConfig DefaultConfig = new MConfig()
	{
		IsEncrypted = false
	};

	/// <inheritdoc/>
	/// <param name="name">Channel name.</param>
	/// <param name="type">Channel type.</param>
	/// <param name="customisation">Customisation.</param>
	/// <param name="config">Configuration.</param>
	public ChannelModel(string name, ChannelType type, MCustomisation? customisation, MConfig? config)
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

	// Basic parameters
	/// <summary>
	/// Parent guild.
	/// </summary>
	public Snowflake GuildId { get; init; }

	/// <summary>
	/// Channel name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Channel type.
	/// </summary>
	public ChannelType Type { get; set; }

	/// <summary>
	/// Raw serialised <see cref="Customisation"/>
	/// </summary>
	public string CustomisationRaw { get; private set; } = null!;

	/// <summary>
	/// Channel customisation.
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
	/// Channel configuration.
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
	/// Channel configuration model.
	/// </summary>
	public class MConfig
	{
		// Encrypted status cannot be changed after creation
		[Required]
		[JsonPropertyName("encrypted")]
		public required bool IsEncrypted { get; init; }

		[JsonPropertyName("parent")] public Snowflake? ParentId { get; set; }
	}

	/// <summary>
	/// Channel customisation model.
	/// </summary>
	public class MCustomisation
	{
	}

	/// <summary>
	/// Channel types.
	/// </summary>
	public enum ChannelType : short
	{
		Text = 0,
		Voice = 1
	}
}
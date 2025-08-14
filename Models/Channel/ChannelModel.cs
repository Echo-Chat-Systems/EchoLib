using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models.Channel;

public class ChannelModel : BaseEntityModel
{
	// Default models
	public static readonly CustomisationModel DefaultCustomisation = new() { };

	public static readonly ConfigModel DefaultConfig = new()
	{
		IsEncrypted = false
	};

	/// <inheritdoc/>
	public ChannelModel(Snowflake guildId, string name, ConfigModel? config = null, CustomisationModel? customisation = null)
	{
		GuildId = guildId;
		Name = name;
		Config = config ?? DefaultConfig;
		Customisation = customisation ?? DefaultCustomisation;
	}

	/// <inheritdoc/>
	public ChannelModel(Snowflake id, Snowflake guildId, string name, ConfigModel? config = null, CustomisationModel? customisation = null) : base(id)
	{
		GuildId = guildId;
		Name = name;
		Config = config ?? DefaultConfig;
		Customisation = customisation ?? DefaultCustomisation;
	}

	// Basic parameters
	/// <summary>
	/// Parent guild.
	/// </summary>
	[JsonIgnore]
	[Required]
	public required Snowflake GuildId { get; init; }

	/// <summary>
	/// Channel name.
	/// </summary>
	[Required]
	public required string Name { get; set; }


	/// <summary>
	/// Channel customisation.
	/// </summary>
	public CustomisationModel? Customisation { get; set; }

	/// <summary>
	/// Channel configuration.
	/// </summary>
	[Required]
	public ConfigModel Config { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Generic;

namespace Models.Channel;

public class ChannelDbm : BaseDbm
{
	// Default models
	public static readonly ChannelCustomisationJm DefaultCustomisation = new() { };

	public static readonly ChannelConfigJm DefaultChannelConfig = new()
	{
		IsEncrypted = false
	};

	/// <inheritdoc/>
	public ChannelDbm(Snowflake guildId, string name, ChannelConfigJm? config = null, ChannelCustomisationJm? customisation = null)
	{
		GuildId = guildId;
		Name = name;
		ChannelConfig = config ?? DefaultChannelConfig;
		Customisation = customisation ?? DefaultCustomisation;
	}

	/// <inheritdoc/>
	public ChannelDbm(Snowflake id, Snowflake guildId, string name, ChannelConfigJm? config = null, ChannelCustomisationJm? customisation = null) : base(id)
	{
		GuildId = guildId;
		Name = name;
		ChannelConfig = config ?? DefaultChannelConfig;
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
	public ChannelCustomisationJm? Customisation { get; set; }

	/// <summary>
	/// Channel configuration.
	/// </summary>
	[Required]
	public ChannelConfigJm ChannelConfig { get; set; }
}
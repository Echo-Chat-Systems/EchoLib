using Models.Channel;
using Models.DatabaseModels;
using Models.Generic;
using Models.Json;

namespace Models.Database.Channel;

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
	public required Snowflake GuildId { get; init; }

	/// <summary>
	/// Channel name.
	/// </summary>
	public required string Name { get; set; }


	/// <summary>
	/// Channel customisation.
	/// </summary>
	public ChannelCustomisationJm? Customisation { get; set; }

	/// <summary>
	/// Channel configuration.
	/// </summary>
	public ChannelConfigJm ChannelConfig { get; set; }
}
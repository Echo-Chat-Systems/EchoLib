using System.Data;
using System.Data.Common;
using Database.Handlers.Interface;
using Database.Models;
using Models.Channel;

namespace Database.Handlers.Defaults.Chat;

public class ChannelsHandler : BaseHandler, IChannelsHandler
{
	public async Task<ChannelDbm> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.channels VALUES (@guild_id, @name, @type, @customisation, @config) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@name", new Parameter { Type = DbType.String, Value = name } },
			{ "@type", new Parameter { Type = DbType.Int16, Value = type, Nullable = true } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = customisation, Nullable = true } },
			{ "@config", new Parameter { Type = DbType.String, Value = config, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new ChannelDbm(reader));
	}

	public async Task<ChannelDbm?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.channels WHERE Id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new ChannelDbm(reader));
	}

	public async Task<ChannelDbm> Update(ChannelDbm channel)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.channels SET guild_id = @guild_id, Name = @name, Type = @type, Customisation = @customisation, Config = @config WHERE Id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = channel.Id } },
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = channel.GuildId } },
			{ "@name", new Parameter { Type = DbType.String, Value = channel.Name } },
			{ "@type", new Parameter { Type = DbType.Int16, Value = channel.Type, Nullable = true } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = channel.CustomisationRaw, Nullable = true } },
			{ "@config", new Parameter { Type = DbType.String, Value = channel.ConfigRaw, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new ChannelDbm(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.channels WHERE Id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.channels WHERE Id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunExists(command);
	}
}
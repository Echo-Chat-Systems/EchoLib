using System.Data;
using System.Data.Common;
using Database.Handlers.Interface;
using Database.Models.Chat;

namespace Database.Handlers.Defaults.Chat;

public class ChannelsHandler : BaseHandler, IChannelsHandler
{
	public async Task<MChannel> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null)
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
		return await RunModify(command, reader => new MChannel(reader));
	}

	public async Task<MChannel?> Get(Guid id)
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
		return await RunGet(command, reader => new MChannel(reader));
	}

	public async Task<MChannel> Update(MChannel mChannel)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.channels SET guild_id = @guild_id, Name = @name, Type = @type, Customisation = @customisation, Config = @config WHERE Id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = mChannel.Id } },
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = mChannel.GuildId } },
			{ "@name", new Parameter { Type = DbType.String, Value = mChannel.Name } },
			{ "@type", new Parameter { Type = DbType.Int16, Value = mChannel.Type, Nullable = true } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = mChannel.CustomisationRaw, Nullable = true } },
			{ "@config", new Parameter { Type = DbType.String, Value = mChannel.ConfigRaw, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MChannel(reader));
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
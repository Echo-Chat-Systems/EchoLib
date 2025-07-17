using System.Data;
using System.Data.Common;
using Database.Models.Chat;

namespace Database.Handlers.Defaults.Chat;

public class GuildsHandler : BaseHandler
{
	public async Task<MGuild> Create(string owner, string name, string? customisation = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.guilds VALUES (@owner, @name, @customisation) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner", new Parameter { Type = DbType.String, Value = owner } },
			{ "@name", new Parameter { Type = DbType.String, Value = name } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = customisation, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MGuild(reader));
	}

	public async Task<MGuild?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guilds WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.String, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new MGuild(reader));
	}

	public async Task<MGuild> Update(MGuild guild)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE chat.guilds SET owner_id = @owner, name = @name, customisation = @customisation WHERE id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner", new Parameter { Type = DbType.String, Value = guild.OwnerId } },
			{ "@name", new Parameter { Type = DbType.String, Value = guild.Name } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = guild.CustomisationRaw, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MGuild(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.guilds WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.guilds WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Create parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunExists(command);
	}
}
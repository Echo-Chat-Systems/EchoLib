using System.Data;
using System.Data.Common;
using Database.Handlers.Interface;
using Models;

namespace Database.Handlers.Defaults.Config;

public class OwnersHandler : BaseHandler, IOwnersHandler
{
	public async Task Add(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO config.owners VALUES (@owner_id)";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner_id", new Parameter { Type = DbType.Guid, Value = ownerId } }
		});

		// Execute command
		await RunModify<object?>(command, _ => null);
	}

	public async Task Remove(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM config.owners WHERE user_id = @owner_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner_id", new Parameter { Type = DbType.Guid, Value = ownerId } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT user_id FROM config.owners WHERE user_id = @owner_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner_id", new Parameter { Type = DbType.Guid, Value = ownerId } }
		});

		// Get result
		return await RunExists(command);
	}

	public async Task<List<Snowflake>> GetAll()
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM config.owners";

		// Execute command
		DbDataReader result = await command.ExecuteReaderAsync();

		List<Snowflake> ids = new();
		while (await result.ReadAsync())
		{
			ids.Add(new Snowflake((ulong)result.GetInt64(0)));
		}

		return ids;
	}
}
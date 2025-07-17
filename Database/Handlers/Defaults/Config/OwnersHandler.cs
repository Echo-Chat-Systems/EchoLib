using System.Data.Common;

namespace Database.Handlers.Defaults.Config;

public class OwnersHandler : BaseHandler
{
	public async Task Add(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO config.owners VALUES (@owner_id)";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@owner_id";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters
		command.Parameters.Add(pOwnerId);

		// Execute command
		await RunModify<object?>(command, _ => null);
	}

	public async Task Remove(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM config.owners WHERE user_id = @owner_id";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@owner_id";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters
		command.Parameters.Add(pOwnerId);

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid ownerId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT user_id FROM config.owners WHERE user_id = @owner_id";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@owner_id";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters
		command.Parameters.Add(pOwnerId);

		// Get result
		return await RunExists(command);
	}

	public async Task<List<Guid>> GetAll()
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM config.owners";

		// Get result
		DbDataReader result = await command.ExecuteReaderAsync();

		List<Guid> ids = [];
		while (result.HasRows) ids.Add(result.GetGuid(0));

		return ids;
	}
}
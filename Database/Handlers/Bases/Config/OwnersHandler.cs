using System.Data.Common;

namespace EchoLib.Database.Handlers.Bases.Config;

public class OwnersHandler : BaseHandler
{
	public async Task Add(Guid ownerId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "INSERT INTO config.owners (user_id) VALUES (@OwnerId)";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@OwnerId";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters to command
		command.Parameters.Add(pOwnerId);

		await command.ExecuteNonQueryAsync();
	}

	public async Task Remove(Guid ownerId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "DELETE FROM config.owners WHERE user_id = @OwnerId";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@OwnerId";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters to command
		command.Parameters.Add(pOwnerId);

		await command.ExecuteNonQueryAsync();
	}

	public async Task<bool> Exists(Guid ownerId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT user_id FROM config.owners WHERE user_id = @OwnerId";

		// Create parameters
		DbParameter pOwnerId = command.CreateParameter();
		pOwnerId.ParameterName = "@OwnerId";
		pOwnerId.DbType = System.Data.DbType.Guid;
		pOwnerId.Value = ownerId;

		// Add parameters to command
		command.Parameters.Add(pOwnerId);

		// Get result
		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}

	public async Task<List<Guid>> GetAll()
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM config.owners";

		// Get result
		DbDataReader result = await command.ExecuteReaderAsync();

		List<Guid> ids = [];
		while (result.HasRows) ids.Add(result.GetGuid(0));

		return ids;
	}
}
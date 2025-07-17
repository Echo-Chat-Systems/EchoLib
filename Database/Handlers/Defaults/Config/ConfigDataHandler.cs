using System.Data;
using System.Data.Common;
using Database.Models.Config;

namespace Database.Handlers.Defaults.Config;

public class ConfigDataHandler : BaseHandler
{
	public async Task<MConfigData> Create(string key, object value)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO config.data (key, value) VALUES (@key, @value) RETURNING *";

		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@key";
		pKey.DbType = DbType.String;
		pKey.Value = key;

		DbParameter pValue = command.CreateParameter();
		pValue.ParameterName = "@value";
		pValue.DbType = DbType.Object; // Adjust DbType as necessary for your value type
		pValue.Value = value;

		// Add parameters
		command.Parameters.Add(pKey);
		command.Parameters.Add(pValue);

		// Execute command
		return await RunModify(command, reader => new MConfigData(reader));
	}

	public async Task<MConfigData?> Get(string key)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM config.data WHERE key = @key";

		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@key";
		pKey.DbType = DbType.String;
		pKey.Value = key;

		// Add parameters
		command.Parameters.Add(pKey);

		// Execute command
		return await RunGet(command, reader => new MConfigData(reader));
	}

	public async Task<MConfigData> Update(MConfigData row)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE config.data SET value = @value WHERE key = @key RETURNING *";

		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@Key";
		pKey.DbType = DbType.String;
		pKey.Value = row.Key;

		DbParameter pValue = command.CreateParameter();
		pValue.ParameterName = "@value";
		pValue.DbType = DbType.Object; // Adjust DbType as necessary for your value type
		pValue.Value = row.Value;

		// Add parameters
		command.Parameters.Add(pKey);
		command.Parameters.Add(pValue);

		// Execute command
		return await RunModify(command, reader => new MConfigData(reader));
	}

	public async Task Delete(string key)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM config.data WHERE key = @key";

		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@key";
		pKey.DbType = DbType.String;
		pKey.Value = key;

		// Add parameters
		command.Parameters.Add(pKey);

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(string key)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT key FROM config.data WHERE key = @key";

		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@key";
		pKey.DbType = DbType.String;
		pKey.Value = key;

		// Add parameters
		command.Parameters.Add(pKey);

		// Get result
		return await RunExists(command);
	}
}
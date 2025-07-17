using System.Data;
using System.Data.Common;
using EchoLib.Database.Models.Config;

namespace EchoLib.Database.Handlers.Defaults.Config;

public class ConfigDataHandler : BaseHandler
{
	public async Task<MConfigData> Create(string key, object value)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
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
		
		// Add parameters to command
		command.Parameters.Add(pKey);
		command.Parameters.Add(pValue);
		
		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MConfigData(reader);
		}
		
		throw new ();
	}

	public async Task<MConfigData?> Get(string key)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM config.data WHERE key = @key";
		
		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@key";
		pKey.DbType = DbType.String;
		pKey.Value = key;
		
		// Add parameters to command
		command.Parameters.Add(pKey);
		
		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MConfigData(reader);
		}
		
		return null; // No data found for the given key
	}

	public async Task<MConfigData> Update(MConfigData row)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "UPDATE config.data SET value = @Value WHERE key = @key RETURNING *";
		
		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@Key";
		pKey.DbType = DbType.String;
		pKey.Value = row.Key;
		
		DbParameter pValue = command.CreateParameter();
		pValue.ParameterName = "@Value";
		pValue.DbType = DbType.Object; // Adjust DbType as necessary for your value type
		pValue.Value = row.Value;
		
		// Add parameters to command
		command.Parameters.Add(pKey);
		command.Parameters.Add(pValue);
		
		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MConfigData(reader);
		}
		
		throw new DataException("Failed to update config data.");
	}

	public async Task Delete(string key)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "DELETE FROM config.data WHERE key = @Key";
		
		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@Key";
		pKey.DbType = DbType.String;
		pKey.Value = key;
		
		// Add parameters to command
		command.Parameters.Add(pKey);
		
		// Execute command
		await command.ExecuteNonQueryAsync();
	}

	public async Task<bool> Exists(string key)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT key FROM config.data WHERE key = @Key";
		
		// Create parameters
		DbParameter pKey = command.CreateParameter();
		pKey.ParameterName = "@Key";
		pKey.DbType = DbType.String;
		pKey.Value = key;
		
		// Add parameters to command
		command.Parameters.Add(pKey);
		
		// Get result
		object? result = await command.ExecuteScalarAsync();
		
		return result != null;
	}
}
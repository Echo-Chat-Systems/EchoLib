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
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@key", new Parameter { Type = DbType.String, Value = key } },
			{ "@value", new Parameter { Type = DbType.Object, Value = value } }
		});

		// Execute command
		return await RunModify(command, reader => new MConfigData(reader));
	}

	public async Task<MConfigData?> Get(string key)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM config.data WHERE key = @key";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@key", new Parameter { Type = DbType.String, Value = key } }
		});

		// Execute command
		return await RunGet(command, reader => new MConfigData(reader));
	}

	public async Task<MConfigData> Update(MConfigData row)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE config.data SET value = @value WHERE key = @key RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@key", new Parameter { Type = DbType.String, Value = row.Key } },
			{ "@value", new Parameter { Type = DbType.Object, Value = row.Value } }
		});

		// Execute command
		return await RunModify(command, reader => new MConfigData(reader));
	}

	public async Task Delete(string key)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM config.data WHERE key = @key";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@key", new Parameter { Type = DbType.String, Value = key } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(string key)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT key FROM config.data WHERE key = @key";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@key", new Parameter { Type = DbType.String, Value = key } }
		});

		// Get result
		return await RunExists(command);
	}
}
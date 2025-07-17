using System.Data;
using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public class ChannelCategoriesHandler : BaseHandler
{
	public async Task<MChannelCategory> Create(Guid guildId, string name, short? type = null,
		string? customisation = null,
		string? config = null)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"INSERT INTO chat.channel_categories VALUES (@guild_id, @name, @type, @customisation, @config)";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = name;

		DbParameter pType = command.CreateParameter();
		pType.ParameterName = "@type";
		pType.DbType = DbType.Int16;
		pType.Value = type ?? (short)0; // Default to 0 if null
		pType.IsNullable = true;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = customisation ?? string.Empty; // Default to empty string if null
		pCustomisation.IsNullable = true;

		DbParameter pConfig = command.CreateParameter();
		pConfig.ParameterName = "@config";
		pConfig.DbType = DbType.String;
		pConfig.Value = config ?? string.Empty; // Default to empty string if null
		pConfig.IsNullable = true;

		// Add parameters to command
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pName);
		command.Parameters.Add(pType);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pConfig);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (reader.RecordsAffected != 1) goto Fail;

		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MChannelCategory(reader);
		}

		Fail:
		await transaction.RollbackAsync();
		throw new InsertFailedException(command);
	}

	public async Task<MChannelCategory?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM chat.channel_categories WHERE Id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync()) return new MChannelCategory(reader);

		return null; // Return null if no category found
	}

	public async Task<MChannelCategory> Update(MChannelCategory channel)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"UPDATE chat.channel_categories SET Name = @name, Type = @type, Customisation = @customisation, Config = @config WHERE Id = @id RETURNING *";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = channel.Id;

		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = channel.Name;

		DbParameter pType = command.CreateParameter();
		pType.ParameterName = "@type";
		pType.DbType = DbType.Int16;
		pType.Value = (short)channel.Type;
		pType.IsNullable = true;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = channel.CustomisationRaw; // Default to empty string if null
		pCustomisation.IsNullable = true;

		DbParameter pConfig = command.CreateParameter();
		pConfig.ParameterName = "@config";
		pConfig.DbType = DbType.String;
		pConfig.Value = channel.ConfigRaw; // Default to empty string if null
		pConfig.IsNullable = true;

		// Add parameters to command
		command.Parameters.Add(pId);
		command.Parameters.Add(pName);
		command.Parameters.Add(pType);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pConfig);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (reader.RecordsAffected != 1) goto Fail;

		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MChannelCategory(reader);
		}

		Fail:
		await transaction.RollbackAsync();
		throw new UpdateFailedException(command);
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "DELETE FROM chat.channel_categories WHERE Id = @Id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@Id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		int rowsAffected = await command.ExecuteNonQueryAsync();
		if (rowsAffected != 1)
		{
			await transaction.RollbackAsync();
			throw new DeleteFailedException(command);
		}

		await transaction.CommitAsync();
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT id FROM chat.channel_categories WHERE Id = @Id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@Id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}
}
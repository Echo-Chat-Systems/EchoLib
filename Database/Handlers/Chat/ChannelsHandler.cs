using System.Data;
using System.Data.Common;
using EchoLib.Database.Models.Chat;

namespace EchoLib.Database.Handlers.Chat;

public class ChannelsHandler : BaseHandler
{
	public async Task<MChannel> Create(Guid guildId, string name, short? type = null, string? customisation = null,
		string? config = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.channels VALUES (@guild_id, @name, @type, @customisation, @config) RETURNING *";
		
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
		pType.Value = type ?? 0; // Default to 0 if null
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
		return await RunModify(command, reader => new MChannel(reader));
	}

	public async Task<MChannel?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.channels WHERE Id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;
		
		// Add parameters to command
		command.Parameters.Add(pId);
		
		// Execute command
		return await RunGet(command, reader => new MChannel(reader));
	}

	public async Task<MChannel> Update(MChannel mChannel)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE chat.channels SET guild_id = @guild_id, Name = @name, Type = @type, Customisation = @customisation, Config = @config WHERE Id = @id RETURNING *";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = mChannel.Id;
		
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = mChannel.GuildId;
		
		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = mChannel.Name;
		
		DbParameter pType = command.CreateParameter();
		pType.ParameterName = "@type";
		pType.DbType = DbType.Int16;
		pType.Value = mChannel.Type; // Default to 0 if null
		pType.IsNullable = true;
		
		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = mChannel.CustomisationRaw; // Default to empty string if null
		pCustomisation.IsNullable = true;
		
		DbParameter pConfig = command.CreateParameter();
		pConfig.ParameterName = "@config";
		pConfig.DbType = DbType.String;
		pConfig.Value = mChannel.ConfigRaw; // Default to empty string if null
		pConfig.IsNullable = true;
		
		// Add parameters to command
		command.Parameters.Add(pId);
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pName);
		command.Parameters.Add(pType);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pConfig);
		
		// Execute command
		return await RunModify(command, reader => new MChannel(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.channels WHERE Id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;
		
		// Add parameters to command
		command.Parameters.Add(pId);
		
		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.channels WHERE Id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;
		
		// Add parameters to command
		command.Parameters.Add(pId);
		
		// Execute command
		return await RunExists(command);
	}
}
using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Chat;

namespace EchoLib.Database.Handlers.Chat;

public class GuildsHandler : BaseHandler
{
	public async Task<MGuild> Create(PublicSigningKey owner, string name, string? customisation = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.guilds VALUES (@owner, @name, @customisation) RETURNING *";

		// Create parameters
		DbParameter pOwner = command.CreateParameter();
		pOwner.ParameterName = "@owner";
		pOwner.DbType = DbType.String;
		pOwner.Value = owner.ToString();

		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = name;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = customisation ?? string.Empty;

		// Add parameters
		command.Parameters.Add(pOwner);
		command.Parameters.Add(pName);
		command.Parameters.Add(pCustomisation);

		// Execute command
		return await RunModify(command, reader => new MGuild(reader));
	}

	public async Task<MGuild?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guilds WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunGet(command, reader => new MGuild(reader));
	}

	public async Task<MGuild> Update(MGuild guild)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE chat.guilds SET name = @name, customisation = @customisation WHERE id = @id RETURNING *";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = guild.Id;

		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = guild.Name;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = guild.CustomisationRaw;

		// Add parameters
		command.Parameters.Add(pId);
		command.Parameters.Add(pName);
		command.Parameters.Add(pCustomisation);

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
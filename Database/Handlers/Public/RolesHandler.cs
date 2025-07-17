using System.Data;
using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public abstract class RolesHandler : BaseHandler
{
	public async Task<MRole> Create(Guid guildId, string name, string customisation, long permissions)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO public.roles VALUES (@guild_id, @name, @customisation, @permissions) RETURNING *";
		
		// Create parameters 
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;
		
		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = name;
		
		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = customisation;
		
		DbParameter pPermissions = command.CreateParameter();
		pPermissions.ParameterName = "@permissions";
		pPermissions.DbType = DbType.Int64;
		pPermissions.Value = permissions;
		
		// Execute command
		return await RunModify(command, reader => new MRole(reader));
	}

	public async Task<MRole?> Get(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.roles WHERE id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = roleId;
		
		// Add parameters
		command.Parameters.Add(pId);
		
		// Execute command
		return await RunGet(command, reader => new MRole(reader));

	}

	public async Task<MRole> Update(MRole role)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE public.roles SET name = @name, customisation = @customisation, permissions = @permissions WHERE id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = role.Id;
		
		DbParameter pName = command.CreateParameter();
		pName.ParameterName = "@name";
		pName.DbType = DbType.String;
		pName.Value = role.Name;
		
		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = role.CustomisationRaw;
		
		DbParameter pPermissions = command.CreateParameter();
		pPermissions.ParameterName = "@permissions";
		pPermissions.DbType = DbType.Int64;
		pPermissions.Value = (long)role.Permissions;
		
		// Add parameters 
		command.Parameters.Add(pId);
		command.Parameters.Add(pName);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pPermissions);
		
		// Execute command
		return await RunModify(command, reader => new MRole(reader));
	}

	public async Task Delete(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM public.roles WHERE id = @id";
		
		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = roleId;
		
		// Add parameters
		command.Parameters.Add(pId);
		
		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid roleId)
	{
		// Create command 
		await using DbCommand command = await Command(true);
		command.CommandText = "SELECT id FROM public.roles WHERE id = @id";
		
		// Create parameters 
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = roleId;
		
		// Add parameters
		command.Parameters.Add(pId);
		
		// Execute command
		return await RunExists(command);
	}
}
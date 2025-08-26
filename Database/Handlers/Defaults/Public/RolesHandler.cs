using System.Data;
using System.Data.Common;

namespace Database.Handlers.Defaults.Public;

public abstract class RolesHandler : BaseHandler
{
	public async Task<MRole> Create(Guid guildId, string name, string customisation, long permissions)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO public.roles VALUES (@guild_id, @name, @customisation, @permissions) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@name", new Parameter { Type = DbType.String, Value = name } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = customisation } },
			{ "@permissions", new Parameter { Type = DbType.Int64, Value = permissions } }
		});

		// Execute command
		return await RunModify(command, reader => new MRole(reader));
	}

	public async Task<MRole?> Get(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = roleId } }
		});

		// Execute command
		return await RunGet(command, reader => new MRole(reader));
	}

	public async Task<MRole> Update(MRole role)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE public.roles SET name = @name, customisation = @customisation, permissions = @permissions WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = role.Id } },
			{ "@name", new Parameter { Type = DbType.String, Value = role.Name } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = role.CustomisationRaw } },
			{ "@permissions", new Parameter { Type = DbType.Int64, Value = (long)role.Permissions } }
		});

		// Execute command
		return await RunModify(command, reader => new MRole(reader));
	}

	public async Task Delete(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM public.roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = roleId } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM public.roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = roleId } }
		});

		// Execute command
		return await RunExists(command);
	}
}
using System.Data;
using System.Data.Common;
using Database.Models;
using Database.Models.Public;

namespace Database.Handlers.Defaults.Public;

public abstract class UserRolesHandler : BaseHandler
{
	public async Task<MUserRole> Create(string user, Guid role)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO public.user_roles VALUES (@user_id, @role_id)";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = user.ToString() } },
			{ "@role_id", new Parameter { Type = DbType.Guid, Value = role } }
		});

		// Execute command
		return await RunModify(command, reader => new MUserRole(reader));
	}

	public async Task<MUserRole?> Get(Guid userRoleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.user_roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = userRoleId } }
		});

		// Execute command
		return await RunGet(command, reader => new MUserRole(reader));
	}

	public async Task<MUserRole?> Get(string user, Guid role)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.user_roles WHERE user_id = @user_id AND role_id = @role_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = user } },
			{ "@role_id", new Parameter { Type = DbType.Guid, Value = role } }
		});

		// Execute command
		return await RunGet(command, reader => new MUserRole(reader));
	}

	public async Task<MUserRole> Update(MUserRole userRole)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE public.user_roles SET user_id = @user_id, role_id = @role_id WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = userRole.Id } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = userRole.UserId } },
			{ "@role_id", new Parameter { Type = DbType.Guid, Value = userRole.RoleId } }
		});

		// Execute command
		return await RunModify(command, reader => new MUserRole(reader));
	}

	public async Task Delete(Guid userRoleId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM public.user_roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = userRoleId } },
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid userRoleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM public.user_roles WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = userRoleId } },
		});

		// Execute command
		return await RunExists(command);
	}

	public async Task<bool> Exists(string user, Guid role)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM public.user_roles WHERE user_id = @user_id AND role_id = @role_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = user.ToString() } },
			{ "@role_id", new Parameter { Type = DbType.Guid, Value = role.ToString() } }
		});

		// Execute command
		return await RunExists(command);
	}

	public async Task<List<MUserRole>> GetMany(string userId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.user_roles WHERE user_id = @user_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } }
		});

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		List<MUserRole> rows = [];

		while (await reader.ReadAsync())
		{
			rows.Add(new MUserRole(reader));
		}

		return rows;
	}

	public async Task<List<MUserRole>> GetMany(Guid roleId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.user_roles WHERE role_id = @role_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@role_id", new Parameter { Type = DbType.Guid, Value = roleId } }
		});

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		List<MUserRole> rows = [];

		while (await reader.ReadAsync())
		{
			rows.Add(new MUserRole(reader));
		}

		return rows;
	}
}
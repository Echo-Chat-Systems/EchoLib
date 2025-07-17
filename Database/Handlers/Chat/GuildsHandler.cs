using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public class GuildsHandler : BaseHandler
{
	public async Task<MGuild> Create(PublicSigningKey owner, string name, string? customisation = null)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "INSERT INTO public.guilds VALUES (@owner, @name, @customisation) RETURNING *";

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

		command.Parameters.Add(pOwner);
		command.Parameters.Add(pName);
		command.Parameters.Add(pCustomisation);

		await using DbDataReader reader = await command.ExecuteReaderAsync();
		
		if (await reader.ReadAsync())
			return new MGuild(reader);

		throw new DataException("Failed to create guild.");
	}

	public async Task<MGuild?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM public.guilds WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		command.Parameters.Add(pId);

		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return new MGuild(reader);

		return null;
	}

	public async Task<MGuild> Update(MGuild guild)
	{
	}

	public async Task Delete(Guid id)
	{
	}

	public async Task<bool> Exists(Guid id)
	{
	}
}
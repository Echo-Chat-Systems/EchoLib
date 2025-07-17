using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public class GuildMembersHandler : BaseHandler
{
	public async Task<MGuildMember> Create(Guid guildId, PublicSigningKey userId, string? nickname = null,
		string? customisation = null)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"INSERT INTO public.guild_members VALUES (@guild_id, @user_id, @nickname, @customisation) RETURNING *";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		DbParameter pNickname = command.CreateParameter();
		pNickname.ParameterName = "@nickname";
		pNickname.DbType = DbType.String;
		pNickname.Value = nickname ?? string.Empty;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = customisation ?? string.Empty;

		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pNickname);
		command.Parameters.Add(pCustomisation);

		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (reader.RecordsAffected != 1) goto Fail;

		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MGuildMember(reader);
		}

		Fail:
		await transaction.RollbackAsync();
		throw new InsertFailedException(command);
	}

	public async Task<MGuildMember?> Get(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM public.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return new MGuildMember(reader);

		return null;
	}

	public async Task<MGuildMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM public.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		command.Parameters.Add(pId);

		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return new MGuildMember(reader);

		return null;
	}

	public async Task<MGuildMember> Update(MGuildMember member)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"UPDATE public.guild_members SET guild_id = @guild_id, user_id = @user_id, nickname = @nickname, customisation_override = @customisation WHERE id = @id RETURNING *";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = member.Id;

		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = member.GuildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = member.UserId.ToString();

		DbParameter pNickname = command.CreateParameter();
		pNickname.ParameterName = "@nickname";
		pNickname.DbType = DbType.String;
		pNickname.Value = member.Nickname ?? string.Empty;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = member.CustomisationOverrideRaw;

		command.Parameters.Add(pId);
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pNickname);
		command.Parameters.Add(pCustomisation);

		await using DbDataReader reader = await command.ExecuteReaderAsync();

		if (reader.RecordsAffected != 1) goto Fail;

		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MGuildMember(reader);
		}

		Fail:
		await transaction.RollbackAsync();
		throw new UpdateFailedException(command);
	}

	public async Task Delete(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "DELETE FROM public.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		int affectedRows = await command.ExecuteNonQueryAsync();

		if (affectedRows != 1)
		{
			await transaction.RollbackAsync();
			throw new DeleteFailedException(command);
		}

		await transaction.CommitAsync();
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "DELETE FROM public.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		command.Parameters.Add(pId);

		int affectedRows = await command.ExecuteNonQueryAsync();

		if (affectedRows != 1)
		{
			await transaction.RollbackAsync();
			throw new DeleteFailedException(command);
		}

		await transaction.CommitAsync();
	}

	public async Task<bool> Exists(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT id FROM public.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT id FROM public.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		command.Parameters.Add(pId);

		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}
}
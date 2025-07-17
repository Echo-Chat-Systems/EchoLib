using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Public;

public class ChannelMembersHandler : BaseHandler
{
	public async Task<MChannelMember> Create(PublicSigningKey userId, Guid channelId, long permissions)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText = "INSERT INTO public.channel_members VALUES (@user_id, @channel_id, @permissions)";

		// Create parameters
		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelId;

		DbParameter pPermissions = command.CreateParameter();
		pPermissions.ParameterName = "@permissions";
		pPermissions.DbType = DbType.Int64;
		pPermissions.Value = permissions;

		// Add parameters to command
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pChannelId);
		command.Parameters.Add(pPermissions);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (reader.RecordsAffected != 1) goto Fail;
		
		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MChannelMember(reader);
		}

		Fail:
		await transaction.RollbackAsync();
		throw new InsertFailedException(command);
	}

	public async Task<MChannelMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText = "SELECT * FROM public.channel_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MChannelMember(reader);
		}

		return null; // Return null if no member found
	}

	public async Task<MChannelMember?> Get(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText =
			"SELECT * FROM public.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelId;

		// Add parameters to command
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pChannelId);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new MChannelMember(reader);
		}

		return null; // Return null if no member found
	}

	public async Task<MChannelMember> Update(MChannelMember channelMember)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"UPDATE public.channel_members SET permissions = @permissions WHERE user_id = @user_id AND channel_id = @channel_id RETURNING *";

		// Create parameters
		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = channelMember.UserId.ToString();

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelMember.ChannelId;

		DbParameter pPermissions = command.CreateParameter();
		pPermissions.ParameterName = "@permissions";
		pPermissions.DbType = DbType.Int64;
		pPermissions.Value = channelMember.Permissions;

		// Add parameters to command
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pChannelId);
		command.Parameters.Add(pPermissions);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (reader.RecordsAffected != 1)
		{
			goto Fail; // If no rows were affected, rollback and throw an exception
		}

		if (await reader.ReadAsync())
		{
			await transaction.CommitAsync();
			return new MChannelMember(reader);
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
		command.CommandText = "DELETE FROM public.channel_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		int rows = await command.ExecuteNonQueryAsync();
		if (rows != 1)
		{
			await transaction.RollbackAsync();
			throw new DeleteFailedException(command);
		}
		
		await transaction.CommitAsync();
	}

	public async Task Delete(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		await using DbTransaction transaction = await command.Connection!.BeginTransactionAsync();
		command.Transaction = transaction;
		command.CommandText =
			"DELETE FROM public.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelId;

		// Add parameters to command
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pChannelId);

		// Execute command
		int rows = await command.ExecuteNonQueryAsync();
		if (rows != 1)
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
		command.CommandText = "SELECT COUNT(*) FROM public.channel_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		await using DbDataReader reader = await command.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return reader.GetInt32(0) > 0; // Return true if count is greater than 0
		}

		return false; // Return false if no member found
	}

	public async Task<bool> Exists(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = DataSource.CreateCommand();
		command.CommandText =
			"SELECT id FROM public.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelId;

		// Add parameters to command
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pChannelId);

		// Execute command
		object? result = await command.ExecuteScalarAsync();

		return result != null;
	}
}
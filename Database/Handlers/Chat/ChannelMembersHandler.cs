using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Chat;

namespace EchoLib.Database.Handlers.Chat;

public class ChannelMembersHandler : BaseHandler
{
	public async Task<MChannelMember> Create(PublicSigningKey userId, Guid channelId, long permissions)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.channel_members VALUES (@user_id, @channel_id, @permissions)";

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
		return await RunModify(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.channel_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters to command
		command.Parameters.Add(pId);

		// Execute command
		return await RunGet(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember?> Get(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT * FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

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
		return await RunGet(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember> Update(MChannelMember channelMember)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.channel_members SET permissions = @permissions WHERE user_id = @user_id AND channel_id = @channel_id RETURNING *";

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
		return await RunModify(command, reader => new MChannelMember(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.channel_members WHERE id = @id";

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

	public async Task Delete(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"DELETE FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

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
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.channel_members WHERE id = @id";

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

	public async Task<bool> Exists(PublicSigningKey userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT id FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

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
		return await RunExists(command);
	}
}
using System.Data;
using System.Data.Common;
using Core.Auth.Signing;
using Database.Handlers.Interface;
using Database.Models.Chat;

namespace Database.Handlers.Defaults.Chat;

public class ChannelMembersHandler : BaseHandler, IChannelMembersHandler
{
	public async Task<MChannelMember> Create(UserId userId, Guid channelId, long permissions)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.channel_members VALUES (@user_id, @channel_id, @permissions)";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = channelId } },
			{ "permissions", new Parameter { Type = DbType.Int64, Value = permissions } }
		});

		// Execute command
		return await RunModify(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember?> Get(UserId userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT * FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = channelId } }
		});

		// Execute command
		return await RunGet(command, reader => new MChannelMember(reader));
	}

	public async Task<MChannelMember> Update(MChannelMember channelMember)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.channel_members SET permissions = @permissions WHERE id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@permissions", new Parameter { Type = DbType.Int64, Value = channelMember.Permissions } }
		});

		// Execute command
		return await RunModify(command, reader => new MChannelMember(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type =  DbType.Guid, Value = id} }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task Delete(UserId userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"DELETE FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = channelId } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type =  DbType.Guid, Value = id} }
		});

		// Execute command
		return await RunExists(command);
	}

	public async Task<bool> Exists(UserId userId, Guid channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT id FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = channelId } }
		});

		// Execute command
		return await RunExists(command);
	}
}
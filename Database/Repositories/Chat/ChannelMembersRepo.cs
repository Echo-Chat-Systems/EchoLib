using System.Data;
using System.Data.Common;
using Models.Crypto.Signing;
using Models.Database.Channel;
using Models.Functional;
using Models.Functional.Crypto.Signing;
using Models.Generic;

namespace Database.Repositories.Chat;

public class ChannelMembersRepo : BaseRepo, IChannelMembersRepo
{
	public async Task<ChannelMemberDbm> Create(UserId userId, Snowflake channelId, long permissions)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO chat.channel_members VALUES (@user_id, @channel_id, @permissions)";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.UInt64, Value = channelId } },
			{ "permissions", new Parameter { Type = DbType.UInt64, Value = permissions } }
		});

		// Execute command
		return await RunModify<ChannelMemberDbm>(command);
	}

	public async Task<ChannelMemberDbm?> Get(Snowflake id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.UInt64, Value = id } }
		});

		// Execute command
		return await RunGet<ChannelMemberDbm>(command);
	}

	public async Task<ChannelMemberDbm?> Get(UserId userId, Snowflake channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT * FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.UInt64, Value = channelId } }
		});

		// Execute command
		return await RunGet<ChannelMemberDbm>(command);
	}

	public async Task<ChannelMemberDbm> Update(ChannelMemberDbm channelMember)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.channel_members SET permissions = @permissions WHERE id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@permissions", new Parameter { Type = DbType.Int64, Value = channelMember. } }
		});

		// Execute command
		return await RunModify<ChannelMemberDbm>(command);
	}

	public async Task Delete(Snowflake id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type =  DbType.UInt64, Value = id} }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task Delete(UserId userId, Snowflake channelId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"DELETE FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.UInt64, Value = channelId } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Snowflake id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.channel_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type =  DbType.UInt64, Value = id} }
		});

		// Execute command
		return await RunExists(command);
	}

	public async Task<bool> Exists(UserId userId, Snowflake channelId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText =
			"SELECT id FROM chat.channel_members WHERE user_id = @user_id AND channel_id = @channel_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } },
			{ "@channel_id", new Parameter { Type = DbType.UInt64, Value = channelId } }
		});

		// Execute command
		return await RunExists(command);
	}
}
using System.Data;
using System.Data.Common;
using Database.Models.Chat;

namespace Database.Handlers.Defaults.Chat;

public class GuildMembersHandler : BaseHandler
{
	public async Task<MGuildMember> Create(Guid guildId, string userId, string? nickname = null,
		string? customisation = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"INSERT INTO chat.guild_members VALUES (@guild_id, @user_id, @nickname, @customisation) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId } },
			{ "@nickname", new Parameter { Type = DbType.String, Value = nickname, Nullable = true } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = customisation, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MGuildMember(reader));
	}

	public async Task<MGuildMember?> Get(Guid guildId, string userId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId } }
		});

		// Execute command
		return await RunGet(command, reader => new MGuildMember(reader));
	}

	public async Task<MGuildMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guild_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new MGuildMember(reader));
	}

	public async Task<MGuildMember> Update(MGuildMember member)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE chat.guild_members SET guild_id = @guild_id, user_id = @user_id, nickname = @nickname, customisation = @customisation WHERE id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = member.Id } },
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = member.GuildId } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = member.UserId } },
			{ "@nickname", new Parameter { Type = DbType.String, Value = member.Nickname, Nullable = false } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = member.CustomisationOverrideRaw } }
		});

		// Execute command
		return await RunModify(command, reader => new MGuildMember(reader));
	}

	public async Task Delete(Guid guildId, string userId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId.ToString() } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.guild_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid guildId, string userId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@user_id", new Parameter { Type = DbType.String, Value = userId } }
		});

		// Execute command
		return await RunExists(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.guild_members WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunExists(command);
	}
}
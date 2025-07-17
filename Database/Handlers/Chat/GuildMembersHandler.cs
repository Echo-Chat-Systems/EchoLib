using System.Data;
using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Chat;

namespace EchoLib.Database.Handlers.Chat;

public class GuildMembersHandler : BaseHandler
{
	public async Task<MGuildMember> Create(Guid guildId, PublicSigningKey userId, string? nickname = null,
		string? customisation = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"INSERT INTO chat.guild_members VALUES (@guild_id, @user_id, @nickname, @customisation) RETURNING *";

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

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pNickname);
		command.Parameters.Add(pCustomisation);

		// Execute command
		return await RunModify(command, reader => new MGuildMember(reader));
	}

	public async Task<MGuildMember?> Get(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		// Execute command
		return await RunGet(command, reader => new MGuildMember(reader));
	}

	public async Task<MGuildMember?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM chat.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;
		
		// Add parameters
		command.Parameters.Add(pId);

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
		
		// Add parameters
		command.Parameters.Add(pId);
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);
		command.Parameters.Add(pNickname);
		command.Parameters.Add(pCustomisation);

		// Execute command
		return await RunModify(command, reader => new MGuildMember(reader));
	}

	public async Task Delete(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		// Execute command
		await RunDelete(command);
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM chat.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Execute command
		command.Parameters.Add(pId);

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid guildId, PublicSigningKey userId)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.guild_members WHERE guild_id = @guild_id AND user_id = @user_id";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pUserId = command.CreateParameter();
		pUserId.ParameterName = "@user_id";
		pUserId.DbType = DbType.String;
		pUserId.Value = userId.ToString();

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pUserId);

		// Execute command
		return await RunExists(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM chat.guild_members WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parmeters
		command.Parameters.Add(pId);

		// Execute command
		return await RunExists(command);
	}
}
using System.Data;
using System.Data.Common;
using Database.Models.Public;

namespace Database.Handlers.Defaults.Public;

public class InvitesHandler : BaseHandler
{
	public async Task<MInvite> Create(Guid guildId, Guid channelId, PublicSigningKey createdBy, int uses,
		string? customisation = null, DateTime? expiry = null,
		PublicSigningKey? targetUser = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"INSERT INTO public.invites (guild_id, channel_id, created_by, uses, customisation, expires_at, target_user) " +
			"VALUES (@guild_id, @channel_id, @created_by, @uses, @customisation, @expiry, @target_user) RETURNING *";

		// Create parameters
		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = guildId;

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = channelId;

		DbParameter pCreatedBy = command.CreateParameter();
		pCreatedBy.ParameterName = "@created_by";
		pCreatedBy.DbType = DbType.String;
		pCreatedBy.Value = createdBy.ToString();

		DbParameter pUses = command.CreateParameter();
		pUses.ParameterName = "@uses";
		pUses.DbType = DbType.Int32;
		pUses.Value = uses;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = customisation;
		pCustomisation.IsNullable = true;

		DbParameter pExpiry = command.CreateParameter();
		pExpiry.ParameterName = "@expiry";
		pExpiry.DbType = DbType.DateTime;
		pExpiry.Value = expiry;
		pExpiry.IsNullable = true;

		DbParameter pTargetUser = command.CreateParameter();
		pTargetUser.ParameterName = "@target_user";
		pTargetUser.DbType = DbType.String;
		pTargetUser.Value = targetUser?.ToString();
		pTargetUser.IsNullable = true;

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pChannelId);
		command.Parameters.Add(pCreatedBy);
		command.Parameters.Add(pUses);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pExpiry);
		command.Parameters.Add(pTargetUser);

		// Execute command
		return await RunModify(command, reader => new MInvite(reader));
	}

	public async Task<MInvite?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.invites WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunGet(command, reader => new MInvite(reader));
	}

	public async Task<MInvite> Update(MInvite invite)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE public.invites SET guild_id = @guild_id, channel_id = @channel_id, created_by = @created_by, " +
			"uses = @uses, customisation = @customisation, expires_at = @target_user, target_user = @target_user WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = invite.Id;

		DbParameter pGuildId = command.CreateParameter();
		pGuildId.ParameterName = "@guild_id";
		pGuildId.DbType = DbType.Guid;
		pGuildId.Value = invite.GuildId;

		DbParameter pChannelId = command.CreateParameter();
		pChannelId.ParameterName = "@channel_id";
		pChannelId.DbType = DbType.Guid;
		pChannelId.Value = invite.ChannelId;

		DbParameter pCreatedBy = command.CreateParameter();
		pCreatedBy.ParameterName = "@created_by";
		pCreatedBy.DbType = DbType.String;
		pCreatedBy.Value = invite.CreatedBy.ToString();

		DbParameter pUses = command.CreateParameter();
		pUses.ParameterName = "@uses";
		pUses.DbType = DbType.Int32;
		pUses.Value = invite.Uses;

		DbParameter pCustomisation = command.CreateParameter();
		pCustomisation.ParameterName = "@customisation";
		pCustomisation.DbType = DbType.String;
		pCustomisation.Value = invite.CustomisationRaw;
		pCustomisation.IsNullable = true;

		DbParameter pExpiry = command.CreateParameter();
		pExpiry.ParameterName = "@expiry";
		pExpiry.DbType = DbType.DateTime;
		pExpiry.Value = invite.ExpiresAt;
		pExpiry.IsNullable = true;

		DbParameter pTargetUser = command.CreateParameter();
		pTargetUser.ParameterName = "@target_user";
		pTargetUser.DbType = DbType.String;
		pTargetUser.Value = invite.TargetUserId?.ToString();
		pTargetUser.IsNullable = true;

		// Add parameters
		command.Parameters.Add(pGuildId);
		command.Parameters.Add(pChannelId);
		command.Parameters.Add(pCreatedBy);
		command.Parameters.Add(pUses);
		command.Parameters.Add(pCustomisation);
		command.Parameters.Add(pExpiry);
		command.Parameters.Add(pTargetUser);

		// Execute command
		return await RunModify(command, reader => new MInvite(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM public.invites WHERE id = @id";

		// Create parameters
		DbParameter pId = command.CreateParameter();
		pId.ParameterName = "@id";
		pId.DbType = DbType.Guid;
		pId.Value = id;

		// Add parameters
		command.Parameters.Add(pId);
		
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

		// Add parameters
		command.Parameters.Add(pId);

		// Execute command
		return await RunExists(command);
	}
}
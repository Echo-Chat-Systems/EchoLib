using System.Data;
using System.Data.Common;
using Database.Models.Public;

namespace Database.Handlers.Defaults.Public;

public class InvitesHandler : BaseHandler
{
	public async Task<MInvite> Create(Guid guildId, Guid channelId, string createdBy, int uses,
		string? customisation = null, DateTime? expiry = null, string? targetUser = null)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"INSERT INTO public.invites (guild_id, channel_id, created_by, uses, customisation, expires_at, target_user) " +
			"VALUES (@guild_id, @channel_id, @created_by, @uses, @customisation, @expiry, @target_user) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = guildId } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = channelId } },
			{ "@created_by", new Parameter { Type = DbType.String, Value = createdBy } },
			{ "@uses", new Parameter { Type = DbType.Int32, Value = uses } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = customisation, Nullable = true } },
			{ "@expiry", new Parameter { Type = DbType.DateTime, Value = expiry, Nullable = true } },
			{ "@target_user", new Parameter { Type = DbType.String, Value = targetUser, Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MInvite(reader));
	}

	public async Task<MInvite?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM public.invites WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new MInvite(reader));
	}

	public async Task<MInvite> Update(MInvite invite)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText =
			"UPDATE public.invites SET guild_id = @guild_id, channel_id = @channel_id, created_by = @created_by, " +
			"uses = @uses, customisation = @customisation, expires_at = @expiry, target_user = @target_user WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = invite.Id } },
			{ "@guild_id", new Parameter { Type = DbType.Guid, Value = invite.GuildId } },
			{ "@channel_id", new Parameter { Type = DbType.Guid, Value = invite.ChannelId } },
			{ "@created_by", new Parameter { Type = DbType.String, Value = invite.CreatedBy.ToString() } },
			{ "@uses", new Parameter { Type = DbType.Int32, Value = invite.Uses } },
			{ "@customisation", new Parameter { Type = DbType.String, Value = invite.CustomisationRaw, Nullable = true } },
			{ "@expiry", new Parameter { Type = DbType.DateTime, Value = invite.ExpiresAt, Nullable = true } },
			{ "@target_user", new Parameter { Type = DbType.String, Value = invite.TargetUserId?.ToString(), Nullable = true } }
		});

		// Execute command
		return await RunModify(command, reader => new MInvite(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM public.invites WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
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
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunExists(command);
	}
}
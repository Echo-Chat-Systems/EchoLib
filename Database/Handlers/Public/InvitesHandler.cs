using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Defaults.Public;

public class InvitesHandler : BaseHandler
{
	public async Task<MInvite> Create(Guid guildId, Guid channelId, PublicSigningKey createdBy, int uses,
		string? customisation = null, DateTime? expiry = null,
		PublicSigningKey? targetUser = null)
	{
	}

	public async Task<MInvite?> Get(Guid id)
	{
	}

	public async Task<MInvite> Update(MInvite invite)
	{
	}

	public async Task Delete(Guid id)
	{
	}

	public async Task<bool> Exists(Guid id)
	{
	}
}
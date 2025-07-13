using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BInvitesHandler : BaseHandler
{
	public abstract Task<MInvite> Create(Guid guildId, Guid channelId, PublicSigningKey createdBy, int uses, string? customisation = null, DateTime? expiry = null,
		PublicSigningKey? targetUser = null);

	public abstract Task<MInvite?> Get(Guid id);
	public abstract Task<MInvite> Update(MInvite invite);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IInvitesHandler
{
	InviteRow Create(Guid guildId, Guid channelId, PublicSigningKey createdBy, int uses, string? customisation = null, DateTime? expiry = null, PublicSigningKey? targetUser = null);
	InviteRow? Get(Guid id);
	InviteRow Update(InviteRow invite);
	void Delete(Guid id);
	bool Exists(Guid id);
}
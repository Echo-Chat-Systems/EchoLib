using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IGuildMembersHandler
{
	GuildMemberRow Create(Guid guildId, PublicSigningKey userId, string? nickname = null, string? customisation = null);
	GuildMemberRow? Get(Guid guildId, PublicSigningKey userId);
	GuildMemberRow? Get(Guid id);
	GuildMemberRow Update(GuildMemberRow member);
	void Delete(Guid guildId, PublicSigningKey userId);
	void Delete(Guid id);
	bool Exists(Guid guildId, PublicSigningKey userId);
	bool Exists(Guid id);
}
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IGuildsHandler
{
	GuildRow Create(PublicSigningKey owner, string name, string? customisation = null);
	GuildRow? Get(Guid id);
	GuildRow Update(GuildRow guild);
	void Delete(Guid id);
	bool Exists(Guid id);
}
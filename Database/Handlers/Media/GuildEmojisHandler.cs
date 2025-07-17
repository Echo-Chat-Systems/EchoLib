using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Media;

namespace EchoLib.Database.Handlers.Media;

public abstract class GuildEmojisHandler : BaseHandler
{
	// TODO: Implement here
	public abstract Task<MGuildEmoji> Create(Guid guildId, PublicSigningKey userId, string name, Guid fileId,
		MGuildEmoji.MediaType? mediaType = null, string? customisation = null);
	
	public abstract Task<MGuildEmoji
}
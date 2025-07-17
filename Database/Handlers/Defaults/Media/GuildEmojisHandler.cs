using Database.Models.Media;

namespace Database.Handlers.Defaults.Media;

public abstract class GuildEmojisHandler : BaseHandler
{
	// TODO: Implement here
	public abstract Task<MGuildEmoji> Create(Guid guildId, PublicSigningKey userId, string name, Guid fileId,
		MGuildEmoji.MediaType? mediaType = null, string? customisation = null);
	
	public abstract Task<MGuildEmoji
}
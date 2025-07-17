namespace EchoLib.Database.Handlers.Media;

public class MediaHandlers
{
	public required FilesHandler Files { get; init; }
	public required GuildEmojisHandler GuildEmojis { get; init; }
}
using EchoLib.Database.Handlers.Media;
using EchoLib.Database.Handlers.Public;

namespace EchoLib.Database.Handlers.HandlerGroups;

public class MediaHandlers
{
	public required FilesHandler Files { get; init; }
	public required MessageAttachmentsHandler MessageAttachments { get; init; }
}
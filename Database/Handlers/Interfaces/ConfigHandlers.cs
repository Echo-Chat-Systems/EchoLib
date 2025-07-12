using EchoLib.Database.Handlers.Interfaces.Config;

namespace EchoLib.Database.Handlers.Interfaces;

public class ConfigHandlers
{
	public required IConfigDataHandler Data { get; init; }
	public required IOwnersHandler Owners { get; init; }
}
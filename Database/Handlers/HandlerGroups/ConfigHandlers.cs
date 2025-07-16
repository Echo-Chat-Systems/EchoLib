using System.Data.Common;
using EchoLib.Database.Handlers.Bases.Config;

namespace EchoLib.Database.Handlers.HandlerGroups;

// ReSharper disable once ClassNeverInstantiated.Global
public class ConfigHandlers
{
	public required ConfigDataHandler Data { get; init; }
	public required OwnersHandler Owners { get; init; }

	public void Populate(HandlersGroup handlers)
	{
		Data.Populate(handlers);
		Owners.Populate(handlers);
	}
}
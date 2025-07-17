namespace EchoLib.Database.Handlers.Config;

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
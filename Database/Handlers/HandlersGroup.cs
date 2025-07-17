namespace EchoLib.Database.Handlers.HandlerGroups;

// ReSharper disable once ClassNeverInstantiated.Global
public class HandlersGroup
{
	public required ChatHandlers Chat { get; init; }
	public required ConfigHandlers Config { get; init; }
	public required MediaHandlers Media { get; init; }
	public required PublicHandlers Public { get; init; }
	public required SecureHandlers Secure { get; init; }

	public void Populate(HandlersGroup handlers)
	{
		Config.Populate(handlers);
		Public.Populate(handlers);
		Secure.Populate(handlers);
	}
}
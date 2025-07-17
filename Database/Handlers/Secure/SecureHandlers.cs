using EchoLib.Database.Handlers.Secure;

namespace EchoLib.Database.Handlers.HandlerGroups;

// ReSharper disable once ClassNeverInstantiated.Global
public class SecureHandlers
{
	public required CertificatesHandler Certificates { get; init; }
	public required ChannelCommitsHandler ChannelCommits { get; init; }
	public required MlsStatesHandler MlsStates { get; init; }
	
	public void Populate(HandlersGroup handlers)
	{
		Certificates.Populate(handlers);
		ChannelCommits.Populate(handlers);
		MlsStates.Populate(handlers);
	}
}
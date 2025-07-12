using EchoLib.Comms;
using EchoLib.Params;

namespace EchoLib.Processes;

public class PingAction : IAction<PingParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Ping;
}
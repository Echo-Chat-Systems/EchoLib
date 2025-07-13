using EchoLib.Comms;
using EchoLib.Processes.Params;

namespace EchoLib.Processes.Actions;

public class PingAction : IAction<PingParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Ping;
}
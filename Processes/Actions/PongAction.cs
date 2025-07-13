using EchoLib.Comms;
using EchoLib.Processes.Params;

namespace EchoLib.Processes.Actions;

public class PongAction : IAction<PongParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Pong;
}
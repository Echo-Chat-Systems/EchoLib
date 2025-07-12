using EchoLib.Comms;
using EchoLib.Params;

namespace EchoLib.Processes;

public class PongAction : IAction<PongParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Pong;
}
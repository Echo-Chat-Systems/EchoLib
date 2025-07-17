using Core.Comms;
using Core.Processes.Params;

namespace Core.Processes.Actions;

public class PongAction : IAction<PongParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Pong;
}
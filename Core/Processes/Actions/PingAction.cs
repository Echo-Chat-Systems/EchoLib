using Core.Comms;
using Core.Processes.Params;

namespace Core.Processes.Actions;

public class PingAction : IAction<PingParams>
{
	public static string Target { get; } = string.Empty;
	public static string Action { get; } = Targets.PingActions.Ping;
}
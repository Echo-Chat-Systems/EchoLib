using EchoLib.Comms;
using EchoLib.Processes.Params.Error;

namespace EchoLib.Processes.Actions.Error;

public class ErrorAction : IAction<ErrorParams>
{
	public static string Target { get; } = Targets.Error;
	public static string Action { get; } = Targets.ErrorActions.ErrorMessage;
}
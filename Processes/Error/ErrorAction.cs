using EchoLib.Comms;
using EchoLib.Params.Auth.Error;

namespace EchoLib.Processes.Error;

public class ErrorAction : IAction<ErrorParams>
{
	public static string Target { get; } = Targets.Error;
	public static string Action { get; } = Targets.ErrorActions.ErrorMessage;
}
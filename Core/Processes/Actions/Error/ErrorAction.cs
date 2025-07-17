using Core.Comms;
using Core.Processes.Params.Error;

namespace Core.Processes.Actions.Error;

public class ErrorAction : IAction<ErrorParams>
{
	public static string Target { get; } = Targets.Error;
	public static string Action { get; } = Targets.ErrorActions.ErrorMessage;
}
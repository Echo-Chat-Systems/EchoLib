using Core.Comms;
using Core.Processes.Params.Auth;

namespace Core.Processes.Actions.Auth.SignIn;

public class SigninStartAction : IAction<SignupParams>
{
	public static string Target => Targets.Auth;
	public static string Action => Targets.AuthActions.SigninStart;
}
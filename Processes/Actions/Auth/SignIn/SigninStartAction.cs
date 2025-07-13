using EchoLib.Comms;
using EchoLib.Processes.Params.Auth;

namespace EchoLib.Processes.Actions.Auth.SignIn;

public class SigninStartAction : IAction<SignupParams>
{
	public static string Target => Targets.Auth;
	public static string Action => Targets.AuthActions.SigninStart;
}
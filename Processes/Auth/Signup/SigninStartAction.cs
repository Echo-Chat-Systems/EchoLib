using EchoLib.Comms;
using EchoLib.Params.Auth;

namespace EchoLib.Processes.Auth.Signup;

public class SigninStartAction : IAction<SignupParams>
{
	public static string Target => Targets.Auth;
	public static string Action => Targets.AuthActions.SigninStart;
}
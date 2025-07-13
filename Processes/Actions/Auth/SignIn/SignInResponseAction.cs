using EchoLib.Comms;
using EchoLib.Processes.Params.Auth.Signin;

namespace EchoLib.Processes.Actions.Auth.SignIn;

public class SignInResponseAction : IAction<SigninResponseParams>
{
	public static string Target { get; } = Targets.Auth;
	public static string Action { get; } = Targets.AuthActions.SigninResponse;
}
using Core.Comms;
using Core.Processes.Params.Auth.Signin;

namespace Core.Processes.Actions.Auth.SignIn;

public class SignInResponseAction : IAction<SigninResponseParams>
{
	public static string Target { get; } = Targets.Auth;
	public static string Action { get; } = Targets.AuthActions.SigninResponse;
}
using Core.Comms;
using Core.Processes.Params.Auth.Signin;

namespace Core.Processes.Actions.Auth.SignIn;

public class SignInChallengeAction : IAction<SigninChallengeParams>
{
	public static string Target { get; } = Targets.Auth;
	public static string Action { get; } = Targets.AuthActions.SigninChallenge;
}
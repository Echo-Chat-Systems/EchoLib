using EchoLib.Comms;
using EchoLib.Params.Auth;

namespace EchoLib.Processes.Auth.Signup;

public class SignupAction : IAction<SignupParams>
{
	public static string Target => Targets.Auth;
	public static string Action => "signup";
}
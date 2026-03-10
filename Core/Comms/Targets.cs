using Core.Processes.Params.Auth.Signin;

namespace Core.Comms;

public static class Targets
{
	public const string Error = "error";
	public const string Guilds = "guilds";
	public const string Auth = "auth";
	public const string Chat = "chat";
	public const string Admin = "admin";
	

	public static class PingActions
	{
		public const string Ping = "ping";
		public const string Pong = "pong";
	}

	public static class ErrorActions
	{
		public const string ErrorMessage = "error";
	}

	public static class AuthActions
	{
		public const string SigninStart = "signin-start";
		public const string SigninChallenge = "signin-challenge";
		public const string SigninResponse = "signin-response";
		public const string SigninComplete = "signin-complete";

	}

	public static Dictionary<string, Dictionary<string, Type>> Actions = new()
	{
		{
			Auth, new Dictionary<string, Type>
			{
				{ AuthActions.SigninStart, typeof(SigninStartParams) },
				{ AuthActions.SigninChallenge, typeof(SigninChallengeParams) },
				{ AuthActions.SigninResponse, typeof(SigninResponseParams) },
				{ AuthActions.SigninComplete, typeof(SigninCompleteParams) },
			}
		},
	};
}
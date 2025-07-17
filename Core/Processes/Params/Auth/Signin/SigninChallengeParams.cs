using System.Text.Json.Serialization;

namespace Core.Processes.Params.Auth.Signin;

public class SigninChallengeParams
{
	[JsonPropertyName("sign-challenge")] public required string SignChallenge { get; set; }
	[JsonPropertyName("encrypt-challenge")] public required string EncryptChallenge { get; set; }
	[JsonPropertyName("ref")] public required string Ref { get; set; }
}
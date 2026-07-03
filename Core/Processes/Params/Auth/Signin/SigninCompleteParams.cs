using System.Text.Json.Serialization;
using Models.Comms.Users.Broadcasts.User;

namespace Core.Processes.Params.Auth.Signin;

public class SigninCompleteParams
{
	[JsonPropertyName("status")] public required int Status { get; set; }
	[JsonPropertyName("cert")] public UserCertificate? Cert { get; set; }
	[JsonPropertyName("msg")] public string? Message { get; set; }
}
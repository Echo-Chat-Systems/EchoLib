using System.Text.Json.Serialization;
using EchoLib.Models.Net.Users.Broadcasts.User;

namespace EchoLib.Processes.Params.Auth.Signin;

public class SigninCompleteParams
{
	[JsonPropertyName("status")] public required int Status { get; set; }
	[JsonPropertyName("cert")] public UserCertificate? Cert { get; set; }
	[JsonPropertyName("msg")] public string? Message { get; set; }
}
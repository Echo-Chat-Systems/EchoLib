using System.Text.Json.Serialization;
using EchoLib.Models.Net.Users.Broadcasts.User;

namespace EchoLib.Params.Auth;

public class SignupParams
{
	[JsonPropertyName("user")] public required UserCertificate User { get; set; }
}
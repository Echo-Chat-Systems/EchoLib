using System.Text.Json.Serialization;
using EchoLib.Models.Net.Users.Broadcasts.User;

namespace EchoLib.Processes.Params.Auth;

public class SignupParams
{
	[JsonPropertyName("user")] public required UserCertificate User { get; set; }
}
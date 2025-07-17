using System.Text.Json.Serialization;
using Core.Models.Net.Users.Broadcasts.User;

namespace Core.Processes.Params.Auth;

public class SignupParams
{
	[JsonPropertyName("user")] public required UserCertificate User { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace Core.Models.Net;

public class Response
{
	[JsonPropertyName("code")]
	[Required]
	public required HttpStatusCode? Code { get; set; }

	[JsonPropertyName("info")]
	public ResponseInfo? Info { get; set; }

	public class ResponseInfo
	{
		[JsonPropertyName("msg")]
		public required string Message { get; set; }
	}
}
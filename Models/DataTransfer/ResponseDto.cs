using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace Models.DataTransfer;

public class ResponseDto
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
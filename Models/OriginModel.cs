using System.ComponentModel.DataAnnotations;

namespace Models;

public class OriginModel
{
	[Required] public required string Host { get; init; }
	[Required] public required int Port { get; init; }
}
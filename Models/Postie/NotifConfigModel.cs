using System.ComponentModel.DataAnnotations;

namespace Models.Postie;

public class NotifConfigModel
{
	[Required] public required string Mode { get; set; }
	[Required] public required IEnumerable<Snowflake> SuppressedRoles { get; set; }
}
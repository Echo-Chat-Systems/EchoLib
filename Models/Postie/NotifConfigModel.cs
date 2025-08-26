using System.ComponentModel.DataAnnotations;
using Models.Generic;

namespace Models.Postie;

public class NotifConfigModel
{
	[Required] public required string Mode { get; set; }
	[Required] public required IEnumerable<Snowflake> SuppressedRoles { get; set; }
}
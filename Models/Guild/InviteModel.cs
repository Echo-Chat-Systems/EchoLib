using System.ComponentModel.DataAnnotations;

namespace Models.Guild;

public class InviteModel : BaseEntityModel
{
	[Required] public required string Code { get; init; }
	[Required] public required Snowflake GuildId { get; init; }

	/// <summary>
	/// User who created the invite.
	/// </summary>
	public Snowflake? Attribution { get; set; }

	/// <summary>
	/// Remaining uses of the invite.
	/// </summary>
	public int? Uses { get; set; }

	public
}
using System.ComponentModel.DataAnnotations;
using Models.Channel;
using Models.Media;

namespace Models.Guild;

public class ExternalGuildModel : GuildModel
{
	[Required] public required IEnumerable<MemberModel> Members { get; set; }
	[Required] public required IEnumerable<ExternalChannelModel> Channels { get; set; }
	[Required] public required IEnumerable<RoleModel> Roles { get; set; }
	[Required] public required MediaModel Media { get; set; }

	public class MediaModel
	{
		[Required] public required IEnumerable<RichMediaModel> Emojis { get; set; }
		[Required] public required IEnumerable<RichMediaModel> Stickers { get; set; }
	}
}
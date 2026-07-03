using System.ComponentModel.DataAnnotations;
using Models.DataTransfer.Channel;
using Models.Media;

namespace Models.Guild;

public class GuildDto : GuildModel
{
	[Required] public required IEnumerable<MemberModel> Members { get; set; }
	[Required] public required IEnumerable<ChannelDto> Channels { get; set; }
	[Required] public required IEnumerable<RoleModel> Roles { get; set; }
	[Required] public required MediaModel Media { get; set; }

	public class MediaModel
	{
		[Required] public required IEnumerable<RichMediaModel> Emojis { get; set; }
		[Required] public required IEnumerable<RichMediaModel> Stickers { get; set; }
	}
}
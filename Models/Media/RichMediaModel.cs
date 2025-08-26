using System.ComponentModel.DataAnnotations;
using Models.Generic;

namespace Models.Media;

public class RichMediaModel : BaseDbm
{
	[Required] public required EmojiFrom From { get; set; }
	[Required] public required string Name { get; set; }
	[Required] public required string Ref { get; set; }

	/// <summary>
	/// Source guild ID if the media is from a guild.
	/// </summary>
	public Snowflake? Source { get; set; }

	/// <summary>
	/// Media size in pixels.
	/// </summary>
	[Required] public required SizeModel Size { get; set; }

	/// <summary>
	/// Media type, either sticker or emoji.
	/// </summary>
	[Required] public required MediaType Type { get; set; }

	public enum EmojiFrom
	{
		Server = 0,
		Guild = 1
	}

	public class SizeModel
	{
		[Required] public required int X { get; set; }
		[Required] public required int Y { get; set; }
	}

	public enum MediaType
	{
		Sticker = 0,
		Emoji = 1
	}
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Auth.Signing;
using Core.Helpers.Snowflake;

namespace Database.Models.NoSql;

public class MMessage()
{
	[Required]
	[JsonPropertyName("id")]
	public Snowflake Id
	{
		get;
		init;
	}

	[Required]
	[JsonPropertyName("user_id")]
	public UserId UserId { get; init; }

	[Required]
	[JsonPropertyName("channel_id")]
	public Guid ChannelId { get; init; }

	[Required] [JsonPropertyName("body")] public string Body { get; init; }

	[Required]
	[JsonPropertyName("metadata")]
	public MMetadata Metadata { get; init; }

	public class MMetadata
	{
		[JsonPropertyName("mls")] public MMls? Mls { get; set; }

		[JsonPropertyName("attachments")] public Guid[] Attachments { get; set; } = [];

		[JsonPropertyName("reactions")] public MReaction[] Reactions { get; set; } = [];

		public class MReaction
		{
			[JsonPropertyName("user_id")]
			[Required]
			public UserId UserId { get; set; }

			[JsonPropertyName("guild_emoji_id")] public Guid? GuildEmojiId { get; set; }
			[JsonPropertyName("emoji_name")] public string? EmojiName { get; set; }
		}

		public class MMls
		{
			[JsonPropertyName("epoch")] [Required] public int? Epoch { get; set; }

			[JsonPropertyName("sender_index")]
			[Required]
			public int? SenderIndex { get; set; }
		}
	}
}
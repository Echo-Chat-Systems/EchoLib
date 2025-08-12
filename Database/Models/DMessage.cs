using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Auth.Signing;
using Core.Helpers.Snowflake;

namespace Database.Models.NoSql;

public class DMessage()
{
	[Required]
	[JsonPropertyName("id")]
	public required Snowflake Id { get; init; }

	[Required]
	[JsonPropertyName("user_id")]
	public required UserId UserId { get; init; }

	[Required]
	[JsonPropertyName("channel_id")]
	public required Snowflake ChannelId { get; init; }

	[Required]
	[JsonPropertyName("body")]
	public required string Body { get; init; }

	[Required]
	[JsonPropertyName("metadata")]
	public required MMetadata Metadata { get; init; }

	public class MMetadata
	{
		[JsonPropertyName("mls")]
		public MMls? Mls { get; set; }

		[JsonPropertyName("attachments")]
		public Snowflake[] Attachments { get; set; } = [];

		[JsonPropertyName("reactions")]
		public MReaction[] Reactions { get; set; } = [];

		public class MReaction
		{
			[Required]
			[JsonPropertyName("user_id")]
			public required UserId UserId { get; set; }

			[JsonPropertyName("guild_emoji_id")]
			public Snowflake? GuildEmojiId { get; set; }
			[JsonPropertyName("emoji_name")]
			public string? EmojiName { get; set; }
		}

		public class MMls
		{
			[Required]
			[JsonPropertyName("epoch")]
			public required int Epoch { get; set; }

			[Required]
			[JsonPropertyName("sender_index")]
			public required int SenderIndex { get; set; }
		}
	}
}
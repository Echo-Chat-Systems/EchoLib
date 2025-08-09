using System.Data;
using Core.Auth.Signing;
using Core.Helpers.Snowflake;
using Core.Models.Other.Permissions;

namespace Database.Models.Chat;

public class MChannelMember : BaseModel
{
	/// <inheritdoc cref="BaseModel"/>
	public MChannelMember(IDataRecord record) : base(record)
	{
		UserId = new UserId(record.GetString(record.GetOrdinal("user_id")));
		ChannelId = new Snowflake(record.GetInt64(record.GetOrdinal("channel_id")));
		Permissions = record.GetInt64(record.GetOrdinal("permissions"));
	}

	/// <inheritdoc cref="BaseModel"/>
	public MChannelMember(UserId userId, Snowflake channelId, long? permissions = null)
	{
		UserId = userId;
		ChannelId = channelId;
		Permissions = permissions ?? 0;
	}

	/// <summary>
	/// User id.
	/// </summary>
	public UserId UserId { get; init; }

	/// <summary>
	/// Channel id.
	/// </summary>
	public Snowflake ChannelId { get; init; }

	/// <summary>
	/// Permissions this user has in the channel.
	/// </summary>
	public long Permissions { get; set; }
}
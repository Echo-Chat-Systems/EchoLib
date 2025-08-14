using System.Data;
using System.Text.Json;
using Core.Helpers;
using Database.Models.Public;
using Models;
using Models.Crypto.Signing;

namespace Database.Models.Chat;

public class MGuildMember : BaseModel
{
	// Default models
	public static readonly UserModel.MProfile DefaultCustomisationOverride = new() { };

	/// <inheritdoc cref="BaseModel"/>
	public MGuildMember(IDataRecord record) : base(record)
	{
		// Assign parameters
		GuildId = new Snowflake(record.GetInt64(record.GetOrdinal("guild_id")));
		UserId = new UserId(record.GetString(record.GetOrdinal("user_id")));
		Nickname = record.GetString(record.GetOrdinal("nickname"));
		CustomisationOverrideRaw = record.GetString(record.GetOrdinal("customisation_override"));

		CustomisationOverride = JsonSerializer.Deserialize<UserModel.MProfile>(CustomisationOverrideRaw, StaticOptions.JsonSerialzer) ?? throw new InvalidDataException(nameof(CustomisationOverrideRaw));
	}

	/// <inheritdoc />
	/// <param name="guildId">Guild id</param>
	/// <param name="userId">User id</param>
	/// <param name="nickname">User nickname override.</param>
	/// <param name="customisationOverride">User profile override.</param>
	public MGuildMember(Snowflake guildId, UserId userId, string? nickname = null, UserModel.MProfile? customisationOverride = null)
	{
		GuildId = guildId;
		UserId = userId;
		Nickname = nickname;
		CustomisationOverride = customisationOverride ?? DefaultCustomisationOverride;
	}

	// Internal fields for models
	private UserModel.MProfile _customisationOverride = null!;

	/// <summary>
	/// Guild id.
	/// </summary>
	public Snowflake GuildId { get; init; }

	/// <summary>
	/// User id.
	/// </summary>
	public UserId UserId { get; init; }

	/// <summary>
	/// Nickname override.
	/// </summary>
	public string? Nickname { get; set; }

	/// <summary>
	/// Raw serialised <see cref="CustomisationOverride"/>
	/// </summary>
	public string CustomisationOverrideRaw { get; private set; } = null!;

	/// <summary>
	/// User profile customisation override.
	/// </summary>
	public UserModel.MProfile CustomisationOverride
	{
		get => _customisationOverride;
		set
		{
			_customisationOverride = value;
			CustomisationOverrideRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
		}
	}
}
using System.Data;
using System.Text.Json;
using Core.Helpers;
using Core.Helpers.Snowflake;

namespace Database.Models.Public;

public class MRole : BaseModel
{
	// Default models
	public static readonly MCustomisation DefaultCustomisation = new() { };

	/// <inheritdoc />
	public MRole(IDataRecord record) : base(record)
	{
		GuildId = new Snowflake(record.GetInt64(record.GetOrdinal("guild_id")));
		Name = record.GetString(record.GetOrdinal("name"));
		Permissions = record.GetInt64(record.GetOrdinal("permissions"));

		Customisation = JsonSerializer.Deserialize<MCustomisation>(record.GetString(record.GetOrdinal("customisation"))) ?? throw new InvalidDataException(nameof(CustomisationRaw));;
	}

	/// <inheritdoc />
	/// <param name="guildId">Guild id.</param>
	/// <param name="name">Role name.</param>
	/// <param name="customisation">Role customisation.</param>
	/// <param name="permissions">Role permissions.</param>
	public MRole(Snowflake guildId, string name, MCustomisation? customisation = null, long? permissions = null)
	{
		GuildId = guildId;
		Name = name;
		Permissions = permissions ?? 0;
		Customisation = customisation ?? DefaultCustomisation;
	}

	// Internal fields for models
	private MCustomisation _customisation = null!;

	/// <summary>
	/// Guild this role belongs to.
	/// </summary>
	public Snowflake GuildId { get; init; }

	/// <summary>
	/// Role name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Role permissions.
	/// </summary>
	public long Permissions { get; set; }

	/// <summary>
	/// Raw serialised <see cref="Customisation"/>
	/// </summary>
	public string CustomisationRaw { get; private set; } = null!;

	/// <summary>
	/// Channel customisation.
	/// </summary>
	public MCustomisation Customisation
	{
		get => _customisation;
		set
		{
			_customisation = value;
			CustomisationRaw = JsonSerializer.Serialize(value, StaticOptions.JsonSerialzer);
		}
	}

	/// <summary>
	/// Role customisation model.
	/// </summary>
	public class MCustomisation
	{
	}
}
using System.Data;
using System.Text.Json;
using Core.Helpers;
using Core.Helpers.Snowflake;

namespace Database.Models.Chat;

public class MGuild : BaseModel
{
	// Default models
	public static readonly MCustomisation DefaultCustomisation = new() { };

	/// <inheritdoc />
	public MGuild(IDataRecord record) : base(record)
	{
		OwnerId = new Snowflake(record.GetInt64(record.GetOrdinal("owner_id")));
		Name = record.GetString(record.GetOrdinal("name"));
		CustomisationRaw = record.GetString(record.GetOrdinal("customisation"));

		DeserialiseModels();
	}

	/// <inheritdoc />
	/// <param name="ownerId">Guild owner id.</param>
	/// <param name="name">Guild name.</param>
	/// <param name="customisation">Guild channels.</param>
	public MGuild(Snowflake ownerId, string name, MCustomisation? customisation = null)
	{
		OwnerId = ownerId;
		Name = name;
		Customisation = customisation ?? DefaultCustomisation;

		DeserialiseModels();
	}

	private void DeserialiseModels()
	{
		// Deserialise models
		_customisation = JsonSerializer.Deserialize<MCustomisation>(CustomisationRaw, StaticOptions.JsonSerialzer)
		                 ?? throw new InvalidDataException(nameof(CustomisationRaw));
	}

	// Internal fields for models
	private MCustomisation _customisation = null!;

	/// <summary>
	/// Guild owner.
	/// </summary>
	public Snowflake OwnerId { get; set; }

	/// <summary>
	/// Guild name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Raw serialised <see cref="Customisation"/>
	/// </summary>
	public string CustomisationRaw { get; private set; } = null!;

	/// <summary>
	/// Guild customisation.
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
	/// Guild customisation model.
	/// </summary>
	public class MCustomisation
	{
	}
}
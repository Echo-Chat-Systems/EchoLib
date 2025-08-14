namespace Models;

public class BaseEntityModel
{
	/// <summary>
	/// Item database id.
	/// </summary>
	public Snowflake Id { get; init; }

	/// <summary>
	/// Initialise a new model.
	/// </summary>
	protected BaseEntityModel() => Id = Snowflake.New();

	/// <summary>
	/// Initialise a new model with a known id.
	/// </summary>
	/// <param name="id">Object id.</param>
	protected BaseEntityModel(Snowflake id)
	{
		Id = id;
	}
}
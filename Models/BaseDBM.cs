using Models.Generic;

namespace Models;

public class BaseDbm
{
	/// <summary>
	/// Item database id.
	/// </summary>
	public Snowflake Id { get; init; }

	/// <summary>
	/// Initialise a new model.
	/// </summary>
	protected BaseDbm() => Id = Snowflake.New();

	/// <summary>
	/// Initialise a new model with a known id.
	/// </summary>
	/// <param name="id">Object id.</param>
	protected BaseDbm(Snowflake id)
	{
		Id = id;
	}
}
using Core.Helpers;
using Core.Helpers.Snowflake;

namespace Core.Models;

public class BaseEntityModel
{
	/// <summary>
	/// Item database id.
	/// </summary>
	public Snowflake Id { get; init; }

	/// <summary>
	/// Initialise a new model.
	/// </summary>
	protected BaseEntityModel() => Id = StaticOptions.SnowflakeGenerator.New();
}
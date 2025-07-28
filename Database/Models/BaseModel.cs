using System.Data;
using Core.Helpers;
using Core.Helpers.Snowflake;

namespace Database.Models;

public class BaseModel
{
	/// <summary>
	/// Item database id.
	/// </summary>
	public Snowflake Id { get; init; }

	/// <summary>
	/// Initialise a model from a record.
	/// </summary>
	/// <param name="record">Record.</param>
	protected BaseModel(IDataRecord record)
	{
		Id = new Snowflake(record.GetInt64(record.GetOrdinal("id")));
	}

	/// <summary>
	/// Initialise a new model.
	/// </summary>
	protected BaseModel() => Id = StaticOptions.SnowflakeGenerator.New();
}
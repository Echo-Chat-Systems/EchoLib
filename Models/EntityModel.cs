namespace Models;

/// <summary>
/// Base entity model. An entity is simply something with an ID. 
/// </summary>
public abstract class EntityModel
{
	/// <summary>
	/// Entity ID.
	/// </summary>
	public required ulong Id { get; init; }
}
namespace EchoLib.Database.Handlers.Interfaces.Config;

public interface IOwnersHandler
{
	void Add(Guid ownerId);
	void Remove(Guid ownerId);
	bool Exists(Guid ownerId);
	Guid[] GetAll();
}
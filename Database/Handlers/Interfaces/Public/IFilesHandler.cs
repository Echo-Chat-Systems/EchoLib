using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IFilesHandler
{
	FileRow Create(PublicSigningKey owner);
	FileRow? Get(Guid id);
	FileRow Update(FileRow file);
	void Delete(Guid id);
	bool Exists(Guid id);
}
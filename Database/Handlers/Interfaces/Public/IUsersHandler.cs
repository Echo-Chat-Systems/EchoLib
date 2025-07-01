using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IUsersHandler
{
	UserRow Create(PublicSigningKey sk, PublicEncryptionKey ek, string username, int tag);
	UserRow? Get(PublicSigningKey sk);
	UserRow Update(UserRow user);
	void Delete(PublicSigningKey sk);
	bool Exists(PublicSigningKey sk);
}
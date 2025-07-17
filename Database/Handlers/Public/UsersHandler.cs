using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Defaults.Public;

public abstract class UsersHandler : BaseHandler
{
	public async Task<MUser> Create(PublicSigningKey sk, PublicEncryptionKey ek, string username, int tag)
	{
	}

	public async Task<MUser?> Get(PublicSigningKey sk)
	{
	}

	public async Task<MUser> Update(MUser user)
	{
	}

	public async Task Delete(PublicSigningKey sk)
	{
	}

	public async Task<bool> Exists(PublicSigningKey sk)
	{
	}
}
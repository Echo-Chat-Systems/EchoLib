using System.Data.Common;
using Database.Models.Public;

namespace Database.Handlers.Defaults.Public;

public abstract class UsersHandler : BaseHandler
{
	public async Task<MUser> Create(PublicSigningKey sk, PublicEncryptionKey ek, string username, int tag, MUser.MProfile profile)
	{
		// Create command
		await using DbCommand command = await Command(true);
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
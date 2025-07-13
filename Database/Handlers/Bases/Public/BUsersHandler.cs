using System.Data.Common;
using EchoLib.Auth.Encryption;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BUsersHandler : BaseHandler
{
	public abstract Task<MUser> Create(PublicSigningKey sk, PublicEncryptionKey ek, string username, int tag);
	public abstract Task<MUser?> Get(PublicSigningKey sk);
	public abstract Task<MUser> Update(MUser user);
	public abstract Task Delete(PublicSigningKey sk);
	public abstract Task<bool> Exists(PublicSigningKey sk);
}
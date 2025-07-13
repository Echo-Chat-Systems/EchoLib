using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BFilesHandler : BaseHandler
{
	public abstract Task<MFile> Create(PublicSigningKey owner);
	public abstract Task<MFile?> Get(Guid id);
	public abstract Task<MFile> Update(MFile file);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}
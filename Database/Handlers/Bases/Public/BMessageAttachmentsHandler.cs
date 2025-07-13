using System.Data.Common;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BMessageAttachmentsHandler : BaseHandler
{
	public abstract Task<MMessageAttachment> Create(Guid messageId, Guid fileId);
	public abstract Task<MMessageAttachment?> Get(Guid messageId, Guid fileId);
	public abstract Task<MMessageAttachment?> Get(Guid id);
	public abstract Task<List<MMessageAttachment>> GetMany(MMessage mMessage);
	public abstract Task<List<MMessageAttachment>> GetMany(MFile file);
	public abstract Task Delete(Guid messageId, Guid fileId);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid messageId, Guid fileId);
	public abstract Task<bool> Exists(Guid id);
}
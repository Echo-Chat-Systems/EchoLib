using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BMessagesHandler : BaseHandler
{
	public abstract Task<MMessage> Create(PublicSigningKey sender, Guid channelId, byte[] body, string metadata, out int? code, int? epoch = null, int? senderIndex = null);
	public abstract Task<MMessage?> Get(Guid id);
	public abstract Task<List<MMessage>> GetMany(Guid channelId, int? epoch = null, int? senderIndex = null, int? limit = null, int? offset = null);
	public abstract Task<MMessage> Update(MMessage mMessage);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid id);
}
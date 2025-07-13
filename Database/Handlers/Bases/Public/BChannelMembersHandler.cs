using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BChannelMembersHandler : BaseHandler
{
	public abstract Task<MChannelMember> Create(PublicSigningKey userId, Guid channelId, long permissions);
	public abstract Task<MChannelMember?> Get(Guid id);
	public abstract Task<MChannelMember?> Get(PublicSigningKey userId, Guid channelId);
	public abstract Task<MChannelMember> Update(MChannelMember channelMember);
	public abstract Task Delete(Guid id);
	public abstract Task Delete(PublicSigningKey userId, Guid channelId);
	public abstract Task<bool> Exists(Guid id);
	public abstract Task<bool> Exists(PublicSigningKey userId, Guid channelId);
}
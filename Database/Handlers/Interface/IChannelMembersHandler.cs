using Core.Auth.Signing;
using Database.Models.Chat;

namespace Database.Handlers.Interface;

public interface IChannelMembersHandler
{
	public Task<MChannelMember> Create(UserId userId, Guid channelId, long permissions);
	public Task<MChannelMember?> Get(Guid id);
	public Task<MChannelMember?> Get(UserId userId, Guid channelId);
	public Task<MChannelMember> Update(MChannelMember channelMember);
	public Task Delete(Guid id);
	public Task Delete(UserId userId, Guid channelId);
	public Task<bool> Exists(Guid id);
	public Task<bool> Exists(UserId userId, Guid channelId);
}
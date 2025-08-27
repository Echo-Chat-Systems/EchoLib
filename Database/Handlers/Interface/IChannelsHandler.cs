using Database.Models;
using Models.Channel;
using Models.Database.Channel;
using Models.DatabaseModels.Channel;

namespace Database.Handlers.Interface;

public interface IChannelsHandler
{
	public Task<ChannelDbm> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public Task<ChannelDbm?> Get(Guid id);
	public Task<ChannelDbm> Update(ChannelDbm channel);
	public Task Delete(Guid id);
	public Task<bool> Exists(Guid id);

	public Task<ChannelMemberDbm> AddMember(Guid channelId, Guid userId, string? role = null);
}
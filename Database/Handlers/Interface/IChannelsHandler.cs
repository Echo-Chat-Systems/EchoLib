using Database.Models;
using Database.Models.Chat;
using Models.Channel;

namespace Database.Handlers.Interface;

public interface IChannelsHandler
{
	public Task<ChannelModel> Create(Guid guildId, string name, short? type = null, string? customisation = null, string? config = null);
	public Task<ChannelModel?> Get(Guid id);
	public Task<ChannelModel> Update(ChannelModel channel);
	public Task Delete(Guid id);
	public Task<bool> Exists(Guid id);

	public Task<MemberModel> AddMember(Guid channelId, Guid userId, string? role = null);
}
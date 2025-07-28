using Core.Auth.Signing;
using Database.Models.Chat;

namespace Database.Handlers.Interface;

public interface IGuildMembersHandler
{
	public Task<MGuildMember> Create(Guid guildId, UserId userId, string? nickname = null, string? customisation = null);
	public Task<MGuildMember?> Get(Guid guildId, UserId userId);
	public Task<MGuildMember?> Get(Guid id);
	public Task<MGuildMember> Update(MGuildMember member);
	public Task Delete(Guid guildId, string userId);
	public Task Delete(Guid id);
	public Task<bool> Exists(Guid guildId, string userId);
	public Task<bool> Exists(Guid id);
}
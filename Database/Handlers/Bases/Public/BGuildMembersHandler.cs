using System.Data.Common;
using EchoLib.Auth.Signing;
using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Bases.Public;

public abstract class BGuildMembersHandler : BaseHandler
{
	public abstract Task<MGuildMember> Create(Guid guildId, PublicSigningKey userId, string? nickname = null, string? customisation = null);
	public abstract Task<MGuildMember?> Get(Guid guildId, PublicSigningKey userId);
	public abstract Task<MGuildMember?> Get(Guid id);
	public abstract Task<MGuildMember> Update(MGuildMember member);
	public abstract Task Delete(Guid guildId, PublicSigningKey userId);
	public abstract Task Delete(Guid id);
	public abstract Task<bool> Exists(Guid guildId, PublicSigningKey userId);
	public abstract Task<bool> Exists(Guid id);
}
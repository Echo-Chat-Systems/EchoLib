using Database.Models;
using Models.Generic;

namespace Database.Repositories.Chat;

// ReSharper disable once ClassNeverInstantiated.Global
public record PagingToken(ulong LastSeen, Guid Channel);

public interface IMessagesRepo
{
	public Task<Snowflake> Add(DMessage message);
	public Task Update(DMessage message);
	public Task Delete(DMessage message);

	public Task<IEnumerable<DMessage>> GetMany(Guid channelId, PagingToken? pagingToken = null, Func<IEnumerable<DMessage>>? selector = null);
}
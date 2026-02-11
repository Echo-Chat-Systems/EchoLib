using Models.Crypto.Signing;
using Models.Functional.Crypto.Signing;

namespace Database.Repositories.Config;

public interface IOwnersRepo
{
	List<UserId> All();
	Task<List<UserId>> AllAsync(CancellationToken ct);

	bool IsOwner(UserId user);
	Task<bool> IsOwner(UserId user, CancellationToken ct);
}
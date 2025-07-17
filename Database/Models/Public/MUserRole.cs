using System.Data;
using Core.Auth.Signing;

namespace Database.Models.Public;

public class MUserRole(IDataRecord record) : BaseModel(record)
{
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("user_id")));
	public Guid RoleId { get; } = record.GetGuid(record.GetOrdinal("role_id"));
}
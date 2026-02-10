using System.Data;
using Models.Crypto.Signing;

namespace Database.Models;

public class MUserRole(IDataRecord record) : BaseModel(record)
{
	public UserId UserId { get; } = new(record.GetString(record.GetOrdinal("user_id")));
	public Guid RoleId { get; } = record.GetGuid(record.GetOrdinal("role_id"));
}
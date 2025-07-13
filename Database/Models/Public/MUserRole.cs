using System.Data;
using EchoLib.Auth.Signing;

namespace EchoLib.Database.Models.Public;

public class MUserRole(IDataRecord record) : BaseModel(record)
{
	public PublicSigningKey UserId { get; } = new((byte[])record.GetValue(record.GetOrdinal("user_id")));
	public Guid RoleId { get; } = record.GetGuid(record.GetOrdinal("role_id"));
}
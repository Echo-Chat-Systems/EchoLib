using System.Data;

namespace EchoLib.Database.Models.Secure;

public class CertificateRow(IDataRecord record) : BaseModel(record)
{
	public byte[] Signature { get; set; } = (byte[])record.GetValue(record.GetOrdinal("signature"));
	public DateTime Expires { get; set; } = record.GetDateTime(record.GetOrdinal("expires"));
	public bool Revoked { get; set; } = record.GetBoolean(record.GetOrdinal("revoked"));
}
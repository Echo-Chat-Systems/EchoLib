using EchoLib.Auth.Signing;

namespace EchoLib.Models.Users.Reputation;

public class Rep
{
	public int Points
	{
		get
		{
			return Remarks.Values.Sum(remark => (int)remark.Direction);
		}
	}

	public required Dictionary<Signature, Remark> Remarks { get; set; }
}
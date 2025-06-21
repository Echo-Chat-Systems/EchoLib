namespace EchoLib.Auth.Signing;

public class PrivateSigningKey
{
	private string _key { get; set; }

	public PrivateSigningKey(string key)
	{
		// TODO: Key perform key integrity checks
		_key = key;
	}
}
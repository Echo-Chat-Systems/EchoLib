using System.Diagnostics.CodeAnalysis;

namespace EchoLib.Core.Snowflake;

public struct Snowflake : IParsable<Snowflake>
{
	public ulong Value { get; }

	public DateTime Timestamp { get; }
	public byte ApiVersion { get; }
	public ulong Increment { get; }

	public Snowflake(ulong snowflake)
	{
		Value = snowflake;
		long timestamp = (long)(snowflake >> 20) + SnowflakeGenerator.Epoch;
		Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
		ApiVersion = (byte)((snowflake >> 16) & 0xF);
		Increment = snowflake & 0xFFFF;
	}

	public static Snowflake Parse(string s, IFormatProvider? provider)
	{
		return new Snowflake(ulong.Parse(s));
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Snowflake result)
	{
		result = default;

		if (!ulong.TryParse(s, out ulong value))
			return false;

		result = new Snowflake(value);

		return true;
	}
}
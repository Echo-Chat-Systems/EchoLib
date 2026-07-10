using System.Diagnostics.CodeAnalysis;

namespace EchoLib.Core.Snowflake;

public struct Snowflake : IParsable<Snowflake>
{
	public ulong Value { get; }

	public DateTime? Timestamp { get; private init; }

	public Type SnowflakeDataType { get; private init; }
	private readonly ISnowflakeData _snowflakeData;
	public int? SnowflakeVersion { get; private init; }

	public T GetSnowflakeData<T>()
		where T : class, ISnowflakeData
	{
		if (typeof(T) != SnowflakeDataType) throw new ArgumentException($"");

		return (T)_snowflakeData;
	}

	public Snowflake(ulong snowflake)
	{
		// Set the value
		Value = snowflake;

		/* Calculate the Snowflake version using bit-shift magic
		 *
		 * We start with a ulong that looks something like this in binary
		 *  0100 0100 0110 0011 1011 0011 0011 1000 0111 0100 1110 0000 0100 1010 0001 1000
		 *  │◀──────────────────────────────────────────────────────────────────────▶│ │◀▶│
		 *   We can't parse any of this ▲ until we have parsed this ───────────────────┘
		 *
		 * All we do to extract it, is wipe the snowflake version all the way to the left << 60
		 *  1000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000
		 *  │◀▶│ │◀──────────────────────────────All Zeros───────────────────────────────▶│
		 *
		 * Now that we just have what we need, we shift it right >> 60 to put the bits back in the correct significant digits
		 *  0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 1000
		 *  │◀──────────────────────────────────────────────────────────────────────▶│ │◀▶│
		 *
		 * This then gets converted into an integer, for this example, Snowflake version 8
		 */
		SnowflakeVersion = (int)((snowflake << 60) >> 60) + 1; // +1 is here as the snowflake in binary starts at 0 which is not useful.

		if (SnowflakeVersion > 16) throw new ArgumentException("Invalid snowflake. Snowflake Version Exceeds 16");

		switch (SnowflakeVersion)
		{
			case 1:
				/* Snowflake V1 works like this:
				 * 0100 0100 0110 0011 1011 0011 0011 1000 0111 0100 1110 0000 0100 1010 0001 0001
				 * │◀───────────────────Timestamp────────────────────▶││◀─┬─▶│ │◀─Sequence─▶│ │◀▶│
				 *                                         Instance Number┘  Snowflake Version┘
				 */
				SnowflakeDataType = typeof(SnowflakeV1);

				// Start by parsing the Timestamp by right shifting 22 bits
				Timestamp = DateTimeOffset.FromUnixTimeMilliseconds((long)(snowflake >> 22) + SnowflakeGenerator.Epoch).UtcDateTime;

				/* Calculate Instance
				 * We start with a ulong that looks something like this in binary
				 *  0100 0100 0110 0011 1011 0011 0011 1000 0111 0100 1110 1010 0100 1010 0001 1000
				 *  │◀───────────────────Timestamp────────────────────▶││◀─┬─▶│ │◀─┬─────────────▶│
				 *                   Instance Number (This is what we want)┘       └Sequence and Snowflake Version
				 *
				 * We left shift by 42 bits to remove the timestamp, which is the first chunk we don't need
				 *  1010 1001 0010 1000 0110 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000
				 *  │◀─┬─▶││◀──────────────┬─▶││◀──────────────────New Junk 0's──────────────────▶│
				 *     └ Instance Number   └ Sequence and Snowflake Version
				 *
				 * Now we shift back to the right 58 bits
				 *  0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0010 1010
				 *  │◀────────────────────────────New Junk 0's────────────────────────────▶││◀─┬─▶│
				 *                                                    Extracted Instance Number┘
				 */
				int instance = (int)((snowflake << 42) >> 58);
				if (instance > 63) throw new ArgumentException($"Instance Number {instance} exceeded theoretical maximum of 63 for Snowflake Version {SnowflakeVersion}");

				/* Calculate Sequence
				 * We start with a ulong that looks something like this in binary
				 *  0100 0100 0110 0011 1011 0011 0011 1000 0111 0100 1110 0000 0100 1010 0001 0001
				 *  │◀──────────────Timestamp & Instance Number──────────────▶│ │◀─Sequence─▶│ │◀▶│
				 *                                                            Snowflake Version┘
				 *
				 * We left shift by 48 bits to remove the timestamp and instance number
				 *  0100 1010 0001 0001 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000
				 *  │◀─Sequence─▶│ │◀▶│ │◀──────────────────────New Junk 0's─────────────────────▶│
				 *                 └Snowflake Version
				 *
				 * We right shift 52 bits
				 *  0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0100 1010 0001
				 *  │◀─────────────────────────New Junk 0's───────────────────────▶│ │◀─Sequence─▶│
				 */
				int sequence = (int)((snowflake << 48) >> 52);
				if (sequence > 4095) throw new ArgumentException($"Sequence Number {sequence} exceeded theoretical maximum of 4095 for Snowflake Version {SnowflakeVersion}");

				_snowflakeData = new SnowflakeV1(instance, sequence);
				break;
			default:
				throw new NotImplementedException($"Snowflake Version {SnowflakeVersion} not supported");
		}
	}

	public Snowflake(long snowflake) : this((ulong)snowflake)
	{
	}

	public interface ISnowflakeData;

	public class SnowflakeV1(int instance, int sequence) : ISnowflakeData
	{
		public int InstanceNumber { get; private init; } = instance;
		public int Sequence { get; private init; } = sequence;
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
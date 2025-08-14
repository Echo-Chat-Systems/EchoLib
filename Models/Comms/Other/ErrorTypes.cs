namespace Models.Comms.Other;

[Flags]
public enum ErrorTypes : int
{
	DeserializationError,
	InvalidParameters,
}
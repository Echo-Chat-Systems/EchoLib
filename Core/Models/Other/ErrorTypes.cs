namespace Core.Models.Other;

[Flags]
public enum ErrorTypes : int
{
	DeserializationError,
	InvalidParameters,
}
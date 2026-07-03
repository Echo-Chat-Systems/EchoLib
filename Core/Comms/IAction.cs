namespace Core.Comms;

public interface IAction<TParams>
{
	static abstract string Target { get; }
	static abstract string Action { get; }
}
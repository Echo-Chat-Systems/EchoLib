using System.Data.Common;

namespace Database;

public class DbException : Exception
{
	private readonly DbCommand _command;

	public DbException(DbCommand command) : base($"Command failed to execute: {command.CommandText}")
	{
		_command = command;
	}

	public DbException(DbCommand command, string message) : base(
		$"Command failed to execute: {command.CommandText} - {message}")
	{
		_command = command;
	}
}

public class InsertFailedException : DbException
{
	public InsertFailedException(DbCommand command, string message) : base(command, message)
	{
	}

	public InsertFailedException(DbCommand command) : base(command)
	{
	}
}

public class UpdateFailedException : DbException
{
	public UpdateFailedException(DbCommand command, string message) : base(command, message)
	{
	}

	public UpdateFailedException(DbCommand command) : base(command)
	{
	}
}

public class DeleteFailedException : DbException
{
	public DeleteFailedException(DbCommand command, string message) : base(command, message)
	{
	}

	public DeleteFailedException(DbCommand command) : base(command)
	{
	}
}

public class NotFoundException : DbException
{
	public NotFoundException(DbCommand command, string message) : base(command, message)
	{
	}

	public NotFoundException(DbCommand command) : base(command)
	{
	}
}
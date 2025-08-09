using System.Data;
using System.Data.Common;
using Database.Handlers.Interface;
using Database.Models.Media;

namespace Database.Handlers.Defaults.Media;

public class FilesHandler : BaseHandler, IFilesHandler
{
	public async Task<MFile> Create(string owner)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "INSERT INTO media.files VALUES (@owner) RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@owner", new Parameter { Type = DbType.String, Value = owner } }
		});

		// Execute command
		return await RunModify(command, reader => new MFile(reader));
	}

	public async Task<MFile?> Get(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT * FROM media.files WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunGet(command, reader => new MFile(reader));
	}

	public async Task<MFile> Update(MFile file)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "UPDATE media.files SET last_accessed = @last_accessed WHERE id = @id RETURNING *";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = file.Id } },
			{ "@last_accessed", new Parameter { Type = DbType.DateTime, Value = file.LastAccessed } }
		});

		// Execute command
		return await RunModify(command, reader => new MFile(reader));
	}

	public async Task Delete(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(true);
		command.CommandText = "DELETE FROM media.files WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		await RunDelete(command);
	}

	public async Task<bool> Exists(Guid id)
	{
		// Create command
		await using DbCommand command = await Command(false);
		command.CommandText = "SELECT id FROM media.files WHERE id = @id";

		// Create parameters
		AddParams(command, new Dictionary<string, Parameter>
		{
			{ "@id", new Parameter { Type = DbType.Guid, Value = id } }
		});

		// Execute command
		return await RunExists(command);
	}
}
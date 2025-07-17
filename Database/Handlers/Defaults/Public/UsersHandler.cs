using System.Data;
using System.Data.Common;
using Database.Models.Public;

namespace Database.Handlers.Defaults.Public;

public abstract class UsersHandler : BaseHandler
{
    public async Task<MUser> Create(string sk, string ek, string username, int tag, MUser.MProfile profile)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText = "INSERT INTO public.users (id, encryption_key, username, tag, profile) VALUES (@sk, @ek, @username, @tag, @profile) RETURNING *";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@sk", new Parameter { Type = DbType.String, Value = sk } },
            { "@ek", new Parameter { Type = DbType.String, Value = ek } },
            { "@username", new Parameter { Type = DbType.String, Value = username } },
            { "@tag", new Parameter { Type = DbType.Int32, Value = tag } },
            { "@profile", new Parameter { Type = DbType.String, Value = profile.ToString() } }
        });

        // Execute command
        return await RunModify(command, reader => new MUser(reader));
    }

    public async Task<MUser?> Get(string sk)
    {
        // Create command
        await using DbCommand command = await Command(false);
        command.CommandText = "SELECT * FROM public.users WHERE id = @sk";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@sk", new Parameter { Type = DbType.String, Value = sk } }
        });

        // Execute command
        return await RunGet(command, reader => new MUser(reader));
    }

    public async Task<MUser> Update(MUser user)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText = "UPDATE public.users SET encryption_key = @ek, username = @username, tag = @tag, profile = @profile WHERE id = @sk";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@sk", new Parameter { Type = DbType.String, Value = user.Id } },
            { "@ek", new Parameter { Type = DbType.String, Value = user.EncryptionKey } },
            { "@username", new Parameter { Type = DbType.String, Value = user.Username } },
            { "@tag", new Parameter { Type = DbType.Int32, Value = user.Tag } },
            { "@profile", new Parameter { Type = DbType.String, Value = user.ProfileRaw } }
        });

        // Execute command
        return await RunModify(command, reader => new MUser(reader));
    }

    public async Task Delete(string sk)
    {
        // Create command
        await using DbCommand command = await Command(true);
        command.CommandText = "DELETE FROM public.users WHERE id = @sk";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@sk", new Parameter { Type = DbType.String, Value = sk } }
        });

        // Execute command
        await RunDelete(command);
    }

    public async Task<bool> Exists(string sk)
    {
        // Create command
        await using DbCommand command = await Command(false);
        command.CommandText = "SELECT id FROM public.users WHERE id = @sk";

        // Create parameters
        AddParams(command, new Dictionary<string, Parameter>
        {
            { "@sk", new Parameter { Type = DbType.String, Value = sk } }
        });

        // Execute command
        return await RunExists(command);
    }
}
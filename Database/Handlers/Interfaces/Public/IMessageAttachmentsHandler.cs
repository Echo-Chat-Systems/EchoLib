using EchoLib.Database.Models.Public;

namespace EchoLib.Database.Handlers.Interfaces.Public;

public interface IMessageAttachmentsHandler
{
	MessageAttachmentRow Create(Guid messageId, Guid fileId);
	MessageAttachmentRow? Get(Guid messageId, Guid fileId);
	MessageAttachmentRow? Get(Guid id);
	List<MessageAttachmentRow> GetMany(MessageRow message);
	List<MessageAttachmentRow> GetMany(FileRow file);
	void Delete(Guid messageId, Guid fileId);
	void Delete(Guid id);
	bool Exists(Guid messageId, Guid fileId);
	bool Exists(Guid id);
}
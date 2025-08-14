using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Core.Comms;
using Core.Processes.Actions.Error;
using Core.Processes.Params.Error;
using Models.Comms.Other;

namespace Core.Helpers;

public static class WebSocketTranslator
{
	/// <summary>
	/// QOL Helper method for sending actions easily.
	/// </summary>
	/// <param name="socket">Target socket connection.</param>
	/// <param name="parameters">Parameters of the action</param>
	/// <typeparam name="TAction">Action Type</typeparam>
	/// <typeparam name="TParams">Action Parameters Type</typeparam>
	public static async Task SendAction<TAction, TParams>(WebSocket socket, TParams parameters)
	where TAction : IAction<TParams>, new()
	{
		// Create a message envelope for the action
		MessageEnvelope<TParams> message = new()
		{
			Target = TAction.Target,
			Data = new ActionWrapper<TParams>
			{
				Action = TAction.Action,
				Params = parameters
			}
		};

		// TODO: Check if this fails when emoji or scripts from other languages are used in fields contained within parameters
		byte[] json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

		await socket.SendAsync(json, WebSocketMessageType.Text, true, CancellationToken.None);
	}

	public static async Task SendError<TParams>(WebSocket socket, ErrorTypes errorType, MessageEnvelope<TParams> miq, string? errorMessage = null)
	{
		await SendAction<ErrorAction, ErrorParams>(socket, new ErrorParams
		{
			Message = errorMessage,
			Type = (int)errorType,
			MessageInQuestion = JsonSerializer.Serialize(miq, StaticOptions.JsonSerialzer)
		});
	}
}
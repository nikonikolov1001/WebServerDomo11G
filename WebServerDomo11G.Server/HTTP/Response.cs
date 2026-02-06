using System;
using WebServerDomo11G.Server.HTTP;

public class Response
{
	public Response(StatusCode statusCode)
	{
	   StatusCode = statusCode;
	   Headers.Add("Server", "My web Server");
	   Headers.Add("Date", $"{DateTime.UtcNow :r}");
	}

	public StatusCode StatusCode {  get; set; }

	public HeaderCollection Headers { get; set; } = new HeaderCollection();

	public string body { get; set; }
}

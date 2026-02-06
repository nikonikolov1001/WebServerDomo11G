using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServerDomo11G.Server
{
	public class HttpServer
	{
		private IPAddress ipAddress;
		private int port;
		private TcpListener serverListener;

		public HttpServer(string ipAddress, int port)
		{
			this.port = port;
			this.ipAddress = IPAddress.Parse(ipAddress);
			serverListener = new TcpListener(this.ipAddress,this.port);
		}

		public void Start()
		{
			serverListener.Start();

			Console.WriteLine($"Server started on port {port}");
			Console.WriteLine("Listening for requests...");
			while (true)
			{
				var connection = serverListener.AcceptTcpClient();
				var networkStream = connection.GetStream();
				var requestString = ReadRequest(networkStream);
				//WriteResponse(networkStream, "Hello from the server");
				//connection.Close();

			}

		}

		private void WriteResponse(NetworkStream networkStream,string message)
		{
			var content = message;
			var contentLength = Encoding.UTF8.GetByteCount(content);

			var response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {contentLength}
MyHeader:MyValue

{content}";

			byte[] responseBytes = Encoding.UTF8.GetBytes(response);
			networkStream.Write(responseBytes);
		}

		private string ReadRequest(NetworkStream networkStream)
		{
			var bufferLength = 1024;
			var buffer = new byte[bufferLength];

			var totalbytes = 0;
			var requestBuilder = new StringBuilder();

			do
			{
				var bytesRead = networkStream.Read(buffer, 0, bufferLength);
				totalbytes += bytesRead;	
				if (totalbytes > 10 * bufferLength)
				{
					throw new InvalidOperationException("The request is too long");
				}
				requestBuilder.Append(Encoding.UTF8.GetString(buffer));
			}
			while (networkStream.DataAvailable);
			return requestBuilder.ToString();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			const String host = "localhost";
			const Int32 port = 10041;

			try
			{
				Console.WriteLine("start");

				Spica.Data.Groonga.GroongaContext context = new Spica.Data.Groonga.GroongaContext();
				Console.WriteLine("create context");

				context.Connect(host, port);
				Console.WriteLine("connect");

				Console.WriteLine();

				for (int i = 0; i < 10; ++i)
				{
					Console.WriteLine("try: {0}", i);

					Stopwatch sw = Stopwatch.StartNew();
					String sendData = "status";
					context.Send(sendData);
					Console.WriteLine("send: {0}", sendData);

					String recvData = context.Recv();
					Console.WriteLine("recv: {0}", recvData);

					Console.WriteLine("elapsed: {0}", sw.ElapsedMilliseconds);
					Console.WriteLine();

					Thread.Sleep(1000);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("error");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}


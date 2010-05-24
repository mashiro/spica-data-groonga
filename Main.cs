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
			try
			{
				Console.WriteLine("start");

				Spica.Data.Groonga.GroongaContext context = new Spica.Data.Groonga.GroongaContext();
				Console.WriteLine("create context");

				context.Connect("localhost", 10041);
				Console.WriteLine("connect");

				for (int i = 0; i < 10; ++i)
				{
					Stopwatch sw = Stopwatch.StartNew();
					context.Send("status");
					Console.WriteLine("send");

					String str = context.Recv();
					Console.WriteLine("recv");
					Console.WriteLine(str);

					Console.WriteLine(sw.ElapsedMilliseconds);
					Thread.Sleep(1000);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("failed");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}


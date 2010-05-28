using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using System.Xml;
using System.Xml.Linq;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using Spica.Data.Groonga;

namespace Test
{
	class Program
	{
		public static IEnumerable<String> EnumerateInputs()
		{
			while (true)
			{
				Console.Write("> ");
				var input = Console.ReadLine();
				if (input == null) break;
				yield return input;
			}
		}

		public static XElement JsonToXElement(String json)
		{
			using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(json), XmlDictionaryReaderQuotas.Max))
				return XElement.Load(jsonReader);
		}

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("program host port");
				return;
			}

			try
			{
				var host = args[0];
				var port = Int32.Parse(args[1]);

				var context = new GroongaContext();
				context.Connect(host, port);

				foreach (var input in EnumerateInputs())
				{
					context.Send(input);
					var recvData = context.Recv();
					if (!String.IsNullOrEmpty(recvData))
						Console.WriteLine(recvData);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}


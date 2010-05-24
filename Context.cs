using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Spica.Data.Groonga
{
	public class GroongaException : Exception
	{
		public GroongaResultCode Result { get; private set; }
		public GroongaException(GroongaResultCode result)
		{
			Result = result;
		}
	}

	public class GroongaContext : IDisposable
	{
		private static Initializer _initializer = null;
		private class Initializer
		{
			public void Init() { GroongaApi.grn_init(); }
			public void Fin() { GroongaApi.grn_fin(); }
			~Initializer() { Fin(); }
		}

		static GroongaContext()
		{
			_initializer = new Initializer();
			_initializer.Init();
		}

		private GroongaApi.grn_ctx _context;
		private Boolean _disposed = false;

		public GroongaContext()
			: this(GroongaContextFlags.None)
		{
		}

		public GroongaContext(GroongaContextFlags flags)
		{
			GroongaResultCode result = GroongaApi.grn_ctx_init(out _context, flags);
			if (result != GroongaResultCode.Success)
				throw new GroongaException(result);
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				GroongaApi.grn_ctx_fin(ref _context);
				_disposed = true;
			}
		}

		public GroongaResultCode Connect(String host, Int32 port)
		{
			return GroongaApi.grn_ctx_connect(ref _context, host, port, 0);
		}

		public void Send(String str)
		{
			Send(str, 0);
		}

		public void Send(String str, Int32 flags)
		{
			GroongaApi.grn_ctx_send(ref _context, str, (UInt32)str.Length, flags);
			if (_context.rc != GroongaResultCode.Success)
				throw new GroongaException(_context.rc);
		}

		public String Recv()
		{
			StringBuilder sb = new StringBuilder();
			IntPtr str;
			UInt32 str_len;
			Int32 flags;

			do {
				GroongaApi.grn_ctx_recv(ref _context, out str, out str_len, out flags);
				if (_context.rc != GroongaResultCode.Success)
					throw new GroongaException(_context.rc);
				sb.Append(Marshal.PtrToStringAnsi(str));
			} while ((flags & GroongaApi.GRN_CTX_MORE) != 0);

			return sb.ToString();
		}
	}
}

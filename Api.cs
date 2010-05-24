using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Spica.Data.Groonga
{
	internal static class GroongaApi
	{
		public const String DllName = "groonga";

		public const Int32 GRN_CTX_MSGSIZE = 0x80;
		public const Int32 GRN_CTX_FIN     = 0xff;

		public const Int32 GRN_CTX_MORE  = (0x01<<0);
		public const Int32 GRN_CTX_TAIL  = (0x01<<1);
		public const Int32 GRN_CTX_HEAD  = (0x01<<2);
		public const Int32 GRN_CTX_QUIET = (0x01<<3);
		public const Int32 GRN_CTX_QUIT  = (0x01<<4);

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
		public unsafe struct grn_user_data
		{
			[FieldOffset(0)]
			public Int32 int_value;

			[FieldOffset(0)]
			public UInt32 id;

			[FieldOffset(0)]
			public void* ptr;
		}

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
		public unsafe struct grn_ctx
		{

			[FieldOffset(0)]
			public GroongaResultCode rc;

			[FieldOffset(4)]
			public Int32 flags;

			[FieldOffset(8)]
			public GroongaEncoding encoding;

			[FieldOffset(12)]
			public Byte ntrace;

			[FieldOffset(13)]
			public Byte errlvl;

			[FieldOffset(14)]
			public Byte stat;

			[FieldOffset(15)]
			public UInt32 seqno;

			[FieldOffset(19)]
			public UInt32 subno;

			[FieldOffset(23)]
			public UInt32 seqno2;

			[FieldOffset(27)]
			public UInt32 errline;

			[FieldOffset(31)]
			public grn_user_data user_data;

			[FieldOffset(35)]
			public grn_ctx* prev;

			[FieldOffset(39)]
			public grn_ctx* next;

			[FieldOffset(43)]
			public SByte* errfile;

			[FieldOffset(47)]
			public SByte* errfunc;

			[FieldOffset(51)]
			public void* impl;

			[FieldOffset(55)]
			public fixed int trace[16];

			[FieldOffset(119)]
			public fixed SByte errbuf[GRN_CTX_MSGSIZE];
		}

		[DllImport(DllName)]
		public static extern GroongaResultCode grn_init();

		[DllImport(DllName)]
		public static extern GroongaResultCode grn_fin();

		[DllImport(DllName)]
		public static extern GroongaResultCode grn_ctx_init(out grn_ctx ctx, GroongaContextFlags flags);

		[DllImport(DllName)]
		public static extern GroongaResultCode grn_ctx_fin(ref grn_ctx ctx);

		[DllImport(DllName, CharSet = CharSet.Ansi)]
		public static extern GroongaResultCode grn_ctx_connect(ref grn_ctx ctx, String host, Int32 port, Int32 flags);

		[DllImport(DllName, CharSet = CharSet.Ansi)]
		public static extern UInt32 grn_ctx_send(ref grn_ctx ctx, String str, UInt32 str_len, Int32 flags);

		[DllImport(DllName, CharSet = CharSet.Ansi)]
		public static extern UInt32 grn_ctx_recv(ref grn_ctx ctx, out IntPtr str, out UInt32 str_len, out Int32 flags);
	}
}


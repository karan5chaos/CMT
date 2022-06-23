using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Outlook;

[ComImport]
[TypeIdentifier]
[Guid("00063034-0000-0000-C000-000000000046")]
[CompilerGenerated]
public interface _MailItem
{
	Attachments Attachments
	{
		[DispId(63509)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	string Subject
	{
		[DispId(55)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(55)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	string CC
	{
		[DispId(3587)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(3587)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	string HTMLBody
	{
		[DispId(62468)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(62468)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	Recipients Recipients
	{
		[DispId(63508)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	string To
	{
		[DispId(3588)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[DispId(3588)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap1_5();

	void _VtblGap2_30();

	void _VtblGap3_6();

	[DispId(61606)]
	void Display([Optional][In][MarshalAs(UnmanagedType.Struct)] object Modal);

	void _VtblGap4_10();

	void _VtblGap5_12();

	void _VtblGap6_11();

	void _VtblGap7_22();
}

using System;
using System.Runtime.InteropServices;

namespace Blue.Private.Win32Imports;

public class Win32
{
	public class VK
	{
		public const int VK_SHIFT = 16;

		public const int VK_CONTROL = 17;

		public const int VK_MENU = 18;

		public const int VK_ESCAPE = 27;

		public static bool IsKeyPressed(int KeyCode)
		{
			return (GetAsyncKeyState(KeyCode) & 0x800) == 0;
		}
	}

	public class WM
	{
		public const int WM_MOUSEMOVE = 512;

		public const int WM_NCMOUSEMOVE = 160;

		public const int WM_NCLBUTTONDOWN = 161;

		public const int WM_NCLBUTTONUP = 162;

		public const int WM_NCLBUTTONDBLCLK = 163;

		public const int WM_LBUTTONDOWN = 513;

		public const int WM_LBUTTONUP = 514;

		public const int WM_KEYDOWN = 256;
	}

	public class HT
	{
		public const int HTERROR = -2;

		public const int HTTRANSPARENT = -1;

		public const int HTNOWHERE = 0;

		public const int HTCLIENT = 1;

		public const int HTCAPTION = 2;

		public const int HTSYSMENU = 3;

		public const int HTGROWBOX = 4;

		public const int HTSIZE = 4;

		public const int HTMENU = 5;

		public const int HTHSCROLL = 6;

		public const int HTVSCROLL = 7;

		public const int HTMINBUTTON = 8;

		public const int HTMAXBUTTON = 9;

		public const int HTLEFT = 10;

		public const int HTRIGHT = 11;

		public const int HTTOP = 12;

		public const int HTTOPLEFT = 13;

		public const int HTTOPRIGHT = 14;

		public const int HTBOTTOM = 15;

		public const int HTBOTTOMLEFT = 16;

		public const int HTBOTTOMRIGHT = 17;

		public const int HTBORDER = 18;

		public const int HTREDUCE = 8;

		public const int HTZOOM = 9;

		public const int HTSIZEFIRST = 10;

		public const int HTSIZELAST = 17;

		public const int HTOBJECT = 19;

		public const int HTCLOSE = 20;

		public const int HTHELP = 21;
	}

	public class Bit
	{
		public static int HiWord(int iValue)
		{
			return (iValue >> 16) & 0xFFFF;
		}

		public static int LoWord(int iValue)
		{
			return iValue & 0xFFFF;
		}
	}

	[DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern short GetAsyncKeyState(int vKey);

	[DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern IntPtr GetDesktopWindow();
}

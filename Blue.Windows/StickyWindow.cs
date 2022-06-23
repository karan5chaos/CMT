using System;
using System.Collections;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Blue.Private.Win32Imports;

namespace Blue.Windows;

public class StickyWindow : NativeWindow
{
	private enum ResizeDir
	{
		Top = 2,
		Bottom = 4,
		Left = 8,
		Right = 0x10
	}

	private delegate bool ProcessMessage(ref Message m);

	private static ArrayList GlobalStickyWindows = new ArrayList();

	private ProcessMessage MessageProcessor;

	private ProcessMessage DefaultMessageProcessor;

	private ProcessMessage MoveMessageProcessor;

	private ProcessMessage ResizeMessageProcessor;

	private bool movingForm;

	private Point formOffsetPoint;

	private Point offsetPoint;

	private bool resizingForm;

	private ResizeDir resizeDirection;

	private Rectangle formOffsetRect;

	private Point mousePoint;

	private Form originalForm;

	private Rectangle formRect;

	private Rectangle formOriginalRect;

	private static int stickGap = 20;

	private bool stickOnResize;

	private bool stickOnMove;

	private bool stickToScreen;

	private bool stickToOther;

	public int StickGap
	{
		get
		{
			return stickGap;
		}
		set
		{
			stickGap = value;
		}
	}

	public bool StickOnResize
	{
		get
		{
			return stickOnResize;
		}
		set
		{
			stickOnResize = value;
		}
	}

	public bool StickOnMove
	{
		get
		{
			return stickOnMove;
		}
		set
		{
			stickOnMove = value;
		}
	}

	public bool StickToScreen
	{
		get
		{
			return stickToScreen;
		}
		set
		{
			stickToScreen = value;
		}
	}

	public bool StickToOther
	{
		get
		{
			return stickToOther;
		}
		set
		{
			stickToOther = value;
		}
	}

	public static void RegisterExternalReferenceForm(Form frmExternal)
	{
		GlobalStickyWindows.Add(frmExternal);
	}

	public static void UnregisterExternalReferenceForm(Form frmExternal)
	{
		GlobalStickyWindows.Remove(frmExternal);
	}

	public StickyWindow(Form form)
	{
		resizingForm = false;
		movingForm = false;
		originalForm = form;
		formRect = Rectangle.Empty;
		formOffsetRect = Rectangle.Empty;
		formOffsetPoint = Point.Empty;
		offsetPoint = Point.Empty;
		mousePoint = Point.Empty;
		stickOnMove = true;
		stickOnResize = true;
		stickToScreen = true;
		stickToOther = true;
		DefaultMessageProcessor = DefaultMsgProcessor;
		MoveMessageProcessor = MoveMsgProcessor;
		ResizeMessageProcessor = ResizeMsgProcessor;
		MessageProcessor = DefaultMessageProcessor;
		AssignHandle(originalForm.Handle);
	}

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	protected override void OnHandleChange()
	{
		if ((int)base.Handle != 0)
		{
			GlobalStickyWindows.Add(originalForm);
		}
		else
		{
			GlobalStickyWindows.Remove(originalForm);
		}
	}

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	protected override void WndProc(ref Message m)
	{
		if (!MessageProcessor(ref m))
		{
			base.WndProc(ref m);
		}
	}

	private bool DefaultMsgProcessor(ref Message m)
	{
		int msg = m.Msg;
		if (msg == 161)
		{
			originalForm.Activate();
			mousePoint.X = (short)Win32.Bit.LoWord((int)m.LParam);
			mousePoint.Y = (short)Win32.Bit.HiWord((int)m.LParam);
			if (OnNCLButtonDown((int)m.WParam, mousePoint))
			{
				m.Result = (IntPtr)((resizingForm || movingForm) ? 1 : 0);
				return true;
			}
		}
		return false;
	}

	private bool OnNCLButtonDown(int iHitTest, Point point)
	{
		Rectangle bounds = originalForm.Bounds;
		offsetPoint = point;
		switch (iHitTest)
		{
		case 2:
			if (stickOnMove)
			{
				offsetPoint.Offset(-bounds.Left, -bounds.Top);
				StartMove();
				return true;
			}
			return false;
		case 13:
			return StartResize((ResizeDir)10);
		case 12:
			return StartResize(ResizeDir.Top);
		case 14:
			return StartResize((ResizeDir)18);
		case 11:
			return StartResize(ResizeDir.Right);
		case 17:
			return StartResize((ResizeDir)20);
		case 15:
			return StartResize(ResizeDir.Bottom);
		case 16:
			return StartResize((ResizeDir)12);
		case 10:
			return StartResize(ResizeDir.Left);
		default:
			return false;
		}
	}

	private bool StartResize(ResizeDir resDir)
	{
		if (stickOnResize)
		{
			resizeDirection = resDir;
			formRect = originalForm.Bounds;
			formOriginalRect = originalForm.Bounds;
			if (!originalForm.Capture)
			{
				originalForm.Capture = true;
			}
			MessageProcessor = ResizeMessageProcessor;
			return true;
		}
		return false;
	}

	private bool ResizeMsgProcessor(ref Message m)
	{
		if (!originalForm.Capture)
		{
			Cancel();
			return false;
		}
		switch (m.Msg)
		{
		case 514:
			EndResize();
			break;
		case 512:
			mousePoint.X = (short)Win32.Bit.LoWord((int)m.LParam);
			mousePoint.Y = (short)Win32.Bit.HiWord((int)m.LParam);
			Resize(mousePoint);
			break;
		case 256:
			if ((int)m.WParam == 27)
			{
				originalForm.Bounds = formOriginalRect;
				Cancel();
			}
			break;
		}
		return false;
	}

	private void EndResize()
	{
		Cancel();
	}

	private void Resize(Point p)
	{
		p = originalForm.PointToScreen(p);
		Screen screen = Screen.FromPoint(p);
		formRect = originalForm.Bounds;
		int right = formRect.Right;
		int bottom = formRect.Bottom;
		if ((resizeDirection & ResizeDir.Left) == ResizeDir.Left)
		{
			formRect.Width = formRect.X - p.X + formRect.Width;
			formRect.X = right - formRect.Width;
		}
		if ((resizeDirection & ResizeDir.Right) == ResizeDir.Right)
		{
			formRect.Width = p.X - formRect.Left;
		}
		if ((resizeDirection & ResizeDir.Top) == ResizeDir.Top)
		{
			formRect.Height = formRect.Height - p.Y + formRect.Top;
			formRect.Y = bottom - formRect.Height;
		}
		if ((resizeDirection & ResizeDir.Bottom) == ResizeDir.Bottom)
		{
			formRect.Height = p.Y - formRect.Top;
		}
		formOffsetRect.X = stickGap + 1;
		formOffsetRect.Y = stickGap + 1;
		formOffsetRect.Height = 0;
		formOffsetRect.Width = 0;
		if (stickToScreen)
		{
			Resize_Stick(screen.WorkingArea, bInsideStick: false);
		}
		if (stickToOther)
		{
			foreach (Form globalStickyWindow in GlobalStickyWindows)
			{
				if (globalStickyWindow != originalForm)
				{
					Resize_Stick(globalStickyWindow.Bounds, bInsideStick: true);
				}
			}
		}
		if (formOffsetRect.X == stickGap + 1)
		{
			formOffsetRect.X = 0;
		}
		if (formOffsetRect.Width == stickGap + 1)
		{
			formOffsetRect.Width = 0;
		}
		if (formOffsetRect.Y == stickGap + 1)
		{
			formOffsetRect.Y = 0;
		}
		if (formOffsetRect.Height == stickGap + 1)
		{
			formOffsetRect.Height = 0;
		}
		if ((resizeDirection & ResizeDir.Left) == ResizeDir.Left)
		{
			int val = formRect.Width + formOffsetRect.Width + formOffsetRect.X;
			if (originalForm.MaximumSize.Width != 0)
			{
				val = Math.Min(val, originalForm.MaximumSize.Width);
			}
			val = Math.Min(val, SystemInformation.MaxWindowTrackSize.Width);
			val = Math.Max(val, originalForm.MinimumSize.Width);
			val = Math.Max(val, SystemInformation.MinWindowTrackSize.Width);
			formRect.X = right - val;
			formRect.Width = val;
		}
		else
		{
			formRect.Width += formOffsetRect.Width + formOffsetRect.X;
		}
		if ((resizeDirection & ResizeDir.Top) == ResizeDir.Top)
		{
			int val2 = formRect.Height + formOffsetRect.Height + formOffsetRect.Y;
			if (originalForm.MaximumSize.Height != 0)
			{
				val2 = Math.Min(val2, originalForm.MaximumSize.Height);
			}
			val2 = Math.Min(val2, SystemInformation.MaxWindowTrackSize.Height);
			val2 = Math.Max(val2, originalForm.MinimumSize.Height);
			val2 = Math.Max(val2, SystemInformation.MinWindowTrackSize.Height);
			formRect.Y = bottom - val2;
			formRect.Height = val2;
		}
		else
		{
			formRect.Height += formOffsetRect.Height + formOffsetRect.Y;
		}
		originalForm.Bounds = formRect;
	}

	private void Resize_Stick(Rectangle toRect, bool bInsideStick)
	{
		if (formRect.Right >= toRect.Left - stickGap && formRect.Left <= toRect.Right + stickGap)
		{
			if ((resizeDirection & ResizeDir.Top) == ResizeDir.Top)
			{
				if (Math.Abs(formRect.Top - toRect.Bottom) <= Math.Abs(formOffsetRect.Top) && bInsideStick)
				{
					formOffsetRect.Y = formRect.Top - toRect.Bottom;
				}
				else if (Math.Abs(formRect.Top - toRect.Top) <= Math.Abs(formOffsetRect.Top))
				{
					formOffsetRect.Y = formRect.Top - toRect.Top;
				}
			}
			if ((resizeDirection & ResizeDir.Bottom) == ResizeDir.Bottom)
			{
				if (Math.Abs(formRect.Bottom - toRect.Top) <= Math.Abs(formOffsetRect.Bottom) && bInsideStick)
				{
					formOffsetRect.Height = toRect.Top - formRect.Bottom;
				}
				else if (Math.Abs(formRect.Bottom - toRect.Bottom) <= Math.Abs(formOffsetRect.Bottom))
				{
					formOffsetRect.Height = toRect.Bottom - formRect.Bottom;
				}
			}
		}
		if (formRect.Bottom < toRect.Top - stickGap || formRect.Top > toRect.Bottom + stickGap)
		{
			return;
		}
		if ((resizeDirection & ResizeDir.Right) == ResizeDir.Right)
		{
			if (Math.Abs(formRect.Right - toRect.Left) <= Math.Abs(formOffsetRect.Right) && bInsideStick)
			{
				formOffsetRect.Width = toRect.Left - formRect.Right;
			}
			else if (Math.Abs(formRect.Right - toRect.Right) <= Math.Abs(formOffsetRect.Right))
			{
				formOffsetRect.Width = toRect.Right - formRect.Right;
			}
		}
		if ((resizeDirection & ResizeDir.Left) == ResizeDir.Left)
		{
			if (Math.Abs(formRect.Left - toRect.Right) <= Math.Abs(formOffsetRect.Left) && bInsideStick)
			{
				formOffsetRect.X = formRect.Left - toRect.Right;
			}
			else if (Math.Abs(formRect.Left - toRect.Left) <= Math.Abs(formOffsetRect.Left))
			{
				formOffsetRect.X = formRect.Left - toRect.Left;
			}
		}
	}

	private void StartMove()
	{
		formRect = originalForm.Bounds;
		formOriginalRect = originalForm.Bounds;
		if (!originalForm.Capture)
		{
			originalForm.Capture = true;
		}
		MessageProcessor = MoveMessageProcessor;
	}

	private bool MoveMsgProcessor(ref Message m)
	{
		if (!originalForm.Capture)
		{
			Cancel();
			return false;
		}
		switch (m.Msg)
		{
		case 514:
			EndMove();
			break;
		case 512:
			mousePoint.X = (short)Win32.Bit.LoWord((int)m.LParam);
			mousePoint.Y = (short)Win32.Bit.HiWord((int)m.LParam);
			Move(mousePoint);
			break;
		case 256:
			if ((int)m.WParam == 27)
			{
				originalForm.Bounds = formOriginalRect;
				Cancel();
			}
			break;
		}
		return false;
	}

	private void EndMove()
	{
		Cancel();
	}

	private void Move(Point p)
	{
		p = originalForm.PointToScreen(p);
		Screen screen = Screen.FromPoint(p);
		if (!screen.WorkingArea.Contains(p))
		{
			p.X = NormalizeInside(p.X, screen.WorkingArea.Left, screen.WorkingArea.Right);
			p.Y = NormalizeInside(p.Y, screen.WorkingArea.Top, screen.WorkingArea.Bottom);
		}
		p.Offset(-offsetPoint.X, -offsetPoint.Y);
		formRect.Location = p;
		formOffsetPoint.X = stickGap + 1;
		formOffsetPoint.Y = stickGap + 1;
		if (stickToScreen)
		{
			Move_Stick(screen.WorkingArea, bInsideStick: false);
		}
		if (stickToOther)
		{
			foreach (Form globalStickyWindow in GlobalStickyWindows)
			{
				if (globalStickyWindow != originalForm)
				{
					Move_Stick(globalStickyWindow.Bounds, bInsideStick: true);
				}
			}
		}
		if (formOffsetPoint.X == stickGap + 1)
		{
			formOffsetPoint.X = 0;
		}
		if (formOffsetPoint.Y == stickGap + 1)
		{
			formOffsetPoint.Y = 0;
		}
		formRect.Offset(formOffsetPoint);
		originalForm.Bounds = formRect;
	}

	private void Move_Stick(Rectangle toRect, bool bInsideStick)
	{
		if (formRect.Bottom >= toRect.Top - stickGap && formRect.Top <= toRect.Bottom + stickGap)
		{
			if (bInsideStick)
			{
				if (Math.Abs(formRect.Left - toRect.Right) <= Math.Abs(formOffsetPoint.X))
				{
					formOffsetPoint.X = toRect.Right - formRect.Left;
				}
				if (Math.Abs(formRect.Left + formRect.Width - toRect.Left) <= Math.Abs(formOffsetPoint.X))
				{
					formOffsetPoint.X = toRect.Left - formRect.Width - formRect.Left;
				}
			}
			if (Math.Abs(formRect.Left - toRect.Left) <= Math.Abs(formOffsetPoint.X))
			{
				formOffsetPoint.X = toRect.Left - formRect.Left;
			}
			if (Math.Abs(formRect.Left + formRect.Width - toRect.Left - toRect.Width) <= Math.Abs(formOffsetPoint.X))
			{
				formOffsetPoint.X = toRect.Left + toRect.Width - formRect.Width - formRect.Left;
			}
		}
		if (formRect.Right < toRect.Left - stickGap || formRect.Left > toRect.Right + stickGap)
		{
			return;
		}
		if (bInsideStick)
		{
			if (Math.Abs(formRect.Top - toRect.Bottom) <= Math.Abs(formOffsetPoint.Y) && bInsideStick)
			{
				formOffsetPoint.Y = toRect.Bottom - formRect.Top;
			}
			if (Math.Abs(formRect.Top + formRect.Height - toRect.Top) <= Math.Abs(formOffsetPoint.Y) && bInsideStick)
			{
				formOffsetPoint.Y = toRect.Top - formRect.Height - formRect.Top;
			}
		}
		if (Math.Abs(formRect.Top - toRect.Top) <= Math.Abs(formOffsetPoint.Y))
		{
			formOffsetPoint.Y = toRect.Top - formRect.Top;
		}
		if (Math.Abs(formRect.Top + formRect.Height - toRect.Top - toRect.Height) <= Math.Abs(formOffsetPoint.Y))
		{
			formOffsetPoint.Y = toRect.Top + toRect.Height - formRect.Height - formRect.Top;
		}
	}

	private int NormalizeInside(int iP1, int iM1, int iM2)
	{
		if (iP1 <= iM1)
		{
			return iM1;
		}
		if (iP1 >= iM2)
		{
			return iM2;
		}
		return iP1;
	}

	private void Cancel()
	{
		originalForm.Capture = false;
		movingForm = false;
		resizingForm = false;
		MessageProcessor = DefaultMessageProcessor;
	}
}

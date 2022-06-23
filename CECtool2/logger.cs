using System;
using System.Drawing;
using System.Windows.Forms;

namespace CECtool2;

internal class logger
{
	private Console_Window cws = new Console_Window();

	public void start()
	{
		cws.Show();
	}

	public void create_entry(string text)
	{
		ListViewItem listViewItem = new ListViewItem();
		listViewItem.ForeColor = Color.Gainsboro;
		listViewItem.Text = DateTime.Now.ToLongTimeString() + " > " + text;
		cws.listView1.Items.Add(listViewItem);
	}
}

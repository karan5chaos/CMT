using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CECtool2;

public class Console_Window : Form
{
	private IContainer components = null;

	public ListView listView1;

	public Console_Window()
	{
		InitializeComponent();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.listView1 = new System.Windows.Forms.ListView();
		base.SuspendLayout();
		this.listView1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.listView1.Location = new System.Drawing.Point(0, 0);
		this.listView1.MultiSelect = false;
		this.listView1.Name = "listView1";
		this.listView1.Size = new System.Drawing.Size(383, 200);
		this.listView1.TabIndex = 0;
		this.listView1.UseCompatibleStateImageBehavior = false;
		this.listView1.View = System.Windows.Forms.View.List;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(383, 200);
		base.Controls.Add(this.listView1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "Console_Window";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Console_Window";
		base.ResumeLayout(false);
	}
}

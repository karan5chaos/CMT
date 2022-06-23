using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CECtool2;

public class panel_custom : Form
{
	private IContainer components = null;

	public panel_custom()
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
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(305, 154);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "panel_custom";
		this.Text = "panel_custom";
		base.ResumeLayout(false);
	}
}

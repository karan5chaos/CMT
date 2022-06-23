using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CECtool2;

public class Loading : Form
{
	private IContainer components = null;

	private ProgressBar progressBar1;

	private Label label1;

	public Loading()
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
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.progressBar1.Location = new System.Drawing.Point(12, 45);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(291, 23);
		this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
		this.progressBar1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(91, 26);
		this.label1.TabIndex = 1;
		this.label1.Text = "Processing Data..\r\nPlease Wait...\r\n";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(315, 80);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.progressBar1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "Loading";
		this.Text = "Loading";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

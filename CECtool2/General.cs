using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CECtool2.Properties;

namespace CECtool2;

public class General : Form
{
	private IContainer components = null;

	private Label label1;

	private MaskedTextBox maskedTextBox1;

	private CheckBox checkBox1;

	private ToolTip toolTip1;

	public General()
	{
		InitializeComponent();
	}

	private void General_Load(object sender, EventArgs e)
	{
		maskedTextBox1.Text = Settings.Default.Threshold.ToString();
		if (Settings.Default.Populate)
		{
			checkBox1.Checked = true;
		}
		else
		{
			checkBox1.Checked = false;
		}
	}

	private void General_FormClosing(object sender, FormClosingEventArgs e)
	{
		Settings.Default.Threshold = Convert.ToInt32(maskedTextBox1.Text);
		if (checkBox1.Checked)
		{
			Settings.Default.Populate = true;
		}
		else
		{
			Settings.Default.Populate = false;
		}
		Settings.Default.Save();
		Settings.Default.Reload();
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
		this.components = new System.ComponentModel.Container();
		this.label1 = new System.Windows.Forms.Label();
		this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(150, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Exception Highlight Threshold -";
		this.maskedTextBox1.Location = new System.Drawing.Point(169, 16);
		this.maskedTextBox1.Mask = "000";
		this.maskedTextBox1.Name = "maskedTextBox1";
		this.maskedTextBox1.Size = new System.Drawing.Size(31, 21);
		this.maskedTextBox1.TabIndex = 1;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(16, 54);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(101, 17);
		this.checkBox1.TabIndex = 2;
		this.checkBox1.Text = "Populate Tables";
		this.toolTip1.SetToolTip(this.checkBox1, "Populate tables with data after processing.\r\n(May speed-up report generation)");
		this.checkBox1.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(219, 82);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.maskedTextBox1);
		base.Controls.Add(this.label1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "General";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "General";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(General_FormClosing);
		base.Load += new System.EventHandler(General_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

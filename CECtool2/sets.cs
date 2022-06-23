using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CECtool2.Properties;

namespace CECtool2;

public class sets : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	public sets()
	{
		InitializeComponent();
	}

	private void sets_Load(object sender, EventArgs e)
	{
		if (Settings.Default.Gen_Mail)
		{
			checkBox2.Checked = true;
		}
		else
		{
			checkBox2.Checked = false;
		}
	}

	private void sets_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (checkBox2.Checked)
		{
			Settings.Default.Gen_Mail = true;
		}
		else
		{
			Settings.Default.Gen_Mail = true;
		}
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.checkBox2);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(183, 102);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "General";
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(23, 61);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(137, 17);
		this.checkBox2.TabIndex = 1;
		this.checkBox2.Text = "Always Generate E-Mail";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(23, 29);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(107, 17);
		this.checkBox1.TabIndex = 0;
		this.checkBox1.Text = "Save Output Path";
		this.checkBox1.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(208, 128);
		base.Controls.Add(this.groupBox1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "sets";
		this.Text = "Settings";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(sets_FormClosing);
		base.Load += new System.EventHandler(sets_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}

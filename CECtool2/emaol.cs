using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CECtool2;

public class emaol : Form
{
	private IContainer components = null;

	private Button button5;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private GroupBox groupBox1;

	private Label label4;

	private Label label3;

	private TextBox textBox6;

	private TextBox textBox5;

	private GroupBox groupBox5;

	private Button button3;

	private TextBox textBox3;

	private GroupBox groupBox3;

	private Button button1;

	private TextBox textBox4;

	private GroupBox groupBox2;

	private Button button2;

	private TextBox textBox2;

	private TextBox textBox1;

	private Label label2;

	private Label label1;

	private Button button4;

	public emaol()
	{
		InitializeComponent();
	}

	private void emaol_Load(object sender, EventArgs e)
	{
		textBox1.Text = email.Default.to;
		textBox2.Text = email.Default.cc;
		textBox4.Text = email.Default.message;
		textBox3.Text = email.Default.subject;
	}

	private void textBox4_TextChanged(object sender, EventArgs e)
	{
		textBox5.Text = textBox4.Text;
	}

	private void dynamic_text(TextBox textBox)
	{
		textBox.Text = textBox.Text.Replace("@-userdata", "user_data");
		textBox.Text = textBox.Text.Replace("@-ruledata", "rule_data");
		textBox.Text = textBox.Text.Replace("@-wodata", "Workorder_data");
		textBox.Text = textBox.Text.Replace("@-namedata", "Rule_name_data");
		textBox.Text = textBox.Text.Replace("@-admindata", "Admin_data");
	}

	private void textBox5_TextChanged(object sender, EventArgs e)
	{
		dynamic_text(textBox5);
	}

	private void textBox6_TextChanged(object sender, EventArgs e)
	{
		dynamic_text(textBox6);
	}

	private void textBox3_TextChanged(object sender, EventArgs e)
	{
		textBox6.Text = textBox3.Text;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		email.Default.to = textBox1.Text;
		email.Default.cc = textBox2.Text;
		email.Default.Save();
		email.Default.Reload();
		toolStripStatusLabel1.Text = "Receipents Saved..";
	}

	private void button3_Click(object sender, EventArgs e)
	{
		email.Default.subject = textBox3.Text;
		email.Default.Save();
		email.Default.Reload();
		toolStripStatusLabel1.Text = "Subject updated..";
	}

	private void button1_Click(object sender, EventArgs e)
	{
		email.Default.message = textBox4.Text;
		email.Default.Save();
		email.Default.Reload();
		toolStripStatusLabel1.Text = "Message Body Saved..";
	}

	private void button5_Click(object sender, EventArgs e)
	{
		email.Default.Save();
		email.Default.Reload();
		toolStripStatusLabel1.Text = "E-mail Settings Saved..";
	}

	private void button4_Click(object sender, EventArgs e)
	{
		MessageBox.Show("List of Available parameters - \nParameters are replaced by the data when application is running.\n\n@-userdata - User_data\n\n@-ruledata - Rule_Code_data\n\n@-wodata - Workorder_data\n\n@-namedata - Rule_name_data\n\n@-admindata - Admin_data");
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
		this.button5 = new System.Windows.Forms.Button();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox6 = new System.Windows.Forms.TextBox();
		this.textBox5 = new System.Windows.Forms.TextBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.button3 = new System.Windows.Forms.Button();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button4 = new System.Windows.Forms.Button();
		this.statusStrip1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.button5.Location = new System.Drawing.Point(687, 329);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(75, 23);
		this.button5.TabIndex = 19;
		this.button5.Text = "Save";
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.statusStrip1.BackColor = System.Drawing.Color.DimGray;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripStatusLabel1 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 358);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(770, 22);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 18;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
		this.toolStripStatusLabel1.Text = "-";
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.textBox6);
		this.groupBox1.Controls.Add(this.textBox5);
		this.groupBox1.Location = new System.Drawing.Point(434, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(328, 323);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Email Preview";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 72);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(49, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Message";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(6, 26);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(42, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "Subject";
		this.textBox6.Location = new System.Drawing.Point(9, 42);
		this.textBox6.Name = "textBox6";
		this.textBox6.ReadOnly = true;
		this.textBox6.Size = new System.Drawing.Size(309, 21);
		this.textBox6.TabIndex = 5;
		this.textBox6.TextChanged += new System.EventHandler(textBox6_TextChanged);
		this.textBox5.Location = new System.Drawing.Point(9, 88);
		this.textBox5.Multiline = true;
		this.textBox5.Name = "textBox5";
		this.textBox5.ReadOnly = true;
		this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.textBox5.Size = new System.Drawing.Size(309, 229);
		this.textBox5.TabIndex = 4;
		this.textBox5.TextChanged += new System.EventHandler(textBox5_TextChanged);
		this.groupBox5.Controls.Add(this.button3);
		this.groupBox5.Controls.Add(this.textBox3);
		this.groupBox5.Location = new System.Drawing.Point(8, 96);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(420, 53);
		this.groupBox5.TabIndex = 16;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Subject";
		this.button3.Location = new System.Drawing.Point(363, 18);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(44, 23);
		this.button3.TabIndex = 4;
		this.button3.Text = "Save";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.textBox3.Location = new System.Drawing.Point(15, 20);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(342, 21);
		this.textBox3.TabIndex = 3;
		this.textBox3.TextChanged += new System.EventHandler(textBox3_TextChanged);
		this.groupBox3.Controls.Add(this.button1);
		this.groupBox3.Controls.Add(this.textBox4);
		this.groupBox3.Location = new System.Drawing.Point(8, 154);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(420, 198);
		this.groupBox3.TabIndex = 14;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Message";
		this.button1.Location = new System.Drawing.Point(6, 163);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(407, 29);
		this.button1.TabIndex = 6;
		this.button1.Text = "Save Message";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox4.Location = new System.Drawing.Point(12, 19);
		this.textBox4.Multiline = true;
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(395, 138);
		this.textBox4.TabIndex = 5;
		this.textBox4.TextChanged += new System.EventHandler(textBox4_TextChanged);
		this.groupBox2.Controls.Add(this.button2);
		this.groupBox2.Controls.Add(this.textBox2);
		this.groupBox2.Controls.Add(this.textBox1);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(8, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(420, 90);
		this.groupBox2.TabIndex = 13;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Receipents";
		this.button2.Location = new System.Drawing.Point(363, 52);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(44, 23);
		this.button2.TabIndex = 2;
		this.button2.Text = "Save";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.textBox2.Location = new System.Drawing.Point(42, 54);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(313, 21);
		this.textBox2.TabIndex = 1;
		this.textBox1.Location = new System.Drawing.Point(44, 21);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(363, 21);
		this.textBox1.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(9, 57);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(24, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "CC :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(22, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "To :";
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button4.BackColor = System.Drawing.Color.DimGray;
		this.button4.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
		this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button4.Location = new System.Drawing.Point(695, 358);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 22);
		this.button4.TabIndex = 20;
		this.button4.Text = "Parameters";
		this.button4.UseVisualStyleBackColor = false;
		this.button4.Click += new System.EventHandler(button4_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(770, 380);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "emaol";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "E-Mail";
		base.Load += new System.EventHandler(emaol_Load);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

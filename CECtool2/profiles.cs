using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CECtool2.Properties;

namespace CECtool2;

public class profiles : Form
{
	private string pr_path = "C:/CEC_tool/profiles";

	private IContainer components = null;

	private SaveFileDialog saveFileDialog1;

	private Button button4;

	private SaveFileDialog saveFileDialog2;

	private GroupBox groupBox1;

	private CheckBox checkBox4;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	public profiles()
	{
		InitializeComponent();
	}

	private void profiles_Load(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
		saveFileDialog1.ShowDialog(this);
	}

	private void button3_Click(object sender, EventArgs e)
	{
	}

	private void saveprofs(string path, string users1, string rules1, string respons1, string prio, string rems, string leds, string dbess, string thres1)
	{
		if (!File.Exists(path))
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(path, Encoding.UTF8);
			xmlTextWriter.WriteStartDocument();
			xmlTextWriter.WriteStartElement("Profiles");
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Close();
			xmlTextWriter.Dispose();
			FileStream fileStream = new FileStream(path, FileMode.Open);
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileStream);
				XmlElement xmlElement = xmlDocument.CreateElement("profile");
				XmlElement xmlElement2 = xmlDocument.CreateElement("users");
				XmlElement xmlElement3 = xmlDocument.CreateElement("rulecodes");
				XmlElement xmlElement4 = xmlDocument.CreateElement("responsibilities");
				XmlElement xmlElement5 = xmlDocument.CreateElement("priority");
				XmlElement xmlElement6 = xmlDocument.CreateElement("remarks");
				XmlElement xmlElement7 = xmlDocument.CreateElement("ledetails");
				XmlElement xmlElement8 = xmlDocument.CreateElement("dbecomments");
				XmlElement xmlElement9 = xmlDocument.CreateElement("threshold");
				XmlText newChild = xmlDocument.CreateTextNode(users1);
				XmlText newChild2 = xmlDocument.CreateTextNode(rules1);
				XmlText newChild3 = xmlDocument.CreateTextNode(respons1);
				XmlText newChild4 = xmlDocument.CreateTextNode(prio);
				XmlText newChild5 = xmlDocument.CreateTextNode(rems);
				XmlText newChild6 = xmlDocument.CreateTextNode(leds);
				XmlText newChild7 = xmlDocument.CreateTextNode(dbess);
				XmlText newChild8 = xmlDocument.CreateTextNode(thres1);
				xmlElement2.AppendChild(newChild);
				xmlElement3.AppendChild(newChild2);
				xmlElement4.AppendChild(newChild3);
				xmlElement5.AppendChild(newChild4);
				xmlElement6.AppendChild(newChild5);
				xmlElement7.AppendChild(newChild6);
				xmlElement8.AppendChild(newChild7);
				xmlElement9.AppendChild(newChild8);
				xmlElement.AppendChild(xmlElement2);
				xmlElement.AppendChild(xmlElement3);
				xmlElement.AppendChild(xmlElement4);
				xmlElement.AppendChild(xmlElement5);
				xmlElement.AppendChild(xmlElement6);
				xmlElement.AppendChild(xmlElement7);
				xmlElement.AppendChild(xmlElement8);
				xmlElement.AppendChild(xmlElement9);
				xmlDocument.DocumentElement.AppendChild(xmlElement);
				fileStream.Close();
				fileStream.Dispose();
				xmlDocument.Save(path);
				MessageBox.Show("Profile Exported Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error Occured : \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				fileStream.Close();
				fileStream.Dispose();
			}
		}
	}

	private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
	}

	private void button4_Click(object sender, EventArgs e)
	{
		saveFileDialog2.ShowDialog(this);
	}

	private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
	{
		string[] array = new string[Settings.Default.Users.Count];
		string[] array2 = new string[Settings.Default.Rule_code.Count];
		string[] array3 = new string[Settings.Default.Responsibility.Count];
		string[] array4 = new string[Settings.Default.Priority.Count];
		string[] array5 = new string[Settings.Default.LE_Details.Count];
		string[] array6 = new string[Settings.Default.Remark.Count];
		string[] array7 = new string[Settings.Default.DBE_comment.Count];
		Settings.Default.Users.CopyTo(array, 0);
		Settings.Default.Rule_code.CopyTo(array2, 0);
		Settings.Default.Responsibility.CopyTo(array3, 0);
		Settings.Default.Priority.CopyTo(array4, 0);
		Settings.Default.LE_Details.CopyTo(array5, 0);
		Settings.Default.Remark.CopyTo(array6, 0);
		Settings.Default.DBE_comment.CopyTo(array7, 0);
		string users = string.Join(",", array);
		string rules = string.Join(",", array2);
		string respons = string.Join(",", array3);
		string prio = string.Join(",", array4);
		string leds = string.Join(",", array5);
		string rems = string.Join(",", array6);
		string dbess = string.Join(",", array7);
		saveprofs(saveFileDialog2.FileName, users, rules, respons, prio, rems, leds, dbess, Settings.Default.Threshold.ToString());
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
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.button4 = new System.Windows.Forms.Button();
		this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.saveFileDialog1.DefaultExt = "cprf";
		this.saveFileDialog1.Filter = "CEC Profiles|*cprf";
		this.saveFileDialog1.InitialDirectory = "C:/CEC_tool/profiles";
		this.saveFileDialog1.RestoreDirectory = true;
		this.saveFileDialog1.Title = "Profile Export Path";
		this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(saveFileDialog1_FileOk);
		this.button4.Location = new System.Drawing.Point(12, 131);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(160, 23);
		this.button4.TabIndex = 7;
		this.button4.Text = "Export";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.saveFileDialog2.DefaultExt = "cprf";
		this.saveFileDialog2.Filter = "CEC Profiles|*cprf";
		this.saveFileDialog2.InitialDirectory = "C:/CEC_tool/profiles";
		this.saveFileDialog2.RestoreDirectory = true;
		this.saveFileDialog2.Title = "Select path to save current settings as profile";
		this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(saveFileDialog2_FileOk);
		this.groupBox1.Controls.Add(this.checkBox4);
		this.groupBox1.Controls.Add(this.checkBox3);
		this.groupBox1.Controls.Add(this.checkBox2);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(160, 113);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Export Profile";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(15, 19);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(56, 17);
		this.checkBox1.TabIndex = 0;
		this.checkBox1.Text = "Filters";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(15, 42);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(102, 17);
		this.checkBox2.TabIndex = 1;
		this.checkBox2.Text = "General Settings";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(15, 65);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(139, 17);
		this.checkBox3.TabIndex = 2;
		this.checkBox3.Text = "Input / Output Locations";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(15, 88);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(93, 17);
		this.checkBox4.TabIndex = 3;
		this.checkBox4.Text = "E-Mail Settings";
		this.checkBox4.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(185, 162);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button4);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "profiles";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Profiles";
		base.Load += new System.EventHandler(profiles_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}

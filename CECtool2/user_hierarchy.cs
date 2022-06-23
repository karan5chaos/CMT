using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using CECtool2.Properties;

namespace CECtool2;

public class user_hierarchy : Form
{
	private delegate void SetTextCallback(string text1, string text2);

	private IContainer components = null;

	private DataGridView dataGridView1;

	private Button button1;

	private TextBox textBox1;

	private Button button2;

	private OpenFileDialog openFileDialog1;

	private DataGridViewTextBoxColumn user;

	private DataGridViewTextBoxColumn flm;

	private BackgroundWorker backgroundWorker1;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel toolStripStatusLabel1;

	public user_hierarchy()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		toolStripStatusLabel1.Text = "Loading Headcounts...";
		backgroundWorker1.RunWorkerAsync();
	}

	public void CsvFileToDatatable(string path, string path2, bool IsFirstRowHeader)
	{
		string text = "No";
		string empty = string.Empty;
		DataTable dataTable = null;
		DataTable dataTable2 = null;
		string empty2 = string.Empty;
		string empty3 = string.Empty;
		try
		{
			empty2 = Path.GetDirectoryName(path);
			empty3 = Path.GetFileName(path);
			empty = "SELECT * FROM [" + empty3 + "]";
			if (IsFirstRowHeader)
			{
				text = "Yes";
			}
			using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + empty2 + ";Extended Properties=\"Text;HDR=" + text + "\""))
			{
				using OleDbCommand selectCommand = new OleDbCommand(empty, connection);
				using OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommand);
				dataTable = new DataTable();
				dataTable.Locale = CultureInfo.CurrentCulture;
				oleDbDataAdapter.Fill(dataTable);
				dataTable2 = dataTable.Clone();
				foreach (DataColumn column in dataTable2.Columns)
				{
					column.DataType = typeof(string);
				}
				foreach (DataRow row2 in dataTable.Rows)
				{
					dataTable2.ImportRow(row2);
				}
				foreach (DataRow row3 in dataTable2.Rows)
				{
					for (int i = 0; i < dataTable2.Columns.Count; i++)
					{
						dataTable2.Columns[i].ReadOnly = false;
						if (string.IsNullOrEmpty(row3[i].ToString()))
						{
							row3[i] = string.Empty;
						}
					}
				}
			}
			dataTable2.TableName = "userlist";
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(dataTable2);
			string xml = dataSet.GetXml();
			string contents = xml.ToString().Replace("_x0020_", "_");
			File.WriteAllText(path2 + "/user_output.xml", contents);
		}
		catch (OleDbException)
		{
			MessageBox.Show("File is open. Data can be inaccurate. Please close the file to avoid any potential issues.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception)
		{
			MessageBox.Show("Error occured..");
		}
		finally
		{
			path = "";
			path2 = "";
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		openFileDialog1.ShowDialog(this);
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		textBox1.Text = openFileDialog1.FileName;
	}

	private void Setlist(string text1, string text2)
	{
		if (dataGridView1.InvokeRequired)
		{
			SetTextCallback method = Setlist;
			Invoke(method, text1, text2);
			return;
		}
		int index = dataGridView1.Rows.Add();
		DataGridViewRow dataGridViewRow = dataGridView1.Rows[index];
		dataGridViewRow.Cells["user"].Value = text1;
		dataGridViewRow.Cells["flm"].Value = text2;
		if (Settings.Default.Users.Contains(text1))
		{
			dataGridViewRow.DefaultCellStyle.BackColor = Color.LimeGreen;
		}
	}

	public void getusers(string variable_path)
	{
		dataGridView1.Rows.Clear();
		FileStream fileStream = new FileStream(variable_path, FileMode.Open);
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.Load(fileStream);
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("userlist");
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				XmlElement xmlElement = (XmlElement)xmlDocument.GetElementsByTagName("userlist")[i];
				XmlElement xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("Atlas_User_ID")[i];
				XmlElement xmlElement3 = (XmlElement)xmlDocument.GetElementsByTagName("First_Level_Manager")[i];
				Setlist(xmlElement2.InnerText, xmlElement3.InnerText);
			}
			fileStream.Close();
			fileStream.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error occured :\n\n" + ex.Message);
			fileStream.Close();
			fileStream.Dispose();
		}
	}

	private void user_hierarchy_Load(object sender, EventArgs e)
	{
		textBox1.Text = Settings.Default.Headcount_path;
		if (textBox1.Text != "" && textBox1.Text != null)
		{
			button1_Click(null, null);
		}
	}

	private void user_hierarchy_FormClosing(object sender, FormClosingEventArgs e)
	{
		Settings.Default.Headcount_path = textBox1.Text;
		Settings.Default.Save();
		Settings.Default.Reload();
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
		CsvFileToDatatable(textBox1.Text, Path.GetDirectoryName(textBox1.Text), IsFirstRowHeader: true);
		getusers(Path.GetDirectoryName(textBox1.Text) + "/user_output.xml");
	}

	private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		toolStripStatusLabel1.Text = "Headcounts Loaded..";
	}

	private void button3_Click(object sender, EventArgs e)
	{
		List<DataGridViewRow> list = new List<DataGridViewRow>();
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
		{
			if (item.DefaultCellStyle.BackColor != Color.LimeGreen)
			{
				list.Add(item);
			}
		}
		list.ForEach(delegate(DataGridViewRow d)
		{
			dataGridView1.Rows.Remove(d);
		});
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.user = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flm = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.button1 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.button2 = new System.Windows.Forms.Button();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.user, this.flm);
		this.dataGridView1.Location = new System.Drawing.Point(12, 12);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.Size = new System.Drawing.Size(327, 509);
		this.dataGridView1.TabIndex = 0;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.user.DefaultCellStyle = dataGridViewCellStyle;
		this.user.HeaderText = "User ID";
		this.user.Name = "user";
		this.user.ReadOnly = true;
		this.flm.HeaderText = "First Level Manager";
		this.flm.Name = "flm";
		this.flm.ReadOnly = true;
		this.button1.Location = new System.Drawing.Point(264, 527);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 1;
		this.button1.Text = "Create";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.textBox1.Location = new System.Drawing.Point(12, 529);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(207, 21);
		this.textBox1.TabIndex = 2;
		this.button2.Location = new System.Drawing.Point(228, 527);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(30, 23);
		this.button2.TabIndex = 3;
		this.button2.Text = "...";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.openFileDialog1.FileName = "openFileDialog1";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripStatusLabel1 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 556);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(353, 22);
		this.statusStrip1.TabIndex = 4;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
		this.toolStripStatusLabel1.Text = "-";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(353, 578);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.textBox1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dataGridView1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "user_hierarchy";
		this.Text = "user_hierarchy";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(user_hierarchy_FormClosing);
		base.Load += new System.EventHandler(user_hierarchy_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

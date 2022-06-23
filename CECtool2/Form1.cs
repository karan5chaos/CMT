using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CECtool2.Properties;
using ClosedXML.Excel;
using ExportToExcel;
using LumenWorks.Framework.IO.Csv;
using Microsoft.Office.Interop.Outlook;

namespace CECtool2;

public class Form1 : Form
{
	private logger log = new logger();

	private dics di = new dics();

	private string excelf = "";

	private string pr_path = "C:/CEC_tool/profiles";

	private DataTable dttable = new DataTable();

	private DataTable dttable22 = new DataTable();

	private Loading l = new Loading();

	private string outfile = "";

	private bool iszip = false;

	private IContainer components = null;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private MenuStrip menuStrip1;

	private FolderBrowserDialog folderBrowserDialog1;

	private OpenFileDialog openFileDialog1;

	private BackgroundWorker backgroundWorker1;

	private ToolTip toolTip1;

	private BackgroundWorker backgroundWorker2;

	private GroupBox groupBox6;

	private Label label1;

	private Button button4;

	private TextBox textBox2;

	private Label label2;

	private Button button3;

	private TextBox textBox1;

	private GroupBox groupBox4;

	private NumericUpDown numericUpDown1;

	private Label label5;

	private CheckBox checkBox3;

	private CheckBox checkBox1;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private SplitContainer splitContainer1;

	private GroupBox groupBox3;

	private DataGridView dataGridView4;

	private SplitContainer splitContainer5;

	private SplitContainer splitContainer2;

	private GroupBox groupBox8;

	private DataGridView dataGridView3;

	private GroupBox groupBox11;

	private DataGridView dataGridView1;

	private SplitContainer splitContainer6;

	private GroupBox groupBox13;

	private DataGridView dataGridView2;

	private GroupBox groupBox14;

	private DataGridView dataGridView5;

	private Panel panel2;

	private Button button1;

	private Button button2;

	private GroupBox groupBox1;

	private DataGridView dataGridView6;

	private DataGridViewTextBoxColumn name;

	private DataGridViewTextBoxColumn state;

	private DataGridViewTextBoxColumn en;

	private DataGridViewTextBoxColumn count;

	private TabPage tabPage2;

	private DataGridView dataGridView7;

	private ToolStripMenuItem generalToolStripMenuItem;

	private DataGridViewTextBoxColumn flmanager;

	private DataGridViewTextBoxColumn coun;

	private ToolStripMenuItem generalToolStripMenuItem1;

	private ToolStripMenuItem eMailToolStripMenuItem;

	private ToolStripMenuItem profilesToolStripMenuItem3;

	private ToolStripMenuItem filtersToolStripMenuItem1;

	private ToolStripMenuItem profilesToolStripMenuItem2;

	private Label label3;

	private TextBox textBox3;

	public Form1()
	{
		InitializeComponent();
		dataGridView2.DoubleBuffered(setting: true);
	}

	private void profiles_Load()
	{
		profilesToolStripMenuItem2.DropDownItems.Clear();
		if (!Directory.Exists(pr_path))
		{
			Directory.CreateDirectory(pr_path);
		}
		string[] files = Directory.GetFiles(pr_path);
		string[] array = files;
		foreach (string text in array)
		{
			if (text.Contains(".cprf"))
			{
				profilesToolStripMenuItem2.DropDownItems.Add(Path.GetFileNameWithoutExtension(text));
			}
			foreach (ToolStripItem dropDownItem in profilesToolStripMenuItem2.DropDownItems)
			{
				if (dropDownItem.Text == Settings.Default.Profile)
				{
					((ToolStripMenuItem)dropDownItem).Checked = true;
					break;
				}
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start(outfile);
		}
		catch
		{
		}
	}

	private void CreateMailItem(string attc)
	{
		try
		{
			log.create_entry("gen_mail_item initialized");
			Process[] processesByName = Process.GetProcessesByName("OUTLOOK");
			Microsoft.Office.Interop.Outlook.Application application = (Microsoft.Office.Interop.Outlook.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0006F03A-0000-0000-C000-000000000046")));
			if (processesByName.Length != 0)
			{
				application = Marshal.GetActiveObject("Outlook.Application") as Microsoft.Office.Interop.Outlook.Application;
			}
			MailItem mailItem = (dynamic)application.CreateItem(OlItemType.olMailItem);
			mailItem.To = email.Default.to;
			mailItem.CC = email.Default.cc;
			mailItem.Attachments.Add(attc, Missing.Value, Missing.Value, Missing.Value);
			if (email.Default.subject != null || email.Default.subject != "")
			{
				mailItem.Subject = dynamictext(email.Default.subject);
			}
			mailItem.HTMLBody = dynamictext(email.Default.message);
			mailItem.Display(false);
			toolStripStatusLabel1.Text = "Mail Generated.";
			mailItem.Recipients.ResolveAll();
			log.create_entry("gen_mail_item finished");
		}
		catch
		{
			MessageBox.Show("Error creating mail.\nPlease check if outlook is configured properly or if email group is provided.");
		}
	}

	private StringBuilder DataGridtoHTML(DataGridView dg)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("<html><body><table class='gridtable' style='font-family: verdana,arial,sans-serif;font-size:11px;color:#333333;border-width: 1px;border-color: #666666;border-collapse: collapse;'>");
		stringBuilder.AppendLine("<tr>");
		for (int i = 0; i < dg.Columns.Count; i++)
		{
			stringBuilder.AppendLine("<th style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>" + dg.Columns[i].HeaderText + "</th>");
		}
		stringBuilder.AppendLine("</tr>");
		for (int i = 0; i < dg.Rows.Count; i++)
		{
			stringBuilder.AppendLine("<tr>");
			foreach (DataGridViewCell cell in dg.Rows[i].Cells)
			{
				if (int.TryParse(cell.Value.ToString(), out var result))
				{
					if (result >= Settings.Default.Threshold)
					{
						stringBuilder.AppendLine("<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;color:black'>" + cell.Value.ToString() + "</td>");
					}
					else
					{
						stringBuilder.AppendLine("<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;color:black'>" + cell.Value.ToString() + "</td>");
					}
				}
				else
				{
					stringBuilder.AppendLine("<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;color:black'>" + cell.Value.ToString() + "</td>");
				}
			}
			stringBuilder.AppendLine("</tr>");
		}
		stringBuilder.AppendLine("<tr>");
		stringBuilder.AppendLine("<td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;color:black'><b>Total</td><td style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;color:black'><b>" + textBox3.Text + "</td>");
		stringBuilder.AppendLine("</tr>");
		stringBuilder.AppendLine("</table></body></html>");
		return stringBuilder;
	}

	private string dynamictext(string text)
	{
		string newValue = DataGridtoHTML(dataGridView3).ToString();
		string newValue2 = DataGridtoHTML(dataGridView4).ToString();
		string newValue3 = DataGridtoHTML(dataGridView1).ToString();
		string newValue4 = DataGridtoHTML(dataGridView2).ToString();
		string newValue5 = DataGridtoHTML(dataGridView5).ToString();
		text = text.Replace("@-userdata", newValue4);
		text = text.Replace("@-ruledata", newValue5);
		text = text.Replace("@-wodata", newValue3);
		text = text.Replace("@-namedata", newValue2);
		text = text.Replace("@-admindata", newValue);
		return text;
	}

	public void CsvFileToDatatable(string path, bool IsFirstRowHeader)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		try
		{
			dttable.Clear();
			dttable.Columns.Clear();
			dttable.Rows.Clear();
			dttable22.Clear();
			dttable22.Columns.Clear();
			dttable22.Rows.Clear();
			CsvReader val = new CsvReader((TextReader)(object)new StreamReader(textBox1.Text), true, ',', 4094);
			try
			{
				val.set_MissingFieldAction((MissingFieldAction)1);
				val.set_DefaultParseErrorAction((ParseErrorAction)1);
				dttable.Load((IDataReader)val);
				foreach (DataColumn column in dttable.Columns)
				{
					column.ColumnName = string.Join("_", column.ColumnName.Split());
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			CsvReader val2 = new CsvReader((TextReader)(object)new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("CECtool2.Resources.IPC_Headcount.csv")), true, ',', 4094);
			try
			{
				val2.set_MissingFieldAction((MissingFieldAction)1);
				val2.set_DefaultParseErrorAction((ParseErrorAction)1);
				dttable22.Load((IDataReader)val2);
				foreach (DataColumn column2 in dttable22.Columns)
				{
					column2.ColumnName = string.Join("_", column2.ColumnName.Split());
				}
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
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
		}
	}

	private void loadsettings(string path)
	{
		try
		{
			Settings.Default.Users.Clear();
			Settings.Default.Responsibility.Clear();
			Settings.Default.Rule_code.Clear();
			Settings.Default.Priority.Clear();
			Settings.Default.Remark.Clear();
			Settings.Default.LE_Details.Clear();
			Settings.Default.DBE_comment.Clear();
			Settings.Default.Threshold = 0;
			Settings.Default.Profile = "";
			Settings.Default.Save();
			Settings.Default.Reload();
			FileStream fileStream = new FileStream(path, FileMode.Open);
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(fileStream);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("profile");
				int num = 0;
				if (num < elementsByTagName.Count)
				{
					XmlElement xmlElement = (XmlElement)xmlDocument.GetElementsByTagName("profile")[num];
					XmlElement xmlElement2 = (XmlElement)xmlDocument.GetElementsByTagName("users")[num];
					XmlElement xmlElement3 = (XmlElement)xmlDocument.GetElementsByTagName("rulecodes")[num];
					XmlElement xmlElement4 = (XmlElement)xmlDocument.GetElementsByTagName("responsibilities")[num];
					XmlElement xmlElement5 = (XmlElement)xmlDocument.GetElementsByTagName("threshold")[num];
					XmlElement xmlElement6 = (XmlElement)xmlDocument.GetElementsByTagName("priority")[num];
					XmlElement xmlElement7 = (XmlElement)xmlDocument.GetElementsByTagName("remarks")[num];
					XmlElement xmlElement8 = (XmlElement)xmlDocument.GetElementsByTagName("ledetails")[num];
					XmlElement xmlElement9 = (XmlElement)xmlDocument.GetElementsByTagName("dbecomments")[num];
					if (xmlElement2.InnerText != "" && xmlElement2.InnerText != null)
					{
						string[] value = xmlElement2.InnerText.Split(',');
						Settings.Default.Users.AddRange(value);
					}
					if (xmlElement3.InnerText != "" && xmlElement3.InnerText != null)
					{
						string[] value2 = xmlElement3.InnerText.Split(',');
						Settings.Default.Rule_code.AddRange(value2);
					}
					if (xmlElement4.InnerText != "" && xmlElement4.InnerText != null)
					{
						string[] value3 = xmlElement4.InnerText.Split(',');
						Settings.Default.Responsibility.AddRange(value3);
					}
					if (xmlElement5.InnerText != "" && xmlElement5.InnerText != null)
					{
						Settings.Default.Threshold = Convert.ToInt32(xmlElement5.InnerText);
					}
					if (xmlElement6.InnerText != "" && xmlElement6.InnerText != null)
					{
						string[] value3 = xmlElement6.InnerText.Split(',');
						Settings.Default.Priority.AddRange(value3);
					}
					if (xmlElement7.InnerText != "" && xmlElement7.InnerText != null)
					{
						string[] value3 = xmlElement7.InnerText.Split(',');
						Settings.Default.Remark.AddRange(value3);
					}
					if (xmlElement8.InnerText != "" && xmlElement8.InnerText != null)
					{
						string[] value3 = xmlElement8.InnerText.Split(',');
						Settings.Default.LE_Details.AddRange(value3);
					}
					if (xmlElement9.InnerText != "" && xmlElement9.InnerText != null)
					{
						string[] value3 = xmlElement9.InnerText.Split(',');
						Settings.Default.DBE_comment.AddRange(value3);
					}
					Settings.Default.Profile = Path.GetFileNameWithoutExtension(path);
					Settings.Default.Save();
					Settings.Default.Reload();
					MessageBox.Show("Profile Loaded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				fileStream.Close();
				fileStream.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Profile not loaded :\n\n" + ex.Message);
				fileStream.Close();
				fileStream.Dispose();
			}
		}
		catch
		{
		}
	}

	private void delete_un()
	{
		if (filters.Default.le_details)
		{
			delete_led(dttable);
		}
		if (filters.Default.priority)
		{
			delete_priority(dttable);
		}
		if (filters.Default.rulecode)
		{
			delete_rulecodes(dttable);
		}
		if (filters.Default.reamrks)
		{
			delete_remarks(dttable);
		}
		if (filters.Default.dbe)
		{
			delete_dbe(dttable);
		}
		if (filters.Default.responsibility)
		{
			keep_respon(dttable);
		}
		if (filters.Default.users)
		{
			keep_users(dttable);
		}
		if (filters.Default.le_status)
		{
			filter_les(dttable);
		}
		try
		{
		}
		catch
		{
		}
	}

	private void filter_les(DataTable dts)
	{
		if (filters.Default.les_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.LE_Details.Contains(dataRow["LE_Status"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.LE_Details.Contains(dataRow["LE_Status"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void delete_led(DataTable dts)
	{
		if (filters.Default.led_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.LE_Details.Contains(dataRow["LE_Details"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.LE_Details.Contains(dataRow["LE_Details"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void delete_priority(DataTable dts)
	{
		if (filters.Default.prio_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.Priority.Contains(dataRow["Priority"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.Priority.Contains(dataRow["Priority"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void delete_remarks(DataTable dts)
	{
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.Remark.Contains(dataRow["Remark"].ToString()) || dataRow["Remark"].ToString() != "")
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void delete_dbe(DataTable dts)
	{
		if (filters.Default.dbe_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.DBE_comment.Contains(dataRow["DBE_Comment"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.DBE_comment.Contains(dataRow["DBE_Comment"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void keep_users(DataTable dts)
	{
		if (filters.Default.usr_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.Users.Contains(dataRow["User_Id"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.Users.Contains(dataRow["User_Id"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void delete_rulecodes(DataTable dts)
	{
		if (filters.Default.rul_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.Rule_code.Contains(dataRow["Rule_Code"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.Rule_code.Contains(dataRow["Rule_Code"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void keep_respon(DataTable dts)
	{
		if (filters.Default.resp_k)
		{
			for (int num = dts.Rows.Count - 1; num >= 0; num--)
			{
				DataRow dataRow = dts.Rows[num];
				if (!Settings.Default.Responsibility.Contains(dataRow["Responsibility"].ToString()))
				{
					dts.Rows.Remove(dataRow);
				}
			}
			return;
		}
		for (int num = dts.Rows.Count - 1; num >= 0; num--)
		{
			DataRow dataRow = dts.Rows[num];
			if (Settings.Default.Responsibility.Contains(dataRow["Responsibility"].ToString()))
			{
				dts.Rows.Remove(dataRow);
			}
		}
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		log.start();
		profiles_Load();
		log.create_entry("CMT_started");
		numericUpDown1.Value = Settings.Default.Threshold;
		dataGridView6.Rows.Add("Users", filters.Default.usr_k.ToString(), filters.Default.users.ToString(), Settings.Default.Users.Count.ToString());
		dataGridView6.Rows.Add("Responsibility", filters.Default.resp_k.ToString(), filters.Default.responsibility, Settings.Default.Responsibility.Count.ToString());
		dataGridView6.Rows.Add("Priority", filters.Default.prio_k.ToString(), filters.Default.priority.ToString(), Settings.Default.Priority.Count.ToString());
		dataGridView6.Rows.Add("Rule Code", filters.Default.rul_k.ToString(), filters.Default.rulecode.ToString(), Settings.Default.Rule_code.Count.ToString());
		dataGridView6.Rows.Add("LE Details", filters.Default.led_k.ToString(), filters.Default.le_details.ToString(), Settings.Default.LE_Details.Count.ToString());
		dataGridView6.Rows.Add("Remarks", filters.Default.rem_k.ToString(), filters.Default.reamrks.ToString(), Settings.Default.Remark.Count.ToString());
		dataGridView6.Rows.Add("DBE Comments", filters.Default.dbe_k.ToString(), filters.Default.dbe.ToString(), Settings.Default.DBE_comment.Count.ToString());
	}

	private void usersToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void button3_Click(object sender, EventArgs e)
	{
		openFileDialog1.ShowDialog(this);
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		textBox1.Text = openFileDialog1.FileName;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			textBox2.Text = folderBrowserDialog1.SelectedPath;
		}
	}

	private void button2_Click_1(object sender, EventArgs e)
	{
		if (textBox1.Text != "" && textBox1.Text != null)
		{
			if (iszip)
			{
				checkBox3.Checked = true;
			}
			if (checkBox3.Checked)
			{
				File.WriteAllLines(textBox1.Text, File.ReadAllLines(textBox1.Text).Skip(4).ToArray());
			}
			log.create_entry("data_table_generation initialized");
			CsvFileToDatatable(textBox1.Text, IsFirstRowHeader: true);
			if (!backgroundWorker2.IsBusy)
			{
				toolStripStatusLabel1.Text = "Processing Data... Please Wait.";
				checkBox3.Checked = false;
				iszip = false;
				outfile = "";
				button1.Enabled = false;
				log.create_entry("b_worker_process initialized");
				backgroundWorker2.RunWorkerAsync();
				button1.Enabled = false;
				button2.Enabled = false;
			}
			else
			{
				toolStripStatusLabel1.Text = "Releasing Resources and restratcing service... Please Wait and try after sometime..";
				backgroundWorker2.CancelAsync();
			}
		}
		else
		{
			MessageBox.Show("Input file not found..");
		}
	}

	private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		button1.Enabled = true;
		button2.Enabled = true;
		backgroundWorker1.Dispose();
		toolStripStatusLabel1.Text = "Processing Data Finished..";
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
		string tempPath = Path.GetTempPath();
	}

	private void eMailToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void generalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		General general = new General();
		general.ShowDialog(this);
	}

	private void profilesToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void convert2xlsx(DataSet ds, string path)
	{
		try
		{
			CreateExcelFile.CreateExcelDocument(ds, path);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Couldn't create Excel file.\r\nException: " + ex.Message);
		}
	}

	private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
	{
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		delete_un();
		if (textBox2.Text != null && textBox2.Text != "")
		{
			try
			{
				outfile = textBox2.Text + "/CEC_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".xlsx";
				XLWorkbook val = new XLWorkbook();
				val.get_Worksheets().Add(dttable, "cec_output");
				val.SaveAs(outfile);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}

	private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		log.create_entry("b_worker_process finished");
		toolStripStatusLabel1.Text = "Data Processed. Populating Tables...";
		textBox3.Text = dttable.Rows.Count.ToString();
		log.create_entry("populate_tab started");
		Pivot pivot = new Pivot(dttable);
		dataGridView1.SuspendLayout();
		dataGridView1.DataSource = pivot.PivotData("Project_Name", "User_Id", AggregateFunction.Count, "LE_Status");
		dataGridView1.ResumeLayout();
		dataGridView2.SuspendLayout();
		DataTable dataTable = pivot.PivotData("User_Id", "User_Id", AggregateFunction.Count, "LE_Status");
		dataGridView2.DataSource = dataTable;
		dataGridView7.Rows.Clear();
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (DataRow row in dataTable.Rows)
		{
			foreach (DataRow row2 in dttable22.Rows)
			{
				if (row2["Windows_Login_ID"].ToString() == row["User_Id"].ToString())
				{
					if (!dictionary.ContainsKey(row2["First_Level_Manager"].ToString()))
					{
						dictionary.Add(row2["First_Level_Manager"].ToString(), Convert.ToInt32(row["Active"].ToString()));
					}
					else if (dictionary.ContainsKey(row2["First_Level_Manager"].ToString()))
					{
						dictionary[row2["First_Level_Manager"].ToString()] += Convert.ToInt32(row["Active"].ToString());
					}
				}
			}
		}
		foreach (KeyValuePair<string, int> item in dictionary)
		{
			dataGridView7.Rows.Add(item.Key, item.Value);
		}
		dataGridView2.ResumeLayout();
		dataGridView3.SuspendLayout();
		dataGridView3.DataSource = pivot.PivotData("Admin_Level_1", "User_Id", AggregateFunction.Count, "LE_Status");
		dataGridView3.ResumeLayout();
		dataGridView4.SuspendLayout();
		dataGridView4.DataSource = pivot.PivotData("Rule_Name", "User_Id", AggregateFunction.Count, "LE_Status");
		dataGridView4.ResumeLayout();
		dataGridView5.SuspendLayout();
		dataGridView5.DataSource = pivot.PivotData("Rule_Code", "User_Id", AggregateFunction.Count, "LE_Status");
		dataGridView5.ResumeLayout();
		log.create_entry("populate_tab finished");
		if (outfile != "")
		{
			button1.Enabled = true;
		}
		toolStripStatusLabel1.Text = "Report Generation Successful..";
		if (checkBox1.Checked && (outfile != "" || outfile != null))
		{
			CreateMailItem(outfile);
		}
	}

	private void button6_Click(object sender, EventArgs e)
	{
	}

	private void headCountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		user_hierarchy user_hierarchy2 = new user_hierarchy();
		user_hierarchy2.ShowDialog(this);
	}

	private void panel1_DragDrop_1(object sender, DragEventArgs e)
	{
		bool flag = false;
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (Path.GetExtension(text.ToString()) == ".csv")
				{
					log.create_entry(".csv file identified");
					iszip = false;
					textBox1.Text = text.ToString();
					flag = true;
				}
				else
				{
					if (!(Path.GetExtension(text.ToString()) == ".zip"))
					{
						continue;
					}
					log.create_entry(".zip file identified");
					iszip = true;
					string text2 = Path.GetDirectoryName(text.ToString()) + "\\" + Path.GetFileNameWithoutExtension(text.ToString());
					if (!Directory.Exists(text2))
					{
						using ZipArchive zipArchive = ZipFile.OpenRead(text);
						log.create_entry("extracting_zip started..");
						using IEnumerator<ZipArchiveEntry> enumerator = zipArchive.Entries.GetEnumerator();
						if (enumerator.MoveNext())
						{
							ZipArchiveEntry current = enumerator.Current;
							Directory.CreateDirectory(text2);
							current.ExtractToFile(Path.Combine(text2, current.FullName));
							textBox1.Text = Path.Combine(text2, current.FullName);
							flag = true;
							log.create_entry("extracting_zip extracted archive_entry " + current.Name);
						}
					}
					else
					{
						MessageBox.Show("Extracted folder already present..");
					}
				}
			}
		}
		if (flag)
		{
			outfile = "";
			button2_Click_1(null, null);
			flag = false;
		}
	}

	private void panel1_DragEnter_1(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			e.Effect = DragDropEffects.Move;
		}
		else
		{
			e.Effect = DragDropEffects.None;
		}
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void splitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
	{
	}

	private void groupBox14_Enter(object sender, EventArgs e)
	{
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void groupBox6_Enter(object sender, EventArgs e)
	{
	}

	private void numericUpDown1_ValueChanged(object sender, EventArgs e)
	{
		Settings.Default.Threshold = Convert.ToInt32(numericUpDown1.Value);
		Settings.Default.Save();
		Settings.Default.Reload();
	}

	private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void profilesToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void eMailToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void button6_Click_1(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
	{
	}

	private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void tabPage2_Click(object sender, EventArgs e)
	{
	}

	private void groupBox8_Enter(object sender, EventArgs e)
	{
	}

	private void filtersToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		Users users = new Users();
		users.ShowDialog(this);
	}

	private void profilesToolStripMenuItem3_Click(object sender, EventArgs e)
	{
		profiles profiles2 = new profiles();
		profiles2.ShowDialog(this);
	}

	private void eMailToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		emaol emaol2 = new emaol();
		emaol2.ShowDialog(this);
	}

	private void generalToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		sets sets2 = new sets();
		sets2.ShowDialog(this);
	}

	private void profilesToolStripMenuItem2_Click(object sender, EventArgs e)
	{
	}

	private void profilesToolStripMenuItem2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		foreach (ToolStripItem dropDownItem in profilesToolStripMenuItem2.DropDownItems)
		{
			((ToolStripMenuItem)dropDownItem).Checked = false;
		}
		if (e.ClickedItem.Text != null && e.ClickedItem.Text != "")
		{
			loadsettings(pr_path + "/" + e.ClickedItem.Text + ".cprf");
			((ToolStripMenuItem)e.ClickedItem).Checked = true;
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
		log.create_entry("Input_file indentified - " + textBox1.Text);
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
		log.create_entry("Output_path defined - " + textBox2.Text);
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox1.Checked)
		{
			log.create_entry("Email_generation enabled");
		}
		else
		{
			log.create_entry("Email_generation disabled");
		}
	}

	private void checkBox3_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox3.Checked)
		{
			log.create_entry("T_4 lines enabled");
		}
		else
		{
			log.create_entry("T_4 lines disabled");
		}
	}

	private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
	{
		log.create_entry("Threshold_VAL changed to " + numericUpDown1.Value);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CECtool2.Form1));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.generalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.eMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.profilesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.filtersToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.profilesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.button4 = new System.Windows.Forms.Button();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.button3 = new System.Windows.Forms.Button();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.label5 = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dataGridView4 = new System.Windows.Forms.DataGridView();
		this.splitContainer5 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.dataGridView3 = new System.Windows.Forms.DataGridView();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.splitContainer6 = new System.Windows.Forms.SplitContainer();
		this.groupBox13 = new System.Windows.Forms.GroupBox();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.groupBox14 = new System.Windows.Forms.GroupBox();
		this.dataGridView5 = new System.Windows.Forms.DataGridView();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.dataGridView7 = new System.Windows.Forms.DataGridView();
		this.flmanager = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coun = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel2 = new System.Windows.Forms.Panel();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dataGridView6 = new System.Windows.Forms.DataGridView();
		this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.en = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.statusStrip1.SuspendLayout();
		this.menuStrip1.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer5).BeginInit();
		this.splitContainer5.Panel1.SuspendLayout();
		this.splitContainer5.Panel2.SuspendLayout();
		this.splitContainer5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		this.groupBox8.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).BeginInit();
		this.groupBox11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.splitContainer6).BeginInit();
		this.splitContainer6.Panel1.SuspendLayout();
		this.splitContainer6.Panel2.SuspendLayout();
		this.splitContainer6.SuspendLayout();
		this.groupBox13.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		this.groupBox14.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView5).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView7).BeginInit();
		this.panel2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView6).BeginInit();
		base.SuspendLayout();
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripStatusLabel1 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 485);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(751, 22);
		this.statusStrip1.TabIndex = 5;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(12, 17);
		this.toolStripStatusLabel1.Text = "-";
		this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
		this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.menuStrip1.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.generalToolStripMenuItem, this.profilesToolStripMenuItem2 });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(167, 24);
		this.menuStrip1.TabIndex = 6;
		this.menuStrip1.Text = "menuStrip1";
		this.generalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.generalToolStripMenuItem1, this.eMailToolStripMenuItem, this.profilesToolStripMenuItem3, this.filtersToolStripMenuItem1 });
		this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
		this.generalToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
		this.generalToolStripMenuItem.Text = "Settings";
		this.generalToolStripMenuItem1.Image = (System.Drawing.Image)resources.GetObject("generalToolStripMenuItem1.Image");
		this.generalToolStripMenuItem1.Name = "generalToolStripMenuItem1";
		this.generalToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
		this.generalToolStripMenuItem1.Text = "General";
		this.generalToolStripMenuItem1.Click += new System.EventHandler(generalToolStripMenuItem1_Click);
		this.eMailToolStripMenuItem.Name = "eMailToolStripMenuItem";
		this.eMailToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
		this.eMailToolStripMenuItem.Text = "E-Mail";
		this.eMailToolStripMenuItem.Click += new System.EventHandler(eMailToolStripMenuItem_Click_1);
		this.profilesToolStripMenuItem3.Image = (System.Drawing.Image)resources.GetObject("profilesToolStripMenuItem3.Image");
		this.profilesToolStripMenuItem3.Name = "profilesToolStripMenuItem3";
		this.profilesToolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
		this.profilesToolStripMenuItem3.Text = "Profiles";
		this.profilesToolStripMenuItem3.Click += new System.EventHandler(profilesToolStripMenuItem3_Click);
		this.filtersToolStripMenuItem1.Image = (System.Drawing.Image)resources.GetObject("filtersToolStripMenuItem1.Image");
		this.filtersToolStripMenuItem1.Name = "filtersToolStripMenuItem1";
		this.filtersToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
		this.filtersToolStripMenuItem1.Text = "Filters";
		this.filtersToolStripMenuItem1.Click += new System.EventHandler(filtersToolStripMenuItem1_Click);
		this.profilesToolStripMenuItem2.Name = "profilesToolStripMenuItem2";
		this.profilesToolStripMenuItem2.Size = new System.Drawing.Size(98, 20);
		this.profilesToolStripMenuItem2.Text = "Select Profile...";
		this.profilesToolStripMenuItem2.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(profilesToolStripMenuItem2_DropDownItemClicked);
		this.profilesToolStripMenuItem2.Click += new System.EventHandler(profilesToolStripMenuItem2_Click);
		this.folderBrowserDialog1.Description = "Select Output Path";
		this.openFileDialog1.Filter = "CSV File|*.csv|Zip File|*.zip";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.backgroundWorker1.WorkerSupportsCancellation = true;
		this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
		this.toolTip1.ShowAlways = true;
		this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
		this.toolTip1.ToolTipTitle = "Tool Info";
		this.backgroundWorker2.WorkerSupportsCancellation = true;
		this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker2_DoWork);
		this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
		this.groupBox6.Controls.Add(this.label1);
		this.groupBox6.Controls.Add(this.button4);
		this.groupBox6.Controls.Add(this.textBox2);
		this.groupBox6.Controls.Add(this.label2);
		this.groupBox6.Controls.Add(this.button3);
		this.groupBox6.Controls.Add(this.textBox1);
		this.groupBox6.Location = new System.Drawing.Point(12, 31);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(277, 93);
		this.groupBox6.TabIndex = 15;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Report Input/Output Location";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 58);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(45, 13);
		this.label1.TabIndex = 12;
		this.label1.Text = "Output :";
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button4.Location = new System.Drawing.Point(231, 53);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(29, 23);
		this.button4.TabIndex = 11;
		this.button4.Text = "..";
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox2.Location = new System.Drawing.Point(66, 54);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(159, 21);
		this.textBox2.TabIndex = 10;
		this.textBox2.TextChanged += new System.EventHandler(textBox2_TextChanged);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 13);
		this.label2.TabIndex = 9;
		this.label2.Text = "Input :";
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button3.Location = new System.Drawing.Point(231, 22);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(29, 23);
		this.button3.TabIndex = 8;
		this.button3.Text = "..";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBox1.Location = new System.Drawing.Point(66, 24);
		this.textBox1.Name = "textBox1";
		this.textBox1.ReadOnly = true;
		this.textBox1.Size = new System.Drawing.Size(159, 21);
		this.textBox1.TabIndex = 7;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.groupBox4.Controls.Add(this.checkBox3);
		this.groupBox4.Controls.Add(this.checkBox1);
		this.groupBox4.Location = new System.Drawing.Point(12, 130);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(277, 52);
		this.groupBox4.TabIndex = 13;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Processing Options";
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(16, 26);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(104, 17);
		this.checkBox3.TabIndex = 11;
		this.checkBox3.Text = "Trim First 4 Lines";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox3.CheckedChanged += new System.EventHandler(checkBox3_CheckedChanged);
		this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(170, 25);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(94, 17);
		this.checkBox1.TabIndex = 9;
		this.checkBox1.Text = "Generate Mail";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.numericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.numericUpDown1.Location = new System.Drawing.Point(595, 4);
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Size = new System.Drawing.Size(40, 21);
		this.numericUpDown1.TabIndex = 13;
		this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.numericUpDown1.ValueChanged += new System.EventHandler(numericUpDown1_ValueChanged_1);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(530, 9);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(59, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Threshold :";
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(295, 31);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(450, 451);
		this.tabControl1.TabIndex = 17;
		this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage1.Controls.Add(this.splitContainer1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(442, 425);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Team Exceptions";
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.splitContainer1.Location = new System.Drawing.Point(3, 3);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
		this.splitContainer1.Panel2.Controls.Add(this.splitContainer5);
		this.splitContainer1.Size = new System.Drawing.Size(436, 419);
		this.splitContainer1.SplitterDistance = 114;
		this.splitContainer1.TabIndex = 15;
		this.groupBox3.Controls.Add(this.dataGridView4);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox3.Location = new System.Drawing.Point(0, 0);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(436, 114);
		this.groupBox3.TabIndex = 16;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Rule Name";
		this.dataGridView4.AllowUserToAddRows = false;
		this.dataGridView4.AllowUserToDeleteRows = false;
		this.dataGridView4.AllowUserToResizeColumns = false;
		this.dataGridView4.AllowUserToResizeRows = false;
		this.dataGridView4.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.DarkGray;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView4.DefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView4.Location = new System.Drawing.Point(3, 17);
		this.dataGridView4.Name = "dataGridView4";
		this.dataGridView4.ReadOnly = true;
		this.dataGridView4.RowHeadersVisible = false;
		this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView4.Size = new System.Drawing.Size(430, 94);
		this.dataGridView4.TabIndex = 14;
		this.dataGridView4.TabStop = false;
		this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer5.Location = new System.Drawing.Point(0, 0);
		this.splitContainer5.Name = "splitContainer5";
		this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer5.Panel1.Controls.Add(this.splitContainer2);
		this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
		this.splitContainer5.Size = new System.Drawing.Size(436, 301);
		this.splitContainer5.SplitterDistance = 139;
		this.splitContainer5.TabIndex = 15;
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Panel1.Controls.Add(this.groupBox8);
		this.splitContainer2.Panel2.Controls.Add(this.groupBox11);
		this.splitContainer2.Size = new System.Drawing.Size(436, 139);
		this.splitContainer2.SplitterDistance = 227;
		this.splitContainer2.TabIndex = 19;
		this.groupBox8.Controls.Add(this.dataGridView3);
		this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox8.Location = new System.Drawing.Point(0, 0);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(227, 139);
		this.groupBox8.TabIndex = 14;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "Admin / Country";
		this.groupBox8.Enter += new System.EventHandler(groupBox8_Enter);
		this.dataGridView3.AllowUserToAddRows = false;
		this.dataGridView3.AllowUserToDeleteRows = false;
		this.dataGridView3.AllowUserToResizeColumns = false;
		this.dataGridView3.AllowUserToResizeRows = false;
		this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkGray;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView3.Location = new System.Drawing.Point(3, 17);
		this.dataGridView3.Name = "dataGridView3";
		this.dataGridView3.ReadOnly = true;
		this.dataGridView3.RowHeadersVisible = false;
		this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView3.Size = new System.Drawing.Size(221, 119);
		this.dataGridView3.TabIndex = 14;
		this.dataGridView3.TabStop = false;
		this.groupBox11.Controls.Add(this.dataGridView1);
		this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox11.Location = new System.Drawing.Point(0, 0);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(205, 139);
		this.groupBox11.TabIndex = 18;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "Project Name/WorkOrder";
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DarkGray;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 17);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(199, 119);
		this.dataGridView1.TabIndex = 15;
		this.dataGridView1.TabStop = false;
		this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer6.Location = new System.Drawing.Point(0, 0);
		this.splitContainer6.Name = "splitContainer6";
		this.splitContainer6.Panel1.Controls.Add(this.groupBox13);
		this.splitContainer6.Panel2.Controls.Add(this.groupBox14);
		this.splitContainer6.Size = new System.Drawing.Size(436, 158);
		this.splitContainer6.SplitterDistance = 227;
		this.splitContainer6.TabIndex = 0;
		this.groupBox13.Controls.Add(this.dataGridView2);
		this.groupBox13.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox13.Location = new System.Drawing.Point(0, 0);
		this.groupBox13.Name = "groupBox13";
		this.groupBox13.Size = new System.Drawing.Size(227, 158);
		this.groupBox13.TabIndex = 19;
		this.groupBox13.TabStop = false;
		this.groupBox13.Text = "User Wise";
		this.dataGridView2.AllowUserToAddRows = false;
		this.dataGridView2.AllowUserToDeleteRows = false;
		this.dataGridView2.AllowUserToResizeColumns = false;
		this.dataGridView2.AllowUserToResizeRows = false;
		this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.DarkGray;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView2.Location = new System.Drawing.Point(3, 17);
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.ReadOnly = true;
		this.dataGridView2.RowHeadersVisible = false;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(221, 138);
		this.dataGridView2.TabIndex = 15;
		this.dataGridView2.TabStop = false;
		this.groupBox14.Controls.Add(this.dataGridView5);
		this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox14.Location = new System.Drawing.Point(0, 0);
		this.groupBox14.Name = "groupBox14";
		this.groupBox14.Size = new System.Drawing.Size(205, 158);
		this.groupBox14.TabIndex = 19;
		this.groupBox14.TabStop = false;
		this.groupBox14.Text = "Rule Code";
		this.dataGridView5.AllowUserToAddRows = false;
		this.dataGridView5.AllowUserToDeleteRows = false;
		this.dataGridView5.AllowUserToResizeColumns = false;
		this.dataGridView5.AllowUserToResizeRows = false;
		this.dataGridView5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView5.DefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView5.Location = new System.Drawing.Point(3, 17);
		this.dataGridView5.Name = "dataGridView5";
		this.dataGridView5.ReadOnly = true;
		this.dataGridView5.RowHeadersVisible = false;
		this.dataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView5.Size = new System.Drawing.Size(199, 138);
		this.dataGridView5.TabIndex = 15;
		this.dataGridView5.TabStop = false;
		this.tabPage2.Controls.Add(this.dataGridView7);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(442, 425);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Manager-wise Exceptions";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.dataGridView7.AllowUserToAddRows = false;
		this.dataGridView7.AllowUserToDeleteRows = false;
		this.dataGridView7.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView7.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView7.Columns.AddRange(this.flmanager, this.coun);
		this.dataGridView7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView7.Location = new System.Drawing.Point(3, 3);
		this.dataGridView7.Name = "dataGridView7";
		this.dataGridView7.RowHeadersVisible = false;
		this.dataGridView7.Size = new System.Drawing.Size(436, 419);
		this.dataGridView7.TabIndex = 0;
		this.flmanager.HeaderText = "First Level Manager";
		this.flmanager.Name = "flmanager";
		this.flmanager.ReadOnly = true;
		this.coun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.coun.HeaderText = "Count";
		this.coun.Name = "coun";
		this.coun.ReadOnly = true;
		this.coun.Width = 60;
		this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.panel2.Controls.Add(this.button1);
		this.panel2.Controls.Add(this.button2);
		this.panel2.Location = new System.Drawing.Point(12, 451);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(277, 29);
		this.panel2.TabIndex = 18;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.button1.Enabled = false;
		this.button1.Location = new System.Drawing.Point(3, 3);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 0;
		this.button1.Text = "Open Report";
		this.button1.UseVisualStyleBackColor = true;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.button2.Location = new System.Drawing.Point(199, 3);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 8;
		this.button2.Text = "Get Status";
		this.button2.UseVisualStyleBackColor = true;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.Controls.Add(this.dataGridView6);
		this.groupBox1.Location = new System.Drawing.Point(12, 188);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(277, 260);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Status";
		this.dataGridView6.AllowDrop = true;
		this.dataGridView6.AllowUserToAddRows = false;
		this.dataGridView6.AllowUserToDeleteRows = false;
		this.dataGridView6.AllowUserToResizeColumns = false;
		this.dataGridView6.AllowUserToResizeRows = false;
		this.dataGridView6.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView6.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDark;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView6.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dataGridView6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView6.Columns.AddRange(this.name, this.state, this.en, this.count);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlDark;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.ScrollBar;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView6.DefaultCellStyle = dataGridViewCellStyle7;
		this.dataGridView6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView6.Location = new System.Drawing.Point(3, 17);
		this.dataGridView6.MultiSelect = false;
		this.dataGridView6.Name = "dataGridView6";
		this.dataGridView6.RowHeadersVisible = false;
		this.dataGridView6.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView6.Size = new System.Drawing.Size(271, 240);
		this.dataGridView6.TabIndex = 0;
		this.dataGridView6.DragDrop += new System.Windows.Forms.DragEventHandler(panel1_DragDrop_1);
		this.dataGridView6.DragEnter += new System.Windows.Forms.DragEventHandler(panel1_DragEnter_1);
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlDark;
		this.name.DefaultCellStyle = dataGridViewCellStyle8;
		this.name.FillWeight = 113.0288f;
		this.name.HeaderText = "Filters";
		this.name.Name = "name";
		this.name.ReadOnly = true;
		this.state.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.state.FillWeight = 113.0288f;
		this.state.HeaderText = "Retain";
		this.state.Name = "state";
		this.state.ReadOnly = true;
		this.state.Width = 63;
		this.en.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
		this.en.FillWeight = 113.0288f;
		this.en.HeaderText = "Enabled";
		this.en.Name = "en";
		this.en.ReadOnly = true;
		this.en.Width = 70;
		this.count.FillWeight = 60.9137f;
		this.count.HeaderText = "Count";
		this.count.Name = "count";
		this.count.ReadOnly = true;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(641, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(62, 13);
		this.label3.TabIndex = 20;
		this.label3.Text = "Exceptions :";
		this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.textBox3.Location = new System.Drawing.Point(709, 3);
		this.textBox3.Name = "textBox3";
		this.textBox3.ReadOnly = true;
		this.textBox3.Size = new System.Drawing.Size(36, 21);
		this.textBox3.TabIndex = 21;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(751, 507);
		base.Controls.Add(this.textBox3);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.numericUpDown1);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.statusStrip1);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.menuStrip1;
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CEC Management Tool";
		base.Load += new System.EventHandler(Form1_Load);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView4).EndInit();
		this.splitContainer5.Panel1.ResumeLayout(false);
		this.splitContainer5.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer5).EndInit();
		this.splitContainer5.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		this.groupBox8.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView3).EndInit();
		this.groupBox11.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.splitContainer6.Panel1.ResumeLayout(false);
		this.splitContainer6.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer6).EndInit();
		this.splitContainer6.ResumeLayout(false);
		this.groupBox13.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		this.groupBox14.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView5).EndInit();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView7).EndInit();
		this.panel2.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView6).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

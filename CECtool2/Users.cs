using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CECtool2.Properties;

namespace CECtool2;

public class Users : Form
{
	private bool isload = false;

	private IContainer components = null;

	private ErrorProvider errorProvider1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem deleteToolStripMenuItem;

	private ContextMenuStrip contextMenuStrip2;

	private ToolStripMenuItem deleteToolStripMenuItem1;

	private ContextMenuStrip contextMenuStrip3;

	private ToolStripMenuItem deleteToolStripMenuItem2;

	private ContextMenuStrip contextMenuStrip4;

	private ToolStripMenuItem toolStripMenuItem1;

	private ContextMenuStrip contextMenuStrip5;

	private ToolStripMenuItem toolStripMenuItem2;

	private ErrorProvider errorProvider2;

	private ContextMenuStrip contextMenuStrip6;

	private ToolStripMenuItem toolStripMenuItem3;

	private ContextMenuStrip contextMenuStrip7;

	private ToolStripMenuItem toolStripMenuItem4;

	private Label label8;

	private DataGridViewTextBoxColumn keys;

	private ToolStripStatusLabel toolStripStatusLabel1;

	public GroupBox groupBox6;

	public GroupBox groupBox9;

	public GroupBox groupBox10;

	public GroupBox groupBox11;

	public StatusStrip statusStrip1;

	public RadioButton radioButton7;

	public RadioButton radioButton6;

	public RadioButton radioButton5;

	public RadioButton radioButton4;

	public RadioButton radioButton3;

	public RadioButton radioButton2;

	public RadioButton radioButton1;

	public TextBox textBox8;

	public DataGridView dataGridView1;

	private ContextMenuStrip contextMenuStrip8;

	public GroupBox groupBox1;

	public RadioButton radioButton13;

	public RadioButton radioButton14;

	private GroupBox groupBox2;

	private LinkLabel linkLabel2;

	private LinkLabel linkLabel1;

	private GroupBox groupBox3;

	private CheckBox checkBox7;

	private CheckBox checkBox6;

	private CheckBox checkBox5;

	private CheckBox checkBox4;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private ToolTip toolTip1;

	public RadioButton radioButton8;

	private CheckBox checkBox8;

	public Users()
	{
		InitializeComponent();
	}

	private void textBox1_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void Users_Load(object sender, EventArgs e)
	{
		refresh_filters();
		mat_col();
		isload = true;
		populate_list();
		populate_list_res();
		populate_list_rc();
		populate_remarks();
		populate_dbe();
		populate_list_pro();
		populate_list_led();
		populate_les();
	}

	private void populate_list()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.Users.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void populate_list_pro()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.Priority.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void populate_les()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.LE_Staus.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void populate_list_led()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.LE_Details.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void populate_remarks()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.Remark.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void populate_dbe()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.DBE_comment.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	public void populate_list_res()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.Responsibility.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	public void populate_list_rc()
	{
		dataGridView1.Rows.Clear();
		StringEnumerator enumerator = Settings.Default.Rule_code.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				dataGridView1.Rows.Add(current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
		}
	}

	private void textBox2_TextChanged(object sender, EventArgs e)
	{
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			int index = 0;
			if (radioButton1.Checked)
			{
				index = 1;
			}
			if (radioButton2.Checked)
			{
				index = 2;
			}
			if (radioButton3.Checked)
			{
				index = 3;
			}
			if (radioButton4.Checked)
			{
				index = 4;
			}
			if (radioButton5.Checked)
			{
				index = 5;
			}
			if (radioButton6.Checked)
			{
				index = 6;
			}
			if (radioButton7.Checked)
			{
				index = 7;
			}
			if (dataGridView1.SelectedCells.Count > 0)
			{
				int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
				DataGridViewRow dataGridViewRow = dataGridView1.Rows[rowIndex];
				string keyword = Convert.ToString(dataGridViewRow.Cells[0].Value);
				delentry(keyword, index);
			}
			else
			{
				contextMenuStrip1.Enabled = false;
			}
		}
		catch
		{
		}
	}

	private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		try
		{
		}
		catch
		{
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void checkBox6_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void contextMenuStrip7_Opening(object sender, CancelEventArgs e)
	{
	}

	private void Users_FormClosing(object sender, FormClosingEventArgs e)
	{
		filters.Default.users = Convert.ToBoolean(checkBox1.CheckState);
		filters.Default.responsibility = Convert.ToBoolean(checkBox2.CheckState);
		filters.Default.rulecode = Convert.ToBoolean(checkBox3.CheckState);
		filters.Default.priority = Convert.ToBoolean(checkBox4.CheckState);
		filters.Default.le_details = Convert.ToBoolean(checkBox5.CheckState);
		filters.Default.reamrks = Convert.ToBoolean(checkBox6.CheckState);
		filters.Default.dbe = Convert.ToBoolean(checkBox7.CheckState);
		filters.Default.le_status = Convert.ToBoolean(checkBox8.CheckState);
		filters.Default.Save();
		filters.Default.Reload();
	}

	private void refresh_filters()
	{
		if (filters.Default.users)
		{
			checkBox1.Checked = true;
			if (filters.Default.usr_k)
			{
				checkBox1.ForeColor = Color.Black;
			}
			else
			{
				checkBox1.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox1.Checked = false;
		}
		if (filters.Default.responsibility)
		{
			checkBox2.Checked = true;
			if (filters.Default.resp_k)
			{
				checkBox2.ForeColor = Color.Black;
			}
			else
			{
				checkBox2.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox2.Checked = false;
		}
		if (filters.Default.rulecode)
		{
			checkBox3.Checked = true;
			if (filters.Default.rul_k)
			{
				checkBox3.ForeColor = Color.Black;
			}
			else
			{
				checkBox3.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox3.Checked = false;
		}
		if (filters.Default.reamrks)
		{
			checkBox6.Checked = true;
			if (filters.Default.rem_k)
			{
				checkBox6.ForeColor = Color.Black;
			}
			else
			{
				checkBox6.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox6.Checked = false;
		}
		if (filters.Default.dbe)
		{
			checkBox7.Checked = true;
			if (filters.Default.dbe_k)
			{
				checkBox7.ForeColor = Color.Black;
			}
			else
			{
				checkBox7.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox7.Checked = false;
		}
		if (filters.Default.priority)
		{
			checkBox4.Checked = true;
			if (filters.Default.prio_k)
			{
				checkBox4.ForeColor = Color.Black;
			}
			else
			{
				checkBox4.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox4.Checked = false;
		}
		if (filters.Default.le_details)
		{
			checkBox5.Checked = true;
			if (filters.Default.led_k)
			{
				checkBox5.ForeColor = Color.Black;
			}
			else
			{
				checkBox5.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox5.Checked = false;
		}
		if (filters.Default.le_status)
		{
			checkBox8.Checked = true;
			if (filters.Default.les_k)
			{
				checkBox8.ForeColor = Color.Black;
			}
			else
			{
				checkBox8.ForeColor = Color.DarkGray;
			}
		}
		else
		{
			checkBox8.Checked = false;
		}
	}

	private void mat_col()
	{
	}

	private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
	{
		foreach (TreeNode node in e.Node.Nodes)
		{
			node.Checked = e.Node.Checked;
		}
	}

	private void textBox8_KeyDown(object sender, KeyEventArgs e)
	{
		errorProvider1.Clear();
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (textBox8.Text != "" || textBox8.Text != null)
		{
			int index = 0;
			bool index2 = false;
			if (radioButton1.Checked)
			{
				index = 1;
			}
			if (radioButton2.Checked)
			{
				index = 2;
			}
			if (radioButton3.Checked)
			{
				index = 3;
			}
			if (radioButton4.Checked)
			{
				index = 4;
			}
			if (radioButton5.Checked)
			{
				index = 5;
			}
			if (radioButton6.Checked)
			{
				index = 6;
			}
			if (radioButton7.Checked)
			{
				index = 7;
			}
			if (radioButton8.Checked)
			{
				index = 8;
			}
			if (radioButton14.Checked)
			{
				index2 = true;
			}
			if (radioButton13.Checked)
			{
				index2 = false;
			}
			addbyselection(textBox8.Text, index, index2);
		}
		else
		{
			errorProvider1.SetError(textBox8, "Field cannot be left blank..");
			toolStripStatusLabel1.Text = "Error Occured..";
		}
	}

	private void addbyselection(string keyword, int index, bool index2)
	{
		switch (index)
		{
		case 1:
			if (!Settings.Default.Users.Contains(keyword) && keyword != "")
			{
				Settings.Default.Users.Add(keyword.ToLower());
				filters.Default.usr_k = index2;
				populate_list();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 2:
			if (!Settings.Default.Responsibility.Contains(keyword) && keyword != "")
			{
				Settings.Default.Responsibility.Add(keyword.ToUpper());
				filters.Default.resp_k = index2;
				populate_list_res();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 3:
			if (!Settings.Default.Rule_code.Contains(keyword) && keyword != "")
			{
				Settings.Default.Rule_code.Add(keyword.ToUpper());
				filters.Default.rul_k = index2;
				populate_list_rc();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 4:
			if (!Settings.Default.Priority.Contains(keyword) && keyword != "")
			{
				Settings.Default.Priority.Add(keyword.ToUpper());
				filters.Default.prio_k = index2;
				populate_list_pro();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 5:
			if (!Settings.Default.LE_Details.Contains(keyword) && keyword != "")
			{
				Settings.Default.LE_Details.Add(keyword);
				filters.Default.led_k = index2;
				populate_list_led();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 6:
			if (!Settings.Default.Remark.Contains(keyword) && keyword != "")
			{
				Settings.Default.Remark.Add(keyword);
				filters.Default.rem_k = index2;
				populate_remarks();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 7:
			if (!Settings.Default.DBE_comment.Contains(keyword) && keyword != "")
			{
				Settings.Default.DBE_comment.Add(keyword);
				filters.Default.dbe_k = index2;
				populate_dbe();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		case 8:
			if (!Settings.Default.LE_Staus.Contains(keyword) && keyword != "")
			{
				Settings.Default.LE_Staus.Add(keyword);
				filters.Default.les_k = index2;
				populate_les();
				toolStripStatusLabel1.Text = keyword + " Added..";
			}
			else
			{
				toolStripStatusLabel1.Text = keyword + " already exists..";
			}
			break;
		}
		Settings.Default.Save();
		Settings.Default.Reload();
		textBox8.Clear();
	}

	private void delentry(string keyword, int index)
	{
		switch (index)
		{
		case 1:
			Settings.Default.Users.Remove(keyword);
			populate_list();
			break;
		case 2:
			Settings.Default.Responsibility.Remove(keyword);
			populate_list_res();
			break;
		case 3:
			Settings.Default.Rule_code.Remove(keyword);
			populate_list_rc();
			break;
		case 4:
			Settings.Default.Priority.Remove(keyword);
			populate_list_pro();
			break;
		case 5:
			Settings.Default.LE_Details.Remove(keyword);
			populate_list_led();
			break;
		case 6:
			Settings.Default.Remark.Remove(keyword);
			populate_remarks();
			break;
		case 7:
			Settings.Default.DBE_comment.Remove(keyword);
			populate_dbe();
			break;
		}
		Settings.Default.Save();
		Settings.Default.Reload();
	}

	private void radioButton1_CheckedChanged(object sender, EventArgs e)
	{
		if (isload)
		{
			isload = false;
		}
		if (!isload)
		{
			if (!filters.Default.usr_k)
			{
				radioButton13.Checked = true;
			}
			else
			{
				radioButton14.Checked = true;
			}
			populate_list();
		}
	}

	private void radioButton2_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Lower;
		if (!filters.Default.resp_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_list_res();
	}

	public void radioButton3_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Upper;
		if (!filters.Default.rul_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_list_rc();
	}

	private void radioButton4_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Upper;
		if (!filters.Default.prio_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_list_pro();
	}

	private void radioButton5_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Normal;
		if (!filters.Default.led_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_list_led();
	}

	private void radioButton6_CheckedChanged(object sender, EventArgs e)
	{
		groupBox1.Enabled = false;
		textBox8.CharacterCasing = CharacterCasing.Normal;
		if (!filters.Default.rem_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_remarks();
	}

	private void radioButton7_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Normal;
		if (!filters.Default.dbe_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_dbe();
	}

	private void textBox8_TextChanged(object sender, EventArgs e)
	{
		if (textBox8.Text.Contains("IGNORE"))
		{
			radioButton4.Checked = true;
		}
		if (textBox8.Text.Contains("NAV") || textBox8.Text.Contains("COM") || textBox8.Text.Contains("CAR") || textBox8.Text.Contains("CON") || textBox8.Text.Contains("CAR") || textBox8.Text.Contains("ADM") || textBox8.Text.Contains("CRF") || textBox8.Text.Contains("GEO") || textBox8.Text.Contains("DEG") || textBox8.Text.Contains("NAM") || textBox8.Text.Contains("SGN") || textBox8.Text.Contains("ADD") || textBox8.Text.Contains("POI") || textBox8.Text.Contains("PAD"))
		{
			radioButton3.Checked = true;
		}
	}

	private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void radioButton14_CheckedChanged(object sender, EventArgs e)
	{
		int num = 0;
		if (radioButton1.Checked)
		{
			num = 1;
		}
		if (radioButton2.Checked)
		{
			num = 2;
		}
		if (radioButton3.Checked)
		{
			num = 3;
		}
		if (radioButton4.Checked)
		{
			num = 4;
		}
		if (radioButton5.Checked)
		{
			num = 5;
		}
		if (radioButton6.Checked)
		{
			num = 6;
		}
		if (radioButton7.Checked)
		{
			num = 7;
		}
		if (radioButton8.Checked)
		{
			num = 8;
		}
		switch (num)
		{
		case 1:
			filters.Default.usr_k = true;
			break;
		case 2:
			filters.Default.resp_k = true;
			break;
		case 3:
			filters.Default.rul_k = true;
			break;
		case 4:
			filters.Default.prio_k = true;
			break;
		case 5:
			filters.Default.led_k = true;
			break;
		case 6:
			filters.Default.rem_k = true;
			break;
		case 7:
			filters.Default.dbe_k = true;
			break;
		case 8:
			filters.Default.les_k = true;
			break;
		}
	}

	private void radioButton13_CheckedChanged(object sender, EventArgs e)
	{
		int num = 0;
		if (radioButton1.Checked)
		{
			num = 1;
		}
		if (radioButton2.Checked)
		{
			num = 2;
		}
		if (radioButton3.Checked)
		{
			num = 3;
		}
		if (radioButton4.Checked)
		{
			num = 4;
		}
		if (radioButton5.Checked)
		{
			num = 5;
		}
		if (radioButton6.Checked)
		{
			num = 6;
		}
		if (radioButton7.Checked)
		{
			num = 7;
		}
		if (radioButton8.Checked)
		{
			num = 8;
		}
		switch (num)
		{
		case 1:
			filters.Default.usr_k = false;
			break;
		case 2:
			filters.Default.resp_k = false;
			break;
		case 3:
			filters.Default.rul_k = false;
			break;
		case 4:
			filters.Default.prio_k = false;
			break;
		case 5:
			filters.Default.led_k = false;
			break;
		case 6:
			filters.Default.rem_k = false;
			break;
		case 7:
			filters.Default.dbe_k = false;
			break;
		case 8:
			filters.Default.les_k = false;
			break;
		}
	}

	private void checkBox7_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		checkBox1.Checked = true;
		checkBox2.Checked = true;
		checkBox3.Checked = true;
		checkBox4.Checked = true;
		checkBox5.Checked = true;
		checkBox6.Checked = true;
		checkBox7.Checked = true;
		checkBox8.Checked = true;
	}

	private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		checkBox1.Checked = false;
		checkBox2.Checked = false;
		checkBox3.Checked = false;
		checkBox4.Checked = false;
		checkBox5.Checked = false;
		checkBox6.Checked = false;
		checkBox7.Checked = false;
		checkBox8.Checked = false;
	}

	private void checkBox7_CheckedChanged_1(object sender, EventArgs e)
	{
	}

	private void radioButton8_CheckedChanged(object sender, EventArgs e)
	{
		textBox8.CharacterCasing = CharacterCasing.Upper;
		if (!filters.Default.les_k)
		{
			radioButton13.Checked = true;
		}
		else
		{
			radioButton14.Checked = true;
		}
		populate_les();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip6 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip7 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.linkLabel2 = new System.Windows.Forms.LinkLabel();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.textBox8 = new System.Windows.Forms.TextBox();
		this.contextMenuStrip8 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.radioButton7 = new System.Windows.Forms.RadioButton();
		this.radioButton6 = new System.Windows.Forms.RadioButton();
		this.radioButton5 = new System.Windows.Forms.RadioButton();
		this.radioButton4 = new System.Windows.Forms.RadioButton();
		this.radioButton3 = new System.Windows.Forms.RadioButton();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.keys = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.radioButton13 = new System.Windows.Forms.RadioButton();
		this.radioButton14 = new System.Windows.Forms.RadioButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.radioButton8 = new System.Windows.Forms.RadioButton();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.contextMenuStrip3.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.contextMenuStrip2.SuspendLayout();
		this.contextMenuStrip6.SuspendLayout();
		this.contextMenuStrip7.SuspendLayout();
		this.contextMenuStrip4.SuspendLayout();
		this.contextMenuStrip5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).BeginInit();
		this.groupBox6.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox9.SuspendLayout();
		this.groupBox10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox11.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.errorProvider1.ContainerControl = this;
		this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.deleteToolStripMenuItem2 });
		this.contextMenuStrip3.Name = "contextMenuStrip3";
		this.contextMenuStrip3.Size = new System.Drawing.Size(108, 26);
		this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
		this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
		this.deleteToolStripMenuItem2.Text = "Delete";
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.deleteToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(132, 26);
		this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStrip1_Opening);
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
		this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
		this.deleteToolStripMenuItem.Text = "Delete";
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.deleteToolStripMenuItem1 });
		this.contextMenuStrip2.Name = "contextMenuStrip2";
		this.contextMenuStrip2.Size = new System.Drawing.Size(108, 26);
		this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
		this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
		this.deleteToolStripMenuItem1.Text = "Delete";
		this.deleteToolStripMenuItem1.Click += new System.EventHandler(deleteToolStripMenuItem1_Click);
		this.contextMenuStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripMenuItem3 });
		this.contextMenuStrip6.Name = "contextMenuStrip3";
		this.contextMenuStrip6.Size = new System.Drawing.Size(108, 26);
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(107, 22);
		this.toolStripMenuItem3.Text = "Delete";
		this.contextMenuStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripMenuItem4 });
		this.contextMenuStrip7.Name = "contextMenuStrip3";
		this.contextMenuStrip7.Size = new System.Drawing.Size(108, 26);
		this.contextMenuStrip7.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStrip7_Opening);
		this.toolStripMenuItem4.Name = "toolStripMenuItem4";
		this.toolStripMenuItem4.Size = new System.Drawing.Size(107, 22);
		this.toolStripMenuItem4.Text = "Delete";
		this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripMenuItem1 });
		this.contextMenuStrip4.Name = "contextMenuStrip3";
		this.contextMenuStrip4.Size = new System.Drawing.Size(108, 26);
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
		this.toolStripMenuItem1.Text = "Delete";
		this.contextMenuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripMenuItem2 });
		this.contextMenuStrip5.Name = "contextMenuStrip3";
		this.contextMenuStrip5.Size = new System.Drawing.Size(108, 26);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
		this.toolStripMenuItem2.Text = "Delete";
		this.errorProvider2.ContainerControl = this;
		this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox6.Controls.Add(this.groupBox3);
		this.groupBox6.Controls.Add(this.groupBox2);
		this.groupBox6.Location = new System.Drawing.Point(12, 7);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(146, 284);
		this.groupBox6.TabIndex = 8;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Enable / Disable Filters";
		this.groupBox3.Controls.Add(this.checkBox8);
		this.groupBox3.Controls.Add(this.checkBox7);
		this.groupBox3.Controls.Add(this.checkBox6);
		this.groupBox3.Controls.Add(this.checkBox5);
		this.groupBox3.Controls.Add(this.checkBox4);
		this.groupBox3.Controls.Add(this.checkBox3);
		this.groupBox3.Controls.Add(this.checkBox2);
		this.groupBox3.Controls.Add(this.checkBox1);
		this.groupBox3.Location = new System.Drawing.Point(11, 66);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(124, 208);
		this.groupBox3.TabIndex = 9;
		this.groupBox3.TabStop = false;
		this.toolTip1.SetToolTip(this.groupBox3, "Applied / Unapplied Filters");
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(16, 159);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(97, 17);
		this.checkBox7.TabIndex = 13;
		this.checkBox7.Text = "DBE Comments";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox7.CheckedChanged += new System.EventHandler(checkBox7_CheckedChanged_1);
		this.checkBox6.AutoSize = true;
		this.checkBox6.Location = new System.Drawing.Point(16, 136);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(67, 17);
		this.checkBox6.TabIndex = 12;
		this.checkBox6.Text = "Remarks";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Location = new System.Drawing.Point(16, 113);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(72, 17);
		this.checkBox5.TabIndex = 11;
		this.checkBox5.Text = "LE Details";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(16, 90);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(61, 17);
		this.checkBox4.TabIndex = 10;
		this.checkBox4.Text = "Priority";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(16, 67);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(73, 17);
		this.checkBox3.TabIndex = 9;
		this.checkBox3.Text = "Rule Code";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(16, 44);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(93, 17);
		this.checkBox2.TabIndex = 8;
		this.checkBox2.Text = "Responsibility";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(16, 21);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(53, 17);
		this.checkBox1.TabIndex = 7;
		this.checkBox1.Text = "Users";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.linkLabel2);
		this.groupBox2.Controls.Add(this.linkLabel1);
		this.groupBox2.Location = new System.Drawing.Point(11, 17);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(124, 48);
		this.groupBox2.TabIndex = 8;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "All Filters";
		this.toolTip1.SetToolTip(this.groupBox2, "Enable / Disable all filters at once.");
		this.linkLabel2.AutoSize = true;
		this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
		this.linkLabel2.Location = new System.Drawing.Point(70, 22);
		this.linkLabel2.Name = "linkLabel2";
		this.linkLabel2.Size = new System.Drawing.Size(43, 13);
		this.linkLabel2.TabIndex = 1;
		this.linkLabel2.TabStop = true;
		this.linkLabel2.Text = "Disable";
		this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
		this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel2_LinkClicked);
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(13, 22);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(39, 13);
		this.linkLabel1.TabIndex = 0;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "Enable";
		this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.groupBox9.Controls.Add(this.textBox8);
		this.groupBox9.Controls.Add(this.label8);
		this.groupBox9.Location = new System.Drawing.Point(12, 297);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(283, 57);
		this.groupBox9.TabIndex = 9;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "Add New Filter";
		this.toolTip1.SetToolTip(this.groupBox9, "Add New Filter parameter to the selected filter category");
		this.textBox8.ContextMenuStrip = this.contextMenuStrip8;
		this.textBox8.Location = new System.Drawing.Point(99, 20);
		this.textBox8.Name = "textBox8";
		this.textBox8.Size = new System.Drawing.Size(162, 21);
		this.textBox8.TabIndex = 1;
		this.textBox8.TextChanged += new System.EventHandler(textBox8_TextChanged);
		this.textBox8.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox8_KeyDown);
		this.contextMenuStrip8.Name = "contextMenuStrip8";
		this.contextMenuStrip8.Size = new System.Drawing.Size(61, 4);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(13, 23);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(80, 13);
		this.label8.TabIndex = 0;
		this.label8.Text = "Enter Keyword :";
		this.groupBox10.Controls.Add(this.radioButton8);
		this.groupBox10.Controls.Add(this.radioButton7);
		this.groupBox10.Controls.Add(this.radioButton6);
		this.groupBox10.Controls.Add(this.radioButton5);
		this.groupBox10.Controls.Add(this.radioButton4);
		this.groupBox10.Controls.Add(this.radioButton3);
		this.groupBox10.Controls.Add(this.radioButton2);
		this.groupBox10.Controls.Add(this.radioButton1);
		this.groupBox10.Location = new System.Drawing.Point(164, 7);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(131, 208);
		this.groupBox10.TabIndex = 2;
		this.groupBox10.TabStop = false;
		this.groupBox10.Text = "Filter Category";
		this.toolTip1.SetToolTip(this.groupBox10, "Filter categories. Select one to see its corresponding parameters");
		this.radioButton7.AutoSize = true;
		this.radioButton7.Location = new System.Drawing.Point(17, 158);
		this.radioButton7.Name = "radioButton7";
		this.radioButton7.Size = new System.Drawing.Size(96, 17);
		this.radioButton7.TabIndex = 6;
		this.radioButton7.TabStop = true;
		this.radioButton7.Text = "DBE Comments";
		this.radioButton7.UseVisualStyleBackColor = true;
		this.radioButton7.CheckedChanged += new System.EventHandler(radioButton7_CheckedChanged);
		this.radioButton6.AutoSize = true;
		this.radioButton6.Location = new System.Drawing.Point(17, 135);
		this.radioButton6.Name = "radioButton6";
		this.radioButton6.Size = new System.Drawing.Size(66, 17);
		this.radioButton6.TabIndex = 5;
		this.radioButton6.TabStop = true;
		this.radioButton6.Text = "Remarks";
		this.radioButton6.UseVisualStyleBackColor = true;
		this.radioButton6.CheckedChanged += new System.EventHandler(radioButton6_CheckedChanged);
		this.radioButton5.AutoSize = true;
		this.radioButton5.Location = new System.Drawing.Point(17, 112);
		this.radioButton5.Name = "radioButton5";
		this.radioButton5.Size = new System.Drawing.Size(71, 17);
		this.radioButton5.TabIndex = 4;
		this.radioButton5.TabStop = true;
		this.radioButton5.Text = "LE Details";
		this.radioButton5.UseVisualStyleBackColor = true;
		this.radioButton5.CheckedChanged += new System.EventHandler(radioButton5_CheckedChanged);
		this.radioButton4.AutoSize = true;
		this.radioButton4.Location = new System.Drawing.Point(17, 89);
		this.radioButton4.Name = "radioButton4";
		this.radioButton4.Size = new System.Drawing.Size(60, 17);
		this.radioButton4.TabIndex = 3;
		this.radioButton4.TabStop = true;
		this.radioButton4.Text = "Priority";
		this.radioButton4.UseVisualStyleBackColor = true;
		this.radioButton4.CheckedChanged += new System.EventHandler(radioButton4_CheckedChanged);
		this.radioButton3.AutoSize = true;
		this.radioButton3.Location = new System.Drawing.Point(17, 66);
		this.radioButton3.Name = "radioButton3";
		this.radioButton3.Size = new System.Drawing.Size(72, 17);
		this.radioButton3.TabIndex = 2;
		this.radioButton3.TabStop = true;
		this.radioButton3.Text = "Rule Code";
		this.radioButton3.UseVisualStyleBackColor = true;
		this.radioButton3.CheckedChanged += new System.EventHandler(radioButton3_CheckedChanged);
		this.radioButton2.AutoSize = true;
		this.radioButton2.Location = new System.Drawing.Point(17, 43);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(92, 17);
		this.radioButton2.TabIndex = 1;
		this.radioButton2.TabStop = true;
		this.radioButton2.Text = "Responsibility";
		this.radioButton2.UseVisualStyleBackColor = true;
		this.radioButton2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
		this.radioButton1.AutoSize = true;
		this.radioButton1.Location = new System.Drawing.Point(17, 20);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(52, 17);
		this.radioButton1.TabIndex = 0;
		this.radioButton1.TabStop = true;
		this.radioButton1.Text = "Users";
		this.radioButton1.UseVisualStyleBackColor = true;
		this.radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.keys);
		this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 17);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(202, 304);
		this.dataGridView1.TabIndex = 10;
		this.keys.HeaderText = "Keyword";
		this.keys.Name = "keys";
		this.keys.ReadOnly = true;
		this.groupBox11.Controls.Add(this.dataGridView1);
		this.groupBox11.Location = new System.Drawing.Point(301, 7);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(208, 324);
		this.groupBox11.TabIndex = 11;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "Filter Entries";
		this.statusStrip1.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripStatusLabel1 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 357);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(521, 22);
		this.statusStrip1.TabIndex = 12;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
		this.toolStripStatusLabel1.Text = "-";
		this.groupBox1.Controls.Add(this.radioButton13);
		this.groupBox1.Controls.Add(this.radioButton14);
		this.groupBox1.Location = new System.Drawing.Point(164, 221);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(131, 70);
		this.groupBox1.TabIndex = 7;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Retain";
		this.toolTip1.SetToolTip(this.groupBox1, "Select weather to retain a paricular parameter or not");
		this.radioButton13.AutoSize = true;
		this.radioButton13.Location = new System.Drawing.Point(17, 43);
		this.radioButton13.Name = "radioButton13";
		this.radioButton13.Size = new System.Drawing.Size(50, 17);
		this.radioButton13.TabIndex = 1;
		this.radioButton13.TabStop = true;
		this.radioButton13.Text = "False";
		this.radioButton13.UseVisualStyleBackColor = true;
		this.radioButton13.CheckedChanged += new System.EventHandler(radioButton13_CheckedChanged);
		this.radioButton14.AutoSize = true;
		this.radioButton14.Location = new System.Drawing.Point(17, 20);
		this.radioButton14.Name = "radioButton14";
		this.radioButton14.Size = new System.Drawing.Size(45, 17);
		this.radioButton14.TabIndex = 0;
		this.radioButton14.TabStop = true;
		this.radioButton14.Text = "True";
		this.radioButton14.UseVisualStyleBackColor = true;
		this.radioButton14.CheckedChanged += new System.EventHandler(radioButton14_CheckedChanged);
		this.radioButton8.AutoSize = true;
		this.radioButton8.Location = new System.Drawing.Point(17, 181);
		this.radioButton8.Name = "radioButton8";
		this.radioButton8.Size = new System.Drawing.Size(67, 17);
		this.radioButton8.TabIndex = 7;
		this.radioButton8.TabStop = true;
		this.radioButton8.Text = "LE Status";
		this.radioButton8.UseVisualStyleBackColor = true;
		this.radioButton8.CheckedChanged += new System.EventHandler(radioButton8_CheckedChanged);
		this.checkBox8.AutoSize = true;
		this.checkBox8.Location = new System.Drawing.Point(16, 182);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(68, 17);
		this.checkBox8.TabIndex = 14;
		this.checkBox8.Text = "LE Status";
		this.checkBox8.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(521, 379);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.groupBox10);
		base.Controls.Add(this.groupBox11);
		base.Controls.Add(this.groupBox9);
		base.Controls.Add(this.groupBox6);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Users";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Manage Filters";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Users_FormClosing);
		base.Load += new System.EventHandler(Users_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.contextMenuStrip3.ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		this.contextMenuStrip2.ResumeLayout(false);
		this.contextMenuStrip6.ResumeLayout(false);
		this.contextMenuStrip7.ResumeLayout(false);
		this.contextMenuStrip4.ResumeLayout(false);
		this.contextMenuStrip5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).EndInit();
		this.groupBox6.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.groupBox10.ResumeLayout(false);
		this.groupBox10.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox11.ResumeLayout(false);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}

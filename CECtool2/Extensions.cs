using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CECtool2;

public static class Extensions
{
	public static string DataTableToCSV(this DataTable datatable, char seperator)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < datatable.Columns.Count; i++)
		{
			stringBuilder.Append(datatable.Columns[i]);
			if (i < datatable.Columns.Count - 1)
			{
				stringBuilder.Append(seperator);
			}
		}
		stringBuilder.AppendLine();
		foreach (DataRow row in datatable.Rows)
		{
			for (int i = 0; i < datatable.Columns.Count; i++)
			{
				stringBuilder.Append(row[i].ToString());
				if (i < datatable.Columns.Count - 1)
				{
					stringBuilder.Append(seperator);
				}
			}
			stringBuilder.AppendLine();
		}
		return stringBuilder.ToString();
	}

	public static void DoubleBuffered(this DataGridView dgv, bool setting)
	{
		Type type = dgv.GetType();
		PropertyInfo property = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
		property.SetValue(dgv, setting, null);
	}
}

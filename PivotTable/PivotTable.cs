using System;
using System.Collections.Generic;
using System.Data;

namespace PivotTable;

public static class PivotTable
{
	public static DataTable GetInversedDataTable(DataTable table, string columnX, params string[] columnsToIgnore)
	{
		DataTable dataTable = new DataTable();
		if (columnX == "")
		{
			columnX = table.Columns[0].ColumnName;
		}
		dataTable.Columns.Add(columnX);
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		if (columnsToIgnore.Length > 0)
		{
			list2.AddRange(columnsToIgnore);
		}
		if (!list2.Contains(columnX))
		{
			list2.Add(columnX);
		}
		foreach (DataRow row in table.Rows)
		{
			string text = row[columnX].ToString();
			if (!list.Contains(text))
			{
				list.Add(text);
				dataTable.Columns.Add(text);
				continue;
			}
			throw new Exception("The inversion used must have unique values for column " + columnX);
		}
		foreach (DataColumn column in table.Columns)
		{
			if (!list.Contains(column.ColumnName) && !list2.Contains(column.ColumnName))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = column.ColumnName;
				dataTable.Rows.Add(dataRow);
			}
		}
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 1; j < dataTable.Columns.Count; j++)
			{
				dataTable.Rows[i][j] = table.Rows[j - 1][dataTable.Rows[i][0].ToString()].ToString();
			}
		}
		return dataTable;
	}

	public static DataTable GetInversedDataTable(DataTable table, string columnX, string columnY, string columnZ, string nullValue, bool sumValues)
	{
		DataTable dataTable = new DataTable();
		if (columnX == "")
		{
			columnX = table.Columns[0].ColumnName;
		}
		dataTable.Columns.Add(columnY);
		List<string> list = new List<string>();
		foreach (DataRow row in table.Rows)
		{
			string text = row[columnX].ToString();
			if (!list.Contains(text))
			{
				list.Add(text);
				dataTable.Columns.Add(text);
			}
		}
		if (columnY != "" && columnZ != "")
		{
			List<string> list2 = new List<string>();
			foreach (DataRow row2 in table.Rows)
			{
				if (!list2.Contains(row2[columnY].ToString()))
				{
					list2.Add(row2[columnY].ToString());
				}
			}
			foreach (string item in list2)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2[0] = item;
				DataRow[] array = table.Select(columnY + "='" + item + "'");
				DataRow[] array2 = array;
				foreach (DataRow dataRow in array2)
				{
					string text2 = dataRow[columnX].ToString();
					foreach (DataColumn column in dataTable.Columns)
					{
						if (!(column.ColumnName == text2))
						{
							continue;
						}
						if (sumValues)
						{
							try
							{
								dataRow2[text2] = Convert.ToDecimal(dataRow2[text2]) + Convert.ToDecimal(dataRow[columnZ]);
							}
							catch
							{
								dataRow2[text2] = dataRow[columnZ];
							}
						}
						else
						{
							dataRow2[text2] = dataRow[columnZ];
						}
					}
				}
				dataTable.Rows.Add(dataRow2);
			}
			if (nullValue != "")
			{
				foreach (DataRow row3 in dataTable.Rows)
				{
					foreach (DataColumn column2 in dataTable.Columns)
					{
						if (row3[column2.ColumnName].ToString() == "")
						{
							row3[column2.ColumnName] = nullValue;
						}
					}
				}
			}
			return dataTable;
		}
		throw new Exception("The columns to perform inversion are not provided");
	}
}

#define TRACE
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExportToExcel;

public class CreateExcelFile
{
	private const int DATE_FORMAT_ID = 1;

	public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
	{
		DataSet dataSet = new DataSet();
		dataSet.Tables.Add(ListToDataTable(list));
		return CreateExcelDocument(dataSet, xlsxFilePath);
	}

	public static DataTable ListToDataTable<T>(List<T> list)
	{
		DataTable dataTable = new DataTable();
		PropertyInfo[] properties = typeof(T).GetProperties();
		foreach (PropertyInfo propertyInfo in properties)
		{
			dataTable.Columns.Add(new DataColumn(propertyInfo.Name, GetNullableType(propertyInfo.PropertyType)));
		}
		foreach (T item in list)
		{
			DataRow dataRow = dataTable.NewRow();
			properties = typeof(T).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!IsNullableType(propertyInfo.PropertyType))
				{
					dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null);
				}
				else
				{
					dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null) ?? DBNull.Value;
				}
			}
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}

	private static Type GetNullableType(Type t)
	{
		Type result = t;
		if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
		{
			result = Nullable.GetUnderlyingType(t);
		}
		return result;
	}

	private static bool IsNullableType(Type type)
	{
		return type == typeof(string) || type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
	}

	public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
	{
		DataSet dataSet = new DataSet();
		dataSet.Tables.Add(dt);
		bool result = CreateExcelDocument(dataSet, xlsxFilePath);
		dataSet.Tables.Remove(dt);
		return result;
	}

	public static bool CreateExcelDocument(DataSet ds, string excelFilename)
	{
		try
		{
			SpreadsheetDocument val = SpreadsheetDocument.Create(excelFilename, (SpreadsheetDocumentType)0);
			try
			{
				WriteExcelFile(ds, val);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			Trace.WriteLine("Successfully created: " + excelFilename);
			return true;
		}
		catch (Exception ex)
		{
			Trace.WriteLine("Failed, exception thrown: " + ex.Message);
			return false;
		}
	}

	private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		spreadsheet.AddWorkbookPart();
		spreadsheet.get_WorkbookPart().set_Workbook(new Workbook());
		((OpenXmlElement)spreadsheet.get_WorkbookPart().get_Workbook()).Append((OpenXmlElement[])(object)new OpenXmlElement[1] { (OpenXmlElement)new BookViews((OpenXmlElement[])(object)new OpenXmlElement[1] { (OpenXmlElement)new WorkbookView() }) });
		WorkbookStylesPart val = ((OpenXmlPartContainer)spreadsheet.get_WorkbookPart()).AddNewPart<WorkbookStylesPart>("rIdStyles");
		Stylesheet stylesheet = new Stylesheet();
		val.set_Stylesheet(stylesheet);
		uint num = 1u;
		Sheets val2 = ((OpenXmlElement)spreadsheet.get_WorkbookPart().get_Workbook()).AppendChild<Sheets>(new Sheets());
		foreach (DataTable table in ds.Tables)
		{
			string tableName = table.TableName;
			WorksheetPart val3 = ((OpenXmlPartContainer)spreadsheet.get_WorkbookPart()).AddNewPart<WorksheetPart>();
			Sheet val4 = new Sheet();
			val4.set_Id(StringValue.op_Implicit(((OpenXmlPartContainer)spreadsheet.get_WorkbookPart()).GetIdOfPart((OpenXmlPart)(object)val3)));
			val4.set_SheetId(UInt32Value.op_Implicit(num));
			val4.set_Name(StringValue.op_Implicit(tableName));
			Sheet val5 = val4;
			((OpenXmlElement)val2).Append((OpenXmlElement[])(object)new OpenXmlElement[1] { (OpenXmlElement)val5 });
			WriteDataTableToExcelWorksheet(table, val3);
			num++;
		}
		((OpenXmlPartRootElement)spreadsheet.get_WorkbookPart().get_Workbook()).Save();
	}

	private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Expected O, but got Unknown
		OpenXmlWriter writer = OpenXmlWriter.Create((OpenXmlPart)(object)worksheetPart, Encoding.ASCII);
		writer.WriteStartElement((OpenXmlElement)new Worksheet());
		writer.WriteStartElement((OpenXmlElement)new SheetData());
		string text = "";
		int count = dt.Columns.Count;
		bool[] array = new bool[count];
		bool[] array2 = new bool[count];
		string[] array3 = new string[count];
		for (int i = 0; i < count; i++)
		{
			array3[i] = GetExcelColumnName(i);
		}
		uint num = 1u;
		OpenXmlWriter obj = writer;
		Row val = new Row();
		val.set_RowIndex(UInt32Value.op_Implicit(num));
		obj.WriteStartElement((OpenXmlElement)(object)val);
		for (int j = 0; j < count; j++)
		{
			DataColumn dataColumn = dt.Columns[j];
			AppendTextCell(array3[j] + "1", dataColumn.ColumnName, ref writer);
			array[j] = dataColumn.DataType.FullName == "System.Decimal" || dataColumn.DataType.FullName == "System.Int32" || dataColumn.DataType.FullName == "System.Double" || dataColumn.DataType.FullName == "System.Single";
			array2[j] = dataColumn.DataType.FullName == "System.DateTime";
		}
		writer.WriteEndElement();
		double num2 = 0.0;
		foreach (DataRow row in dt.Rows)
		{
			num++;
			OpenXmlWriter obj2 = writer;
			Row val2 = new Row();
			val2.set_RowIndex(UInt32Value.op_Implicit(num));
			obj2.WriteStartElement((OpenXmlElement)(object)val2);
			for (int j = 0; j < count; j++)
			{
				text = row.ItemArray[j].ToString();
				text = ReplaceHexadecimalSymbols(text);
				if (array[j])
				{
					num2 = 0.0;
					if (double.TryParse(text, out num2))
					{
						text = num2.ToString();
						AppendNumericCell(array3[j] + num, text, ref writer);
					}
				}
				else if (array2[j])
				{
					string cellStringValue = "";
					if (DateTime.TryParse(text, out var result))
					{
						cellStringValue = result.ToShortDateString();
					}
					AppendTextCell(array3[j] + num, cellStringValue, ref writer);
				}
				else
				{
					AppendTextCell(array3[j] + num, text, ref writer);
				}
			}
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.Close();
	}

	private static void AppendTextCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Expected O, but got Unknown
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		OpenXmlWriter obj = writer;
		Cell val = new Cell();
		((CellType)val).set_CellValue(new CellValue(cellStringValue));
		((CellType)val).set_CellReference(StringValue.op_Implicit(cellReference));
		((CellType)val).set_DataType(EnumValue<CellValues>.op_Implicit((CellValues)4));
		obj.WriteElement((OpenXmlElement)(object)val);
	}

	private static void AppendNumericCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Expected O, but got Unknown
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		OpenXmlWriter obj = writer;
		Cell val = new Cell();
		((CellType)val).set_CellValue(new CellValue(cellStringValue));
		((CellType)val).set_CellReference(StringValue.op_Implicit(cellReference));
		((CellType)val).set_DataType(EnumValue<CellValues>.op_Implicit((CellValues)1));
		obj.WriteElement((OpenXmlElement)(object)val);
	}

	private static string ReplaceHexadecimalSymbols(string txt)
	{
		string pattern = "[\0-\b\v\f\u000e-\u001f&]";
		return Regex.Replace(txt, pattern, "", RegexOptions.Compiled);
	}

	public static string GetExcelColumnName(int columnIndex)
	{
		if (columnIndex < 26)
		{
			return ((char)(65 + columnIndex)).ToString();
		}
		char c;
		char c2;
		if (columnIndex < 702)
		{
			c = (char)(65 + columnIndex / 26 - 1);
			c2 = (char)(65 + columnIndex % 26);
			return $"{c}{c2}";
		}
		int num = columnIndex / 26 / 26;
		int num2 = (columnIndex - num * 26 * 26) / 26;
		if (num2 == 0)
		{
			num2 = 26;
			num--;
		}
		int num3 = columnIndex - num * 26 * 26 - num2 * 26;
		c = (char)(65 + num - 1);
		c2 = (char)(65 + num2 - 1);
		char c3 = (char)(65 + num3);
		return $"{c}{c2}{c3}";
	}
}

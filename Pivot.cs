using System;
using System.Data;
using System.Linq;

public class Pivot
{
	private DataTable _SourceTable = new DataTable();

	public Pivot(DataTable SourceTable)
	{
		_SourceTable = SourceTable;
	}

	public DataTable PivotData(string RowField, string DataField, AggregateFunction Aggregate, params string[] ColumnFields)
	{
		DataTable dataTable = new DataTable();
		string Separator = ".";
		var enumerable = (from x in _SourceTable.AsEnumerable()
			select new
			{
				Name = x.Field<object>(RowField)
			}).Distinct();
		var orderedEnumerable = from m in (from x in _SourceTable.AsEnumerable()
				select new
				{
					Name = ColumnFields.Select((string n) => x.Field<object>(n)).Aggregate((object a, object b) => a = string.Concat(a, Separator, b.ToString()))
				}).Distinct()
			orderby m.Name
			select m;
		dataTable.Columns.Add(RowField);
		foreach (var item in orderedEnumerable)
		{
			dataTable.Columns.Add(item.Name.ToString());
		}
		foreach (var item2 in enumerable)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow[RowField] = item2.Name.ToString();
			foreach (var item3 in orderedEnumerable)
			{
				string text = string.Concat(RowField, " = '", item2.Name, "'");
				string[] array = item3.Name.ToString().Split(Separator.ToCharArray(), StringSplitOptions.None);
				for (int i = 0; i < ColumnFields.Length; i++)
				{
					string text2 = text;
					text = text2 + " and " + ColumnFields[i] + " = '" + array[i] + "'";
				}
				dataRow[item3.Name.ToString()] = GetData(text, DataField, Aggregate);
			}
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}

	private object GetData(string Filter, string DataField, AggregateFunction Aggregate)
	{
		try
		{
			DataRow[] source = _SourceTable.Select(Filter);
			object[] array = source.Select((DataRow x) => x.Field<object>(DataField)).ToArray();
			return Aggregate switch
			{
				AggregateFunction.Average => GetAverage(array), 
				AggregateFunction.Count => array.Count(), 
				AggregateFunction.Exists => (array.Count() == 0) ? "False" : "True", 
				AggregateFunction.First => GetFirst(array), 
				AggregateFunction.Last => GetLast(array), 
				AggregateFunction.Max => GetMax(array), 
				AggregateFunction.Min => GetMin(array), 
				AggregateFunction.Sum => GetSum(array), 
				_ => null, 
			};
		}
		catch (Exception)
		{
			return "#Error";
		}
	}

	private object GetAverage(object[] objList)
	{
		return (objList.Count() == 0) ? null : ((object)(Convert.ToDecimal(GetSum(objList)) / (decimal)objList.Count()));
	}

	private object GetSum(object[] objList)
	{
		return (objList.Count() == 0) ? null : ((object)objList.Aggregate(0m, (decimal x, object y) => x += Convert.ToDecimal(y)));
	}

	private object GetFirst(object[] objList)
	{
		return (objList.Count() == 0) ? null : objList.First();
	}

	private object GetLast(object[] objList)
	{
		return (objList.Count() == 0) ? null : objList.Last();
	}

	private object GetMax(object[] objList)
	{
		return (objList.Count() == 0) ? null : objList.Max();
	}

	private object GetMin(object[] objList)
	{
		return (objList.Count() == 0) ? null : objList.Min();
	}
}

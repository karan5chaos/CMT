using System;
using System.Xml.Linq;

namespace CECtool2;

internal class Csvconverter
{
	public static XDocument ConvertCsvToXML(string csvString, string[] separatorField)
	{
		string[] separator = new string[1] { "\r\n" };
		string[] array = csvString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"));
		XElement xElement = new XElement("root");
		for (int i = 0; i < array.Length; i++)
		{
			if (i > 0)
			{
				xElement.Add(rowCreator(array[i], array[0], separatorField));
			}
		}
		xDocument.Add(xElement);
		return xDocument;
	}

	private static XElement rowCreator(string row, string firstRow, string[] separatorField)
	{
		string[] array = row.Split(separatorField, StringSplitOptions.None);
		string[] array2 = firstRow.Split(separatorField, StringSplitOptions.None);
		XElement xElement = new XElement("exception");
		for (int i = 0; i < array.Length; i++)
		{
			XElement content = new XElement("var", new XAttribute("name", array2[i]), new XAttribute("value", array[i]));
			xElement.Add(content);
		}
		return xElement;
	}
}

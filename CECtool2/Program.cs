using System;
using System.Reflection;
using System.Windows.Forms;

namespace CECtool2;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		string embeddedResource = "CECtool2.Resources.ClosedXML.dll";
		EmbeddedAssembly.Load(embeddedResource, "ClosedXML.dll");
		string embeddedResource2 = "CECtool2.Resources.DocumentFormat.OpenXml.dll";
		EmbeddedAssembly.Load(embeddedResource2, "DocumentFormat.OpenXml.dll");
		string embeddedResource3 = "CECtool2.Resources.LumenWorks.Framework.IO.dll";
		EmbeddedAssembly.Load(embeddedResource3, "LumenWorks.Framework.IO.dll");
		string embeddedResource4 = "CECtool2.Resources.Newtonsoft.Json.dll";
		EmbeddedAssembly.Load(embeddedResource4, "Newtonsoft.Json.dll");
		AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Form1());
	}

	private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
	{
		return EmbeddedAssembly.Get(args.Name);
	}
}

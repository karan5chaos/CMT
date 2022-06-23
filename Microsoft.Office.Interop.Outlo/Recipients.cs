using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Outlook;

[ComImport]
[TypeIdentifier]
[DefaultMember("Item")]
[Guid("0006303B-0000-0000-C000-000000000046")]
[CompilerGenerated]
public interface Recipients : IEnumerable
{
	void _VtblGap1_8();

	[DispId(126)]
	bool ResolveAll();
}

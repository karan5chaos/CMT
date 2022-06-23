using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CECtool2;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
internal sealed class filters : ApplicationSettingsBase
{
	private static filters defaultInstance = (filters)SettingsBase.Synchronized(new filters());

	public static filters Default => defaultInstance;

	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	[UserScopedSetting]
	public bool users
	{
		get
		{
			return (bool)this["users"];
		}
		set
		{
			this["users"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("True")]
	[DebuggerNonUserCode]
	public bool responsibility
	{
		get
		{
			return (bool)this["responsibility"];
		}
		set
		{
			this["responsibility"] = value;
		}
	}

	[DefaultSettingValue("True")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool rulecode
	{
		get
		{
			return (bool)this["rulecode"];
		}
		set
		{
			this["rulecode"] = value;
		}
	}

	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	[UserScopedSetting]
	public bool reamrks
	{
		get
		{
			return (bool)this["reamrks"];
		}
		set
		{
			this["reamrks"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("True")]
	[DebuggerNonUserCode]
	public bool dbe
	{
		get
		{
			return (bool)this["dbe"];
		}
		set
		{
			this["dbe"] = value;
		}
	}

	[DefaultSettingValue("e")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public char r_i
	{
		get
		{
			return (char)this["r_i"];
		}
		set
		{
			this["r_i"] = value;
		}
	}

	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	[UserScopedSetting]
	public bool priority
	{
		get
		{
			return (bool)this["priority"];
		}
		set
		{
			this["priority"] = value;
		}
	}

	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	[UserScopedSetting]
	public bool le_details
	{
		get
		{
			return (bool)this["le_details"];
		}
		set
		{
			this["le_details"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool usr_k
	{
		get
		{
			return (bool)this["usr_k"];
		}
		set
		{
			this["usr_k"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public bool resp_k
	{
		get
		{
			return (bool)this["resp_k"];
		}
		set
		{
			this["resp_k"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool rul_k
	{
		get
		{
			return (bool)this["rul_k"];
		}
		set
		{
			this["rul_k"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool dbe_k
	{
		get
		{
			return (bool)this["dbe_k"];
		}
		set
		{
			this["dbe_k"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool prio_k
	{
		get
		{
			return (bool)this["prio_k"];
		}
		set
		{
			this["prio_k"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool led_k
	{
		get
		{
			return (bool)this["led_k"];
		}
		set
		{
			this["led_k"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool rem_k
	{
		get
		{
			return (bool)this["rem_k"];
		}
		set
		{
			this["rem_k"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("False")]
	[DebuggerNonUserCode]
	public bool les_k
	{
		get
		{
			return (bool)this["les_k"];
		}
		set
		{
			this["les_k"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("False")]
	public bool le_status
	{
		get
		{
			return (bool)this["le_status"];
		}
		set
		{
			this["le_status"] = value;
		}
	}
}

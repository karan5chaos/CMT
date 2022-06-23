using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CECtool2.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
[CompilerGenerated]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>add_users</string>\r\n</ArrayOfString>")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public StringCollection Users
	{
		get
		{
			return (StringCollection)this["Users"];
		}
		set
		{
			this["Users"] = value;
		}
	}

	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>GENERAL</string>\r\n</ArrayOfString>")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public StringCollection Responsibility
	{
		get
		{
			return (StringCollection)this["Responsibility"];
		}
		set
		{
			this["Responsibility"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>NAV222</string>\r\n</ArrayOfString>")]
	public StringCollection Rule_code
	{
		get
		{
			return (StringCollection)this["Rule_code"];
		}
		set
		{
			this["Rule_code"] = value;
		}
	}

	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	[UserScopedSetting]
	public int Threshold
	{
		get
		{
			return (int)this["Threshold"];
		}
		set
		{
			this["Threshold"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("_profile")]
	[DebuggerNonUserCode]
	public string Profile
	{
		get
		{
			return (string)this["Profile"];
		}
		set
		{
			this["Profile"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public bool Populate
	{
		get
		{
			return (bool)this["Populate"];
		}
		set
		{
			this["Populate"] = value;
		}
	}

	[DefaultSettingValue("False")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public bool Gen_Mail
	{
		get
		{
			return (bool)this["Gen_Mail"];
		}
		set
		{
			this["Gen_Mail"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("True")]
	public bool ignore
	{
		get
		{
			return (bool)this["ignore"];
		}
		set
		{
			this["ignore"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>_comment</string>\r\n</ArrayOfString>")]
	public StringCollection DBE_comment
	{
		get
		{
			return (StringCollection)this["DBE_comment"];
		}
		set
		{
			this["DBE_comment"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>_remark</string>\r\n</ArrayOfString>")]
	[DebuggerNonUserCode]
	public StringCollection Remark
	{
		get
		{
			return (StringCollection)this["Remark"];
		}
		set
		{
			this["Remark"] = value;
		}
	}

	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>_priority</string>\r\n</ArrayOfString>")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public StringCollection Priority
	{
		get
		{
			return (StringCollection)this["Priority"];
		}
		set
		{
			this["Priority"] = value;
		}
	}

	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>_details</string>\r\n</ArrayOfString>")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public StringCollection LE_Details
	{
		get
		{
			return (StringCollection)this["LE_Details"];
		}
		set
		{
			this["LE_Details"] = value;
		}
	}

	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string Headcount_path
	{
		get
		{
			return (string)this["Headcount_path"];
		}
		set
		{
			this["Headcount_path"] = value;
		}
	}

	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string S_path
	{
		get
		{
			return (string)this["S_path"];
		}
		set
		{
			this["S_path"] = value;
		}
	}

	[DefaultSettingValue("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string>ACTIVE</string>\r\n</ArrayOfString>")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public StringCollection LE_Staus
	{
		get
		{
			return (StringCollection)this["LE_Staus"];
		}
		set
		{
			this["LE_Staus"] = value;
		}
	}

	private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
	{
	}

	private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
	{
	}
}

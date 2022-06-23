using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CECtool2;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
[CompilerGenerated]
internal sealed class email : ApplicationSettingsBase
{
	private static email defaultInstance = (email)SettingsBase.Synchronized(new email());

	public static email Default => defaultInstance;

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("")]
	public string to
	{
		get
		{
			return (string)this["to"];
		}
		set
		{
			this["to"] = value;
		}
	}

	[DefaultSettingValue("")]
	[UserScopedSetting]
	[DebuggerNonUserCode]
	public string cc
	{
		get
		{
			return (string)this["cc"];
		}
		set
		{
			this["cc"] = value;
		}
	}

	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string message
	{
		get
		{
			return (string)this["message"];
		}
		set
		{
			this["message"] = value;
		}
	}

	[DebuggerNonUserCode]
	[UserScopedSetting]
	[DefaultSettingValue("")]
	public string subject
	{
		get
		{
			return (string)this["subject"];
		}
		set
		{
			this["subject"] = value;
		}
	}
}

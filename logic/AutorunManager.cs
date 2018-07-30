using System;
using System.Reflection;
using Microsoft.Win32;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class used to manage app startup behavior via OS registry.
	/// </summary>
	public class AutorunManager
	{
		// parts of registry path to write to
		private const string _registrySubkey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string _registryPath = "HKEY_CURRENT_USER\\" + _registrySubkey;
		private const string _registryValue = "Win-M++";
		// location of currently running executable file
		private readonly string _pathToExe = "\"" + Assembly.GetExecutingAssembly().Location + "\"";
		
		/// <summary>
		/// Field to get or set Autorun for running assembly.
		/// </summary>
		public bool AutorunEnabled
		{
			get
			{
				return IsAutorunEnabled();
			}
			
			set
			{
				if (value)
				{
					EnableAutorun();
				}
				else
				{
					DisableAutorun();
				}
			}
		}
		
		/// <summary>
		/// Constructor that does literally nothing.
		/// </summary>
		public AutorunManager()
		{
		}
		
		/// <summary>
		/// Checks whether current assembly is written in run as "Win-M++"
		/// </summary>
		/// <returns>True when it is written, and thus autorun is enabled,
		/// false otherwise.</returns>
		private bool IsAutorunEnabled()
		{
			var registryValue = (String)Registry.GetValue(_registryPath, _registryValue, "no");
			return registryValue.Equals(_pathToExe);
		}
		
		/// <summary>
		/// Enables autorun by writing path to current assembly to run key as "Win-M++".
		/// </summary>
		private void EnableAutorun()
		{
			Registry.SetValue(_registryPath, _registryValue, _pathToExe);
		}
		
		/// <summary>
		/// Disables autorun by deleting "Win-M++" value from run key.
		/// </summary>
		private void DisableAutorun()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(_registrySubkey, true);
			if (key != null)
			{
				key.DeleteValue(_registryValue);
			}
		}
	}
}

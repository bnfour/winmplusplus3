using System;
using System.Collections.Generic;
using System.IO;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that manages loading list of window titles excluded from minimizing.
	/// Also holds hardcoded built-in list for English Win 7/8/10.
	/// </summary>
	public class ExcludedLoader
	{
		/// <summary>
		/// (Hardcoded) file name to look for excluded window titles.
		/// </summary>
		private readonly string _filename = AppDomain.CurrentDomain.BaseDirectory + "\\exceptions.txt";

		/// <summary>
		/// Variable to cache load results.
		/// </summary>
		private IReadOnlyCollection<string> _excluded = null;

		/// <summary>
		/// Fallback entries for English Win 7/8/10.
		/// </summary>
		private static readonly List<string> _defaults = new List<string>(3)
		{
			"Start", "Start menu", "Program Manager"
		};

		/// <summary>
		/// Property to access default values.
		/// </summary>
		public IReadOnlyCollection<string> Defaults => _defaults.AsReadOnly();

		/// <summary>
		/// Exclusions loaded from file.
		/// </summary>
		public IReadOnlyCollection<string> Excluded => _excluded ?? (_excluded = Load());
		
		/// <summary>
		/// Method to load exclusions from file.
		/// Throws ApplicationException on IO errors.
		/// </summary>
		/// <returns>Loaded list of window titles.</returns>
		private IReadOnlyCollection<string> Load()
		{
			var loaded = new List<string>();
			try
			{
				using (var sr = new StreamReader(_filename))
				{
					while (!sr.EndOfStream)
					{
						loaded.Add(sr.ReadLine());
					}
				}
				return loaded.AsReadOnly();
			}
			catch (IOException)
			{
				throw new ApplicationException($"Unable to load file {_filename}");
			}
		}
	}
}

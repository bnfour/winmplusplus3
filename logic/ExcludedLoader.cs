using System;
using System.IO;
using System.Collections.Generic;

namespace winmplusplus3
{
	/// <summary>
	/// Class that manages loading list of window titles excluded from minimizing.
	/// Also holds built-in list for english Win7 and Win10.
	/// </summary>
	public class ExcludedLoader
	{
		/// <summary>
		/// File name to look for excluded window titles.
		/// </summary>
		private const string _filename = "exceptions.txt";
		
		/// <summary>
		/// Default list for english Win7 and Win10.
		/// </summary>
		public List<string> Defaults
		{
			get { return new List<string> {"Start", "Start menu", "Program Manager"}; }
		}
		
		/// <summary>
		/// List loaded from file.
		/// </summary>
		public List<string> Excluded
		{
			get { return Load(); }
		}
		
		/// <summary>
		/// Constructor that does literally nothing.
		/// </summary>
		public ExcludedLoader()
		{
		}
		
		/// <summary>
		/// Method to load exclusions from file.
		/// Throws ApplicationException on IO errors.
		/// </summary>
		/// <returns>Loaded list of window titles.</returns>
		private List<string> Load()
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
				return loaded;
			}
			catch (IOException)
			{
				throw new ApplicationException(String.Format("Unable to load file {0}", _filename));
			}
		}
	}
}

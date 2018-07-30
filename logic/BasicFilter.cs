using System;
using System.Collections.Generic;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Basic filter class. Filters all windows to visible with non-blank title not in excluded list.
	/// </summary>
	public class BasicFilter : IFilter
	{
		/// <summary>
		/// Field to store exclusions passed to constructor.
		/// </summary>
		private readonly List<string> _excluded;
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="toExclude">List of windows titles to exclude from filtered list.</param>
		public BasicFilter(List<string> toExclude)
		{
			_excluded = toExclude;
		}
		
		/// <summary>
		/// Actual filtering method.
		/// To pass through Window must be visible and have non-blank name not in excluded list.
		/// </summary>
		/// <param name="toFilter">List of Windows to filter.</param>
		/// <returns>Filtered list.</returns>
		public List<Window> Filter(List<Window> toFilter)
		{
			var filtered = new List<Window>();
			foreach (var window in toFilter)
			{
				if (window.Visible && window.Title.Length > 0 && !_excluded.Contains(window.Title))
				{
					filtered.Add(window);
				}
			}
			return filtered;
		}
	}
}

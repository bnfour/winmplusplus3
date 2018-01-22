using System;
using System.Collections.Generic;

namespace winmplusplus3
{
	/// <summary>
	/// Class that filters only otherwise valid Windows (see BasicFilter) which is also 
	/// on the same screen as currently focused Window.
	/// </summary>
	public class CurrentScreenFilter : BasicFilter
	{
		/// <summary>
		/// Field to store currently focused window in order to compare Screens to its Screen.
		/// </summary>
		private readonly Window _foregroundWindow;
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="toExclude">List of windows titles to exclude from filtered list.</param>
		/// <param name="foregroundWindow">Currently focused Window.</param>
		public CurrentScreenFilter(List<string> toExclude, Window foregroundWindow): base(toExclude)
		{
			_foregroundWindow = foregroundWindow;
		}
		
		/// <summary>
		/// Actual filtering method, calls BasicFilter.Filter() first.
		/// Only Windows on the same Screen with the currently focused one pass through.
		/// </summary>
		/// <param name="toFilter">List of Windows to filter.</param>
		/// <returns>Filtered list.</returns>
		public List<Window> Filter(List<Window> toFilter)
		{
			var screen  = _foregroundWindow.Screen;
			var filtered = new List<Window>();
			
			foreach (var window in base.Filter(toFilter))
			{
				if (window.Screen.Equals(screen))
				{
					filtered.Add(window);
				}
			}
			return filtered;
		}
	}
}

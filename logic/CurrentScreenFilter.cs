using System;
using System.Collections.Generic;

using Screen = System.Windows.Forms.Screen;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Class that filters only otherwise valid Windows (see BasicFilter) which is also 
	/// on the same screen as currently focused Window.
	/// </summary>
	public class CurrentScreenFilter : BasicFilter, IFilter
	{
		/// <summary>
		/// Field to store currently focused window in order
		/// to compare Screens to its Screen.
		/// </summary>
		private readonly Screen _foregroundWindowScreen;
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="toExclude">List of windows titles
		/// to exclude from filtered list.</param>
		/// <param name="foregroundWindow">Currently focused Window.</param>
		public CurrentScreenFilter(IReadOnlyCollection<string> toExclude,
			Window foregroundWindow) : base(toExclude)
		{
			_foregroundWindowScreen = foregroundWindow.Screen;
		}

		/// <summary>
		/// Fitering predicate.
		/// To pass through Window must be visible 
		/// and have non-blank name not in excluded list
		/// and have Screen property that matches Screen set in this filter's constructor.
		/// </summary>
		/// <param name="window">Window to check minimizing eligibility for.</param>
		/// <returns>True if window should be minimized, false otherwise.</returns>
		public override bool Filter(Window window)
		{
			return base.Filter(window) && window.Screen.Equals(_foregroundWindowScreen);
		}
	}
}

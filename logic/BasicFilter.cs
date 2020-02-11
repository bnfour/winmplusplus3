using System;
using System.Collections.Generic;
using System.Linq;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// Basic filter class.
	/// Filters all windows to visible with non-blank titlenot in excluded list.
	/// </summary>
	public class BasicFilter : IFilter
	{
		/// <summary>
		/// Field to store exclusions passed to constructor.
		/// </summary>
		private readonly IReadOnlyCollection<string> _excluded;
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="toExclude">Collection of windows titles 
		/// to exclude from filtered list.</param>
		public BasicFilter(IReadOnlyCollection<string> toExclude)
		{
			_excluded = toExclude;
		}

		/// <summary>
		/// Fitering predicate.
		/// To pass through Window must be visible 
		/// and have non-blank name not in excluded list.
		/// </summary>
		/// <param name="window">Window to check minimizing eligibility for.</param>
		/// <returns>True if window should be minimized, false otherwise.</returns>
		public virtual bool Filter(Window window)
		{
			return window.Visible && window.Title.Length > 0 && !_excluded.Contains(window.Title);
		}
	}
}

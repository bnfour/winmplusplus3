using System;
using System.Collections.Generic;

namespace winmplusplus3.Logic
{
	/// <summary>
	/// An interface for Window filter.
	/// Takes List<Window> as argument, filters off some Windows based on some criteria and returns filtered list.
	/// </summary>
	public interface IFilter
	{
		/// <summary>
		/// The filter method.
		/// </summary>
		/// <param name="toFilter">List of Windows to filter.</param>
		/// <returns>Filtered list.</returns>
		List<Window> Filter(List<Window> toFilter);
	}
}

namespace winmplusplus3.Logic
{
	/// <summary>
	/// An interface for Window filter.
	/// Declares a predicate that should return true if a Window instance should be
	/// minimized according to that filter.
	/// </summary>
	public interface IFilter
	{
		/// <summary>
		/// The filter predicate.
		/// </summary>
		/// <param name="window">Window to check minimizing eligibility for.</param>
		/// <returns>True if window should be minimized, false otherwise.</returns>
		bool Filter(Window window);
	}
}

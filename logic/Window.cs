﻿using System;

namespace winmplusplus3
{
	/// <summary>
	/// Class that holds read-only data about window needed for app's operation:
	/// window handle for minimizing;
	/// window title for checking for excluded windows;
	/// Screen window is on for filtering;
	/// whether window is visible.
	/// </summary>
	public class Window
	{
		/// <summary>
		/// Window handle.
		/// </summary>
		public IntPtr hWnd {get; private set;}
		
		/// <summary>
		/// Window title.
		/// </summary>
		public string Title {get; private set;}
		
		/// <summary>
		/// Screen most of the window reside on.
		/// </summary>
		public System.Windows.Forms.Screen Screen {get; private set;}
		
		/// <summary>
		/// 
		/// </summary>
		public bool Visible {get; private set;}
		
		/// <summary>
		/// Constructor that sets all the parameters.
		/// </summary>
		/// <param name="hwnd">Window handle to set.</param>
		/// <param name="title">Window title to set.</param>
		/// <param name="screen">Screen to set.</param>
		/// <param name="visible">Whether window is visible.</param>
		public Window(IntPtr hwnd, string title, System.Windows.Forms.Screen screen, bool visible)
		{
			hWnd = hwnd;
			Title = title;
			Screen = screen;
			Visible = visible;
		}
	}
}

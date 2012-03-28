// 
// PanedBackend.cs
//  
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Xwt.Backends;

namespace Xwt.GtkBackend
{
	public class PanedBackend: WidgetBackend, IPanedBackend
	{
		public PanedBackend ()
		{
		}
		
		protected new Gtk.Paned Widget {
			get { return (Gtk.Paned)base.Widget; }
			set { base.Widget = value; }
		}
		
		protected new IPanedEventSink EventSink {
			get { return (IPanedEventSink)base.EventSink; }
		}

		public void Initialize (Orientation dir)
		{
			if (dir == Orientation.Horizontal)
				Widget = new Gtk.HPaned ();
			else
				Widget = new Gtk.VPaned ();
			Widget.Show ();
		}
		
		public void SetPanel (int panel, IWidgetBackend widget, bool resize)
		{
			if (panel == 1)
				Widget.Pack1 (((WidgetBackend)widget).RootWidget, resize, false);
			else
				Widget.Pack2 (((WidgetBackend)widget).RootWidget, resize, false);
		}
		
		public void RemovePanel (int panel)
		{
			if (panel == 1)
				Widget.Remove (Widget.Child1);
			else
				Widget.Remove (Widget.Child2);
		}
		
		public void UpdatePanel (int panel, bool resize)
		{
			if (panel == 1)
				((Gtk.Paned.PanedChild)Widget [Widget.Child1]).Resize = resize;
			else
				((Gtk.Paned.PanedChild)Widget [Widget.Child2]).Resize = resize;
		}
		
		public double Position {
			get { return Widget.Position; }
			set { Widget.Position = (int) value; }
		}

		public Size GetDecorationSize ()
		{
			throw new NotSupportedException ();
		}

		public void GetPanelSizes (double totalSize, out double panel1Size, out double panel2Size)
		{
			throw new NotSupportedException ();
		}
	}
}


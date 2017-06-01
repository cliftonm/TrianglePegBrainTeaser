/* 
* Copyright (c) Marc Clifton
* The Code Project Open License (CPOL) 1.02
* http://www.codeproject.com/info/cpol10.aspx
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Clifton.Core.ExtensionMethods;

namespace FlowSharpLib
{
	public static class ExtensionMethods
	{
		public static string ToHtmlColor(this Color c, char prefix = '#')
		{
			return prefix + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
		}
	}
}

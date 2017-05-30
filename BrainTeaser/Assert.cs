using System;

namespace BrainTeaser
{
	public static class Assert
	{
		public static void That(bool b, string msg)
		{
			if (!b)
			{
				throw new Exception(msg);
			}
		}
	}
}

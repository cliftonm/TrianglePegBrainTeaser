using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clifton.Core.ExtensionMethods;

namespace YieldTest
{
	class Program
	{
		static void Main(string[] args)
		{
			YieldTest(0).ForEach(n => Console.WriteLine(n));
		}

		static IEnumerable<int> YieldTest(int n)
		{
			yield return n;

			if (++n < 5)
			{
				foreach (int q in YieldTest(n))
				{
					yield return q;
				}
			}
		}
	}
}

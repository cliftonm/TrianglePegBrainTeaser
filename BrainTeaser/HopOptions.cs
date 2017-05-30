using System.Collections.Generic;

namespace BrainTeaser
{
	public class HopOptions
	{
		public bool OptionAvailable { get { return hopIdx < hops.Count; } }

		protected List<Hop> hops;
		protected int hopIdx;

		public Hop CurrrentHop { get { return hops[hopIdx]; } }

		public HopOptions(List<Hop> hops)
		{
			this.hops = hops;
			hopIdx = 0;
		}

		public bool NextOptionIndex()
		{
			return ++hopIdx < hops.Count;
		}
	}
}

using System.Collections.Generic;

namespace BrainTeaser
{
	public class RecursiveYieldAlgorithm : Algorithm
	{
		/// <summary>
		/// Used for single stepping.
		/// </summary>
		protected IEnumerator<Hop> hopEnumerator;

		public RecursiveYieldAlgorithm(Board board) : base(board)
		{
		}

		public override void Initialize(int startCellIdx)
		{
			base.Initialize(startCellIdx);
		}

		public override void Run()
		{
			foreach(Hop hop in Step(new HopOptions(board.GetAllowedHops())))
			{
				++Iterations;
				board.HopPeg(hop);
				Solved = board.RemainingPegs == 1;

				if (Solved)
				{
					// It's amazing how we can break out of a recursive yield!
					// But that's actually because the yield operator flattens 
					// the recursion into iteration!
					break;
				}
			}
		}

		public override void StartStepper()
		{
			// We must get the enumerator now.  Calling GetEnumerator() in an IEnumerable<T> 
			// results in MoveNext returning true, but Current returning false.
			// This is because a call to GetEnumerator() always creates a new enumerator instance!
			hopEnumerator = Step(new HopOptions(board.GetAllowedHops())).GetEnumerator();
		}

		public override bool Step()
		{
			++Iterations;
			bool ret = hopEnumerator.MoveNext();

			if (ret)
			{
				Hop hop = hopEnumerator.Current;
				board.HopPeg(hop);
				ret = !(board.RemainingPegs == 1);
			}

			return ret;
		}

		public override Hop PushHop()
		{
			return null;
		}

		protected IEnumerable<Hop> Step(HopOptions hopOptions)
		{
			while (hopOptions.OptionAvailable)
			{
				Hop hop = hopOptions.CurrrentHop;
				UndoStack.Push(hop);
				yield return hop;

				List<Hop> allowedHops = board.GetAllowedHops();

				foreach (Hop nextHop in Step(new HopOptions(allowedHops)))
				{
					yield return nextHop;
				}

				UndoStack.Pop();
				board.UndoHopPeg(hop);
				hopOptions.NextOptionIndex();
			}
		}
	}
}

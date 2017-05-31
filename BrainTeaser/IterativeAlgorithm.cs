using System.Collections.Generic;

namespace BrainTeaser
{
	public class IterativeAlgorithm : Algorithm
	{
		public Stack<HopOptions> HopStack { get; protected set; }

		public IterativeAlgorithm(Board board) : base(board)
		{
			HopStack = new Stack<HopOptions>();
		}

		public override void Initialize(int startCellIdx)
		{
			base.Initialize(startCellIdx);
			HopStack.Clear();
		}

		public override void Run()
		{
			while (board.RemainingPegs > 1 && Step())
			{
				Hop hop = PushHop();
			}

			Solved = board.RemainingPegs == 1;
		}

		public override void StartStepper()
		{
		}

		public override bool Step()
		{
			++Iterations;
			bool next = true;
			List<Hop> allowedHops = board.GetAllowedHops();

			if (allowedHops.Count == 0)
			{
				// No solution.  Unwind.
				next = Unwind();
			}
			else
			{
				HopOptions hopOptions = new HopOptions(allowedHops);
				HopStack.Push(hopOptions);
			}

			return next;
		}

		public override Hop PushHop()
		{
			Hop hop = HopStack.Peek().CurrrentHop;
			board.HopPeg(hop);
			UndoStack.Push(hop);

			return hop;
		}

		protected bool Unwind()
		{
			bool more = false;

			while (!more && HopStack.Count > 0)
			{
				Hop undoHop = UndoStack.Pop();
				board.UndoHopPeg(undoHop);
				more = HopStack.Peek().NextOptionIndex();

				if (!more)
				{
					HopStack.Pop();
				}
			}

			return more;
		}
	}
}

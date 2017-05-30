using System.Collections.Generic;

namespace BrainTeaser
{
	public abstract class Algorithm
	{
		public Stack<Hop> UndoStack { get; protected set; }
		public bool Solved { get; protected set; }
		public int Iterations { get; protected set; }

		protected Board board;

		public abstract void Run();
		public abstract void StartStepper();
		public abstract bool Step();
		public abstract Hop PushHop();

		public Algorithm(Board board)
		{
			UndoStack = new Stack<Hop>();
			this.board = board;
		}

		public virtual void Initialize(int startCellIdx)
		{
			Solved = false;
			Iterations = 0;
			int numPegs = board.NumCells;

			// Start with one peg removed.
			board.FillAllCells();
			board.RemovePeg(startCellIdx);
			UndoStack.Clear();
		}
	}
}

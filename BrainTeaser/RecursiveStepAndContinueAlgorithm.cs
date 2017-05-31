using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrainTeaser
{
	public class RecursiveStepAndContinueAlgorithm : Algorithm
	{
		/// <summary>
		/// Used for single stepping.
		/// </summary>
		protected Semaphore hopSemaphore;

		public RecursiveStepAndContinueAlgorithm(Board board) : base(board)
		{
			hopSemaphore = new Semaphore(0, 1);
		}

		public override void Initialize(int startCellIdx)
		{
			base.Initialize(startCellIdx);
		}

		public override void Run()
		{
			Step(new HopOptions(board.GetAllowedHops()), Callback);
		}

		public override void StartStepper()
		{
			Task.Factory.StartNew(() =>
			{
				hopSemaphore.WaitOne();
				Step(new HopOptions(board.GetAllowedHops()), SingleStepCallback);
			});
		}

		public override bool Step()
		{
			++Iterations;
			bool isSolved = !(board.RemainingPegs == 1);
			hopSemaphore.Release();

			return isSolved;
		}

		public override Hop PushHop()
		{
			return null;
		}

		/// <summary>
		/// Return true if a solution has been found, which cancels the recursion.
		/// </summary>
		protected bool Callback(Hop hop)
		{
			++Iterations;
			board.HopPeg(hop);
			Solved = board.RemainingPegs == 1;

			return Solved;
		}

		protected bool SingleStepCallback(Hop hop)
		{
			board.HopPeg(hop);
			Solved = board.RemainingPegs == 1;
			hopSemaphore.WaitOne();

			return Solved;
		}

		protected bool Step(HopOptions hopOptions, Func<Hop, bool> callback)
		{
			while (hopOptions.OptionAvailable)
			{
				Hop hop = hopOptions.CurrrentHop;
				UndoStack.Push(hop);

				if (callback(hop))
				{
					return false;
				}

				List<Hop> allowedHops = board.GetAllowedHops();
				bool cont = Step(new HopOptions(allowedHops), callback);

				if (!cont)
				{
					return false;
				}

				UndoStack.Pop();
				board.UndoHopPeg(hop);
				hopOptions.NextOptionIndex();
			}

			return true;
		}
	}
}

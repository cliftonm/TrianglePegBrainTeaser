using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Clifton.Core.ExtensionMethods;

namespace BrainTeaser
{
	public static class ExtensionMethods
	{
		public static void ForEach(this int i, Action<int> action, int startIdx = 0)
		{
			for (int n = startIdx; n < i + startIdx; n++) action(n);
		}
	}

	/// <summary>
	/// Struct, so it can be used as a value field for indexing a dictionary.
	/// </summary>
	public struct Location
	{
		public int Row { get; set; }
		public int Column { get; set; }

		public Location(int row, int column)
		{
			Row = row;
			Column = column;
		}
	}

	public class Hop
	{
		public int FromCellIndex { get; protected set; }
		public int ToCellIndex { get; protected set; }
		public int HoppedCellIndex { get; protected set; }

		public Color FromCellColor { get; set; }
		public Color HoppedCellColor { get; set; }
		public Color ToCellColor { get; set; }

		public Hop(int from, int to, int hopped)
		{
			FromCellIndex = from;
			ToCellIndex = to;
			HoppedCellIndex = hopped;
		}

		/// <summary>
		/// We clone the Hop so that the color state is preserved for this *specific* hop.
		/// Otherwise, the Hop instance might be re-used in a later iteration and the previous
		/// Hop instance's color state will be overwritten by the new hop.
		/// </summary>
		/// <returns></returns>
		public Hop Clone()
		{
			Hop hop = new Hop(FromCellIndex, ToCellIndex, HoppedCellIndex);

			return hop;
		}

		public override string ToString()
		{
			string ret = "Finished!";

			if (FromCellIndex != -1)
			{
				ret = FromCellIndex.ToString() + " to " + ToCellIndex.ToString() + " over " + HoppedCellIndex.ToString();
			}

			return ret;
		}
	}

	public class Cell
	{
		public Color Color { get; set; }

		// Semantically friendly:
		public bool HasPeg { get { return state; } set { state = value; } }
		public bool IsEmpty { get { return !state; } }

		protected bool state;

		private static Random rnd = new Random(DateTime.Now.Second);
		private static Color[] colors = { Color.Red, Color.Blue, Color.Yellow };
		// private static int cidx = 0;

		public Cell()
		{
			Color = colors[rnd.Next(colors.Length)];
			//Color = colors[cidx];
			//cidx = (cidx + 1) % 3;
		}
	}

	public class Row
	{
		public Cell[] Cells { get; protected set; }

		public Row(int numCells)
		{
			Cells = new Cell[numCells];
			numCells.ForEach(n => Cells[n] = new Cell());
		}
	}

	public class CellChangeEventArgs : EventArgs
	{
		public Hop Hop { get; set; }
		public bool State { get; set; }
	}

	// 3 blues
	// 5 reds
	// 6 yellows

	public class Board
	{
		public event EventHandler<CellChangeEventArgs> DoHop;
		public event EventHandler<CellChangeEventArgs> UndoHop;

		public int NumCells { get { return flatView.Count; } }

		// TODO: Optimize, because we can subtract one when we hop, add one when we undo a hop.
		public int RemainingPegs { get { return flatView.Count(c => c.HasPeg); } }

		protected Row[] rows;
		protected List<Cell> flatView;

		protected Dictionary<Location, int> cellToIndexMap;
		// protected Dictionary<int, Location> indexToCellMap;
		protected List<Hop> allHops;

		protected int numRows;

		public Board(int numRows)
		{
			this.numRows = numRows;
			InitializeRowsAndColumns(numRows);
			InitializeFlatView();
			InitializeCellToIndexMap();
			// InitializeIndexToCellMap();
			InitializeAllHops();
			FillAllCells();
		}

		public Color GetPegColor(int n)
		{
			return flatView[n].Color;
		}

		public void FillAllCells()
		{
			flatView.ForEach(c => c.HasPeg = true);
		}

		public void RemovePeg(int n)
		{
			flatView[n].HasPeg = false;
		}

		/// <summary>
		/// Move a peg to the empty "to" cell, removing the peg hopped over.
		/// </summary>
		public void HopPeg(Hop hop)
		{
			Assert.That(flatView[hop.FromCellIndex].HasPeg, "Expected from cell to be have a peg.");
			Assert.That(flatView[hop.ToCellIndex].IsEmpty, "Expected to cell to be empty.");
			Assert.That(flatView[hop.HoppedCellIndex].HasPeg, "Expected to cell have a peg.");

			// Perform hop.  From cell is emptied, cell being hopped is emptied, to cell is occupied.
			flatView[hop.FromCellIndex].HasPeg = false;
			flatView[hop.HoppedCellIndex].HasPeg = false;
			flatView[hop.ToCellIndex].HasPeg = true;

			// Save color state of from/hopped/to cells
			hop.FromCellColor = flatView[hop.FromCellIndex].Color;
			hop.HoppedCellColor = flatView[hop.HoppedCellIndex].Color;
			hop.ToCellColor = flatView[hop.ToCellIndex].Color;

			// Update color of To with color of From.
			// We can ignore the others because the UI handles that.
			flatView[hop.ToCellIndex].Color = hop.FromCellColor;

			DoHop.Fire(this, new CellChangeEventArgs() { Hop = hop, State = true });
		}

		/// <summary>
		/// The reverse process, restores pegs in the to cell, hopped cell, and removes the peg in the from cell.
		/// </summary>
		public void UndoHopPeg(Hop hop)
		{
			Assert.That(flatView[hop.FromCellIndex].IsEmpty, "Expected from cell to be empty.");
			Assert.That(flatView[hop.ToCellIndex].HasPeg, "Expected to cell to have a peg.");
			Assert.That(flatView[hop.HoppedCellIndex].IsEmpty, "Expected to cell to be empty.");

			flatView[hop.FromCellIndex].HasPeg = true;
			flatView[hop.HoppedCellIndex].HasPeg = true;
			flatView[hop.ToCellIndex].HasPeg = false;

			// Restore colors:
			flatView[hop.FromCellIndex].Color = hop.FromCellColor;
			flatView[hop.HoppedCellIndex].Color = hop.HoppedCellColor;
			flatView[hop.ToCellIndex].Color = hop.ToCellColor;

			UndoHop.Fire(this, new CellChangeEventArgs() { Hop = hop, State = false });
		}

		public int GetIndex(int row, int col)
		{
			int idx;
			bool found = cellToIndexMap.TryGetValue(new Location(row, col), out idx);
			Assert.That(found, string.Format("Location at {0}, {1} does not exist.", row, col));

			return idx;
		}

		public List<Hop> GetAllowedHops()
		{
			// For each possible cell...
			// where the cell has a peg...
			// we have the index of the cell...
			// and join with hops having a "from" index of the cells with pegs...
			// and the hop has a "to" that is empty and a hopped cell that has a peg.
			// and clone the hops, because we need to preserve color state information for the specific hop,
			// as the exact hop may occur again in a later iteration.

			var allowedHops = Enumerable.Range(0, flatView.Count()).
				Where(n => flatView[n].HasPeg).
				Join(allHops, pegIdx => pegIdx, hop => hop.FromCellIndex, (pegIdx, hop) => hop).
				Where(hop => flatView[hop.ToCellIndex].IsEmpty && flatView[hop.HoppedCellIndex].HasPeg).
				Select(hop => hop.Clone()).ToList();

			return allowedHops;
		}

		protected void InitializeRowsAndColumns(int numRows)
		{
			rows = new Row[numRows];
			numRows.ForEach(n => rows[n] = new Row(n + 1));
		}

		protected void InitializeFlatView()
		{
			flatView = new List<Cell>();
			rows.ForEach(r => r.Cells.ForEach(c => flatView.Add(c)));
		}

		protected void InitializeCellToIndexMap()
		{
			cellToIndexMap = new Dictionary<Location, int>();
			int n = 0;
			rows.ForEachWithIndex((r, ridx) => r.Cells.ForEachWithIndex((c, cidx) => cellToIndexMap[new Location(ridx, cidx)] = n++));
		}

		//protected void InitializeIndexToCellMap()
		//{
		//	indexToCellMap = new Dictionary<int, Location>();
		//	int n = 0;
		//	rows.ForEachWithIndex((r, ridx) => r.Cells.ForEachWithIndex((c, cidx) => indexToCellMap[n++]=new Location(ridx, cidx)));
		//}

		protected void InitializeAllHops()
		{
			// The idea here is to allow for diagonal down-left and down-right hops and left-to-right hops.
			allHops = new List<Hop>();
			rows.ForEachWithIndex((r, ridx) =>
			{
				r.Cells.ForEachWithIndex((c, cidx) =>
					{
						AddHorizontalHop(r, ridx, cidx);

						// Check for valid diagonal hop, which is any hop downward where the target cell exists.
						if (rows.Length > ridx + 2)
						{
							AddDiagonalLeftHop(r, ridx, cidx);
							AddDiagonalRightHop(r, ridx, cidx);
						}
					});
			});
		}

		protected void AddHorizontalHop(Row r, int ridx, int cidx)
		{
			if (r.Cells.Length > cidx + 2)
			{
				int fromIdx = GetIndex(ridx, cidx);
				int toIdx = GetIndex(ridx, cidx + 2);
				int hopIdx = GetIndex(ridx, cidx + 1);
				AddHop(fromIdx, toIdx, hopIdx);
			}
		}

		protected void AddDiagonalLeftHop(Row r, int ridx, int cidx)
		{
			// Note that the column index stay constant.
			int fromIdx = GetIndex(ridx, cidx);
			int toIdx = GetIndex(ridx + 2, cidx);
			int hopIdx = GetIndex(ridx + 1, cidx);
			AddHop(fromIdx, toIdx, hopIdx);
		}

		protected void AddDiagonalRightHop(Row r, int ridx, int cidx)
		{
			// Note how the col index increments.
			// We know that the column to the right always exists because it's a triangle!
			int fromIdx = GetIndex(ridx, cidx);
			int toIdx = GetIndex(ridx + 2, cidx + 2);
			int hopIdx = GetIndex(ridx + 1, cidx + 1);
			AddHop(fromIdx, toIdx, hopIdx);
		}

		/// <summary>
		/// Add both "forward" and "backward" hop.
		/// </summary>
		protected void AddHop(int fromIdx, int toIdx, int hopIdx)
		{
			allHops.Add(new Hop(fromIdx, toIdx, hopIdx));
			allHops.Add(new Hop(toIdx, fromIdx, hopIdx));
		}
	}
}

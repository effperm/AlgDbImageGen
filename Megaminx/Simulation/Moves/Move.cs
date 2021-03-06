﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleImageGenerator.Mega.Simulation.Moves
{
    abstract class Move
    {
        protected virtual int[] EdgeCycle1 { get; set; }
        protected virtual int[] EdgeCycle2 { get; set; }
        protected virtual int[] CornerCycle1 { get; set; }
        protected virtual int[] CornerCycle2 { get; set; }
        protected virtual int[] CornerCycle3 { get; set; }

        public int[][] EdgeCycles
        {
            get
            {
                return new[] { EdgeCycle1, EdgeCycle2 };
            }
        }
        public int[][] CornerCycles
        {
            get
            {
                return new[] { CornerCycle1, CornerCycle2, CornerCycle3 };
            }
        }
    }
}

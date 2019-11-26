using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    static class AStarSearch
    {
        public Path PathToTarget(int StartX, int StartY, int EndX, int EndY,
                                 int Rows, int Cols, char[][] Map)
        {
            Path pthThis;
            int iBest = 0;
            int iPaths = -1;

            List<Path> loPaths = new List<Path>();

            AddPathMove(loPaths, null, "UP", StartX, StartY - 1, EndX, EndY, Rows, Cols, Map);
            AddPathMove(loPaths, null, "DOWN", StartX, StartY + 1, EndX, EndY, Rows, Cols, Map);
            AddPathMove(loPaths, null, "LEFT", StartX - 1, StartY, EndX, EndY, Rows, Cols, Map);
            AddPathMove(loPaths, null, "RIGHT", StartX + 1, StartY, EndX, EndY, Rows, Cols, Map);

            // Assumption is you can always move in at least one direction!
            iBest = BestPath(loPaths);
            pthThis = loPaths[iBest];
            loPaths.RemoveAt(iBest);

            while (   pthThis != null &&
                   !( pthThis.EndX == EndX && pthThis.EndY == EndY ))
            {
                iPaths = pthThis.Moves.Count();

                AddPathMove(loPaths, pthThis, "UP", pthThis.EndX, pthThis.EndY - 1, EndX, EndY, Rows, Cols, Map);
                AddPathMove(loPaths, pthThis, "DOWN", pthThis.EndX, pthThis.EndY + 1, EndX, EndY, Rows, Cols, Map);
                AddPathMove(loPaths, pthThis, "LEFT", pthThis.EndX - 1, pthThis.EndY, EndX, EndY, Rows, Cols, Map);
                AddPathMove(loPaths, pthThis, "RIGHT", pthThis.EndX + 1, pthThis.EndY, EndX, EndY, Rows, Cols, Map);

                if (loPaths.Count() > 0)
                {
                    iBest = BestPath(loPaths);
                    pthThis = loPaths[iBest];
                    loPaths.RemoveAt(iBest);
                }
                else
                    pthThis = null;

            }

            return pthThis;
        }

        private bool AddPathMove(List<Path> lpPaths, Path pthRoot, string Move,
                                 int PosX, int PosY,
                                 int TargetX, int TargetY,
                                 int Rows, int Cols, char[][] Map)
        {
            Path pthResult;
            string sLastMove;
            int iDistanceToTarget;

            if (pthRoot != null)
            {
                sLastMove = pthRoot.LastMove;

                if ((sLastMove == "UP" && Move == "DOWN") ||
                    (sLastMove == "DOWN" && Move == "UP") ||
                    (sLastMove == "LEFT" && Move == "RIGHT") ||
                    (sLastMove == "RIGHT" && Move == "LEFT"))
                    return false;

                if (pthRoot.AlreadyVisited(PosX, PosY))
                    return false;
            }

            if (CanMove(PosX, PosY, Rows, Cols, Map))
            {
                iDistanceToTarget = CalcDistToTarget(PosX, PosY, TargetX, TargetY);

                if (pthRoot == null)
                    pthResult = new Path(Move, PosX, PosY, iDistanceToTarget);
                else
                    pthResult = pthRoot.Extend(Move, PosX, PosY, iDistanceToTarget);

                lpPaths.Add(pthResult);
            }
            return true;
        }

        private int CalcDistToTarget(int PosX, int PosY, int TargetX, int TargetY)
        {
            int iXDiff = Math.Abs(PosX - TargetX);
            int iYDiff = Math.Abs(PosY - TargetY);

            //return iXDiff + iYDiff;

            double dAdjustedDist = (iXDiff + iYDiff) * (1 + cdTargetBias);
            return Convert.ToInt32(dAdjustedDist);

            //double dDistance = Math.Sqrt(Math.Pow(iXDiff,2) + Math.Pow(iYDiff,2));
            //return Convert.ToInt32(dDistance);
        }

        private int BestPath(List<Path> Paths)
        {
            int iResult = 0;
            int iScoreDiff = 0;
            bool bIsBetter = false;

            Path pthBest = Paths[0];  // Note assumes Paths is not an empty list!

            for (int i = 1; i < Paths.Count; i++)
            {
                iScoreDiff = pthBest.CompareTo(Paths[i]);
                bIsBetter = (iScoreDiff < 0);

                if (bIsBetter)
                {
                    iResult = i;
                    pthBest = Paths[i];
                }
                // TODO - determine a method to make this work!
                //
                //else if (iScoreDiff == 0)
                //{
                //    Paths.RemoveAt(i);
                //    i--;
                //}
            }

            return iResult;
        }
    }
}

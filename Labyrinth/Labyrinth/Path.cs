using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class Path : IComparable<Path>
    {
        List<string> _lsMoves = new List<string>();
        List<string> _lsPosns = new List<string>();

        int _iDistanceToTarget = 0;
        int _iEndX = 0;
        int _iEndY = 0;

        bool _bBlocked = false;

        public Path(string Move, int EndX, int EndY, int DistanceToTarget)
        {
            _lsMoves.Add(Move);
            _lsPosns.Add(PointToString(EndX, EndY));

            _iEndX = EndX;
            _iEndY = EndY;

            _iDistanceToTarget = DistanceToTarget;
        }

        public Path(List<string> Moves, List<string> Positions, int EndX, int EndY, int DistanceToTarget)
        {
            _lsMoves = new List<string>(Moves);
            _lsPosns = new List<string>(Positions);

            _iEndX = EndX;
            _iEndY = EndY;

            _iDistanceToTarget = DistanceToTarget;
        }

        public Path(List<string> Moves, List<string> Positions, string NextMove, int EndX, int EndY, int DistanceToTarget)
        {
            _lsMoves = new List<string>(Moves);
            _lsMoves.Add(NextMove);

            _lsPosns = new List<string>(Positions);
            _lsPosns.Add(PointToString(EndX, EndY));

            _iEndX = EndX;
            _iEndY = EndY;

            _iDistanceToTarget = DistanceToTarget;
        }

        public Path Copy(Path Original)
        {
            return new Path(Original.Moves, Original.Positions, Original.EndX, Original.EndY, Original.DistanceToTarget);
        }

        public Path Extend(string Move, int EndX, int EndY, int DistanceToTarget)
        {
            Path pthNew = new Path(_lsMoves, _lsPosns, Move, EndX, EndY, DistanceToTarget);
            return pthNew;
        }

        public List<string> Moves
        {
            get { return _lsMoves; }
        }

        public List<string> Positions
        {
            get { return _lsPosns; }
        }

        public string LastMove
        {
            get
            {
                if (_lsMoves.Count > 0)
                    return _lsMoves[_lsMoves.Count - 1];
                else
                    return "";
            }
        }

        public string NextMove
        {
            get
            {
                if (_lsMoves.Count > 0)
                    return _lsMoves[0];
                else
                    return "";
            }
        }

        public string TakeMove
        {
            get
            {
                if (_lsMoves.Count > 0)
                {
                    string sResult = _lsMoves[0];
                    _lsMoves.RemoveAt(0);
                    //_lsPosns.RemoveAt(0);
                    return sResult;
                }
                else
                    return "";
            }
        }

        public int EndX
        {
            get { return _iEndX; }
        }

        public int EndY
        {
            get { return _iEndY; }
        }

        public int DistanceToTarget
        {
            get { return _iDistanceToTarget; }
        }

        public int Cost
        {
            get { return _lsMoves.Count; }
        }

        public bool Blocked
        {
            get { return _bBlocked; }
            set { _bBlocked = value; }
        }

        public int CompareTo(Path PathToCompare)
        {
            // >0, this Path ie better
            // <0, this Path is worse
            // =0, both Paths are equivalent
            if (PathToCompare == null)
                return 1;
            else
                return (PathToCompare.Cost + PathToCompare.DistanceToTarget) -
                       (this.Cost + this.DistanceToTarget);
        }

        public bool AlreadyVisited(int EndX, int EndY)
        {
            if (_lsPosns.Contains(PointToString(EndX, EndY)))
                return true;

            return false;
        }

        public string PointToString(int EndX, int EndY)
        {
            return EndX.ToString() + "," + EndY.ToString();
        }
    }
}

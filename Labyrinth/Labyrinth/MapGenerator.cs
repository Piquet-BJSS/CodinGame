using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    class MapGenerator
    {
        public List<string> Map(int MapID)
        {
            List<string> Map = new List<string>();

            Map.Add("##############################");
            Map.Add("#............................#");
            Map.Add("#.#######################.#..#");
            Map.Add("#.....T.................#.#..#");
            Map.Add("#.....#.................#.#..#");
            Map.Add("#.#######################.#..#");
            Map.Add("#.....##......##......#....###");
            Map.Add("#...####..##..##..##..#..#...#");
            Map.Add("#.........##......##.....#...#");
            Map.Add("###########################.##");
            Map.Add("#......#......#..............#");
            Map.Add("#...X..#.....................#");
            Map.Add("#...#..####################..#");
            Map.Add("#...#........................#");
            Map.Add("##############################");

            return Map;
        }

        public List<string> ObscureMap(List<string> Map)
        {
            List<string> HiddenMap = new List<string>();

            foreach (string Line in Map)
            {
                HiddenMap.Add(new string('?', Line.Length));
            }

            return HiddenMap;
        }

        public List<string> RevealMap(List<string> Map, List<string> MapInClear, int FromX, int FromY, int ToX, int ToY)
        {
            int iX = 0;
            int iMaxY = 0;
            int iYFrom = 0;
            int iYTo = 0;
            string sUpdatedLine;
            List<string> UpdatedMap = new List<string>();

            // Assumes :
            //   - at least one entry in both Map and MapClear
            //   - Map and MapClear have the same dimensions

            foreach(string HiddenRow in Map)
            {
                if (iX >= FromX && iX <= ToX)
                {
                    iMaxY = HiddenRow.Length - 1;
                    iYFrom = FromY < 0 ? 0 : FromY;
                    iYTo = ToY > iMaxY ? iMaxY - 1 : ToY;

                    sUpdatedLine = iYFrom > 0 ? HiddenRow.Substring(0,iYFrom) : "";
                    sUpdatedLine += iYTo < iMaxY ? MapInClear[iX].Substring(iYFrom, iYFrom-iYTo+1 ) : "";
                    sUpdatedLine += iYTo < iMaxY ? HiddenRow.Substring(iYTo+1, iMaxY-iYTo ): "";
                }
                else
                    sUpdatedLine = HiddenRow;

                UpdatedMap.Add(sUpdatedLine);
                iX++;
            }

            return UpdatedMap;
        }
    }
}

//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine.Tilemaps;

//public static class TileDataMatrixUtility
//{
//    public static (Cell[], Cell[], Cell[]) GetConnections(int originX, int originY, Cell[,] tiles)
//    {
//        var origin = tiles[originX, originY];

//        var width = tiles.GetLength(0);
//        var height = tiles.GetLength(1);

//        var horizontalConnections = new List<Cell>();
//        var verticalConnections = new List<Cell>();

//        List<Cell> squareConnections = GetSquareRB(originX, originY, tiles, origin);

//        for (var x = originX - 1; x >= 0; x--)
//        {
//            var other = tiles[x, originY];

//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            horizontalConnections.Add(other);
//        }

//        for (var x = originX + 1; x < width; x++)
//        {
//            var other = tiles[x, originY];
//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            horizontalConnections.Add(other);
//        }

//        for (var y = originY - 1; y >= 0; y--)
//        {
//            var other = tiles[originX, y];
//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            verticalConnections.Add(other);
//        }

//        for (var y = originY + 1; y < height; y++)
//        {
//            var other = tiles[originX, y];
//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            verticalConnections.Add(other);
//        }

//        return (horizontalConnections.ToArray(), verticalConnections.ToArray(), squareConnections.ToArray());
//    }

//    private static List<Cell> GetSquareRB(int originX, int originY, Cell[,] tiles, Cell origin)
//    {
//        var squareConnections = new List<Cell>();
//        for (int x = -1; x <= 1; x += 2)
//        {
//            for (int y = -1; y <= 1; y += 2)
//            {
//                try
//                {
//                    var other1T = tiles[originX, originY + y];
//                    var other2T = tiles[originX + x, originY];
//                    var other3T = tiles[originX + x, originY + y];
//                    if (IsCompatibleTyles(other1T.Type, origin.Type) && 
//                        IsCompatibleTyles(other2T.Type, origin.Type) && 
//                        IsCompatibleTyles(other3T.Type, origin.Type))
//                    {
//                        squareConnections.Add(other1T);
//                        squareConnections.Add(other2T);
//                        squareConnections.Add(other3T);
//                    }
//                    else
//                    {
//                        break;
//                    }
//                }
//                catch (System.Exception)
//                {
//                    break;
//                }
//            }
//        }
//        return squareConnections;
//    }

//    private static bool IsCompatibleTyles(TileType origin, TileType other)
//    {
//        switch (origin)
//        {
//            case TileType.Empty:
//            case TileType.Stone:
//            case TileType.Free:
//            case TileType.SparkHorisontal:
//            case TileType.SparkVertical:
//            case TileType.Plane:
//            case TileType.Moodlet:
//            case TileType.Bomb:
//                return false;
//        }

//        if (origin == other) return true;

//        return false;
//    }

//    public static Match FindBestMatch(Cell[,] tiles)
//    {
//        var bestMatch = default(Match);

//        for (var y = 0; y < tiles.GetLength(1); y++)
//        {
//            for (var x = 0; x < tiles.GetLength(0); x++)
//            {
//                var tile = tiles[x, y];

//                var (h, v, s) = GetConnections(x, y, tiles);

//                var match = new Match(tile, h, v, s);

//                if (match.Score < 0) continue;

//                if (bestMatch == null)
//                {
//                    bestMatch = match;
//                }
//                else if (match.Score > bestMatch.Score) bestMatch = match;
//            }
//        }

//        return bestMatch;
//    }

//    public static Cell[] GetSquareConnections(int originX, int originY, Cell[,] tiles)
//    {
//        var origin = tiles[originX, originY];

//        var width = tiles.GetLength(0);
//        var height = tiles.GetLength(1);

//        var squareConnections = new List<Cell>();

//        for (var x = originX - 1; x >= 0; x--)
//        {
//            var other = tiles[x, originY];

//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            squareConnections.Add(other);
//        }

//        for (var x = originX + 1; x < width; x++)
//        {
//            var other = tiles[x, originY];
//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            squareConnections.Add(other);
//        }

//        for (var y = originY - 1; y >= 0; y--)
//        {
//            var other = tiles[y, originY];

//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            squareConnections.Add(other);
//        }

//        for (var y = originY + 1; y < width; y++)
//        {
//            var other = tiles[y, originY];
//            if (!IsCompatibleTyles(other.Type, origin.Type)) break;

//            squareConnections.Add(other);
//        }

//        return squareConnections.ToArray();
//    }
//}
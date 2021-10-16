using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    static public List<Vector2Int> CreatePath(int MatrixSize)
    {
        List<Vector2Int> resultPath = new List<Vector2Int>();

        float minLen = MatrixSize * MatrixSize / 3;

        Vector2Int PrevCell=Vector2Int.zero;
        Vector2Int NextCell=Vector2Int.zero;

        int PathDirection = Random.Range(0, 4);
        switch (PathDirection)
        {
            case 0:
                NextCell = new Vector2Int(Random.Range(1, MatrixSize - 1), MatrixSize - 1);
                break;
            case 1:
                NextCell = new Vector2Int(MatrixSize - 1, Random.Range(1, MatrixSize - 1));
                break;
            case 2:
                NextCell = new Vector2Int(Random.Range(1, MatrixSize - 1), 0);
                break;
            case 3:
                NextCell = new Vector2Int(0, Random.Range(1, MatrixSize - 1));
                break;
        }

        resultPath.Add(NextCell);
        PrevCell = NextCell;
        NextCell.y = (PathDirection ==0||PathDirection==2) ? PrevCell.y - 1 + PathDirection : PrevCell.y;
        NextCell.x = ((PathDirection == 1 || PathDirection == 3)) ? PrevCell.x - 2 + PathDirection : PrevCell.x;
        resultPath.Add(NextCell);

        for (int i = 0; i < MatrixSize*MatrixSize; i++)
        {
            PrevCell = NextCell;
            List<int> EnableWay = new List<int>() { 0, 1, 2, 3 };

            bool IsNeedToCheck = true;

            while (IsNeedToCheck & EnableWay.Count != 0)
            {
                PathDirection = Random.Range(0, EnableWay.Count);
                switch (EnableWay[PathDirection])
                {
                    case 0:
                        NextCell = PrevCell + Vector2Int.up;
                        break;
                    case 1:
                        NextCell = PrevCell + Vector2Int.right;
                        break;
                    case 2:
                        NextCell = PrevCell + Vector2Int.down;
                        break;
                    case 3:
                        NextCell = PrevCell + Vector2Int.left;
                        break;
                }
                EnableWay.Remove(EnableWay[PathDirection]);
                IsNeedToCheck = IskNearbyCell(PrevCell, NextCell, resultPath) || (IsNearbyBorder(NextCell, MatrixSize));
            }

            if (IsNeedToCheck)
            {
                break;
            }
            resultPath.Add(NextCell);

        }

        if (resultPath.Count < minLen)
        {
            resultPath = CreatePath(MatrixSize);
        }

        return resultPath;
    }

    private static bool IskNearbyCell(Vector2Int PrevVector,Vector2Int CurrentVector,List<Vector2Int> Path)
    {
        bool result = false;
        foreach(Vector2Int CellId in Path)
        {
            result = ((CellId != PrevVector) & ((CurrentVector-CellId).magnitude<=1 )) || result;
        }

        return result;
    }

    private static bool IsNearbyBorder(Vector2 vector,int mapSize)
    {
        return (vector.x < 1) || (vector.x > mapSize - 2) || (vector.y < 1) || (vector.y > mapSize - 2);
    }
}

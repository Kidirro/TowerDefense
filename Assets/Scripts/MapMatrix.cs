using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMatrix : MonoBehaviour
{

    static public MapMatrix Instance => _instance;
    private static MapMatrix _instance;

    private List<List<Cell>> _cellData;
    private List<List<GameObject>> _cellGameObjects;
    private List<Vector2Int> _pathData;

    public int PathLenght => _pathData.Count;

    private GameObject _cellPrefab => Resources.Load<GameObject>("Prefab/cellPrefab");

    private GameObject _cellSelectPrefab => Resources.Load<GameObject>("Prefab/CellSelected");


    public int MatrixSize
    {
        get { return _matrixSize; }
    }

    [SerializeField] private int _matrixSize;

    public float CellSize
    {
        get { return _cellSize; }
    }

    private float _cellSize;

    private void CreateMatrix()
    {
        float StartPositionX = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth * (500.0f / 2960.0f), 0)).x;
        float EndPositionX = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth * (2460.0f / 2960.0f), 0)).x;

        float StartPositionY = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y; 
        float High = Camera.main.ScreenToWorldPoint(new Vector2(0, Camera.main.pixelHeight)).y - Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        bool isHighBigger = High > EndPositionX - StartPositionX;

        _cellSize = (isHighBigger) ? (EndPositionX - StartPositionX) / (float )_matrixSize : High / (float) _matrixSize;
        StartPositionX += _cellSize ;
        StartPositionY += _cellSize / 2;
        float Remain = (isHighBigger) ? (High - _matrixSize * _cellSize) / 2 : (EndPositionX - StartPositionX - _matrixSize * _cellSize) / 2;

        Vector2 StartPositionMatrix = (isHighBigger) ? new Vector2(StartPositionX, StartPositionY+Remain) : new Vector2(StartPositionX + Remain, StartPositionY);
        _cellData = new List<List<Cell>>();
        _cellGameObjects = new List<List<GameObject>>();
        for (int i = 0; i < _matrixSize; i++)
        {
            _cellData.Add(new List<Cell>());
            _cellGameObjects.Add(new List<GameObject>());
            for(int j = 0; j < _matrixSize; j++)
            {
                GameObject newCellObject = Instantiate(_cellPrefab);
                newCellObject.transform.position = new Vector2(StartPositionMatrix.x+i * _cellSize, StartPositionMatrix.y+j * _cellSize);
                newCellObject.name="["+i+"]["+j+"]cell";
                newCellObject.transform.SetParent(this.transform);
                newCellObject.transform.localScale = new Vector2(_cellSize,_cellSize);

                Cell newCellData = new Cell(cellType.plane,newCellObject.transform);
                _cellData[i].Add(newCellData);
                _cellGameObjects[i].Add(newCellObject);
            }
        }
    }

    private void Awake()
    {
        _instance = this;
        CreateMatrix();
        _pathData = Path.CreatePath(_matrixSize);
        foreach(Vector2Int id in _pathData)
        {
            _cellGameObjects[id.x][id.y].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public Vector2 GetPositionFromId(Vector2Int id)
    {
        return _cellGameObjects[id.x][id.y].transform.position;
    }

    public Vector2 GetPositionFromId(int x,int y)
    {
        return _cellGameObjects[x][y].transform.position;
    }

    public Cell GetCellFromId(Vector2Int id)
    {
        return _cellData[id.x][id.y];
    }

    public Cell GetCellFromId(int x,int y)
    {
        return _cellData[x][y];
    }

    public void SetCellFromId(int x, int y,Cell newCell)
    {
        _cellData[x][y] = newCell;
    }

    public void SetCellFromId(Vector2Int id,Cell newCell)
    {
        _cellData[id.x][id.y] = newCell;
    }

    public Vector2 GetPositionFromIdPath(int id)
    {
        return _cellData[_pathData[id].x][_pathData[id].y].CellTransform.position;
    }
}

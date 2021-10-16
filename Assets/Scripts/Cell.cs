using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Класс клетки.
public class Cell
{
    //Тип клетки. Дорога или плоскость
    public cellType CellType
    {
        get { return _cellType; }
        set { _cellType = value;}
    }

    private cellType _cellType;


    public Transform CellTransform {
        get { return _cellTransform; }
    }
    private Transform _cellTransform;

    //Ссылка на башню 
    public Tower BuildedTower
    {
        get { return _buildedTower; }
        set { _buildedTower = value; }
    }

    private Tower _buildedTower;

    public Cell(cellType type,Transform transform)
    {
        _cellType = type;
        _cellTransform = transform;
    }
}

public enum cellType
{
    road,
    plane
}

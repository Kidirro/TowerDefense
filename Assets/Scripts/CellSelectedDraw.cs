using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelectedDraw : MonoBehaviour
{
    private MapMatrix _map;

    void Start()
    {
        GlobalEvent.Subscribe(TypesEvent.CellChosing, OnSelectedCell);
        _map = MapMatrix.Instance;
        transform.localScale = new Vector2(_map.CellSize, _map.CellSize);
        Debug.Log("Size of cell: " + _map.CellSize);
        transform.position = _map.GetPositionFromId(SelectedCell.SelectedСell);
    }

    public void OnSelectedCell()
    {
        transform.position = _map.GetPositionFromId(SelectedCell.SelectedСell);
    }
}

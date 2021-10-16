using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    static public Builder Instance => _instance;
    private static Builder _instance;

    private MapMatrix _map => MapMatrix.Instance;

    private GameObject _towerPrefab;

    private void Start()
    {
        _towerPrefab = Resources.Load<GameObject>("Prefab/TowerObj");
        _instance = this;
    }

    public void BuildingTower(Tower tower)
    {
        Cell ChocedCell = _map.GetCellFromId(SelectedCell.SelectedСell);
        if (ChocedCell.BuildedTower == null)
        {
            GameObject TowerObject = Instantiate(_towerPrefab);
            TowerObject.GetComponent<TowerMono>().TowerInfo = tower;
            TowerObject.transform.position = _map.GetPositionFromId(SelectedCell.SelectedСell.x, SelectedCell.SelectedСell.y);
            TowerObject.transform.localScale = new Vector2(MapMatrix.Instance.CellSize, MapMatrix.Instance.CellSize);
            ChocedCell.BuildedTower = tower;
            _map.SetCellFromId(SelectedCell.SelectedСell, ChocedCell);
        }
    }
}

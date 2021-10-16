using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _health;

    private int _targetCellId;
    private Vector2 _targetCellPosition;
    private float _distanceToTarget => new Vector2(_targetCellPosition.x - transform.position.x, _targetCellPosition.y - transform.position.y).magnitude;

    private MapMatrix _map;

    private void OnEnable()
    {
        _health = 5;
        _map = MapMatrix.Instance;

        _targetCellId = 0;
        transform.position = _map.GetPositionFromIdPath(0);
        _targetCellPosition = transform.position;
    }

    void Update()
    {
        
        if (_distanceToTarget==0)
        {
            _targetCellId++;
            if (_targetCellId == _map.PathLenght) { _targetCellId = 0;
                GlobalEvent.EventTrigger(TypesEvent.EnemyFinish);
            }
            _targetCellPosition = _map.GetPositionFromIdPath(_targetCellId) + new Vector2(Random.Range(-_map.CellSize/4,_map.CellSize/4), Random.Range(-_map.CellSize / 4, _map.CellSize / 4));
            Debug.Log(_targetCellPosition);
        }

        transform.position = Vector2.MoveTowards(transform.position, _targetCellPosition , _speed * Time.deltaTime);
    }

    public void TakeDamage(float Damage)
    {
        _health -= Damage;
        if (_health <= 0) gameObject.SetActive(false);
    }
}

interface TakeDamage
{
    void TakeDamage();
}
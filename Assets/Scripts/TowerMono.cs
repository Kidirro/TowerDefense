using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerMono : MonoBehaviour
{
    public Tower TowerInfo
    {
        get { return _tower; }
        set
        {
            _tower = value;

            gameObject.layer = (_tower.Target==TargetType.Enemy)? 9:10;

            switch (_tower.Collider)
            {
                case ColliderType.Circle:
                    CircleCollider2D circle = GetComponent<CircleCollider2D>();
                    if (!circle) circle = gameObject.AddComponent<CircleCollider2D>();
                    circle.radius = _tower.Radius + 0.5f;
                    break;
                case ColliderType.Cross:
                    BoxCollider2D[] collider2Ds = GetComponents<BoxCollider2D>();
                    if (collider2Ds.Length == 0)
                    {
                        BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
                        boxCollider2D.size = new Vector2(1,15);
                        boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
                        boxCollider2D.size = new Vector2(15, 1);
                    }
                    else
                    {
                        collider2Ds[0].size = new Vector2(1, 10);
                        collider2Ds[1].size = new Vector2(10, 1);
                    }
                    break;
            }
            var emissionRate = _particleFire.emission;
            emissionRate.rateOverTime = _tower.Rate;

            GetComponent<SpriteRenderer>().sprite = _tower.TowerSprite;
        }
    }

    private Tower _tower;

    private List<EnemyMono> _enemiesList;
    private List<TowerMono> _towersList;


    bool _isInArea = false;

    [SerializeField] private ParticleSystem _particleFire;

    private void Start()
    {
        _enemiesList = new List<EnemyMono>();
        _towersList = new List<TowerMono>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_tower.Target == TargetType.Enemy & collision.gameObject.tag == "Enemy")
        {
            _enemiesList.Add(collision.gameObject.GetComponent<EnemyMono>());
            if (!_isInArea)
            {
                _isInArea = true;
                StartCoroutine(Rotating());
                StartCoroutine(Shoting());
            }
        }
        else if (_tower.Target == TargetType.Tower & collision.gameObject.tag == "Tower")
        {
            _towersList.Add(collision.gameObject.GetComponent<TowerMono>());
        }
        else if (_tower.Target == TargetType.Enemy & collision.gameObject.tag != "Tower") Debug.LogWarning("Unknown object! Check tag on:" + collision.gameObject.name);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_tower.Target == TargetType.Enemy) _enemiesList.Remove(collision.gameObject.GetComponent<EnemyMono>());
        if (_tower.Target == TargetType.Tower) _towersList.Remove(collision.gameObject.GetComponent<TowerMono>());
    }

    IEnumerator Shoting()
    {
        _particleFire.Play();
        while (_enemiesList.Count > 0)
        {
            switch (_tower.TargetCountType)
            {
                case TargetNumber.Loner:
                    for (int i = 0; i<_tower.TargetCount & i < _enemiesList.Count; i++)
                    {
                        _enemiesList[i].TakeDamage(_tower.Damage);
                    }
                    break;
                case TargetNumber.All:
                    foreach(EnemyMono enemy in _enemiesList)
                    {
                        enemy.TakeDamage(_tower.Damage);
                    }
                    break;
            }
            _enemiesList[0].TakeDamage(_tower.Damage);
            yield return new WaitForSecondsRealtime(_tower.Rate);
        }
        _particleFire.Stop();
        _isInArea = false;
    }

    IEnumerator Rotating()
    {
        while (_enemiesList.Count > 0)
        {
            try
            {
                Vector2 diff = _enemiesList[0].transform.position - transform.position;

                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            }
            catch
            {
                Debug.Log(_tower.Target);
            }

            yield return null;
        }
    }
}

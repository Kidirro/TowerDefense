using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public float Radius;
    public float Rate;
    public float Damage;
    public float Price;

    public Sprite TowerSprite;
    public Sprite TowerChosedSprite;


    public bool IsRotating;

    public string Name;
    public string Description;

    public int TargetCount;

    public TargetType Target;
    public ImpactTypeEnemy ImpactEnemy;
    public ImpactTypeFriend ImpactFriend;
    public ColliderType Collider;
    public TargetNumber TargetCountType;
    public List<ImpactTypeEnemy> DebuffList;
    public List<ImpactTypeFriend> BuffList;
}

public enum ImpactTypeFriend
{
    None,
    BuffDamage,
    BuffRate,
    BuffRange
}
public enum ImpactTypeEnemy
{
    None,
    Freeze,
    Teleport
}

public enum ColliderType
{
    Circle,
    Cross
}


public enum TargetNumber
{
    Loner,
    All
}

public enum TargetType
{
    Tower,
    Enemy
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float Speed;
    public float Health;

    public Sprite EnemySprite;

    public string Name;
    public bool IsHordeOpportunity;
}

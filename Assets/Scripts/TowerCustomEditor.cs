using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tower))]
public class ApartmentCustomInspector : Editor
{
    public override void OnInspectorGUI()
    { 
        Tower tower = (Tower)target; 

        tower.TowerSprite = (Sprite) EditorGUILayout.ObjectField("Sprite", tower.TowerSprite, typeof(Sprite), false);
        tower.TowerChosedSprite = (Sprite) EditorGUILayout.ObjectField("Chosen",tower.TowerChosedSprite, typeof(Sprite), false);
        tower.Name = EditorGUILayout.TextField("Название башни", tower.Name);
        tower.Description = EditorGUILayout.TextField("Описание башни", tower.Description);
        EditorGUILayout.Space();
        switch (tower.Target= (TargetType)EditorGUILayout.EnumPopup("Тип цели", tower.Target))
        {
            case TargetType.Enemy:
                tower.Rate = EditorGUILayout.FloatField("Скорость атаки", Mathf.Clamp(tower.Rate, 0, 50));
                tower.Damage = EditorGUILayout.FloatField("Урон", Mathf.Clamp(tower.Damage, 0, 50));
                tower.ImpactEnemy = (ImpactTypeEnemy)EditorGUILayout.EnumPopup("Дебаф", tower.ImpactEnemy);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Лист дебафов");
                if (GUI.Button(new Rect(110,280, 25, 25),"+"))
                {
                    tower.BuffList.Add(0);
                }
                if (GUI.Button(new Rect(135, 280, 25, 25), "-"))
                {
                    if (tower.BuffList.Count > 0)
                    {
                        tower.BuffList.Remove(tower.BuffList[tower.BuffList.Count - 1]);
                    }
                }
                for (int i = 0; i < tower.BuffList.Count; i++)
                {
                    tower.BuffList[i] = (ImpactTypeFriend)EditorGUILayout.EnumPopup("Баф"+i, tower.BuffList[i]);
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                tower.TargetCountType = (TargetNumber)EditorGUILayout.EnumPopup("Тип количества целей", tower.TargetCountType);
                if (tower.TargetCountType == TargetNumber.Loner)
                {
                    tower.TargetCount = EditorGUILayout.IntSlider("Количество целей", (int)tower.TargetCount, 1, 30);
                }
                break;
            case TargetType.Tower:
                tower.ImpactFriend = (ImpactTypeFriend) EditorGUILayout.EnumPopup("Баф союзникам",tower.ImpactFriend);
                break;

        }

        tower.Collider = (ColliderType) EditorGUILayout.EnumPopup("Тип коллайдера", tower.Collider);
        if (tower.Collider == ColliderType.Circle)
        {
            tower.Radius = EditorGUILayout.IntSlider("Радиус атаки",(int)tower.Radius, 0, 30);
        }
    }

}

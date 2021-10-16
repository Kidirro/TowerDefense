using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    static public ShopUI Instance => _instance;
    private static ShopUI _instance;

    [SerializeField] private Image _infoBG;

    [SerializeField] private Text _infoNameText;
    [SerializeField] private Text _infoDescriptionText;

    private void Start()
    {
        _instance = this;
        _infoBG.gameObject.SetActive(false);
    }

    public void ShowInfo()
    {
        _infoBG.gameObject.SetActive(!_infoBG.IsActive());
    }

    public void PrintInfoTower(Tower tower)
    {
        _infoNameText.text = tower.Name;
        _infoDescriptionText.text = tower.Description;
    }
}

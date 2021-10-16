using UnityEngine;
using UnityEngine.UI;

public class TowerBuyButton  : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    private Button _button;
    private Image _image;

    private bool _isButtonSelected=false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        ChangeTower();
    }

    private void ChangeTower()
    {
        _image.sprite = _tower.TowerSprite;
        _button.targetGraphic = _image;
        SpriteState st =  new SpriteState();
        st.pressedSprite = _tower.TowerChosedSprite;
        st.selectedSprite = _tower.TowerChosedSprite;
        _button.spriteState = st;
    }
    
    public void OnButtonDeselect()
    {
        _isButtonSelected = false;
    }

    public void OnButtonClick()
    {
        if (_isButtonSelected)
        {
            Builder.Instance.BuildingTower(_tower);
            _isButtonSelected = false;
        }
        else
        {
            ShopUI.Instance.PrintInfoTower(_tower);
            _isButtonSelected = true;
        }
    }
}

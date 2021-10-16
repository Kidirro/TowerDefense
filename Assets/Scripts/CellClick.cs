using UnityEngine;
using UnityEngine.EventSystems;

public class CellClick : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        SelectedCell.NameDecryption(name);
        GlobalEvent.EventTrigger(TypesEvent.CellChosing);
    }
}

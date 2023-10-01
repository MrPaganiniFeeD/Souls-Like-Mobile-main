using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private Image _aimImageArea;
    public Vector2 PointerPosition { get; private set; } 
    
    public bool IsClicked { get; private set; }
    public Image AimImage => _aimImageArea;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("On click");
        IsClicked = true;
  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On click");
        IsClicked = true;
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
        PointerPosition = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsClicked = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerPosition = Vector2.zero;
    }
}

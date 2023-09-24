using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackArea : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _attackArea;

    public event UnityAction Clicked;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_attackArea.rectTransform, eventData.position, null, out _))
            Clicked?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeftHandAttackButton : MonoBehaviour, IAttackButton, IPointerClickHandler
{
    public event Action Clicked;

    [SerializeField] private Image _buttonImage; 
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_buttonImage.rectTransform, eventData.position, null, out _))
            Clicked?.Invoke();
    }
}

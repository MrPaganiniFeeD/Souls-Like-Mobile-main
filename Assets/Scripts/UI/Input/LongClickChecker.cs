using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;

public class LongClickChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent<IItemInfo> _clicked;

    private CanvasGroup _canvasGroup;

    private float _startClick;
    private float _endClick;



    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _startClick = 0f;
        _endClick = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startClick = Time.time;
           
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _endClick = Time.time;

        if (_endClick - _startClick > 0.51f && _canvasGroup.blocksRaycasts)
        {
            var UIItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            Debug.Log(UIItem);
            _clicked?.Invoke(UIItem.ItemInfo);
            _startClick = 0f;
            _endClick = 0f;
        }



    }
}



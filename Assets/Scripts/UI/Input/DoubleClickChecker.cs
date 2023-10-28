using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoubleClickChecker : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private UnityEvent<IItemInfo, IInventorySlot> _doubleClicked;

    private float _firstClickTime;
    private float _timeBetweenClicks;
    private bool _coroutineAllowed;
    private int _clickCounter;

    private void Start()
    {
        _firstClickTime = 0f;
        _timeBetweenClicks = 0.2f;
        _clickCounter = 0;
        _coroutineAllowed = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _clickCounter += 1;

        if(_clickCounter == 1 && _coroutineAllowed)
        {

            var UIitem = eventData.pointerDrag.GetComponent<UIItem>();
            var UISlot = eventData.pointerDrag.GetComponentInParent<UISlot>();

            _firstClickTime = Time.time;
            //StartCoroutine(DoubleClickDetection(UIitem.ItemInfo, UISlot.Slot));
        }
    }

    private IEnumerator DoubleClickDetection(IItemInfo itemInfo, IInventorySlot slot)
    {
        _coroutineAllowed = false;
        while(Time.time < _firstClickTime + _timeBetweenClicks)
        {
            if (_clickCounter == 2)
            {
                Debug.Log("doubleClick");
                _doubleClicked?.Invoke(itemInfo, slot);
                break;
            }
            yield return new WaitForEndOfFrame();

        }
        _clickCounter = 0;
        _firstClickTime = 0f;
        _coroutineAllowed = true;
    }




}


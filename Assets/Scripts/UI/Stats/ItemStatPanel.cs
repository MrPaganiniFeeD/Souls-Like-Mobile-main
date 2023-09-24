using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemStatPanel : MonoBehaviour
{
    private UIStatItem[] _uiStatItems;

    private void Awake()
    {
        _uiStatItems = GetComponentsInChildren<UIStatItem>();
    }

    private void OnEnable()
    {
        
    }

    private void ShowStats(ItemBuffStats buffStats)
    {
        foreach (var uiStat in _uiStatItems)
        {
            //_uiStatItems.SetValue();
        }
        
    }
    
}

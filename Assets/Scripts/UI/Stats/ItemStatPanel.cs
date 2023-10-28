using System;
using UnityEngine;

public class ItemStatPanel : MonoBehaviour
{
    private UIStatItem[] _uiStatItems;

    private void Awake()
    {
        _uiStatItems = GetComponentsInChildren<UIStatItem>();
        Clear();
    }

    public void UpdateStats(ItemBuffStats buffStats)
    {
        var allStats = buffStats.GetAllStats();
        for (var i = 0; i < _uiStatItems.Length; i++)
        {
            var uiStat = _uiStatItems[i];
            uiStat.UpdateValue(allStats[i]);
        }
    }

    public void Clear()
    {
        foreach (var uiStat in _uiStatItems) 
            uiStat.Clear();
    }

    public void OnDisable()
    {
        Clear();
    }
}

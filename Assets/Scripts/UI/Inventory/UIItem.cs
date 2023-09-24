using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _amount;

    public IItemInfo ItemInfo { get; private set; }
    public void Refresh(IInventorySlot slot)
    {
        ItemInfo = slot.Item.Info;
        _imageIcon.sprite = ItemInfo.Icon;
        _imageIcon.gameObject.SetActive(true);
        UpdateAmountText(slot);
    }

    private void UpdateAmountText(IInventorySlot slot)
    {
        var textAmountEnabled = slot.Amount > 1;
        _amount.gameObject.SetActive(textAmountEnabled);
        
        if (textAmountEnabled)
            _amount.text = $"x{slot.Amount}";

    }

    private void UpdateItemIcon()
    {
        _imageIcon.sprite = ItemInfo.Icon;
        _imageIcon.gameObject.SetActive(true);
        
    }

    private void Cleanup()
    {
        _amount.gameObject.SetActive(false);
        _imageIcon.gameObject.SetActive(false);
    }
}

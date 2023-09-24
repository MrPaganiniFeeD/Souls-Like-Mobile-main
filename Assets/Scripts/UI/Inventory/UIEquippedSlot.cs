using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEquippedSlot : UISlot
{
    [SerializeField] private Image _typeSlotIcon;
    private void DisableTypeSlotIcon()
    {
        _typeSlotIcon.enabled = false;
    }

    private void EnableTypeSlotIcon()
    {
        _typeSlotIcon.enabled = true;
    }

    public override void OnInstallItem()
    {
        DisableTypeSlotIcon();
    }

    public override void OnUninstallItem()
    {
        EnableTypeSlotIcon();
    }
}

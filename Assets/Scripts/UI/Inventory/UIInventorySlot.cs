using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class UIInventorySlot : MonoBehaviour, IPointerClickHandler
{
    [FormerlySerializedAs("UIItem")] [SerializeField] private UIItem _uiItem;
    [SerializeField] private Image _onEquippedImage;
    protected TooltipItem Tooltip;
    
    public IInventorySlot Slot { get; private set; }

    public void Construct(IInventorySlot slot, TooltipItem tooltipItem)
    {
        Tooltip = tooltipItem;
        SetSlot(slot);
    }
    public void Refresh()
    {
        if (Slot != null)
            _uiItem.Refresh(Slot);
    }
    public void SetSlot(IInventorySlot slot)
    {
        Slot = slot;
        if(Slot.Item != null)
            Slot.Item.State.StateChanged += OnItemStateChange;
        slot.InstalledItem += OnInstallItem;
        slot.UninstalledItem += OnUninstallItem;
    }

    public void OnDestroy()
    {
        if (Slot != null)
        {
            if(Slot.Item != null)
                Slot.Item.State.StateChanged -= OnItemStateChange;
            Slot.InstalledItem -= OnInstallItem;
            Slot.UninstalledItem -= OnUninstallItem;
        }        
    }

    public void OnInstallItem(Item item)
    {
    }

    public void OnUninstallItem(Item item)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Tooltip.Open();
        Tooltip.UpdateInfo(Slot.Item);
    }

    private void OnItemStateChange()
    {
        _uiItem.Refresh(Slot);
        if (Slot.Item.State.IsEquipped)
            _onEquippedImage.gameObject.SetActive(true);
        else
            _onEquippedImage.gameObject.SetActive(false);
    }
}

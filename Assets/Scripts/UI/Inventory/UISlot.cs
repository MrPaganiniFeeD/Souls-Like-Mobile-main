using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UISlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private UIItem _uiItem;
    public IInventorySlot Slot { get; private set; }

    private IUIInventory _uIInventory;

    private void Awake()
    {
        _uIInventory = GetComponentInParent<IUIInventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        UIItem otherItemUI = eventData.pointerDrag.GetComponent<UIItem>();
        if (otherItemUI == null)
            return;

        UISlot otherSlotUI = otherItemUI.GetComponentInParent<UISlot>();
        IInventorySlot otherSlot = otherSlotUI.Slot;
        IInventory inventory = _uIInventory.Inventory;

        //inventory.TransitFromSlotToSlot(otherSlot, Slot);

        Refresh();
        otherSlotUI.Refresh();
    }
    public void Refresh()
    {
        if (Slot != null)
            _uiItem.Refresh(Slot);
    }
    public void SetSlot(IInventorySlot slot)
    {
        Slot = slot;
        slot.InstalledItem += OnInstallItem;
        slot.UninstalledItem += OnUninstallItem;
    }
    
    public void OnDestroy()
    {
        if (Slot != null)
        {
            Slot.InstalledItem -= OnInstallItem;
            Slot.UninstalledItem -= OnUninstallItem;
        }        
    }
    public abstract void OnInstallItem();
    public abstract void OnUninstallItem();

}

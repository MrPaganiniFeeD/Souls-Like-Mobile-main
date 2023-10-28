using UnityEngine;
using UnityEngine.UI;

public class UIEquippedSlot : MonoBehaviour
{
    [SerializeField] private Image _typeSlotIcon;
    [SerializeField] private Image _itemIcon;
    [SerializeField] public EquippedSlotType TypeSlot;

    public IEquippedSlot Slot { get; private set; }
    

    public void OnInstallItem(Item item) => 
        DisableTypeSlotIcon();

    public void OnUninstallItem(Item item) => 
        EnableTypeSlotIcon();

    public void Construct(IEquippedSlot equippedSlot)
    {
        Slot = equippedSlot;
        Slot.EquippedItem += OnEquippedItem;
        Slot.UnequippedItem += OnUnequippedItem;
    }
    

    public void OnEquippedItem(EquippedEventInfo eventInfo)
    { 
        var info = eventInfo.ItemInfo;
        if(eventInfo.IsDefaultItem)
            return;
        DisableTypeSlotIcon();
        _itemIcon.enabled = true;
        _itemIcon.sprite = info.Icon;
    }

    public void OnUnequippedItem(EquippedEventInfo eventInfo)
    {
        EnableTypeSlotIcon();
        _itemIcon.enabled = false;
        _itemIcon.sprite = null;
    }

    private void EnableTypeSlotIcon() => 
        _typeSlotIcon.enabled = true;

    private void DisableTypeSlotIcon() => 
        _typeSlotIcon.enabled = false;

    private void OnDestroy()
    {
        if(Slot != null)
        {
            Slot.EquippedItem -= OnEquippedItem;
            Slot.UnequippedItem -= OnUnequippedItem;
        }
    }
}

public enum EquippedSlotType
{
    WeaponL,
    WeaponR,
    Shield,
    Armor,
    Boots,
    Hamlet
}

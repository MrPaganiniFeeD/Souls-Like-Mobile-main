using Infrastructure.Services.Inventory;
using Inventory.InventoryWithSlots.Equipped;
using UnityEngine;
using Zenject;

namespace UI.Inventory
{
    public class UIInventoryEquipped : MonoBehaviour
    {
        public UIRenderInventory RenderInventory { get; private set; }
        private InventoryEquipped _inventory;

        private UIEquippedSlot[] _uiSlots;
        private UIWeaponSlotSystem _uiWeaponSlotSystem;

        [Inject]
        public void Construct(IInventoryService inventoryService)
        {
            _inventory = inventoryService.InventoryEquipped;
        }
    
        private void Awake()
        {
            _uiSlots = GetComponentsInChildren<UIEquippedSlot>();
            _uiWeaponSlotSystem = GetComponentInChildren<UIWeaponSlotSystem>();
        }

        private void Start()
        {
            _uiWeaponSlotSystem.Construct(_inventory.WeaponSlot);
            foreach (var uiSlot in _uiSlots)
            {
                foreach (IEquippedSlot slot in _inventory.Slots)
                {
                    switch (uiSlot.TypeSlot)
                    {
                        case EquippedSlotType.Armor:
                            if(slot.Type == EquippedItemType.Armor)
                                uiSlot.Construct(slot);
                            break;
                        case EquippedSlotType.Boots:
                            if(slot.Type == EquippedItemType.Boots)
                                uiSlot.Construct(slot);
                            break;
                        case EquippedSlotType.Hamlet:
                            if(slot.Type == EquippedItemType.Hamlet)
                                uiSlot.Construct(slot);
                            break;
                    }
                }
            }
        }
    }
}

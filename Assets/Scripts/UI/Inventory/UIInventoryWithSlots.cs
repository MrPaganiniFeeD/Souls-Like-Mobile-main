using Infrastructure.Services.Inventory;
using UnityEngine;
using Zenject;

namespace UI.Inventory
{
    public class UIInventoryWithSlots : MonoBehaviour, IUIInventory
    {
        public IInventory Inventory { get; private set; }
        public UIRenderInventory RenderInventory { get; private set; }

        private UISlot[] _uiSlots;

        [Inject]
        public void Constructor(IInventoryService inventoryService)
        {
            Inventory = inventoryService.InventoryWithSlots;
        }
    
        private void Awake()
        {
            _uiSlots = GetComponentsInChildren<UISlot>();
        }
        private void Start()
        {
            RenderInventory = new UIRenderInventory(Inventory, _uiSlots);
        }

    }
}
